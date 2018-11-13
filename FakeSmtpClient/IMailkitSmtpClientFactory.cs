namespace FakeSmtpClient
{
    public interface IMailkitSmtpClientFactory
    {
        IMailKitSmtpClient Create();
    }
}
