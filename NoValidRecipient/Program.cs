using System;
using System.Net.Mail;
using System.Threading;
using FakeSmtpClient;
using MimeKit;

namespace NoValidRecipient
{
    class Program
    {
        private static FakeSmtpServer.FakeSmtpServer _fakeSmtpServer;

        static void Main(string[] args)
        {
            StartFakeSmtpServer();

            try
            {
                Timer smtpClientSendTimer = new Timer(SendMessageToServerEveryInterval, "", TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(2));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Timer restartSmtpServertimer = new Timer(RestartFakeSmtpServer, "", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

            Thread.Sleep(Timeout.Infinite);
        }

        private static void StartFakeSmtpServer()
        {
            _fakeSmtpServer = new FakeSmtpServer.FakeSmtpServer();
            _fakeSmtpServer.StartServer().GetAwaiter();
        }

        private static void RestartFakeSmtpServer(object state)
        {
            _fakeSmtpServer.StopServer();
            StartFakeSmtpServer();                        
        }

        private static void SendMessageToServerEveryInterval(object state)
        {
            var fakeSmtpClientFactory = new MailkitSmtpClientFactory();
            var fakeSmtpClient = fakeSmtpClientFactory.Create();
            fakeSmtpClient.Connect("localhost", 50000);

            var mailMessage = CreateMailMessage();
            fakeSmtpClient.Send(MimeMessage.CreateFromMailMessage(mailMessage),
                                new MailboxAddress(string.Empty, string.Empty),
                                new[] { new MailboxAddress("Dick", "dick@mail.localhost.com") });
        }

        private static MailMessage CreateMailMessage()
        {
            var mailMessage = new MailMessage {From = new MailAddress("tom@mail.localhost.com")};
            mailMessage.To.Add("dick@mail.mail.localhost.com");
            mailMessage.Subject = "Test Subject";
            mailMessage.Body = "Test Body";
            mailMessage.IsBodyHtml = false;

            return mailMessage;
        }
    }
}
