
using Help;
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

namespace Clear
{
    public partial class Clear : BaseForm.BaseForm
    {
        private Assistant.Model.systeminfo IS_Clear = null;
        private Assistant.Model.systeminfo LD_Clear = null;
        private Assistant.Model.systeminfo DD_Clear = null;

        public Clear()
        {
            InitializeComponent();
            base.label_name.Text = "清理";
            this.checkBox_zd.CheckedChanged += checkBox_zd_CheckedChanged;
            this.comboBox_sd_dd.SelectedIndex = 0;
            this.comboBox_sd_ld.SelectedIndex = 0;
            this.comboBox_zd_dd.SelectedIndex = 0;
            this.comboBox_zd_ld.SelectedIndex = 0;

            this.button_dd.Click += button_dd_Click;
            this.button_ld.Click += button_ld_Click;

            this.button_save.Click += button_save_Click;

            //读取配置信息
            IS_Clear = new Assistant.BLL.systeminfo().GetModel(Assistant.Model.SystemInfoType.IS_Clear);
            LD_Clear = new Assistant.BLL.systeminfo().GetModel(Assistant.Model.SystemInfoType.LD_Clear);
            DD_Clear = new Assistant.BLL.systeminfo().GetModel(Assistant.Model.SystemInfoType.DD_Clear);
            checkBox_zd.Checked = IS_Clear.SystemValue == "1";
            string[] ld = LD_Clear.SystemValue.Split(',');
            string[] dd = DD_Clear.SystemValue.Split(',');

            numericUpDown_zd_ld.Value = int.Parse(ld[0]);
            comboBox_zd_ld.SelectedIndex = int.Parse(ld[1]);
            numericUpDown_zd_dd.Value = int.Parse(dd[0]);
            comboBox_zd_dd.SelectedIndex = int.Parse(dd[1]);
        }

        void button_save_Click(object sender, EventArgs e)
        {
            if (!ShowMsg("是否确认（" + (checkBox_zd.Checked ? "开启" : "关闭") + "）自动清理功能？", Assistant.Model.ShowMsgType.question)) return;
            
            IS_Clear.SystemValue = checkBox_zd.Checked ? "1" : "0";
            LD_Clear.SystemValue = ((int)numericUpDown_zd_ld.Value) + "," + comboBox_zd_ld.SelectedIndex;
            DD_Clear.SystemValue = ((int)numericUpDown_zd_dd.Value) + "," + comboBox_zd_dd.SelectedIndex;

            new Assistant.BLL.systeminfo().Update(IS_Clear);
            new Assistant.BLL.systeminfo().Update(LD_Clear);
            new Assistant.BLL.systeminfo().Update(DD_Clear);

            ShowMsg("修改成功！\n\n\t如果开启了自动清理，程序每次启动将自动清理过期信息！");
            this.Close();
        }

        void button_ld_Click(object sender, EventArgs e)
        {
            if (!ShowMsg(string.Format("是否确定删除（{0}{1}）之前的来电信息？",
                numericUpDown_sd_ld.Value.ToString() + (comboBox_sd_ld.SelectedIndex == 0 ? "个" : ""),
                comboBox_sd_ld.SelectedItem.ToString()), Assistant.Model.ShowMsgType.question)) return;

            string time = GetTime(comboBox_sd_ld.SelectedIndex, (int)numericUpDown_sd_ld.Value);

            new Assistant.BLL.callrecord().DeleteByTime(time);
            ShowMsg("删除成功！");
        }

        void button_dd_Click(object sender, EventArgs e)
        {
            if (!ShowMsg(string.Format("是否确定删除（{0}{1}）之前的订单信息？",
                numericUpDown_sd_dd.Value.ToString() + (comboBox_sd_dd.SelectedIndex == 0 ? "个" : ""),
                comboBox_sd_dd.SelectedItem.ToString()), Assistant.Model.ShowMsgType.question)) return;

            string time = GetTime(comboBox_sd_dd.SelectedIndex, (int)numericUpDown_sd_dd.Value);

            new Assistant.BLL.orderinifo().DeleteByTime(time);
            ShowMsg("删除成功！");
        }

        void checkBox_zd_CheckedChanged(object sender, EventArgs e)
        {
            panel_zd.Enabled = this.checkBox_zd.Checked;
        }
    }
}
