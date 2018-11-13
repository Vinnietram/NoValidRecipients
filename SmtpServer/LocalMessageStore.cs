using System;
using System.Threading;
using System.Threading.Tasks;
using SmtpServer;
using SmtpServer.Mail;
using SmtpServer.Protocol;
using SmtpServer.Storage;

namespace FakeSmtpServer
{
    public class LocalMessageStore : IMessageStore
    {
        private readonly Action<string> _updateSessionStatusLabel;

        public LocalMessageStore(Action<string> updateSessionStatusLabel)
        {
            _updateSessionStatusLabel = updateSessionStatusLabel;
        }

        public Task<SmtpResponse> SaveAsync(ISessionContext context, IMessageTransaction transaction, CancellationToken cancellationToken)
        {
            _updateSessionStatusLabel($"Message received from {transaction.From.AsAddress()}");
            return Task.FromResult(SmtpResponse.Ok);
        }
    }
}
