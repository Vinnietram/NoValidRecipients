using System;
using SmtpServer;
using SmtpServer.Storage;

namespace FakeSmtpServer
{
    public class LocalMessageStoreFactory : IMessageStoreFactory
    {
        private readonly Action<string> _updateSessionStatusLabel;

        public LocalMessageStoreFactory(Action<string> updateSessionStatusLabel)
        {
            _updateSessionStatusLabel = updateSessionStatusLabel;
        }

        public IMessageStore CreateInstance(ISessionContext context)
        {
            return new LocalMessageStore(_updateSessionStatusLabel);
        }
    }
}
