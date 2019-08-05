using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseForm
{
    public partial class Pager : UserControl
    {
        public delegate void RefreshPage();
        private RefreshPage _refresh;
        /// <summary>
        /// 页显示数
        /// </summary>
        private int _PageSize = 20;
        /// <summary>
        /// 页总数
        /// </summary>
        private int _PageCount = 0;
        /// <summary>
        ///  页码
        /// </summary>
        private int _PageIndex = 1;
        /// <summary>
        /// 数据条数
        /// </summary>
        private int _Count = 0;
        /// <summary>
        /// 跳转页码
        /// </summary>
        private int _GoIndex = 0;

        public void isEnable()
        {
            try
            {
                if (_PageIndex == 1)
                {
                    this.lbtnfrist.Enabled = false;
                    this.lbtnUP.Enabled = false;
                }
                else
                {
                    this.lbtnUP.Enabled = true;
                    this.lbtnfrist.Enabled = true;
                }

                if (this._PageIndex == this._PageCount)
                {
                    this.lbtnDown.Enabled = false;
                    this.lbtnlast.Enabled = false;
                }
                else
                {
                    this.lbtnDown.Enabled = true;
                    this.lbtnlast.Enabled = true;

                }
                if (this._Count == 0)
                {
                    this.lbtnDown.Enabled = false;
                    this.lbtnlast.Enabled = false;
                    this.lbtnfrist.Enabled = false;
                    this.lbtnUP.Enabled = false;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 获取或设置页显示数量
        /// </summary>
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        /// <summary>
        /// 获取或设置页数量
        /// </summary>
        public int PageCount
        {
            get { return _PageCount; }
            set
            {
                _PageCount = value;
                labpcount.Text = _PageCount.ToString();
            }
        }

        /// <summary>
        /// 获取或设置页码
        /// </summary>
        public int PageIndex
        {
            get { return Convert.ToInt32(labindex.Text); }
            set { _PageIndex = value; }
        }

        /// <summary>
        /// 获取或设置数据总数量
        /// </summary>
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                _Count = value;
                label_gj.Text = _Count.ToString();
                label_gj.Visible = _Count != 0;
            }
        }
        /// <summary>
        /// 获取或设置跳转页面
        /// </summary>
        public int GoIndex
        {
            get { return _GoIndex; }
            set { _GoIndex = value; }
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public RefreshPage RefreshData
        {
            set
            {
                _refresh = value;

            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Pager()
        {
            InitializeComponent();

            lbtnfrist.LinkClicked += lbtnfrist_LinkClicked;
            lbtnUP.LinkClicked += lbtnUP_LinkClicked;
            lbtnDown.LinkClicked += lbtnDown_LinkClicked;
            lbtnlast.LinkClicked += lbtnlast_LinkClicked;
            lbtnGO.LinkClicked += lbtnGO_LinkClicked;
        }

        void lbtnGO_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //跳转
            int tmp = 0;
            try
            {
                tmp = Convert.ToInt32(tbxGo.Text);
            }
            catch
            {
                tmp = 1;
                tbxGo.Text = "1";
            }
            if (tmp <= 0)
            {
                tmp = 1;
            }
            int tmp2 = Convert.ToInt32(labpcount.Text);
            if (tmp > tmp2)
            {
                _PageIndex = tmp2;
            }
            else
            {
                _PageIndex = tmp;
            }
            labindex.Text = _PageIndex.ToString();
            tbxGo.Text = _PageIndex.ToString();
            _refresh();
            isEnable();
        }

        void lbtnlast_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //最后页
            _PageIndex = Convert.ToInt32(labpcount.Text);
            labindex.Text = labpcount.Text;
            _refresh();
            isEnable();
        }

        void lbtnDown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //下一页
            int tmp = Convert.ToInt32(labpcount.Text);
            int tmp2 = Convert.ToInt32(labindex.Text);
            tmp2 = tmp2 + 1;
            if (tmp2 > tmp)
            {
                _PageIndex = tmp;
                labindex.Text = tmp.ToString();
            }
            else
            {
                _PageIndex = tmp2;
                labindex.Text = tmp2.ToString();
            }
            _refresh();
            isEnable();
        }

        void lbtnUP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //上一页
            int tmp = Convert.ToInt32(labindex.Text);
            tmp = tmp - 1;
            if (tmp <= 0)
            {
                tmp = 1;
            }
            _PageIndex = tmp;
            labindex.Text = _PageIndex.ToString();
            _refresh();
            isEnable();
        }

        void lbtnfrist_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //第一页
            _PageIndex = 1;
            labindex.Text = _PageIndex.ToString();
            _refresh();
            isEnable();
        }

        private void label_gj_VisibleChanged(object sender, EventArgs e)
        {
            label6.Visible = label_gj.Visible;
        }
    }
}
