using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SetPhonePwd
{
    public partial class SepPhonePwd : BaseForm.BaseForm
    {
        public SepPhonePwd()
        {
            InitializeComponent();
            base.label_name.Text = "设置手机访问密码";

            //生成二维码
            string url = Help.ConfigManager.GetAppConfig("AssistantWeb") + "home/index" + "?keyvalue=" + Help.Helper.KeyValue;
            Image image = new Help.EWM().CreateCode(url);
            this.pictureBox1.Image = image;

            this.button_save.Click += button_save_Click;
            this.textBox_pwd.Text = Help.Helper.PhonePWD.SystemValue;
        }

        void button_save_Click(object sender, EventArgs e)
        {
            if (!ShowMsg("是否确认修改手机访问密码？", Assistant.Model.ShowMsgType.question))
                return;
            Help.Helper.PhonePWD.SystemValue = this.textBox_pwd.Text;
            new Assistant.BLL.systeminfo().Update(Help.Helper.PhonePWD);
            //更新服务器，告知客户端变更访问密码
            if (Help.SocketHelp.client != null && Help.SocketHelp.client.Connected)
            {
                Help.SocketHelp.CreateSocket().Send(2, Help.Helper.PhonePWD.SystemValue);
            }
            this.Close();
        }
    }
}
