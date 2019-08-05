using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Help
{
    public class SendEmail
    {
        #region 基础变量
        /// <summary>
        /// 发件地址
        /// </summary>
        private string fromEmailAdds = "18628285768@163.com";
        /// <summary>
        /// SMTP地址
        /// </summary>
        private string strSMTP = "smtp.163.com";
        /// <summary>
        /// 邮箱登录名
        /// </summary>
        private string username = "18628285768@163.com";
        /// <summary>
        /// 邮箱密码/qq邮箱填写独立密码
        /// </summary>
        private string userpwd = "xl18628285768";
        #endregion

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toEmailAdds">目标地址</param>
        /// <param name="subject">标题</param>
        /// <param name="body">正文</param>
        /// <param name="files">附件物理路径（无:NULL）</param>
        /// <returns>true:发送成功</returns>
        public string Send(string toEmailAdds, string subject, string body, string[] files)
        {
            try
            {
                //确定smtp服务器地址。实例化一个Smtp客户端
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(strSMTP);

                //构造一个发件人地址对象
                MailAddress from = new MailAddress(fromEmailAdds);
                //构造一个收件人地址对象
                MailAddress to = new MailAddress(toEmailAdds);

                //构造一个Email的Message对象
                MailMessage message = new MailMessage(from, to);

                //为 message 添加附件
                try
                {
                    if (files != null)
                    {
                        foreach (string fileName in files)
                        {
                            //构造一个附件对象
                            Attachment attach = new Attachment(fileName);
                            //得到文件的信息
                            ContentDisposition disposition = attach.ContentDisposition;
                            disposition.CreationDate = System.IO.File.GetCreationTime(fileName);
                            disposition.ModificationDate = System.IO.File.GetLastWriteTime(fileName);
                            disposition.ReadDate = System.IO.File.GetLastAccessTime(fileName);
                            //向邮件添加附件
                            message.Attachments.Add(attach);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                //添加邮件主题和内容
                message.Subject = subject;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = body;
                message.BodyEncoding = Encoding.UTF8;

                //设置邮件的信息
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = false;

                //如果服务器支持安全连接，则将安全连接设为true。
                //gmail支持，163不支持，如果是gmail则一定要将其设为true
                //  if (cmbBoxSMTP.SelectedText == "smpt.163.com")
                client.EnableSsl = false;
                //  else
                //client.EnableSsl = true;

                //设置用户名和密码。
                //string userState = message.Subject;
                client.UseDefaultCredentials = false;
                //用户登陆信息
                NetworkCredential myCredentials = new NetworkCredential(username, userpwd);
                client.Credentials = myCredentials;
                //发送邮件
                client.Send(message);
                //提示发送成功
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
