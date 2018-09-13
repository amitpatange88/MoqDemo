using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ExternalApps
{
    public class Mail
    {
        //Default values for mail configuration.
        private SmtpClient _client;

        public Mail()
        {
            this._client = new SmtpClient();
        }

        public virtual void Send(string Host, string From, string Password, string To, string Subject, string Body, int Port = 25)
        {
            //Decode password here.
            Password = Mail.Base64Decode(Password);

            MailMessage mail = new MailMessage(From, To);
            _client.Port = Port;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.UseDefaultCredentials = false;
            _client.EnableSsl = true;
            _client.Credentials = new NetworkCredential(From, Password);
            _client.Host = Host;
            mail.Subject = Subject;
            mail.Body = Body;

            //Send mail.
            _client.Send(mail);
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
