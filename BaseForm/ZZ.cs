using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseForm
{
    public partial class ZZ : Form
    {
        public ZZ()
        {
            InitializeComponent();

            this.pictureBox1.MouseDoubleClick += pictureBox1_MouseDoubleClick;
            this.pictureBox1.MouseLeave += pictureBox1_MouseLeave;
        }

        void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            index = 0;
            label1.Text = "数据加载中，请稍等...";
        }

        void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            index++;
            if (index > 3)
            {
                panel1.Width += 200;
                label1.Text = "您正在强制关闭当前，关闭请继续双击！";
                if (index >= 10)
                    this.Close();
            }
        }
        /// <summary>
        /// 强制关闭计数
        /// </summary>
        private int index = 0;

        private void ZZ_Load(object sender, EventArgs e)
        {
            this.panel1.Location = new Point(this.Width / 2 - panel1.Width + 50, this.Height / 2 - this.panel1.Height);
        }
    }
}
