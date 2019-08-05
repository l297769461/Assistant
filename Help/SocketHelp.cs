using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;

namespace Help
{
    /// <summary>
    /// 套接字帮助类
    /// </summary>
    public class SocketHelp
    {
        /// <summary>
        /// 发送服务器消息的队列
        /// </summary>
        private static ConcurrentQueue<byte[]> _ConcurrenProducts = new ConcurrentQueue<byte[]>();

        /// <summary>
        /// 是否设置为不自动重连
        /// </summary>
        public bool IsNoConnect = false;

        /// <summary>
        /// 连接次数统计
        /// </summary>
        private int IndexConnect = 0;

        /// <summary>
        /// 是否同步显示气泡信息
        /// </summary>
        private bool IsShowQP = false;

        public delegate void w_sock(string txt);
        /// <summary>
        /// 设置连接相关的提示语
        /// </summary>
        public static w_sock f_sock;

        /// <summary>
        /// 单例对象
        /// </summary>
        private static SocketHelp _SocketHelp = null;

        private SocketHelp()
        {
        }

        /// <summary>
        /// 发送数据回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            SocketSend s = ar.AsyncState as SocketSend;
            try
            {
                int sendLength = s.SckSend.EndSend(ar);
            }
            catch (Exception)
            {
                _ConcurrenProducts.Enqueue(s.SendBuf);
            }
        }

        /// <summary>
        /// 单例对象构建
        /// </summary>
        /// <returns></returns>
        public static SocketHelp CreateSocket()
        {
            if (_SocketHelp == null)
                return _SocketHelp = new SocketHelp();
            return _SocketHelp;
        }

        /// <summary>
        /// 全局连接对象
        /// </summary>
        public static Socket client = null;

        /// <summary>
        /// 服务器连接
        /// </summary>
        /// <param name="pd">true:显示气泡信息</param>
        public void Connect(bool pd = false)
        {
            IsShowQP = pd;
            System.Threading.Thread th = new System.Threading.Thread(f_Connect);
            th.IsBackground = true;
            th.Start();
        }

        /// <summary>
        /// 执行连接
        /// </summary>
        /// <param name="ob"></param>
        private void f_Connect(object ob)
        {
            //休眠1秒
            System.Threading.Thread.Sleep(1000);

            //读取服务器ip地址和端口
            string _ipport = ConfigManager.GetAppConfig("ServiceIPPort");
            if (string.IsNullOrEmpty(_ipport) || !_ipport.Contains(","))
                return;

            //界面提示语
            f_sock("正在尝试连接服务器...");
            if (IsShowQP)
            {
                //气泡提示语
                Helper.f_qp("正在尝试连接服务器...");
            }
            string[] _ipports = _ipport.Split(',');
            IPAddress ipaddress = IPAddress.Parse(_ipports[0]);
            int port = int.Parse(_ipports[1]);
            IPEndPoint endpoint = new IPEndPoint(ipaddress, port);
            if (client == null)
            {
                //连接前，get一次服务器
                GetServer();
                //开始异步连接
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(endpoint, ConnectCallback, client);
            }
        }

