using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JFJRepresentacoes
{
    public static class Settings
    {
        public static string Secret = "0E7ABB89019388DAC739E3CDBA66BF045D8A7284346131487992A1A9C065637F8D64D8CE23BE410D111B33724B202C67265E90E037A7BAAA32510484472032CF";

        public static string HashPassword(string password)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] hashBytes;
            using (HashAlgorithm hash = SHA1.Create())
                hashBytes = hash.ComputeHash(encoding.GetBytes(password));

            StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
            }

            return hashValue.ToString();
        }

        public static void SendEmail(string Destinatario, string CorpoEmail, string Assunto)
        {
            Execute(Destinatario, CorpoEmail, Assunto).Wait();
        }

        private async static Task Execute(string Destinatario, string CorpoEmail, string Assunto)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("dudufranz13@gmail.com", "JFJ Representacoes");
            var subject = Assunto;
            var to = new EmailAddress(Destinatario, "");
            var plainTextContent = CorpoEmail;
            var htmlContent = $"<p>{CorpoEmail}</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }

}
