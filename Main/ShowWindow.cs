using Assistant.Model;
using Help;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Main
{
    public partial class ShowWindow : BaseForm.BaseForm
    {
        /// <summary>
        /// 来电信息
        /// </summary>
        private callrecord call = null;
        /// <summary>
        /// 通过来电号码匹配现有客户信息
        /// </summary>
        customerinfo c = null;
        /// <summary>
        /// 窗口关闭倒计时
        /// </summary>
        int index_cloxe = 10;

        public ShowWindow(string phoneNuber, int uboxHandle)
        {
            InitializeComponent();

            this.Tag = phoneNuber;

            this.Load += ShowWindow_Load;
            this.FormClosed += ShowWindow_FormClosed;
            this.button_print.Click += button_print_Click;
            this.button_yjl.Click += button_yjl_Click;
            this.button_hl.Click += button_hl_Click;
            base.label_name.Text = "来电提醒";
            base.button_close.Click -= base.button_close_Click;
            base.button_close.Click += button_hl_Click;

            this.label_phone.Text = phoneNuber;
            this.label_ldsj.Text = base.FormatDatatime(DateTime.Now);

            call = new callrecord()
            {
                CallRecordId = Guid.NewGuid().ToString(),
                CreateTime = DateTime.Now,
                Phone = phoneNuber,
                UpdateTime = DateTime.Now,
                KeyValue = Help.Helper.KeyValue
            };


            //录入来电记录数据
            Task task = new Task(() =>
            {
                foreach (var item in new Assistant.BLL.customercontact().GetList(" Phone ='" + phoneNuber + "' "))
                {
                    c = new Assistant.BLL.customerinfo().GetModel(item.CustomerInfoId);

                    call.CustomerInfoId = c.CustomerInfoId;
                    call.Number = c.Number;
                    call.BottledWaterPrice = c.BottledWaterPrice;
                    call.BrandName = c.BrandName;
                    call.Address = c.Address;
                    call.Notes = c.Notes;

                    this.BeginInvoke((Action)delegate
                    {
                        this.label_bh.Text = c.Number;
                        this.label_dj.Text = c.BottledWaterPrice + "/桶";
                        this.label_dj.Tag = c.BottledWaterPrice;
                        this.label_pp.Text = c.BrandName;
                        this.richTextBox_dz.Text = c.Address;
                        //激活打印功能
                        button_print.Visible = true;
                    });
                }
                new Assistant.BLL.callrecord().Add(call);
            });
            task.Start();


            //更新线路的来电信息guid
            foreach (var item in Main.l_lines)
            {
                if (item.Handle == uboxHandle)
                {
                    item.CallRecordId = call.CallRecordId;
                    item.CallTime = call.CreateTime;
                }
            }

            ////是否开启手机管理
            //if (Help.Helper.IsPhone.SystemValue == "1")
            //{
            //    System.Threading.Thread th = new System.Threading.Thread(SendDataToServer);
            //    th.IsBackground = true;
            //    th.Start();
            //}

            //启动自动关闭计时器
            int time = 0;
            try
            {
                time = int.Parse(Help.ConfigManager.GetAppConfig("WindowCloseTime"));
            }
            catch (Exception)
            { }
            if (time > 0)
            {
                index_cloxe = time * 60;
                timer_close.Tick += (sender, e) =>
                {
                    if (index_cloxe <= 0)
                    {
                        //数据更新
                        EditCallRecord(3);//订单忽略
                        this.Close();//关闭窗口
                    }
                    index_cloxe--;
                    label2.Text = "弹窗关闭-" + (index_cloxe > 60 ? ((double)index_cloxe / 60).ToString("f1") + "分" : index_cloxe + "秒");
                };
                timer_close.Enabled = true;
                timer_close.Start();
                button_close.MouseHover += (sender, e) =>
                {
                    if (timer_close.Enabled)
                        timer_close.Stop();
                };
                button_close.MouseLeave += (sender, e) =>
                {
                    if (!timer_close.Enabled)
                        timer_close.Start();
                };
            }
        }

        /// <summary>
        /// 发送数据到服务器
        /// </summary>
        private void SendDataToServer()
        {
            //Help.SocketHelp.CreateSocket().Send(1, new JavaScriptSerializer().Serialize(call));
            string json = System.Web.HttpUtility.UrlEncode(new JavaScriptSerializer().Serialize(call));
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = Help.ConfigManager.GetAppConfig("AssistantWeb") + "Public/HandlerAddCall.ashx?CallInfo=" + json,//URL     必需项    
                Method = "get",//URL     可选项 默认为Get   
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写   
                Cookie = "",//字符串Cookie     可选项   
                Referer = "",//来源URL     可选项   
                Postdata = "",//Post数据     可选项GET时不需要写   
                Timeout = 100000,//连接超时时间     可选项默认为100000    
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000   
                UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)",//用户的浏览器类型，版本，操作系统     可选项有默认值   
                ContentType = "text/html",//返回类型    可选项有默认值   
                Allowautoredirect = false,//是否根据301跳转     可选项   
                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数   
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024    
                ProxyIp = "",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数    
                //ProxyPwd = "123456",//代理服务器密码     可选项    
                //ProxyUserName = "administrator",//代理服务器账户名     可选项   
                ResultType = ResultType.String
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
        }

        void button_hl_Click(object sender, EventArgs e)
        {
            if (this.Tag == null || base.ShowMsg("是否确认忽略此来电？", ShowMsgType.question))
            {
                //数据更新
                EditCallRecord(3);
                this.Close();
            }
        }

        void ShowWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            //关闭原因判断
            switch (e.CloseReason)
            {
                case CloseReason.None:
                    break;
                case CloseReason.WindowsShutDown:
                    break;
                case CloseReason.MdiFormClosing:
                    break;
                case CloseReason.UserClosing:
                    break;
                case CloseReason.TaskManagerClosing:
                    ShowMsg("当前有第三方程序强制关闭弹窗，请处理！");
                    break;
                case CloseReason.FormOwnerClosing:
                    break;
                case CloseReason.ApplicationExitCall:
                    break;
                default:
                    break;
            }

            //重新定位所有弹出窗的位置
            BaseForm.BaseForm.l_ShowWindows.Remove(this);
            if (BaseForm.BaseForm.l_ShowWindows.Count > 0)
            {
                int width;
                int height;
                GetWindowInfo(out width, out height);

                //第1个弹窗
                BaseForm.BaseForm.l_ShowWindows[0].Location = new Point(width - this.Width - 2, height - this.Height - 2);
                for (int i = 1; i < BaseForm.BaseForm.l_ShowWindows.Count; i++)
                {
                    //在集合最后一个窗体向上移动
                    int y = BaseForm.BaseForm.l_ShowWindows[i - 1].Location.Y - this.Height - 2;
                    int x = BaseForm.BaseForm.l_ShowWindows[i - 1].Location.X;
                    //判断是否遮挡
                    if (y < 0)
                    {
                        y = BaseForm.BaseForm.l_ShowWindows.First().Location.Y;
                        //存在遮挡，向左偏移
                        x -= this.Width + 2;
                    }
                    BaseForm.BaseForm.l_ShowWindows[i].Location = new Point(x, y);
                }
            }

            //更新主窗体实时来电
            Help.Helper.f_bind_lddd();
        }

        void button_yjl_Click(object sender, EventArgs e)
        {
            if (this.Tag == null || base.ShowMsg("是否确认关闭\n\t注：请手抄！？", ShowMsgType.question))
            {
                //数据更新
                EditCallRecord(1);
                //创建订单
                AddOrder(1);

                this.Close();
            }
        }

        void button_print_Click(object sender, EventArgs e)
        {
            if (this.Tag == null || base.ShowMsg("是否确认打印小票？", ShowMsgType.question))
            {
                if (base.Print(
                    this.label_phone.Text,
                    new customerinfo()
                    {
                        Number = this.label_bh.Text,
                        BottledWaterPrice = label_dj.Tag == null ? 0 : decimal.Parse(label_dj.Tag.ToString()),
                        BrandName = this.label_pp.Text,
                        Address = richTextBox_dz.Text
                    }))
                {
                    //数据更新
                    EditCallRecord(2);
                    //创建订单
                    AddOrder(2);

                    this.Close();
                }
                else
                    base.ShowMsg("打印异常，请手抄！", ShowMsgType.error);
            }
        }

        void ShowWindow_Load(object sender, EventArgs e)
        {
            //定位，不能重叠
            Loction();
            //记录当前到弹窗集合
            BaseForm.BaseForm.l_ShowWindows.Add(this);
            ////开启托盘闪烁
            //BaseForm.f_Flashing();

            //更新主窗体实时来电
            //Help.Helper.f_bind_lddd();
        }

        /// <summary>
        /// 窗口定位
        /// </summary>
        private void Loction()
        {
            //坐标
            int x = 0, y = 0;
            if (BaseForm.BaseForm.l_ShowWindows.Count == 0)
            {

                int width;
                int height;
                GetWindowInfo(out width, out height);

                //首个弹窗
                x = width - this.Width - 2;
                y = height - this.Height - 2;
            }
            else
            {

                //在集合最后一个窗体向上移动
                y = BaseForm.BaseForm.l_ShowWindows.Last().Location.Y - this.Height - 2;
                x = BaseForm.BaseForm.l_ShowWindows.Last().Location.X;
                //判断是否遮挡
                if (y < 0)
                {
                    y = BaseForm.BaseForm.l_ShowWindows.First().Location.Y;
                    //存在遮挡，向左偏移
                    x -= this.Width + 2;
                }
            }

            this.Location = new Point(x, y);
        }

        /// <summary>
        /// 获取当前窗口可用宽度和高度
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void GetWindowInfo(out int width, out int height)
        {
            //任务栏大小
            Size OutTaskBarSize = new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);
            Size ScreenSize = new Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            Size TaskBarSize = new Size((ScreenSize.Width - (ScreenSize.Width - OutTaskBarSize.Width)), (ScreenSize.Height - OutTaskBarSize.Height));

            //获取当前屏幕
            Screen screen = Screen.PrimaryScreen;
            width = screen.Bounds.Width;
            height = screen.Bounds.Height - TaskBarSize.Height;
        }

        /// <summary>
        /// 数据库更新操作
        /// </summary>
        /// <param name="type"></param>
        private void EditCallRecord(int type)
        {
            Task task = new Task(() =>
            {
                if (!new Assistant.BLL.callrecord().Update(call.CallRecordId, base.FormatDatatime(call.CreateTime), type))
                {
                    Log.WriteLog("来电处理失败！" + call.CallRecordId + " " + base.FormatDatatime(call.CreateTime));
                    this.BeginInvoke((Action)delegate
                    {
                        ShowMsg("来电处理失败！", ShowMsgType.error);
                    });
                }

                //如果连接了服务器，更新服务器数据
                if (Help.SocketHelp.client != null && Help.SocketHelp.client.Connected && Help.SocketHelp.client.Connected && this.Tag != null)
                {
                    //启动线程更新服务器
                    call.handlingType = type;
                    System.Threading.Thread th = new System.Threading.Thread(f_UpdateServerCall);
                    th.IsBackground = true;
                    th.Start();
                }
            });
            task.Start();
        }

        /// <summary>
        /// 更新服务器来电数据
        /// </summary>
        /// <param name="ob"></param>
        private void f_UpdateServerCall(object ob)
        {
            string json = new JavaScriptSerializer().Serialize(call);
            Help.SocketHelp.CreateSocket().Send(3, json);
        }

        /// <summary>
        /// 添加订单信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private void AddOrder(int type)
        {
            Task task = new Task(() =>
            {
                orderinifo o = new orderinifo();
                o.BottledNumber = 1;
                o.CreateTime = DateTime.Now;
                o.Notes = "";
                o.OrderHandlingType = (int)type;
                o.OrderInfoId = Guid.NewGuid().ToString();
                o.Phone = call.Phone;
                o.UpdateTime = o.CreateTime;
                o.KeyValue = Help.Helper.KeyValue;

                if (c != null)
                {
                    o.Notes = c.Notes;
                    o.Number = c.Number;
                    o.BrandName = c.BrandName;
                    o.Address = c.Address;
                    o.BottledWaterPrice = c.BottledWaterPrice;
                    o.CustomerInfoId = c.CustomerInfoId;
                }
                if (!new Assistant.BLL.orderinifo().Add(o))
                {
                    Log.WriteLog("订单创建失败！" + o.Phone);
                    this.BeginInvoke((Action)delegate
                    {
                        ShowMsg("订单创建失败！", ShowMsgType.error);
                    });
                }
                else
                {
                    //如果连接了服务器，开启线程推送信息到服务器 
                    if (Help.SocketHelp.client != null && Help.SocketHelp.client.Connected)
                    {
                        //启动线程更新服务器
                        call.handlingType = type;
                        System.Threading.Thread th = new System.Threading.Thread(f_CreateServerorder);
                        th.IsBackground = true;
                        th.Start(o);
                    }
                }
            });
            task.Start();
        }

        /// <summary>
        /// 推送订单到服务器
        /// </summary>
        /// <param name="ob"></param>
        private void f_CreateServerorder(object ob)
        {
            string json = new JavaScriptSerializer().Serialize(ob as Assistant.Model.orderinifo);
            Help.SocketHelp.CreateSocket().Send(4, json);
        }

    }
}
