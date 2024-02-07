using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using BusinessAccessLayer.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Services
{

    public class CartServices : ICart
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ShoppingCartVM GetShoppingCart(string userId)
        {
            var shoppingCart = new ShoppingCartVM
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId, includeProperties: "Product")
            };

            foreach (var cart in shoppingCart.ShoppingCartList)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                shoppingCart.OrderTotal += (cart.Price * cart.Count);
            }

            return shoppingCart;
        }

        public void IncreaseQuantity(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;
            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();
        }

        public void DecreaseQuantity(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            if (cartFromDb.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }

            _unitOfWork.Save();
        }

        public void RemoveItem(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cartFromDb);
            _unitOfWork.Save();
        }

        public double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else if (shoppingCart.Count <= 100)
            {
                return shoppingCart.Product.Price50;
            }
            else
            {
                return shoppingCart.Product.Price100;
            }
        }
    }

}