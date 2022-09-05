using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using XetTuyenVLU.Helpers;
using XetTuyenVLU.Interfaces;
using XetTuyenVLU.Models;
using XetTuyenVLU.SettingsForMail;

namespace XetTuyenVLU.Services
{
    public class MailService : IMailService
    {
        private readonly XetTuyenVLUContext _context;
        private readonly MailSettings _mailSettings;
        private readonly AlternateViewHelper _alternateViewHelper;
        public MailService(XetTuyenVLUContext context, IOptions<MailSettings> mailSettings)
        {
            _context = context;
            _mailSettings = mailSettings.Value;
            _alternateViewHelper = new AlternateViewHelper();
        }

        public async Task<bool> SendEmailAsync(MailRequest request)
        {
            var thongBao = await _context.ThongBao.FirstOrDefaultAsync(x => x.TrangThaiId == 1 && x.LoaiThongBaoId == 1);
            if (thongBao == null)
                throw new Exception($"Không tìm thấy thông báo có ID {thongBao.ID}!");
            string MailText = thongBao.Content;
            MailText = MailText.Replace("[0]", request.FullName)
                               .Replace("[1]", request.AdmissionCode);
            var mailMessage = new MailMessage();
            AlternateView alterView = _alternateViewHelper.ContentToAlternateView(MailText);
            mailMessage.AlternateViews.Add(alterView);
            var email = (MimeMessage)mailMessage;
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = "Thông báo từ Trường đại học Văn Lang";
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            var result = await smtp.SendAsync(email);
            smtp.Disconnect(true);
            return result != "";
        }
    }
}
