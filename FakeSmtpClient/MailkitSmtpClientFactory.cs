using MailKit;

namespace FakeSmtpClient
{
    public class MailkitSmtpClientFactory : IMailkitSmtpClientFactory
    {
        private readonly IProtocolLogger _logger;

        public MailkitSmtpClientFactory()
        {
            _logger = new MailProtocolLogger();
        }

        public IMailKitSmtpClient Create()
        {
            return new MailkitSmtpClientAdaptor(_logger);
        }
    }
}