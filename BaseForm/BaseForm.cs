using Assistant.Model;
using Help;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaseForm
{
    public partial class BaseForm : Form
    {
        /// <summary>
        /// 移动窗体需要
        /// </summary>
        private int x, y = 0;
        /// <summary>
        /// 遮罩
        /// </summary>
        private static Form f_ZZ = new ZZ();



        /// <summary>
        /// 拨号委托
        /// </summary>
        /// <param name="Phone"></param>
        /// <returns></returns>
        public delegate bool w_bh(string Phone);
        /// <summary>
        /// 拨号回调
        /// </summary>
        public static w_bh f_bh;

        public BaseForm()
        {
            InitializeComponent();

            //事件注册
            panel_title.MouseDown += panel_title_MouseDown;
            panel_title.MouseMove += panel_title_MouseMove;
            panel_title.MouseDoubleClick += button_max_Click;

            //顶部3个按钮
            button_min.Click += button_min_Click;
            button_max.Click += button_max_Click;
            button_close.Click += button_close_Click;

            this.FormClosing += BaseForm_FormClosing;

            //是否置顶
            this.TopMost = Help.Helper.IsTopMost != null && Help.Helper.IsTopMost.SystemValue == "1";
        }

        void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        { }

        /// <summary>
        /// 张开遮罩
        /// </summary>
        protected void OpenZZ(BaseForm f)
        {
            //判断当前窗口有无标题栏
            //f_ZZ.Width = f.Width - 6;
            //f_ZZ.Height = f.Height - 6;
            //f_ZZ.Show();
            //f_ZZ.Location = new Point(f.Location.X + 2, f.Location.Y + 2);
        }

        /// <summary>
        /// 张开遮罩
        /// </summary>
        protected void CloseZZ()
        {
            //f_ZZ.Hide();
        }

        protected virtual void button_close_Click(object sender, EventArgs e)
        {
            if (ShowMsg("是否关闭当前窗口？", ShowMsgType.question))
            {
                this.Close();
            }
        }

        void button_max_Click(object sender, EventArgs e)
        {
            if (this.button_max.Enabled)
                this.WindowState = this.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
        }

        void button_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        void panel_title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Location = new Point(Control.MousePosition.X - x, Control.MousePosition.Y - y);
            }
        }

        void panel_title_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.Location.X;
            y = e.Location.Y;
        }

        #region 弹窗封装
        /// <summary>
        /// 弹窗封装
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ShowMsg(string text, ShowMsgType type = ShowMsgType.defalut)
        {
            switch (type)
            {
                case ShowMsgType.defalut:
                    MessageBox.Show(text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return true;
                case ShowMsgType.error:
                    MessageBox.Show(text, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return true;
                case ShowMsgType.warning:
                    MessageBox.Show(text, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                case ShowMsgType.question:
                    return DialogResult.Yes == MessageBox.Show(text, "请问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                default:
                    break;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 获取当前窗体的高度
        /// </summary>
        /// <returns></returns>
        public virtual int GetWidth()
        {
            return this.Width;
        }

        /// <summary>
        /// 获取当前窗体宽度
        /// </summary>
        /// <returns></returns>
        public virtual int GetHeight()
        {
            return this.Height;
        }

        /// <summary>
        /// 当前弹窗集合
        /// </summary>
        public static List<Form> l_ShowWindows = new List<Form>();

        /// <summary>
        /// 托盘闪烁委托
        /// </summary>
        public delegate void w_Flashing(bool isClose = false);

        /// <summary>
        /// 托盘闪烁委托调用
        /// </summary>
        public static w_Flashing f_Flashing;
        
        /// <summary>
        /// 打印对象
        /// </summary>
        private StringReader sr = null;

        /// <summary>
        /// 执行打印
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool Print(string str)
        {
            bool result = true;
            try
            {
                sr = new StringReader(str);
                PrintDocument pd = new PrintDocument();
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                pd.DefaultPageSettings.Margins.Top = 0;
                pd.DefaultPageSettings.Margins.Left = 0;
                System.Drawing.Printing.PageSettings df = new System.Drawing.Printing.PageSettings();
                //int width = 58;
                //int _width = (int)(width / 25.4 * 100);
                //df.PaperSize = new PaperSize("custom", _width, 320);
                df.Margins = new Margins(0, 0, 0, 0);
                pd.DefaultPageSettings = df;
                pd.PrinterSettings.PrinterName = pd.DefaultPageSettings.PrinterSettings.PrinterName;//默认打印机
                pd.PrintPage += pd_PrintPage;
                pd.Print();
                //PrintDocument pd = new PrintDocument();
                //pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                //pd.DefaultPageSettings.Margins.Top = 0;
                //pd.DefaultPageSettings.Margins.Left = 0;
                //pd.DefaultPageSettings.PaperSize.Width = 320;
                //pd.DefaultPageSettings.PaperSize.Height = 5150;
                //pd.PrinterSettings.PrinterName = pd.DefaultPageSettings.PrinterSettings.PrinterName;//默认打印机
                //pd.PrintPage += pd_PrintPage;
                //pd.Print();
            }
            catch (Exception ex)
            {
                Log.WriteLog("打印内容：" + str);
                Log.WriteLog("打印异常：" + ex.Message + " " + ex.StackTrace);
                result = false;
                ShowMsg(ex.Message, ShowMsgType.error);
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return result;
        }

        /// <summary>
        /// 执行打印
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool Print(string phone, customerinfo c, DateTime? CallTime = null)
        {
            if (CallTime == null)
                CallTime = DateTime.Now;
            return Print(GetPrintText(phone, c, (DateTime)CallTime));
        }

        /// <summary>
        /// 执行打印
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool Print(orderinifo o)
        {
            return Print(GetPrintText(o));
        }

        void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Font printFont = new Font("Arial", 7.5f);//打印字体
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = "";
            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
            while (count < linesPerPage && ((line = sr.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }
            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }

        /// <summary>
        /// 格式化时间格式
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string FormatDatatime(DateTime? t)
        {
            if (t == null) return "";
            return ((DateTime)t).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 格式化来电处理类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string FormatHandlingType(int? type)
        {
            if (type == null) return "未处理";

            List<HandlingTypeModel> l = GetHandlingTypes();
            l = l.Where(c => c.id == type).ToList();
            if (l.Count == 0) return "错误";
            return l[0].text;
        }
        /// <summary>
        /// 格式化订单处理类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string FormatHandlingTypeByOrder(int? type)
        {
            if (type == null) return "未处理";

            List<HandlingTypeModel> l = GetHandlingTypesByOrder();
            l = l.Where(c => c.id == type).ToList();
            if (l.Count == 0) return "错误";
            return l[0].text;
        }

        /// <summary>
        /// 获取所有来电处理类型
        /// </summary>
        /// <returns></returns>
        public List<HandlingTypeModel> GetHandlingTypes()
        {
            List<HandlingTypeModel> l = new List<HandlingTypeModel>();
            l.Add(new HandlingTypeModel()
            {
                id = 0,
                text = "未接"
            });
            l.Add(new HandlingTypeModel()
            {
                id = 1,
                text = "手抄"
            });
            l.Add(new HandlingTypeModel()
            {
                id = 2,
                text = "打印"
            });
            l.Add(new HandlingTypeModel()
            {
                id = 3,
                text = "忽略"
            });
            return l;
        }


        /// <summary>
        /// 获取所有订单处理类型
        /// </summary>
        /// <returns></returns>
        public List<HandlingTypeModel> GetHandlingTypesByOrder()
        {
            List<HandlingTypeModel> l = new List<HandlingTypeModel>();
            l.Add(new HandlingTypeModel()
            {
                id = 1,
                text = "手抄"
            });
            l.Add(new HandlingTypeModel()
            {
                id = 2,
                text = "打印"
            });
            return l;
        }

        /// <summary>
        /// 获取打印字符串
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public string GetPrintText(string phone, customerinfo c, DateTime time)
        {
            StringBuilder str = new StringBuilder(); 
            str.Append("来电号码：" + phone + "\n");
            str.Append("客户编号：" + c.Number + "\n");
            str.Append("桶装水单价：" + c.BottledWaterPrice + "/桶\n");
            str.Append("饮水品牌：" + c.BrandName + "\n");
            str.Append("来电时间：" + FormatDatatime(time) + "\n");
            string dz = SetAdds_str(c.Address);
            str.Append("送水地址：" + dz + "\n");
            str.Append("" + FormatDatatime(DateTime.Now) + "***********************************");
            return str.ToString();
        }

        private static string SetAdds_str(string adds)
        {
            string dz = "";
            if (!string.IsNullOrEmpty(adds))
            {
                dz = adds.Replace("\n", "");
                int i = 0, length = dz.Length;
                //首行剩余8个字符
                i += 8;
                while (i < length)
                {
                    dz = dz.Insert(i, "\n");
                    //新行可用字符
                    i += 14;
                }
            }
            return dz;
        }

        /// <summary>
        /// 获取打印字符串
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public string GetPrintText(orderinifo o)
        {
            StringBuilder str = new StringBuilder();
            str.Append("来电号码：" + o.Phone + "\n");
            str.Append("客户编号：" + o.Number + "\n");
            str.Append("桶装水单价：" + o.BottledWaterPrice + "/桶\n");
            str.Append("饮水品牌：" + o.BrandName + "\n");
            str.Append("来电时间：" + FormatDatatime(o.CreateTime) + "\n");
            string dz = SetAdds_str(o.Address);
            str.Append("送水地址：" + dz + "\n");
            if (!string.IsNullOrEmpty(o.Notes))
            {
                dz = SetAdds_str(o.Notes);
                str.Append("订单备注：" + dz + "\n");
            }
            str.Append("" + FormatDatatime(DateTime.Now) + "***********************************");
            return str.ToString();
        }

        /// <summary>
        /// 获取分解的桶装水单价
        /// </summary>
        /// <param name="BottledWaterPrice"></param>
        /// <returns></returns>
        public decimal[] GetBottledWaterPrice(decimal? BottledWaterPrice)
        {
            if (BottledWaterPrice == null)
                return new decimal[] { 0, 0 };

            decimal a = Math.Floor((decimal)BottledWaterPrice);
            decimal b = ((decimal)BottledWaterPrice) - a;
            if (b < 1)
            {
                string str = b.ToString();
                if (str.LastIndexOf("0") == str.Length - 1)
                    str = str.Substring(0, 3);
                int i = str.IndexOf('.');
                b = decimal.Parse(str.Substring(i + 1));
            }
            return new decimal[] { a, b };
        }

        /// <summary>
        /// 清理配置转换为时间格式
        /// </summary>
        /// <param name="comboxIndex"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetTime(int comboxIndex, int value)
        {
            int _moth = 0;
            if (comboxIndex == 0)
                _moth -= value;
            else
                _moth -= value * 12;
            string time = DateTime.Now.AddMonths(_moth).ToString("yyyy-MM-dd");
            return time;
        }

    }
}
