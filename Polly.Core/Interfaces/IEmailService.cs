using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Polly.Core.Interfaces
{
    public interface IEmailService
    {
        long GenerateRandom();
        string encriptarMD5(string llave);
        string desencriptarBase64(string llave);
        string encriptarBase64(string llave);
        bool esEmail(string correo);
        Boolean SendMailSys(string to, string cc, string bcc, string subject, string body, string attachmentFilenames);
        Boolean SendMailSys2(string to, string cc, string bcc, string subject, string body, string attachmentFilenames);
        Boolean SendMail(string UserName, string UserMail, string Password, string ServerOut, Boolean SSL, MailPriority priority, Int32 Puerto, string To, string Cc, string Bcc, string Subject, string Body, string attachmentFilenames);
    }
}
