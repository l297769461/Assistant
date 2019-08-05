using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ConfigManager
{
    public partial class ConfigManage : BaseForm.BaseForm
    {
        /// <summary>
        /// 更新程序的配置文件
        /// </summary>
        System.IO.FileInfo f_update = null;

        public ConfigManage()
        {
            InitializeComponent();
            base.label_name.Text = "系统配置管理";

            this.checkBox_isnew.CheckedChanged += checkBox_isnew_CheckedChanged;
            this.checkBox_isphone.CheckedChanged += checkBox_isphone_CheckedChanged;
            this.Load += ConfigManage_Load;
            this.button_save.Click += button_save_Click;
            //读取更新程序的配置
            f_update = new System.IO.FileInfo(Directory.GetCurrentDirectory() + "/Update.xml");
        }

        void checkBox_isphone_CheckedChanged(object sender, EventArgs e)
        {
            textBox_ip.Enabled = checkBox_isphone.Checked;
            textBox_pront.Enabled = checkBox_isphone.Checked;
            textBox_url.Enabled = checkBox_isphone.Checked;
        }

        void button_save_Click(object sender, EventArgs e)
        {
            if (!ShowMsg("是否确定保存当前配置信息？\r\n\r\n\t[部分重启软件生效！]", Assistant.Model.ShowMsgType.question)) return;
            //是否自动检测更新
            if (this.checkBox_isnew.Checked)
                UpdateConfig(false);
            Help.ConfigManager.UpdateAppConfig("AutoUpdate", this.checkBox_isnew.Checked ? "1" : "0");
            //是否置顶
            Help.Helper.f_TopMost(this.checkBox_istop.Checked);
            //是否开启手机远程
            if (this.checkBox_isphone.Checked)
            {
                Help.ConfigManager.UpdateAppConfig("ServiceIPPort", string.Format("{0},{1}", textBox_ip.Text, textBox_pront.Text));
                Help.ConfigManager.UpdateAppConfig("AssistantWeb", this.textBox_url.Text);
            }
            //判断是否开启手机远程的配置值，是否变动过
            if (!(Help.Helper.IsPhone.SystemValue == "1" && this.checkBox_isphone.Checked ||
                Help.Helper.IsPhone.SystemValue != "1" && !this.checkBox_isphone.Checked))
            {
                Help.Helper.IsPhone.SystemValue = checkBox_isphone.Checked ? "1" : "0";
                Help.Helper.f_Set_IsPhone();
            }
            else
            {
                Help.Helper.IsPhone.SystemValue = checkBox_isphone.Checked ? "1" : "0";
            }
            new Assistant.BLL.systeminfo().Update(Help.Helper.IsPhone);

            //来电关闭的自动提示框
            Help.ConfigManager.UpdateAppConfig("WindowCloseTime", ((int)numericUpDown_time.Value).ToString());
            ShowMsg("保存成功！\r\n\r\n\t[部分重启软件生效！]");
            this.Close();
        }

        void ConfigManage_Load(object sender, EventArgs e)
        {
            //读取读取配置信息
            this.checkBox_isnew.Checked = Help.ConfigManager.GetAppConfig("AutoUpdate") == "1";
            this.checkBox_istop.Checked = Help.Helper.IsTopMost.SystemValue == "1";
            string[] ippront = Help.ConfigManager.GetAppConfig("ServiceIPPort").Split(',');
            this.textBox_ip.Text = ippront[0];
            this.textBox_pront.Text = ippront[1];
            this.textBox_url.Text = Help.ConfigManager.GetAppConfig("AssistantWeb");
            try
            {
                numericUpDown_time.Value = int.Parse(Help.ConfigManager.GetAppConfig("WindowCloseTime"));
            }
            catch (Exception)
            {
                numericUpDown_time.Value = 0;
            }
            if (this.checkBox_isnew.Checked)
                UpdateConfig(true);
            this.checkBox_isphone.Checked = Help.Helper.IsPhone.SystemValue == "1";
        }

        private void UpdateConfig(bool pd)
        {
            if (f_update.Exists)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(f_update.FullName);
                XmlElement gen = xml.DocumentElement;
                foreach (XmlNode item in gen.ChildNodes)
                {
                    switch (item.Name)
                    {
                        case "Path":
                            if (pd)
                                this.textBox_update_url.Text = item.Attributes["updateurl"].Value;
                            else
                                item.Attributes["updateurl"].Value = this.textBox_update_url.Text;
                            break;
                        default:
                            break;
                    }
                }
                if (!pd)
                    xml.Save(f_update.FullName);
            }
        }

        void checkBox_isnew_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox_update.Enabled = this.checkBox_isnew.Checked;
            if (this.checkBox_isnew.Checked)
                UpdateConfig(true);
        }
    }
};