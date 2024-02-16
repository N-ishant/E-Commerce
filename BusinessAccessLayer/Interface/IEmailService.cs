using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Interface
{
    public interface IEmailService
    {
        //Task SendTestEmail(UserEmailOptions emailOptions);
        // public Task SendEmailAsync(string email, string subject, string message);

        public Task SendEmailAsync(string email, string subject);

    }
}
