using System;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace FakeSmtpClient
{
    public class SmtpRelayClientImp : SmtpClient
    {
        public SmtpRelayClientImp(IProtocolLogger protocolLogger) : base(protocolLogger)
        {
        }

        protected override void OnSenderAccepted(MimeMessage message, MailboxAddress mailbox, SmtpResponse response)
        {
        }

        protected override void OnNoRecipientsAccepted(MimeMessage message)
        {
            throw new ApplicationException("The message has no valid recipients");
        }
    }
}
