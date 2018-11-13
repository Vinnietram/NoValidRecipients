using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using SmtpServer;

namespace FakeSmtpServer
{
    public class FakeSmtpServer
    {
        private CancellationTokenSource _cancellationTokenSource;
        private readonly LocalMessageStoreFactory _localMessageStoreFactory;

        public FakeSmtpServer()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _localMessageStoreFactory = new LocalMessageStoreFactory(UpdateSessionStatusLabel);
        }

        public async Task StartServer()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var options = new SmtpServerOptionsBuilder()
                .ServerName("localhost")
                .Port(50000, true)
                .MessageStore(_localMessageStoreFactory)
                .Build();
            try
            {
                var smtpServer = new global::SmtpServer.SmtpServer(options);
                smtpServer.SessionCreated += SmtpServerOnSessionCreated;
                smtpServer.SessionCompleted += SmtpServerOnSessionCompleted;
                Console.WriteLine("SMTP Server: Started");
                await smtpServer.StartAsync(_cancellationTokenSource.Token);

            }
            catch (OperationCanceledException)
            {
                UpdateSessionStatusLabel("SMTP Server: Stopping");
            }
        }

        public void StopServer()
        {
            _cancellationTokenSource.Cancel();
        }

        private void SmtpServerOnSessionCompleted(object sender, SessionEventArgs e)
        {
            Console.WriteLine("SMTP Server: Session Completed");
        }

        private void SmtpServerOnSessionCreated(object sender, SessionEventArgs e)
        {
            Console.WriteLine("SMTP Server: Session Created");
        }

        private void UpdateSessionStatusLabel(string newSessionState)
        {
            Console.WriteLine($"SMTP Server: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture)}\t{newSessionState}{Environment.NewLine}");
        }
    }
}
