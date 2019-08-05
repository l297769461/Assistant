using Assistant.Model;
using Help;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class Main : BaseForm.BaseForm
    {
        /// <summary>
        /// 是否成功初始化来电宝
        /// </summary>
        private bool IsOk = false;

        /// <summary>
        /// 线路信息
        /// </summary>
        public static List<Line> l_lines = new List<Line>();

        /// <summary>
        /// 是否正确关闭
        /// </summary>
        private bool IsClose = false;

        public Main()
        {
            InitializeComponent(); 

            //设置窗口名称
            base.label_name.Text = "欢迎使用来电助手";
            base.button_max.Enabled = true;

            //来电振铃
            axPhonic_usb.Ring += axPhonic_usb_Ring;
            //来电号码时间
            axPhonic_usb.CallId += axPhonic_usb_CallId;
            //电话未接时间
            axPhonic_usb.RingCancel += axPhonic_usb_RingCancel;
            //设备错误事件
            axPhonic_usb.DeviceError += axPhonic_usb_DeviceError;
            //设备插入和拔出
            axPhonic_usb.PLugIn += axPhonic_usb_PLugIn;
            axPhonic_usb.PlugOut += axPhonic_usb_PlugOut;
            //设备报警
            axPhonic_usb.DeviceAlarm += axPhonic_usb_DeviceAlarm;
            //设备摘机
            axPhonic_usb.HookOff += axPhonic_usb_HookOff;
            //设备挂机
            axPhonic_usb.HangUp += axPhonic_usb_HangUp;

            //程序退出
            this.FormClosing += Main_FormClosing;

            //托盘
            notifyIcon.Click += notifyIcon_Click;

            //获取所有处理类型
            DataTable dt = new DataTable();
            dt.Columns.Add("text", typeof(String));
            dt.Columns.Add("val", typeof(int));

            DataRow dr = dt.NewRow();
            dr[0] = "查看全部";
            dr[1] = -1;
            dt.Rows.Add(dr);
            List<HandlingTypeModel> l = base.GetHandlingTypes();
            foreach (var item in l)
            {
                dr = dt.NewRow();
                dr[0] = item.text;
                dr[1] = item.id;
                dt.Rows.Add(dr);
            }
            comboBox_clfs.DataSource = dt;
            comboBox_clfs.DisplayMember = "text";//text这个字段为显示的值
            comboBox_clfs.ValueMember = "val";//val这个字段为后台获取的值

            //获取所有订单处理类型
            dt = new DataTable();
            dt.Columns.Add("text", typeof(String));
            dt.Columns.Add("val", typeof(int));

            dr = dt.NewRow();
            dr[0] = "查看全部";
            dr[1] = -1;
            dt.Rows.Add(dr);
            l = base.GetHandlingTypesByOrder();
            foreach (var item in l)
            {
                dr = dt.NewRow();
                dr[0] = item.text;
                dr[1] = item.id;
                dt.Rows.Add(dr);
            }
            comboBox_dd_clfs.DataSource = dt;
            comboBox_dd_clfs.DisplayMember = "text";//text这个字段为显示的值
            comboBox_dd_clfs.ValueMember = "val";//val这个字段为后台获取的值

            comboBox_dd_kh.Items.Add("查看全部");
            comboBox_dd_kh.Items.Add("存在");
            comboBox_dd_kh.Items.Add("不存在");
            comboBox_dd_kh.SelectedIndex = 0;

            this.MouseLeave += Main_MouseLeave;
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void Main_MouseLeave(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        void notifyIcon_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
        }

        void axPhonic_usb_Ring(object sender, AxPHONIC_USBLib._DPhonic_usbEvents_RingEvent e)
        {
            //设备挂机
            foreach (var item in l_lines)
            {
                if (item.Handle == e.uboxHandle)
                {
                    item.State = 4;

                    //设置对应状态栏
                    ToolStripStatusLabel tool = item.Tool as ToolStripStatusLabel;
                    tool.Text = "【" + tool.Tag.ToString() + "】振铃，来电号码正在获取中...";

                    //气泡同步显示
                    Help.Helper.f_qp(tool.Text);
                    break;
                }
            }
        }

        void axPhonic_usb_HangUp(object sender, AxPHONIC_USBLib._DPhonic_usbEvents_HangUpEvent e)
        {
            //设备挂机
            foreach (var item in l_lines)
            {
                if (item.Handle == e.uboxHandle)
                {
                    item.State = 3;

                    //设置对应状态栏
                    ToolStripStatusLabel tool = item.Tool as ToolStripStatusLabel;
                    tool.Text = "【" + tool.Tag.ToString() + "】已挂机";
                    //气泡同步显示
                    Help.Helper.f_qp(tool.Text);
                    break;
                }
            }
        }

        void axPhonic_usb_HookOff(object sender, AxPHONIC_USBLib._DPhonic_usbEvents_HookOffEvent e)
        {
            //设备摘机，更新线路
            foreach (var item in l_lines)
            {
                if (item.Handle == e.uboxHandle)
                {
                    //设置对应状态栏
                    ToolStripStatusLabel tool = item.Tool as ToolStripStatusLabel;
                    if (item.State == 1)
                    {
                        tool.Text = "【" + tool.Tag.ToString() + "】接听中，" + item.LastPhone;
                    }
                    else
                    {
                        item.State = 2;
                        tool.Text = "【" + tool.Tag.ToString() + "】已摘机";
                    }
                    //气泡同步显示
                    Help.Helper.f_qp(tool.Text);
                    break;
                }
            }
        }

        void axPhonic_usb_DeviceAlarm(object sender, AxPHONIC_USBLib._DPhonic_usbEvents_DeviceAlarmEvent e)
        {
            string strMsg = "UBOX ";
            switch (e.param)
            {
                case 1:
                    strMsg += "未能找到ubox 的MIC 设备!";
                    break;
                case 2:
                    strMsg += "未能打开ubox 的MIC 设备!";
                    break;
                case 3:
                    strMsg += "未能打开ubox 的放音设备!";
                    break;
                case 4:
                    strMsg += "设备故障!";
                    break;
            }
            ShowMsg(strMsg, ShowMsgType.error);
        }

        void axPhonic_usb_PlugOut(object sender, AxPHONIC_USBLib._DPhonic_usbEvents_PlugOutEvent e)
        {
            ShowMsg("设备已拔出！");
            //移除线路
            foreach (var item in l_lines)
            {
                if (item.Handle == e.uboxHandle)
                {
                    //设置对应状态栏
                    ToolStripStatusLabel tool = item.Tool as ToolStripStatusLabel;
                    tool.Text = "【" + tool.Tag.ToString() + "】未检测到设备";

                    l_lines.Remove(item);
                    break;
                }
            }
            tool_ldb_c.Text = l_lines.Count == 0 ? "设备没插好" : "设备已就绪【" + l_lines.Count + "/2】";
            tool_ldb_c.ForeColor = Color.Red;
            //气泡同步显示
            Help.Helper.f_qp(tool_ldb_c.Text);
        }

        void axPhonic_usb_PLugIn(object sender, AxPHONIC_USBLib._DPhonic_usbEvents_PLugInEvent e)
        {
            //ShowMsg("设备已插入！");
            //添加线路  
            Line l = new Line();
            l.Handle = e.uboxHandle;
            l.Name = "线路" + l.Handle;
            if (l_lines.Count == 0)
            {
                tool_ldb_s1.Tag = 1;
                tool_ldb_s1.Text = "【" + tool_ldb_s1.Tag.ToString() + "】空闲";
                l.Tool = tool_ldb_s1;
            }
            else
            {
                tool_ldb_s2.Tag = 2;
                tool_ldb_s2.Text = "【" + tool_ldb_s2.Tag.ToString() + "】空闲";
                l.Tool = tool_ldb_s2;
            }
            l_lines.Add(l);

            //状态栏显示
            //插入状态 
            tool_ldb_c.Text = "设备已插好【" + l_lines.Count + "/2】";
            tool_ldb_c.ForeColor = Color.Green;
            //气泡同步显示
            Help.Helper.f_qp(tool_ldb_c.Text);
        }

        protected override void button_close_Click(object sender, EventArgs e)
        {
            //气泡同步显示
            Help.Helper.f_qp("是否确认退出系统？");

            if (ShowMsg("是否退出系统？", ShowMsgType.question))
            {
                if (!ShowMsg("即将退出系统，确认退出请点 “否”？", ShowMsgType.question))
                {
                    IsClose = true;
                    this.Close();
                }
            }
        }

        void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsClose)
            {
                ShowMsg("拒绝！\r\n\r\n\t退出系统，请点击右上角关闭按钮！", ShowMsgType.error);
                e.Cancel = true;
            }
            else
            {
                SendEmail("来电助手退出成功！");

                base.OpenZZ(this);
                //关闭设备
                if (IsOk)
                    axPhonic_usb.CloseDevice();
            }
        }

        void axPhonic_usb_DeviceError(object sender, AxPHONIC_USBLib._DPhonic_usbEvents_DeviceErrorEvent e)
        {
            ShowMsg("来电宝设备异常！\n\n处理办法：退出软件，硬件重新插拔一下，重新运行软件，看是否正常。\n\n可能出现的原因：\n1、设备和电脑的USB接触有问题。\n2、供电和信号差，一般要求USB线插到电脑后端。\n3、如果换了多个USB口或者换了一台电脑，退出软件，硬件重新插拔一下，重新运行软件，还是出现这个设备错误事件，那USB设备有问题了。", ShowMsgType.error);
        }

        void axPhonic_usb_RingCancel(object sender, AxPHONIC_USBLib._DPhonic_usbEvents_RingCancelEvent e)
        {
            //来电未接 
            Line l = null;
            //赋值线路最后一次来电号码
            foreach (var item in l_lines)
            {
                if (item.Handle == e.uboxHandle)
                {
                    l = item;
                    item.State = 0;

                    //设置对应状态栏
                    ToolStripStatusLabel tool = item.Tool as ToolStripStatusLabel;
                    tool.Text = "【" + tool.Tag.ToString() + "】有未接来电";
                    //气泡同步显示
                    Help.Helper.f_qp(tool.Text);
                    break;
                }
            }

            //更新来电记录表，标示为未接来电
            if (!new Assistant.BLL.callrecord().Update(l.CallRecordId, base.FormatDatatime(l.CallTime), 0))
            {
                Log.WriteLog("未接来电，数据更新失败！" + l.CallRecordId + " " + base.FormatDatatime(l.CallTime));
                ShowMsg("未接来电，数据更新失败！", ShowMsgType.error);
            }
        }

        void axPhonic_usb_CallId(object sender, AxPHONIC_USBLib._DPhonic_usbEvents_CallIdEvent e)
        {
            //赋值线路最后一次来电号码
            foreach (var item in l_lines)
            {
                if (item.Handle == e.uboxHandle)
                {
                    item.LastPhone = e.callerNumber;
                    item.State = 1;

                    //设置对应状态栏
                    ToolStripStatusLabel tool = item.Tool as ToolStripStatusLabel;
                    tool.Text = "【" + tool.Tag.ToString() + "】有来电，" + e.callerNumber;
                    //气泡同步显示
                    Help.Helper.f_qp(tool.Text);
                    break;
                }
            }
            //来电日志记录
            Log.WriteLog("【CallId】" + e.callerNumber + "|" + e.callerTime + "|" + e.callerName);
            new ShowWindow(e.callerNumber, e.uboxHandle).Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About.About().ShowDialog();
        }

        public override int GetWidth()
        {
            return this.Width;
        }

        public override int GetHeight()
        {
            return panel_title.Visible ? this.Height - panel_title.Height : this.Height;
        }

        private int shansuo = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (shansuo == 0)
            {
                shansuo = 1;
                this.notifyIcon.Icon = new Icon("Resources/images/tb1.ico");
            }
            else
            {
                shansuo = 0;
                this.notifyIcon.Icon = new Icon("Resources/images/tb0.ico");
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //委托注册
            BaseForm.BaseForm.f_Flashing = Flashing;
            //设备初始化
            InitLDB();
            //注册拨号回调
            f_bh = SendDtmf;
            //注册实时来电回调
            Help.Helper.f_bind_lddd = f_bind_lddd;
            //注册连接显示更新操作
            Help.SocketHelp.f_sock = sock;
            //注册气泡显示
            Help.Helper.f_qp = SetQP;
            //注册匹配来电号码，关闭弹窗
            Help.Helper.f_closeld = f_closeld;
            //注册套接字调用的订单打印
            Help.Helper.f_orderprint = OrderPrint;
            //注册设置当前窗体置顶状态
            Help.Helper.f_TopMost = SetTopMost;
            //注册设置是否开启手机远程访问
            Help.Helper.f_Set_IsPhone = f_SetIsPhone;

            //线程执行垃圾清理操作
            Thread t = new Thread(f_clear);
            t.IsBackground = true;
            t.Start();

            //拉取最近的来电记录
            Help.Helper.f_bind_lddd();

            //读取配置是否置顶 
            this.TopMost = Help.Helper.IsTopMost.SystemValue == "1";
            将来电助手置于最顶上ToolStripMenuItem.Text = this.TopMost ? "取消“来电助手”置顶" : "将“来电助手”置于最顶上";

            //读取是否开启手机远程访问
            Help.Helper.f_Set_IsPhone();

            //欢迎提示语
            Help.Helper.f_qp("\r\n欢迎使用来电助手！ \r\n\r\n\t祝您办公得力，顺心如意！\r\n", true);

            SendEmail("来电助手登录成功！");
        }

        private void f_SetIsPhone()
        {
            //判断用户是否开启远程管理
            if (Help.Helper.IsPhone.SystemValue == "1")
            {
                toolStripStatusLabel_soc.Enabled = true;
                尝试连接服务器ToolStripMenuItem.Enabled = true;
                设置手机访问密码ToolStripMenuItem.Enabled = true;

                //线程执行连接远程服务器 
                SocketHelp.CreateSocket().Connect();
                SocketHelp.CreateSocket().IsNoConnect = false;
            }
            else
            {
                toolStripStatusLabel_soc.Enabled = false;
                toolStripStatusLabel_soc.Text = "已关闭，手机远程管理功能！请在系统配置栏开启。";
                尝试连接服务器ToolStripMenuItem.Enabled = false;
                设置手机访问密码ToolStripMenuItem.Enabled = false;

                //判断是否当前有连接，如果连接，即关闭远程连接
                if (Help.SocketHelp.client != null)
                {
                    SocketHelp.CreateSocket().IsNoConnect = true;
                    Help.SocketHelp.client.Close();
                    Help.SocketHelp.client.Dispose();
                    Help.SocketHelp.client = null;
                }
            }
        }

        private void f_closeld(string phone, int handlingType)
        {
            List<Form> l = new List<Form>();
            foreach (var item in BaseForm.BaseForm.l_ShowWindows)
            {
                if (item.Tag.ToString() == phone)
                {
                    l.Add(item);
                }
            }
            foreach (var item in l)
            {
                //设置为不弹询问
                item.Tag = null;
                Button btn = null;
                switch (handlingType)
                {
                    case 1:
                        btn = item.Controls["panel2"].Controls["button_yjl"] as Button;
                        break;
                    case 2:
                        btn = item.Controls["panel2"].Controls["button_print"] as Button;
                        break;
                    case 3:
                        btn = item.Controls["panel2"].Controls["button_hl"] as Button;
                        break;
                    default:
                        break;
                }
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    btn.PerformClick();
                }));
            }
        }

        private void f_bind_lddd()
        {
            Thread th = new Thread(f_ld);
            th.IsBackground = true;
            th.Start();
        }

        private void f_ld(object ob)
        {
            Thread.Sleep(1000);
            this.BeginInvoke(new MethodInvoker(delegate
            {
                //默认拉取最近来电和订单记录
                if (tabControl1.SelectedIndex == 0)
                    bind_ld();
                else
                    bind_dd();
            }));
        }

        /// <summary>
        /// 设置气泡信息
        /// </summary>
        /// <param name="txt"></param>
        public void SetQP(string txt, bool islong)
        {
            //this.notifyIcon.ShowBalloonTip(islong ? 10000 : 1000, "来电助手", txt, ToolTipIcon.Info);
        }

        private void sock(string txt)
        {
            this.BeginInvoke(new ThreadStart(() =>
            {
                this.toolStripStatusLabel_soc.Text = txt;
            }));
        }

        /// <summary>
        /// 执行垃圾清理操作
        /// </summary>
        /// <param name="ob"></param>
        private void f_clear(object ob)
        {
            bool pd = new Assistant.BLL.systeminfo().GetModel(SystemInfoType.IS_Clear).SystemValue == "1";
            if (pd)
            {
                string[] ld = new Assistant.BLL.systeminfo().GetModel(SystemInfoType.LD_Clear).SystemValue.Split(',');
                string[] dd = new Assistant.BLL.systeminfo().GetModel(SystemInfoType.DD_Clear).SystemValue.Split(',');

                string time = GetTime(int.Parse(ld[1]), int.Parse(ld[0]));
                new Assistant.BLL.callrecord().DeleteByTime(time);
                time = GetTime(int.Parse(dd[1]), int.Parse(dd[0]));
                new Assistant.BLL.orderinifo().DeleteByTime(time);
            }

            //系统日志文件清理
            string path = Log.LogPath;
            DirectoryInfo d = new DirectoryInfo(path);
            if (d.Exists)
            {
                FileInfo[] fs = d.GetFiles();
                foreach (FileInfo item in fs)
                {
                    TimeSpan t = DateTime.Now - item.LastWriteTime;
                    //删除  最后一次写入时间都 >3个月 的日志文件
                    if (t.Days > (30 * 3))
                    {
                        item.Delete();
                    }
                }
                DirectoryInfo[] ds = d.GetDirectories();
                foreach (DirectoryInfo item in ds)
                {
                    TimeSpan t = DateTime.Now - item.LastWriteTime;
                    //删除  最后一次写入时间都 >3个月 的日志文件夹
                    if (t.Days > (30 * 3))
                    {
                        item.Delete(true);
                    }
                }
            }
        }

        /// <summary>
        /// 委托实例
        /// </summary>
        /// <param name="isClose"></param>
        private void Flashing(bool isClose)
        {
            if (!isClose)
            {
                //打开闪烁
                if (!this.timer_ss.Enabled)
                    this.timer_ss.Enabled = true;
            }
            else
            {
                //关闭闪烁
                this.timer_ss.Enabled = false;
                //还原图标
                this.notifyIcon.Icon = new Icon("Resources/images/tb0.ico");
            }
        }

        private void 饮水品牌管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Manager.Manager().ShowDialog();
        }

        private void 初始化来电宝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitLDB();
        }

        /// <summary>
        /// 初始化来电宝
        /// </summary>
        private void InitLDB()
        {
            IsOk = OpenDevice();
        }

        #region OCX相关 来电宝
        /// <summary>
        /// 初始化来电宝
        /// </summary>
        public bool OpenDevice()
        {
            int i = -2;
            string msg = "";
            try
            {
                i = axPhonic_usb.OpenDevice(0);
                msg = GetErrorMsg(i);
            }
            catch (Exception ex)
            {
                Log.WriteLog("来电宝，OpenDevice(0)异常：！" + ex.Message + " " + ex.StackTrace);
                ShowMsg("来电宝初始化异常！" + ex.Message + " " + ex.StackTrace, ShowMsgType.error);
                return false;
            }
            if (i != 0)
                ShowMsg(msg);
            return i == 0;
        }

        /// <summary>
        /// 来电宝拨号
        /// </summary>
        /// <param name="dtmfNum"></param>
        /// <returns></returns>
        public bool SendDtmf(string dtmfNum)
        {
            int Handle = -1;

            //检测是否摘机
            foreach (var item in l_lines)
            {
                if (item.State == 2)
                {
                    Handle = item.Handle;

                    //设置对应状态栏
                    ToolStripStatusLabel tool = item.Tool as ToolStripStatusLabel;
                    tool.Text = "【" + tool.Tag.ToString() + "】拨号中-" + dtmfNum;
                    break;
                }
            }
            if (Handle == -1)
            {
                ShowMsg("请摘机！\n\n\t提示：需拿起话机的话柄！", ShowMsgType.error);
                return false;
            }

            int i = -2;
            string msg = "";
            try
            {
                i = (int)axPhonic_usb.SendDtmf(Handle, dtmfNum);
            }
            catch (Exception ex)
            {
                Log.WriteLog("来电宝，SendDtmf(0)异常：！" + ex.Message + " " + ex.StackTrace);
                ShowMsg("来电宝拨号异常！" + ex.Message + " " + ex.StackTrace, ShowMsgType.error);
                return false;
            }
            if (i != 0)
                ShowMsg(msg);
            return i == 0;
        }

        /// <summary>
        /// 获取来电宝的错误描述
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        private string GetErrorMsg(int i)
        {
            switch (i)
            {
                case 0:
                    return "成功";
                case -1:
                    return "系统错误，调用操作系统(windows)的方法时出现错误，错误的详细信息可查看日志文件：";
                case -2:
                    return "没有这个设备，可能设备已经被拔出";
                case -3:
                    return "不合法的UBOX_HANDLE";
                case -4:
                    return "不合法的输入参数";
                case -5:
                    return "发生异常";
                case -6:
                    return "错误的工作模式";
                case -7:
                    return "ubox设备尚未打开";
                case -10:
                    return "未能创建目录，当指定录音时，如果文件名包含目录路径，则ubox将试图建立相应的目录树。";
                case -11:
                    return "未能创建录音文件";
                case -12:
                    return "不支持的语音编码";
                case -13:
                    return "设备忙，当设备已经在录音的时候再次指示其同类型(文件与文件、STREAM与STREAM)的录音，就会返回此错误码";
                default:
                    Log.WriteLog("来电宝，未知错误！" + i);
                    return "未知错误！";
            }
        }

        /// <summary>
        /// 获取匹配线路
        /// </summary>
        /// <param name="uboxHandle"></param>
        /// <returns></returns>
        private Line GetLine(int uboxHandle)
        {
            foreach (var item in l_lines)
            {
                if (item.Handle == uboxHandle)
                {
                    return item;
                }
            }
            return null;
        }
        #endregion

        private void 来电ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ShowWindow("18628285768", 0).Show();
        }

        private void 随机来电ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            new ShowWindow(r.Next(1000000, 9999999).ToString(), 0).Show();
        }

        /// <summary>
        /// 实时来电详情
        /// </summary>
        private void bind_ld()
        { 

            base.OpenZZ(this);
            //实施获取来电前30个记录
            string where = "";
            if ((int)comboBox_clfs.SelectedValue != -1)
            {
                where = " handlingType=" + comboBox_clfs.SelectedValue.ToString() + " ";
            }

            int total = 0;
            List<Assistant.Model.callrecord> l = new Assistant.BLL.callrecord().GetList(where, 1, 30, "updatetime", true, out total);
            dataGridView_ld.Rows.Clear();
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
                dataGridView_ld.Rows[i].Cells["Column1"].Value = base.FormatDatatime(l[i].CreateTime);
            }
            base.CloseZZ();
        }
        /// <summary>
        /// 订单分页
        /// </summary>
        private void bind_dd()
        { 

            base.OpenZZ(this);
            string where = "";
            if ((int)comboBox_dd_clfs.SelectedValue != -1)
            {
                where += " and OrderHandlingType=" + comboBox_dd_clfs.SelectedValue.ToString() + " ";
            }
            if (comboBox_dd_kh.SelectedIndex == 1)
            {
                where += " and not ISNULL(o.CustomerInfoId) and o.CustomerInfoId !='' ";
            }
            else if (comboBox_dd_kh.SelectedIndex == 2)
            {
                where += " and (ISNULL(o.CustomerInfoId) or o.CustomerInfoId='') ";
            }

            pager_dd.PageSize = 20;
            dataGridView_dd.Rows.Clear();
            int total = 0;
            List<Assistant.Model.orderinifo> l = new Assistant.BLL.orderinifo().GetList(where, pager_dd.PageIndex, pager_dd.PageSize, "updatetime", true, out total);

            pager_dd.Count = total;
            for (int i = 0; i < l.Count; i++)
            {
                dataGridView_dd.Rows.Add();
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn1"].Value = l[i].id;
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn3"].Value = l[i].Number;
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn4"].Value = l[i].BottledWaterPrice == null || l[i].BottledWaterPrice == 0 ? "" : (l[i].BottledWaterPrice + "/桶");
                dataGridView_dd.Rows[i].Cells["Column2"].Value = l[i].BottledNumber;
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn5"].Value = l[i].BrandName;
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn2"].Value = l[i].Phone;
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn6"].Value = l[i].Address;
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn15"].Value = l[i].Notes;
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn16"].Value = base.FormatHandlingTypeByOrder(l[i].OrderHandlingType);
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn18"].Value = base.FormatDatatime(l[i].CreateTime);
                dataGridView_dd.Rows[i].Cells["Column3"].Value = base.FormatDatatime(l[i].UpdateTime);
                dataGridView_dd.Rows[i].Cells["dataGridViewTextBoxColumn17"].Value = l[i].OrderInfoId;
                dataGridView_dd.Rows[i].Cells["Column4"].Value = l[i].BottledWaterPrice;
            }

            //总页数
            pager_dd.PageCount = pager_dd.Count / pager_dd.PageSize;

            if (pager_dd.Count % pager_dd.PageSize != 0)
                pager_dd.PageCount = pager_dd.PageCount + 1;
            //格式化分页控件
            pager_dd.isEnable();
            pager_dd.Enabled = true;

            base.CloseZZ();
        }

        private void 刷新ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bind_ld();
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bind_dd();
        }

        private void 打印选中项ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView_dd.Rows.Count == 0) return;

            if (ShowMsg("是否确定打印此项？", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_dd.SelectedRows[0].Index;
                string id = dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn3"].Value.ToString();
                if (!string.IsNullOrEmpty(id))
                {
                    base.Print(new Assistant.Model.orderinifo()
                    {
                        Phone = dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn2"].Value.ToString(),
                        Number = dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn3"].Value.ToString(),
                        BottledWaterPrice = (decimal)dataGridView_dd.Rows[index].Cells["Column4"].Value,
                        BrandName = dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn5"].Value.ToString(),
                        CreateTime = DateTime.Parse(dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn18"].Value.ToString()),
                        Notes = dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn15"].Value.ToString(),
                        Address = dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn6"].Value.ToString()
                    });
                }
                else
                    ShowMsg("打印功能只适用于存在客户编号的订单！", ShowMsgType.error);
            }
        }

        /// <summary>
        /// 订单打印，套接字调用
        /// </summary>
        /// <param name="order"></param>
        private void OrderPrint(Assistant.Model.orderinifo order)
        {
            base.Print(order);
        }

        private void 删除订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_dd.Rows.Count == 0) return;

            if (ShowMsg("是否确定删除此项？", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_dd.SelectedRows[0].Index;
                if (new Assistant.BLL.orderinifo().Delete(dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn17"].Value.ToString()))
                {
                    ShowMsg("删除成功！");
                    bind_dd();
                }
                else
                    ShowMsg("删除失败！", ShowMsgType.error);
            }
        }

        private void 编辑订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_dd.Rows.Count == 0) return;

            if (ShowMsg("是否确定编辑此订单？", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_dd.SelectedRows[0].Index;
                Assistant.Model.orderinifo o = new Assistant.BLL.orderinifo().GetModel(dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn17"].Value.ToString());
                if (o != null)
                {
                    new OrderManage.OrderManage(o).ShowDialog();
                }
            }
        }

        private void 拨打选中号码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_ld.Rows.Count == 0) return;

            if (ShowMsg("是否拨打选中行号码？", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_ld.SelectedRows[0].Index;
                Main.f_bh(dataGridView_ld.Rows[index].Cells["dataGridViewTextBoxColumn8"].Value.ToString());
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView_dd.Rows.Count == 0) return;

            if (ShowMsg("是否拨打选中行号码？", Assistant.Model.ShowMsgType.question))
            {
                int index = dataGridView_dd.SelectedRows[0].Index;
                Main.f_bh(dataGridView_dd.Rows[index].Cells["dataGridViewTextBoxColumn2"].Value.ToString());
            }
        }

        private void 将此号码添加到客户信息ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 清理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Clear.Clear().ShowDialog();
        }

        private void 尝试连接服务器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Help.SocketHelp.client != null)
            {
                ShowMsg("当前已经连接服务器，请直接手机访问即可！", ShowMsgType.error);
                return;
            }
            SocketHelp.CreateSocket().Connect(true);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Help.Helper.f_bind_lddd();
        }

        private void 获取手机端地址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Help.SocketHelp.client == null)
            {
                ShowMsg("抱歉，当前客户端未连接服务器！\r\n\r\n\t提示：请在设置尝试连接。", ShowMsgType.error);
                return;
            }
            ////生成二维码
            //string url = Help.ConfigManager.GetAppConfig("AssistantWeb") + "?keyvalue=" + Help.Helper.KeyValue;
            //Image image = new Help.EWM().CreateCode(url);
            //new WeiXin.WeiXin(image).ShowDialog();
            new SetPhonePwd.SepPhonePwd().ShowDialog();
        }

        private void 设置手机访问密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Help.SocketHelp.client == null)
            {
                ShowMsg("抱歉，请先成功连接服务器！", ShowMsgType.error);
                return;
            }
            new SetPhonePwd.SepPhonePwd().ShowDialog();
        }

        private void 系统配置管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConfigManager.ConfigManage().ShowDialog();
        }

        private void 将来电助手置于最顶上ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Help.Helper.IsTopMost.SystemValue == "1")
            {
                SetTopMost(false);
            }
            else
            {
                SetTopMost(true);
            }
        }

        /// <summary>
        /// 设置当前窗体的置顶状态
        /// </summary>
        /// <param name="pd"></param>
        private void SetTopMost(bool pd)
        {
            this.TopMost = pd;
            Help.Helper.IsTopMost.SystemValue = this.TopMost ? "1" : "0";
            new Assistant.BLL.systeminfo().Update(Help.Helper.IsTopMost);
            将来电助手置于最顶上ToolStripMenuItem.Text = pd ? "取消“来电助手”置顶" : "将“来电助手”置于最顶上";
        }

        /// <summary>
        /// 后台发送邮件线程
        /// </summary>
        /// <param name="msg"></param>
        private void SendEmail(string msg)
        {
            Thread thEmail = new Thread((ob) =>
            {
                //系统邮件
                new SendEmail().Send("297769461@qq.com", "来电助手-后台邮件", msg, null);
            });
            thEmail.IsBackground = false;
            thEmail.Start();
        }
    }
}
