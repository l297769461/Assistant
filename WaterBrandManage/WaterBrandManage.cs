using Assistant.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WaterBrandManage
{
    public partial class WaterBrandManage : BaseForm.BaseForm
    {
        /// <summary>
        /// false：新增；true：编辑
        /// </summary>
        private bool pd_view = false;
        /// <summary>
        /// 编辑对象
        /// </summary>
        private Assistant.Model.waterbrand w = null;

        public WaterBrandManage(Assistant.Model.waterbrand _w = null)
        {
            InitializeComponent();

            base.label_name.Text = "饮用水品牌管理";

            this.button1.Click += button1_Click;
            this.button2.Click += button2_Click;

            w = _w;
            if (w != null)
            {
                this.textBox1.Text = w.BrandName;
                this.richTextBox1.Text = w.Notes;
                pd_view = true;
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            if (ShowMsg("是否关闭当前窗口？", ShowMsgType.question))
                this.Close();
        }


        void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                ShowMsg("饮用水品牌名称不能为空！", ShowMsgType.error);
                return;
            }
            if (!pd_view)
            {
                if (ShowMsg("是否确定新增？", ShowMsgType.question))
                {

                    w = new waterbrand();
                    w.BrandName = this.textBox1.Text;
                    w.Notes = this.richTextBox1.Text;
                    w.WaterBrandId = Guid.NewGuid().ToString();
                    w.CreateTime = DateTime.Now;
                    w.UpdateTime = w.CreateTime;
                    if (new Assistant.BLL.waterbrand().Add(w))
                    {

                        ShowMsg("新增成功！");
                        //界面更新
                        Help.Helper.f_manager_bind(2);
                        this.Close();
                    }
                    else
                        ShowMsg("品牌名称重复，请修改！", ShowMsgType.error);
                }
            }
            else
            {

                if (ShowMsg("是否确定修改？", ShowMsgType.question))
                {
                    w.BrandName = this.textBox1.Text;
                    w.Notes = this.richTextBox1.Text;
                    w.CreateTime = w.CreateTime;
                    w.UpdateTime = DateTime.Now;
                    if (new Assistant.BLL.waterbrand().Update(w))
                    {
                        ShowMsg("修改成功！");
                        //界面更新
                        Help.Helper.f_manager_bind(2);
                        this.Close();
                    }
                    else
                        ShowMsg("品牌名称重复，请修改！", ShowMsgType.error);
                }
            }
        }


    }
}
