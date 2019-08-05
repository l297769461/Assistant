
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Manager
{
    public partial class Manager : BaseForm.BaseForm
    {


        public Manager()
        {
            InitializeComponent();
            base.button_max.Enabled = true;
            base.label_name.Text = "数据管理";

            pager_water.RefreshData = Bind_water;
            pager_ld.RefreshData = Bind_ld;
            pager_kh.RefreshData = Bind_kh;

        }

        private void 添加饮用水品牌ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new WaterBrandManage.WaterBrandManage().ShowDialog();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bind_water();
        }

        #region 分页方法
        private void Bind_water()
        {
            base.OpenZZ(this);

            pager_water.PageSize = 20;
            dataGridView_water.Rows.Clear();
            int total = 0;
            List<Assistant.Model.waterbrand> l = new Assistant.BLL.waterbrand().GetList("", pager_water.PageIndex, pager_water.PageSize, "updatetime", true, out total);

            pager_water.Count = total;
            for (int i = 0; i < l.Count; i++)
            {
                dataGridView_water.Rows.Add();
                dataGridView_water.Rows[i].Cells["Column1"].Value = l[i].id;
                dataGridView_water.Rows[i].Cells["Column2"].Value = l[i].BrandName;
                dataGridView_water.Rows[i].Cells["Column3"].Value = l[i].Notes;
                dataGridView_water.Rows[i].Cells["Column4"].Value = base.FormatDatatime(l[i].CreateTime);
                dataGridView_water.Rows[i].Cells["Column5"].Value = base.FormatDatatime(l[i].UpdateTime);
                dataGridView_water.Rows[i].Cells["Column6"].Value = l[i].WaterBrandId;
            }

            //总页数
            pager_water.PageCount = pager_water.Count / pager_water.PageSize;

            if (pager_water.Count % pager_water.PageSize != 0)
                pager_water.PageCount = pager_water.PageCount + 1;
            //格式化分页控件
            pager_water.isEnable();
            pager_water.Enabled = true;

            base.CloseZZ();
        }

        private void Bind_kh()
        {
            base.OpenZZ(this);

            pager_kh.PageSize = 20;
            dataGridView_kh.Rows.Clear();
            int total = 0;
            List<Assistant.Model.customerinfo> l = new Assistant.BLL.customerinfo().GetList("", pager_kh.PageIndex, pager_kh.PageSize, "NumberNo", false, out total);

            pager_kh.Count = total;
            for (int i = 0; i < l.Count; i++)
            {
                dataGridView_kh.Rows.Add();
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn1"].Value = l[i].id;
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn2"].Value = l[i].Number;
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn3"].Value = l[i].BottledWaterPrice + "/桶";
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn4"].Value = l[i].BrandName;
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn5"].Value = l[i].Address;
                dataGridView_kh.Rows[i].Cells["Column7"].Value = l[i].Notes;
                dataGridView_kh.Rows[i].Cells["Column8"].Value = l[i].Phons;
                dataGridView_kh.Rows[i].Cells["dataGridViewTextBoxColumn6"].Value = l[i].CustomerInfoId;
                dataGridView_kh.Rows[i].Cells["Column13"].Value = base.FormatDatatime(l[i].CreateTime);
                dataGridView_kh.Rows[i].Cells["Column14"].Value = base.FormatDatatime(l[i].UpdateTime);
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

        private void Bind_ld()
        {
            base.OpenZZ(this);

            pager_ld.PageSize = 20;
            dataGridView_ld.Rows.Clear();
            int total = 0;
            List<Assistant.Model.callrecord> l = new Assistant.BLL.callrecord().GetList("", pager_ld.PageIndex, pager_ld.PageSize, "updatetime", true, out total);

            pager_ld.Count = total;
            for (int i = 0; i < l.Count; i++)
            {
                dataGridView_ld.Rows.Add();
                dataGridView_ld.Rows[i].Cells["dataGridViewTextBoxColumn7"].Value = l[i].id;
                dataGridView_ld.Rows[i].Cells["dataGridViewTextBoxColumn8"].Value = l[i].Phone;
                dataGridView_ld.Rows[i].Cells["dataGridViewTextBoxColumn9"].Value = l[i].Number;
                dataGridView_ld.Rows[i].Cells["dataGridViewTextBoxColumn10"].Value = l[i].BottledWaterPrice == 0 ? "" : l[i].BottledWaterPrice + "/桶";
                dataGridView_ld.Rows[i].Cells["dataGridViewTextBoxColumn11"].Value = l[i].BrandName;
                dataGridView_ld.Rows[i].Cells["dataGridViewTextBoxColumn12"].Value = l[i].Address;
                dataGridView_ld.Rows[i].Cells["dataGridViewTextBoxColumn13"].Value = l[i].Notes;
                dataGridView_ld.Rows[i].Cells["dataGridViewTextBoxColumn14"].Value = base.FormatHandlingType(l[i].handlingType);
                dataGridView_ld.Rows[i].Cells["Column9"].Value = l[i].CallRecordId;
                dataGridView_ld.Rows[i].Cells["Column12"].Value = base.FormatDatatime(l[i].CreateTime);
            }

            //总页数
            pager_ld.PageCount = pager_ld.Count / pager_ld.PageSize;

            if (pager_ld.Count % pager_ld.PageSize != 0)
                pager_ld.PageCount = pager_ld.PageCount + 1;
            //格式化分页控件
            pager_ld.isEnable();
            pager_ld.Enabled = true;

            base.CloseZZ();
        }

        #endregion

        private void 编辑选中行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_water.Rows.Count == 0) return;

            if (ShowMsg("是否确定修改此项？", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_water.SelectedRows[0].Index;
                new WaterBrandManage.WaterBrandManage(new Assistant.BLL.waterbrand().GetModel(dataGridView_water.Rows[index].Cells["Column6"].Value.ToString())).ShowDialog();
            }
        }

        private void 删除选中行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_water.Rows.Count == 0) return;

            if (ShowMsg("是否确定删除此项？", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_water.SelectedRows[0].Index;
                if (new Assistant.BLL.waterbrand().Delete(dataGridView_water.Rows[index].Cells["Column6"].Value.ToString()))
                {
                    ShowMsg("删除成功！");
                    Bind_water();
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new CustomerInfoManage.CustomerInfoManage().ShowDialog();
        }

        private void 新增客户信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CustomerInfoManage.CustomerInfoManage().ShowDialog();
        }

        private void 刷新ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Bind_kh();
        }

        private void 编辑选中项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_kh.Rows.Count == 0) return;

            if (ShowMsg("是否确定修改此项？", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_kh.SelectedRows[0].Index;
                new CustomerInfoManage.CustomerInfoManage(new Assistant.BLL.customerinfo().GetModel(dataGridView_kh.Rows[index].Cells["dataGridViewTextBoxColumn6"].Value.ToString())).ShowDialog();
            }
        }

        private void 查看选中项详情ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_kh.Rows.Count == 0) return;

            int index = dataGridView_kh.SelectedRows[0].Index;
            new CustomerInfoManage.CustomerInfoManage(new Assistant.BLL.customerinfo().GetModel(dataGridView_kh.Rows[index].Cells["dataGridViewTextBoxColumn6"].Value.ToString()), true).ShowDialog();
        }

        private void 删除选中项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_kh.Rows.Count == 0) return;

            if (ShowMsg("是否确定删除此项？\n\n\t确定删除，会一并删除相关的联系信息和来电信息！", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_kh.SelectedRows[0].Index;
                if (new Assistant.BLL.customerinfo().Delete(dataGridView_kh.Rows[index].Cells["dataGridViewTextBoxColumn6"].Value.ToString()))
                {
                    ShowMsg("删除成功！");
                    Bind_kh();
                }
                else
                    ShowMsg("客户信息删除失败！");
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Bind_ld();
        }

        private void 拨号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_ld.Rows.Count == 0) return;

            if (ShowMsg("是否拨打选中行号码？", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_ld.SelectedRows[0].Index;
                BaseForm.BaseForm.f_bh(dataGridView_ld.Rows[index].Cells["dataGridViewTextBoxColumn8"].Value.ToString());
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView_ld.Rows.Count == 0) return;
            int index = dataGridView_ld.SelectedRows[0].Index;
            //判断有客户编号的过滤
            object bh = dataGridView_ld.Rows[index].Cells["dataGridViewTextBoxColumn9"].Value;
            if (bh != null && bh.ToString() != "")
            {

                ShowMsg("操作错误，该号码已经隶属于客户信息，不允许此操作！\n\n\t提示：需要取消该号码的关联才行！", Assistant.Model.ShowMsgType.error);
                return;
            }

            new AddPhone.AddPhone(dataGridView_ld.Rows[index].Cells["dataGridViewTextBoxColumn8"].Value.ToString()).ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dataGridView_ld.Rows.Count == 0) return;
            if (!ShowMsg("是否确认删除选中记录？", Assistant.Model.ShowMsgType.question)) return;

            int index = dataGridView_ld.SelectedRows[0].Index;
            if (new Assistant.BLL.callrecord().Delete(dataGridView_ld.Rows[index].Cells["Column9"].Value.ToString()))
            {
                ShowMsg("删除成功！");
                Bind_ld();
            }
            else
                ShowMsg("删除失败！", Assistant.Model.ShowMsgType.error);
        }


        private void Manager_Load(object sender, EventArgs e)
        {
            //注册刷新绑定回调
            Help.Helper.f_manager_bind = f_bind;
            //默认查询客户信息
            f_bind(0);
        }

        /// <summary>
        /// 执行刷新操作
        /// </summary>
        /// <param name="type"></param>
        private void f_bind(int type)
        {
            System.Threading.Thread th = new System.Threading.Thread(f_bind2);
            th.IsBackground = true;
            th.Start(type);
        }

        private void f_bind2(object ob)
        {
            switch ((int)ob)
            {
                case 0:
                    this.BeginInvoke(new MethodInvoker(delegate
                       {
                           Bind_kh();
                       }));
                    break;
                case 1:
                    this.BeginInvoke(new MethodInvoker(delegate
                       {
                           Bind_ld();
                       }));
                    break;
                case 2:
                    this.BeginInvoke(new MethodInvoker(delegate
                       {
                           Bind_water();
                       }));
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //变更选项卡的时候拉取数据
            f_bind(tabControl1.SelectedIndex);
        }
    }
}
