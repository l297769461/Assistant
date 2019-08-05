using Assistant.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomerInfoManage
{
    public partial class CustomerInfoManage : BaseForm.BaseForm
    {
        /// <summary>
        /// 窗口初始宽度
        /// </summary>
        private int oldSplitterDistance = 0;
        /// <summary>
        /// 当前操作客户对象
        /// </summary>
        private customerinfo c = null;
        /// <summary>
        /// 编辑的联系人行id
        /// </summary>
        private int edit_rindex = -1;

        public CustomerInfoManage(customerinfo _c = null, bool ischeck = false)
        {
            InitializeComponent();
            base.button_max.Enabled = true;
            base.label_name.Text = "客户信息管理";

            this.button_save.Click += button_save_Click;
            this.button_cancel.Click += button_cancel_Click;

            this.SizeChanged += CustomerInfoManage_SizeChanged;
            oldSplitterDistance = this.splitContainer1.SplitterDistance;

            //加载饮水品牌下拉框
            //创建一个数据集
            DataTable dt = new DataTable();
            dt.Columns.Add("text", typeof(String));
            dt.Columns.Add("val", typeof(String));

            DataRow dr = dt.NewRow();
            dr[0] = "请选择";
            dr[1] = "-1";
            dt.Rows.Add(dr);
            foreach (var item in new Assistant.BLL.waterbrand().GetList(""))
            {
                dr = dt.NewRow();
                dr[0] = item.BrandName;
                dr[1] = item.WaterBrandId;
                dt.Rows.Add(dr);
            }
            comboBox_pp.DataSource = dt;
            comboBox_pp.DisplayMember = "text";//text这个字段为显示的值
            comboBox_pp.ValueMember = "val";//val这个字段为后台获取的值

            this.button_add_cancel.Click += button_add_cancel_Click;
            this.button_add_save.Click += button_add_save_Click;
            this.linkLabel_cs.LinkClicked += linkLabel_cs_LinkClicked;

            c = _c;
            if (c != null)
            {
                this.textBox_bh.Text = c.Number;
                this.richTextBox_bzxx.Text = c.Notes;
                this.richTextBox_adds.Text = c.Address;
                this.comboBox_pp.SelectedValue = c.WaterBrandId;
                decimal[] ds = GetBottledWaterPrice(c.BottledWaterPrice);
                this.numericUpDown_y.Value = ds[0];
                this.numericUpDown_j.Value = ds[1];

                //激活刷新
                toolStripMenuItem4.Enabled = true;
                //默认检索一次
                Bind_kh();

                //只允许编辑系统生成的客户编号
                this.linkLabel_cs.Enabled = c.Number.IndexOf('#') == 0;
                this.textBox_bh.Enabled = c.Number.IndexOf('#') == 0;
            }
            else
            {
                //新增，激活生成按钮
                this.linkLabel_cs.Enabled = true;
                this.textBox_bh.Enabled = true;
            }

            //查看视图
            if (ischeck)
            {
                groupBox1.Enabled = false;
                for (int i = 0; i < contextMenuStrip_lxxx.Items.Count; i++)
                {
                    contextMenuStrip_lxxx.Items[i].Enabled = false;
                }
                //激活刷新
                toolStripMenuItem4.Enabled = true;
                button_save.Enabled = false;
            }
        }

        void linkLabel_cs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            this.textBox_bh.Text = "#" + r.Next(100000000, 999999999);
            ShowMsg("客户编号已系统生成，可自行修改！\n\t提示：如果提示重复，请再次生成即可！");
        }

        void button_add_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox_phone.Text))
            {
                ShowMsg("联系号码为必填项！", ShowMsgType.error);
                return;
            }
            if (c != null)
            {
                //刷新，以获取最新信息
                Bind_kh();
            }
            //重复判断
            for (int i = 0; i < dataGridView_lxxx.Rows.Count; i++)
            {
                if (
                   edit_rindex == -1 && this.textBox_phone.Text == dataGridView_lxxx.Rows[i].Cells["Column2"].Value.ToString() ||
                    edit_rindex != -1 && i != edit_rindex && this.textBox_phone.Text == dataGridView_lxxx.Rows[i].Cells["Column2"].Value.ToString()
                    )
                {
                    dataGridView_lxxx.Rows[i].Selected = true;
                    ShowMsg("该号码已存在，不允许重复添加！", ShowMsgType.error);
                    return;
                }
            }
            //添加到dataview
            DataGridViewRow r = new DataGridViewRow();
            r.CreateCells(dataGridView_lxxx);
            r.Cells[0].Value = "*";
            r.Cells[1].Value = this.textBox_phone.Text;
            r.Cells[2].Value = this.textBox_name.Text;
            r.Cells[3].Value = this.textBox_bzxx.Text;
            r.Cells[4].Value = base.FormatDatatime(DateTime.Now);
            r.Cells[5].Value = base.FormatDatatime(DateTime.Now);
            r.Cells[6].Value = "-1";

            //隐藏历史数据
            if (edit_rindex != -1)
                dataGridView_lxxx.Rows[edit_rindex].Visible = false;

            dataGridView_lxxx.Rows.Insert(0, r);

            ShowMsg("已添加到临时保存，确认新增请点击保存按钮！");
            if (edit_rindex != -1)
            {
                edit_rindex = -1;
                //解冻
                dataGridView_lxxx.Enabled = true;
            }
            groupBox2.Visible = false;
        }

        void button_add_cancel_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            if (edit_rindex != -1)
            {
                edit_rindex = -1;
                //解冻
                dataGridView_lxxx.Enabled = true;
            }
        }

        void CustomerInfoManage_SizeChanged(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = oldSplitterDistance;
        }

        void button_cancel_Click(object sender, EventArgs e)
        {
            if (ShowMsg("是否关闭当前窗口？", ShowMsgType.question))
                this.Close();
        }

        void button_save_Click(object sender, EventArgs e)
        {
            if (!ShowMsg("是否确认新增次客户信息？", ShowMsgType.question)) return;

            if (string.IsNullOrEmpty(this.textBox_bh.Text))
            {
                ShowMsg("客户编号为必填项！", ShowMsgType.error);
                return;
            }
            if (this.numericUpDown_y.Value + this.numericUpDown_j.Value == 0)
            {
                ShowMsg("桶装水单价为必填项！", ShowMsgType.error);
                return;
            }
            if (this.comboBox_pp.SelectedValue.ToString() == "-1")
            {
                ShowMsg("饮水品牌为必填项！", ShowMsgType.error);
                return;
            }
            if (string.IsNullOrEmpty(this.richTextBox_adds.Text))
            {
                ShowMsg("送水地址为必填项！", ShowMsgType.error);
                return;
            } 

            //入库客户信息
            if (c == null)
            {
                //新增
                c = new customerinfo();
                c.Address = richTextBox_adds.Text;
                c.BottledWaterPrice = this.numericUpDown_y.Value + this.numericUpDown_j.Value / 10;
                c.CreateTime = DateTime.Now;
                c.CustomerInfoId = Guid.NewGuid().ToString();
                c.Notes = richTextBox_bzxx.Text;
                c.Number = textBox_bh.Text;
                c.UpdateTime = c.CreateTime;
                c.WaterBrandId = this.comboBox_pp.SelectedValue.ToString();
                if (!new Assistant.BLL.customerinfo().Add(c))
                {
                    c = null;
                    ShowMsg("保存失败，系统检测到该客户编号已经存在！", ShowMsgType.error);
                    return;
                }
            }
            else
            {
                c.Address = richTextBox_adds.Text;
                c.BottledWaterPrice = this.numericUpDown_y.Value + this.numericUpDown_j.Value / 10;
                c.Notes = richTextBox_bzxx.Text;
                c.Number = textBox_bh.Text;
                c.UpdateTime = DateTime.Now;
                c.WaterBrandId = this.comboBox_pp.SelectedValue.ToString();
                if (!new Assistant.BLL.customerinfo().Update(c))
                {
                    c = null;
                    ShowMsg("保存失败，系统检测到该客户编号已经存在！", ShowMsgType.error);
                    return;
                }
            }
            //客户联系信息处理
            foreach (DataGridViewRow item in dataGridView_lxxx.Rows)
            {
                if (item.Visible)
                {
                    //新增
                    bool pd = new Assistant.BLL.customercontact().Add(
                            new Assistant.Model.customercontact()
                            {
                                CustomerContactId = Guid.NewGuid().ToString(),
                                CustomerInfoId = c.CustomerInfoId,
                                Phone = item.Cells["Column2"].Value.ToString(),
                                Name = item.Cells["Column3"].Value == null ? "" : item.Cells["Column3"].Value.ToString(),
                                Notes = item.Cells["Column4"].Value == null ? "" : item.Cells["Column4"].Value.ToString(),
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now
                            }
                           );
                    if (!pd)
                    {
                        ShowMsg("保存失败，系统检测到联系方式已存在其他客户信息中！", ShowMsgType.error);
                        return;
                    }
                }
                if (item.Cells[6].Value.ToString() != "-1")
                {
                    //删除历史的
                    new Assistant.BLL.customercontact().Delete(item.Cells[6].Value.ToString());
                }
            }
            ShowMsg("保存成功！");
            //界面更新
            Help.Helper.f_manager_bind(0);
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.groupBox2.Visible = true;
            this.textBox_phone.Text = "";
            this.textBox_name.Text = "";
            this.textBox_bzxx.Text = "";
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Bind_kh();
        }

        //分页方法
        private void Bind_kh()
        {
            if (c == null) return;
            //清空数据库数据库，保留临时新增数据
            List<DataGridViewRow> l_del = new List<DataGridViewRow>();
            foreach (DataGridViewRow item in dataGridView_lxxx.Rows)
            {
                if (item.Cells["Column6"].Value.ToString() != "-1")
                {
                    l_del.Add(item);
                }
            }
            foreach (var item in l_del)
            {
                dataGridView_lxxx.Rows.Remove(item);
            }

            pager_kh.PageSize = 20;
            int total = 0;
            List<Assistant.Model.customercontact> l = new Assistant.BLL.customercontact().GetList(" CustomerInfoId='" + c.CustomerInfoId + "' ", pager_kh.PageIndex, pager_kh.PageSize, "createtime", true, out total);

            pager_kh.Count = total;
            for (int i = 0; i < l.Count; i++)
            {
                dataGridView_lxxx.Rows.Add();
                int length = dataGridView_lxxx.Rows.Count - 1;
                dataGridView_lxxx.Rows[length].Cells["Column1"].Value = l[i].id;
                dataGridView_lxxx.Rows[length].Cells["Column2"].Value = l[i].Phone;
                dataGridView_lxxx.Rows[length].Cells["Column3"].Value = l[i].Name;
                dataGridView_lxxx.Rows[length].Cells["Column4"].Value = l[i].Notes;
                dataGridView_lxxx.Rows[length].Cells["Column5"].Value = base.FormatDatatime(l[i].CreateTime);
                dataGridView_lxxx.Rows[length].Cells["Column7"].Value = base.FormatDatatime(l[i].UpdateTime);
                dataGridView_lxxx.Rows[length].Cells["Column6"].Value = l[i].CustomerContactId;
            }

            //总页数
            pager_kh.PageCount = pager_kh.Count / pager_kh.PageSize;

            if (pager_kh.Count % pager_kh.PageSize != 0)
                pager_kh.PageCount = pager_kh.PageCount + 1;
            //格式化分页控件
            pager_kh.isEnable();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView_lxxx.Rows.Count == 0) return;
            int i = dataGridView_lxxx.SelectedRows[0].Index;
            if (dataGridView_lxxx.Rows[i].Cells["Column6"].Value.ToString() != "-1")
            {
                if (!ShowMsg("是否确定编辑此项？", ShowMsgType.question)) return;
            }

            //显示编辑框
            this.groupBox2.Visible = true;
            this.textBox_phone.Text = dataGridView_lxxx.Rows[i].Cells["Column2"].Value.ToString();
            this.textBox_name.Text = dataGridView_lxxx.Rows[i].Cells["Column3"].Value == null ? ""
                : dataGridView_lxxx.Rows[i].Cells["Column3"].Value.ToString();
            this.textBox_bzxx.Text = dataGridView_lxxx.Rows[i].Cells["Column4"].Value == null ? ""
                : dataGridView_lxxx.Rows[i].Cells["Column4"].Value.ToString();
            edit_rindex = i;
            //冻结
            dataGridView_lxxx.Enabled = false;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dataGridView_lxxx.Rows.Count == 0) return;
            int i = dataGridView_lxxx.SelectedRows[0].Index;

            if (!ShowMsg("是否确定删除此项？\n\n\t注意：删除不可恢复，及时失效，无需点击保存！", ShowMsgType.question)) return;
            if (dataGridView_lxxx.Rows[i].Cells["Column6"].Value.ToString() != "-1")
            {
                if (!new Assistant.BLL.customercontact().Delete(dataGridView_lxxx.Rows[i].Cells["Column6"].Value.ToString()))
                {
                    ShowMsg("删除失败！", ShowMsgType.error);
                    return;
                }
            }
            dataGridView_lxxx.Rows.RemoveAt(i);
        }
    }
}
