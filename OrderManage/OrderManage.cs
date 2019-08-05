using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace OrderManage
{
    public partial class OrderManage : BaseForm.BaseForm
    {
        /// <summary>
        /// 当前操作对象
        /// </summary>
        private Assistant.Model.orderinifo o = null;

        public OrderManage(Assistant.Model.orderinifo _o)
        {
            InitializeComponent();

            label_name.Text = "订单管理";
            o = _o;

            this.label_ddbh.Text = o.id.ToString();
            this.label_khbh.Text = o.Number;
            this.label_phone.Text = o.Phone;
            this.label_clfs.Text = base.FormatHandlingTypeByOrder(o.OrderHandlingType);
            this.richTextBox_adds.Text = o.Address;
            this.richTextBox_ddbz.Text = o.Notes;
            decimal[] ds = base.GetBottledWaterPrice(o.BottledWaterPrice);
            this.numericUpDown_y.Value = ds[0];
            this.numericUpDown_j.Value = ds[1];
            this.numericUpDown_sl.Value = o.BottledNumber;
            this.label_pp.Text = o.BrandName;

            button_save.Click += button_save_Click;
        }

        void button_save_Click(object sender, EventArgs e)
        {
            if (!ShowMsg("是否确认保存该订单？", Assistant.Model.ShowMsgType.question)) return;

            if (numericUpDown_y.Value + numericUpDown_j.Value == 0)
            {
                ShowMsg("桶装水单价不允许为空！", Assistant.Model.ShowMsgType.error);
                return;
            }
            o.BottledWaterPrice = numericUpDown_y.Value + numericUpDown_j.Value / 10;
            o.BottledNumber = (int)numericUpDown_sl.Value;
            o.Notes = richTextBox_ddbz.Text;
            o.CreateTime = o.CreateTime;
            o.UpdateTime = DateTime.Now;
            if (new Assistant.BLL.orderinifo().Update(o))
            {
                ShowMsg("修改成功！");
                //界面更新
                Help.Helper.f_bind_lddd();
                this.Close();
                //开启线程执行通知服务器同步数据
                if (Help.SocketHelp.client != null && Help.SocketHelp.client.Connected)
                {
                    System.Threading.Thread th = new System.Threading.Thread(ServerOrder);
                    th.IsBackground = true;
                    th.Start();
                }
            }
            else
                ShowMsg("修改失败！", Assistant.Model.ShowMsgType.error);
        }

        /// <summary>
        /// 通知服务器
        /// </summary>
        private void ServerOrder(object ob)
        {
            Help.SocketHelp.CreateSocket().Send(5, string.Format("{0},{1},{2},{3},{4}", o.OrderInfoId, o.BottledWaterPrice, o.BottledNumber, o.Notes, Help.Helper.KeyValue));
        }
    }
}
