using System.Collections.Generic;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace FakeSmtpClient
{
    internal class MailkitSmtpClientAdaptor : IMailKitSmtpClient
    {
        private readonly SmtpRelayClientImp _smtpClient;

        internal MailkitSmtpClientAdaptor(IProtocolLogger logger)
        {
            _smtpClient = new SmtpRelayClientImp(logger);
        }

        public void Connect(string host, int port)
        {
            _smtpClient.Connect(host, port, SecureSocketOptions.None);
            _smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
        }

        public void Send(MimeMessage mimeMessage, MailboxAddress sender,
            IEnumerable<MailboxAddress> recipients)
        {
            _smtpClient.Send(mimeMessage, sender, recipients);
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}