using Assistant.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeiXin
{
    public partial class WeiXin : BaseForm.BaseForm
    {
        public WeiXin(Image _image = null)
        {
            InitializeComponent();

            label_name.Text = "扫一扫";
            if (_image != null)
            {
                this.pictureBox1.Image = _image;
            }
            else
                this.pictureBox1.Image = imageList1.Images[0];
        }
    }
}
