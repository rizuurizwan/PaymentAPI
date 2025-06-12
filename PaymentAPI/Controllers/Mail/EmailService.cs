using System.Net;
using System.Net.Mail;
using PaymentAPI.Controllers.Mail;

public class EmailService
{
    private readonly string _smtpHost = "smtp.gmail.com";
    private readonly int _smtpPort = 587;
    private readonly string _smtpUser = "mohamedrizwan145ar@gmail.com"; // your gmail
    private readonly string _smtpPass = "xjus gdfk njlg niuf";  // Gmail app password or actual password if less secure enabled

    public bool SendEmail(ContactFormModel model)
    {
        try
        {
            var fromAddress = new MailAddress(_smtpUser, "Your Company");
            var toAddress = new MailAddress("mohamedrizwan145ar@gmail.com"); // who receives the email

            var mail = new MailMessage(fromAddress, toAddress)
            {
                Subject = "New Contact Form Submission",
                Body = $@"
                        Name: {model.name}
                        Company: {model.company}
                        Phone: {model.phone}
                        Mobile: {model.mobile}
                        Email: {model.email}
                        Enquiry: {model.email}
                        Comments: {model.comments}
                "
            };

            using var smtp = new SmtpClient(_smtpHost, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true,
            };

            smtp.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            // log error (ex.Message)
            return false;
        }
    }
}