        /// <summary>
        /// 请求一次服务器
        /// </summary>
        private static void GetServer()
        {
            string url = Help.ConfigManager.GetAppConfig("AssistantWeb") + "?keyvalue=" + Help.Helper.KeyValue;
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,//URL     必需项    
                Method = "get",//URL     可选项 默认为Get   
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写   
                Cookie = "",//字符串Cookie     可选项   
                Referer = "",//来源URL     可选项   
                Postdata = "",//Post数据     可选项GET时不需要写   
                Timeout = 5000,//连接超时时间     可选项默认为100000    
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

        private void ConnectCallback(IAsyncResult ar)
        {
            Socket sock = ar.AsyncState as Socket;
            try
            {
                sock.EndConnect(ar);
                //连接成功，重置连接次数统计
                IndexConnect = 0;
                //服务器连接成功
                f_sock("已连接服务器，点我获取地址");
                if (IsShowQP)
                {
                    //气泡提示语
                    Helper.f_qp("连接成功，可以登录app，手机上管理啦！");
                }
                //成功连接服务器，继续发送未发送的消息
                Send();
                StateObject so = new StateObject(sock);
                sock.BeginReceive(so.buffer, 0, so.buffer.Length, SocketFlags.None, ReceiveCallback, so);
            }
            catch (Exception ex)
            {
                //判断是否不允许重连
                if (IsNoConnect) return;

                Help.Log.WriteLog("连接失败，" + ex.Message);
                //试图访问套接字时发生错误
                f_sock("服务器拒绝，连接失败！正在尝试重连【" + (10 - IndexConnect) + "】");
                if (IsShowQP)
                {
                    //气泡提示语
                    Helper.f_qp("服务器拒绝，连接失败！");
                }
                client = null;
                IndexConnect++;
                if (IndexConnect < 10)
                    Connect(IsShowQP);
                else
                {
                    //重置连接次数统计
                    IndexConnect = 0;
                    //试图访问套接字时发生错误
                    f_sock("服务器拒绝，已尝试10次连接。请尝试设置-手动连接！");
                }
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            StateObject so = ar.AsyncState as StateObject;
            try
            {
                int length = so.sock.EndReceive(ar);
                string recvmsg = System.Text.Encoding.UTF8.GetString(so.buffer, 0, length);
                switch (recvmsg)
                {
                    case "key":
                        //上传key和手机访问密码
                        so.sock.Send(System.Text.Encoding.UTF8.GetBytes(Helper.KeyValue + "," + Help.Helper.PhonePWD.SystemValue));
                        break;
                    default:
                        Go(recvmsg);
                        break;
                }
                //接受服务器的数据
                so.buffer = new byte[1024];
                so.sock.BeginReceive(so.buffer, 0, so.buffer.Length, SocketFlags.None, ReceiveCallback, so);

            }
            catch (Exception ex)
            {
                //判断是否不允许重连
                if (IsNoConnect) return;

                Help.Log.WriteLog("连接断开，" + ex.Message);
                //连接断开
                f_sock("服务器连接断开，正在尝试重连！");
                if (IsShowQP)
                {
                    //气泡提示语
                    Helper.f_qp("服务器连接断开，正在尝试重连！");
                }
                client = null;
                Connect();
            }
        }

        /// <summary>
        /// 分析并执行命令
        /// </summary>
        /// <param name="data"></param>
        private void Go(string data)
        {
            string[] datas = data.Split('❤');
            switch (datas[0])
            {
                case "1":
                    //手机端处理来电信息
                    string[] str1 = datas[1].Split(',');
                    UpdateCall(str1[0], str1[1], int.Parse(str1[2]), str1[3]);
                    break;
                case "2":
                    //执行订单打印
                    Assistant.Model.orderinifo order = new JavaScriptSerializer().Deserialize<Assistant.Model.orderinifo>(datas[1]);
                    Help.Helper.f_orderprint(order);
                    break;
                case "3":
                    //信息订单编辑同步
                    string[] str3 = datas[1].Split(',');
                    UpdateOrder(str3[0], decimal.Parse(str3[1]), int.Parse(str3[2]), str3[3]);
                    break;
                default:
                    break;
            }
        }

        private void UpdateOrder(string OrderInfoId, decimal BottledWaterPrice, int BottledNumber, string Notes)
        {

            Assistant.Model.orderinifo order = new Assistant.BLL.orderinifo().GetModel(OrderInfoId);
            order.BottledWaterPrice = BottledWaterPrice;
            order.BottledNumber = BottledNumber;
            order.Notes = Notes;
            if (new Assistant.BLL.orderinifo().Update(order))
            {

                //通知界面更新
                Help.Helper.f_bind_lddd();
            }
        }

        /// <summary>
        /// 处理来电信息
        /// </summary>
        /// <param name="CallRecordId"></param>
        /// <param name="CreateTime"></param>
        /// <param name="handlingType"></param>
        private void UpdateCall(string CallRecordId, string CreateTime, int handlingType, string phone)
        {
            if (new Assistant.BLL.callrecord().Update(CallRecordId, CreateTime, handlingType))
            {
                //通知界面更新
                Help.Helper.Bind_ld(phone, handlingType);
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="zl">指令标示</param>
        /// <param name="txt"></param>
        public void Send(int zl, string txt)
        {
            //把需要发送的信息加入队列
            byte[] SendBuf = System.Text.Encoding.UTF8.GetBytes(zl + "❤" + txt);
            _ConcurrenProducts.Enqueue(SendBuf);
            Send();
        }

        /// <summary>
        /// 执行发送
        /// </summary>
        private void Send()
        {
            System.Threading.ThreadPool.QueueUserWorkItem((ob) =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(500);

                    bool pd = false;
                    byte[] SendBuf;
                    if (client != null && client.Connected && _ConcurrenProducts.TryDequeue(out SendBuf))
                    {
                        try
                        {
                            client.BeginSend(SendBuf, 0, SendBuf.Length, SocketFlags.None, new AsyncCallback(SendCallback), new SocketSend() { SckSend = client, SendBuf = SendBuf });
                        }
                        catch (Exception)
                        {
                            _ConcurrenProducts.Enqueue(SendBuf);
                            pd = true;
                        }
                    }
                    else
                        pd = true;
                    if (pd)
                        break;
                }
            });
        }
    }
    public class StateObject
    {
        public byte[] buffer = new byte[1024];
        public Socket sock;
        public string remotepoint;
        public StateObject(Socket sock)
        {
            this.sock = sock;
            this.remotepoint = sock.RemoteEndPoint.ToString();
        }
    }

    public class SocketSend
    {

        public Socket SckSend { get; set; }
        public byte[] SendBuf { get; set; }
    }
}
