using Common.Models.Configs;
using MailKit.Net.Smtp;
using MimeKit;

namespace Common.Helpers
{
    /// <summary>
    /// 邮箱操作帮助类
    /// </summary>
    public class EmailHelper
    {
        public static EmailConfig EmailConfig {  get; set; }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toEmailAddress">收件人</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public static async Task<bool> SendEmail(string toEmailAddress, string content)
        {
            // 创建一个包含邮件内容的 MimeMessage 对象
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Fly Dance", EmailConfig.SystemEmail));
            email.To.Add(MailboxAddress.Parse(toEmailAddress));

            // 创建邮件正文
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = content;
            // 如果需要发送 HTML 内容，可以使用以下代码：
            // bodyBuilder.HtmlBody = "<h1>这是 HTML 格式的邮件内容。</h1>";
            email.Body = bodyBuilder.ToMessageBody();

            // 配置 SMTP 客户端
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    // 连接到 SMTP 服务器并发送邮件
                    await smtpClient.ConnectAsync("smtp.163.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect); // 使用 SSL/TLS 端口 465 或 STARTTLS 端口 587
                    await smtpClient.AuthenticateAsync(EmailConfig.SystemEmail, EmailConfig.SystemEmailTokenPassword);
                    await smtpClient.SendAsync(email);
                    await smtpClient.DisconnectAsync(true);

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发送邮件时出错: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
