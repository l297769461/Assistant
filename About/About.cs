
using Help;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace About
{
    public partial class About : BaseForm.BaseForm
    {
        public About()
        {
            InitializeComponent();

            base.label_name.Text = "关于 - 感谢使用本软件";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            linkLabel3.LinkClicked += linkLabel3_LinkClicked;
            linkLabel4.LinkClicked += linkLabel4_LinkClicked;
        }

        void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!ShowMsg("是否确定把刚才的错误日志发送给许灵？", Assistant.Model.ShowMsgType.question)) return;

            string path = Directory.GetCurrentDirectory() + "/Logs/Cal-InfoLog-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
            if (!File.Exists(path))
            {
                ShowMsg("今天还没有错误日志哟！");
                return;
            }

            //拷贝日志副本

            string msg = new SendEmail().Send(
                  "297769461@qq.com",
                  "来电助手-错误日志",
                  "请见副本",
                  new string[] { path });
            if (msg == "true")
            {
                ShowMsg("邮件发送成功！");
            }
            else
                ShowMsg("邮件发送失败！" + msg);
        }

        void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetDataObject("297769461@qq.com");
            ShowMsg("邮箱地址已复制！");
        }

        void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new WeiXin.WeiXin().ShowDialog();
        }

        void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "tencent://message/?uin=297769461");
        }
    }
}
