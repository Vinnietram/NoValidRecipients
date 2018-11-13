using System;
using System.Text;
using MailKit;

namespace FakeSmtpClient
{  
    public class MailProtocolLogger : IProtocolLogger
    {
        public void LogClient(byte[] buffer, int offset, int count)
        {
            var message = Encoding.ASCII.GetString(buffer, offset, count);
            Console.WriteLine($"SmtpClient: command sent {message}");
        }

        public void LogConnect(Uri uri)
        {
            Console.WriteLine($"SmtpClient: command sent {uri}");
        }

        public void LogServer(byte[] buffer, int offset, int count)
        {
            var message = Encoding.ASCII.GetString(buffer, offset, count);
            Console.WriteLine($"SmtpClient: command received {message}");
        }

        public void Dispose()
        {
            // no resources to dispose of, IProtocolLogger inherits IDisposable
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {           
        }
    }
}
