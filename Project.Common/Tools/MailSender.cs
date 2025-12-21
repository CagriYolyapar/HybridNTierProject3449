using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Tools
{
    public static class MailSender
    {

        #region TestEmailiVeSifre

        //testemail3172@gmail.com

        //rvzhpxwpegickwtq

        #endregion

        //Gönderim yapacak olan mail'den google Account'a gidin oradan security alanına girin
        //Signing in Google 2-step Verification secegini secin
        //Sayfanın alt tarafına Application Passwords(Uygulama sifreleri) buradan bir tanesini secip password generate deyin
        //Cıkan password 4 tane kelimeden olusacak ve bosluklu olacka. O boslukları silip password'unu elde edebilirsiniz...Mail gönderimleriniz burada yaptıgınız gibi baska bir proje üzerinden olacak ise sistemlerden sizde o sifre istenecektir

        //cdad ddasd ddss rasd
        //cdadddasdddssrasd

        public static void Send(string receiver,string password = "rvzhpxwpegickwtq",string body="Test mesajıdır",string subject = "Test",string sender = "testemail3172@gmail.com")
        {
            MailAddress senderEmail = new MailAddress(sender);
            MailAddress receiverEmail = new MailAddress(receiver);

            //2-step verification'i yaptıgınızdan emin olun
            SmtpClient smtpClient = new()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address,password)
            };

            using(MailMessage message = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body,
            })
            {
                smtpClient.Send(message);
            }
        }
    }
}
