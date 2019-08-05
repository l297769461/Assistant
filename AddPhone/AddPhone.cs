using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AddPhone
{
    public partial class AddPhone : BaseForm.BaseForm
    {
        public AddPhone(string phone)
        {
            InitializeComponent();

            base.button_max.Enabled = true;
            base.label_name.Text = "添加到现有客户信息";
            this.button_query.Click += button_query_Click;
            this.label_phone.Text = phone;
            pager_kh.RefreshData = Bind_kh;
        }

        void button_query_Click(object sender, EventArgs e)
        {
            Bind_kh();
        }

        private void 查看选中项详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_kh.Rows.Count == 0) return;

            int index = dataGridView_kh.SelectedRows[0].Index;
            new CustomerInfoManage.CustomerInfoManage(new Assistant.BLL.customerinfo().GetModel(dataGridView_kh.Rows[index].Cells["dataGridViewTextBoxColumn6"].Value.ToString()), true).ShowDialog();
        }

        private void 添加到选中客户信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_kh.Rows.Count == 0) return;
            string name = textBox_name.Text;
            string bz = textBox_bz.Text;

            if (!ShowMsg("是否确定将此信息添加到选中客户？\n\n联系号码：" + this.label_phone.Text + "\n姓名：" + textBox_name.Text + "\n备注：" + textBox_bz.Text, Assistant.Model.ShowMsgType.question))
            {
                return;
            }

            int index = dataGridView_kh.SelectedRows[0].Index;
            if (new Assistant.BLL.customercontact().Add2(new Assistant.Model.customercontact()
            {
                CreateTime = DateTime.Now,
                CustomerContactId = Guid.NewGuid().ToString(),
                CustomerInfoId = dataGridView_kh.Rows[index].Cells["dataGridViewTextBoxColumn6"].Value.ToString(),
                Name = name,
                Phone = label_phone.Text,
                UpdateTime = DateTime.Now,
                Notes = bz
            }))
            {
                ShowMsg("添加成功！");
                this.Close();
            }
            else
                ShowMsg("添加失败，可能该号码已存在客户信息重复了！", Assistant.Model.ShowMsgType.error);
        }


        private void Bind_kh()
        {
            base.OpenZZ(this);
            string where = " 1=1 ";
            if (!string.IsNullOrEmpty(this.textBox_bh.Text))
            {
                where += " and Number like '%" + textBox_bh.Text + "%'";
            }
            if (!string.IsNullOrEmpty(this.textBox_adds.Text))
            {
                where += " and Address like '%" + textBox_adds.Text + "%'";
            }

            pager_kh.PageSize = 10;
            dataGridView_kh.Rows.Clear();
            int total = 0;
            List<Assistant.Model.customerinfo> l = new Assistant.BLL.customerinfo().GetList(where, pager_kh.PageIndex, pager_kh.PageSize, "NumberNo", false, out total);

            pager_kh.Count = total;
            for (int i = 0; i < l.Count; i++)
            {
                dataGridView_kh.Rows.Add();
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn_kh_bh"].Value = l[i].id;
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn2"].Value = l[i].Number;
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn3"].Value = l[i].BottledWaterPrice + "/桶";
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn4"].Value = l[i].BrandName;
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn5"].Value = l[i].Address;
                dataGridView_kh.Rows[i].Cells["Column7"].Value = l[i].Notes;
                dataGridView_kh.Rows[i].Cells["Column8"].Value = l[i].Phons;
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn6"].Value = l[i].CustomerInfoId;
            }

            //总页数
            pager_kh.PageCount = pager_kh.Count / pager_kh.PageSize;

            if (pager_kh.Count % pager_kh.PageSize != 0)
                pager_kh.PageCount = pager_kh.PageCount + 1;
            //格式化分页控件
            pager_kh.isEnable();
            pager_kh.Enabled = true;

            base.CloseZZ();
        }
    }
}
