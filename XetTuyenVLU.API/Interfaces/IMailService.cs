using XetTuyenVLU.SettingsForMail;

namespace XetTuyenVLU.Interfaces
{
    public interface IMailService
    {
        public Task<bool> SendEmailAsync(MailRequest mailRequest);
    }
}
