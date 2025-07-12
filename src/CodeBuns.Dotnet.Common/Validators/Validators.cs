using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CodeBuns.Dotnet.Common.Validators
{
    public class Validators
    {
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || email.Length > 254)
                return false;

            email = email.Trim();

            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (!regex.IsMatch(email))
                return false;

            // Check if the email can be parsed by MailAddress
            // You can remove this part due performance concerns, but it adds an extra layer of validation
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
