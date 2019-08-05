using Assistant.Public;
using Help;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Assistant
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("抱歉，来电助手 正在运行中...", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            log4net.Config.XmlConfigurator.Configure();//初始化日志组件 
            /*
            //校验
            //读取秘钥文件
            string path = Directory.GetCurrentDirectory() + "/key.txt";
            if (!File.Exists(path))
            {
                MessageBox.Show("抱歉，系统未检测到授权文件！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader r = new StreamReader(f);
            string keyold = r.ReadToEnd();
            r.Close();
            f.Close();
            //获取硬盘标示和cpu标示
            HardwareInfo hardwareInfo = new HardwareInfo();
            //获取硬盘序列号
            string hardDiskID = hardwareInfo.GetHardDiskID();
            //获取CPU序列号   
            string cpuID = hardwareInfo.GetCpuID();
             * */
            //字符串加密
            string keynew = "001";//StringHelper.DesEncrypt(hardDiskID + cpuID);
            //缓存系统唯一标示
            Help.Helper.KeyValue = keynew;
            //if (keynew != keyold)
            //{
            //    MessageBox.Show("抱歉，系统检测到当前授权文件不匹配！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //获取是否自动打开版本更新
            string AutoUpdate = ConfigurationManager.AppSettings["AutoUpdate"];
            if ((!string.IsNullOrEmpty(AutoUpdate)) && AutoUpdate == "1")
            {
                UpdateConfig.XmlModel xmlmodel = UpdateConfig.Config.GetModel();
                if (xmlmodel != null)
                {
                    //版本校验
                    int VersionCheck = xmlmodel.VersionState;
                    if (VersionCheck == 1 ||//更新连接服务器失败
                        VersionCheck == 2//更新连接服务器失败
                        )
                    {
                        xmlmodel.VersionState = 0;
                        UpdateConfig.Config.SetModel(xmlmodel);
                    }
                    else if (VersionCheck == 0  //更新失败
                        )
                    {
                        //校验错误
                        //执行更新程序，并关闭当前程序
                        string exepadth = Directory.GetCurrentDirectory() + "/Update.exe";
                        //更新程序是否存在
                        if (File.Exists(exepadth))
                        {
                            //更新程序的配置文件是否存在
                            if (File.Exists(Directory.GetCurrentDirectory() + "/Update.xml"))
                            {
                                Process.Start(exepadth);
                                return;
                            }
                            else
                                MessageBox.Show("抱歉，系统未检测到更新程序的配置文件，本次更新中断！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("抱歉，系统未检测到更新程序，本次更新中断！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            //数据库读取基础配置
            GetConfig();

            //删除日志
            DeleLog();

            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += Application_ThreadException;
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main.Main());
        }

        /// <summary>
        /// 获取系统基础配置信息
        /// </summary>
        private static void GetConfig()
        {
            //是否自动清理
            Assistant.Model.systeminfo sys_is_cler = new BLL.systeminfo().GetModel(Model.SystemInfoType.IS_Clear);
            //来电清理的时间
            Assistant.Model.systeminfo sys_ld_cler = new BLL.systeminfo().GetModel(Model.SystemInfoType.LD_Clear);
            //订单清理的时间
            Assistant.Model.systeminfo sys_dd_cler = new BLL.systeminfo().GetModel(Model.SystemInfoType.DD_Clear);
            //手机端访问密码
            Help.Helper.PhonePWD = new BLL.systeminfo().GetModel(Model.SystemInfoType.PhonePWD);
            //是否开启手机端
            Help.Helper.IsPhone = new Model.systeminfo() { SystemValue = "0" }; //new BLL.systeminfo().GetModel(Model.SystemInfoType.IsPhone);
            //是否置顶
            Help.Helper.IsTopMost = new BLL.systeminfo().GetModel(Model.SystemInfoType.IsTopMost);
            if (sys_is_cler == null)
            {
                //没有此配置信息
                sys_is_cler = new Model.systeminfo();
                sys_is_cler.CreateTime = DateTime.Now;
                sys_is_cler.Notes = "系统是否开启自动清理的功能";
                sys_is_cler.SystemInfoId = Guid.NewGuid().ToString();
                sys_is_cler.SystemType = Model.SystemInfoType.IS_Clear;
                sys_is_cler.SystemValue = "1";
                sys_is_cler.UpdateTime = sys_is_cler.CreateTime;
                new Assistant.BLL.systeminfo().Add(sys_is_cler);
            }
            if (sys_ld_cler == null)
            {
                //没有此配置信息
                sys_ld_cler = new Model.systeminfo();
                sys_ld_cler.CreateTime = DateTime.Now;
                sys_ld_cler.Notes = "系统自动清理来电信息的时长";
                sys_ld_cler.SystemInfoId = Guid.NewGuid().ToString();
                sys_ld_cler.SystemType = Model.SystemInfoType.LD_Clear;
                sys_ld_cler.SystemValue = "3,0";
                sys_ld_cler.UpdateTime = sys_ld_cler.CreateTime;
                new Assistant.BLL.systeminfo().Add(sys_ld_cler);
            }
            if (sys_dd_cler == null)
            {
                //没有此配置信息
                sys_dd_cler = new Model.systeminfo();
                sys_dd_cler.CreateTime = DateTime.Now;
                sys_dd_cler.Notes = "系统自动清理订单信息的时长";
                sys_dd_cler.SystemInfoId = Guid.NewGuid().ToString();
                sys_dd_cler.SystemType = Model.SystemInfoType.DD_Clear;
                sys_dd_cler.SystemValue = "3,0";
                sys_dd_cler.UpdateTime = sys_dd_cler.CreateTime;
                new Assistant.BLL.systeminfo().Add(sys_dd_cler);
            }
            if (Help.Helper.PhonePWD == null)
            {
                //没有此配置信息
                Help.Helper.PhonePWD = new Model.systeminfo();
                Help.Helper.PhonePWD.CreateTime = DateTime.Now;
                Help.Helper.PhonePWD.Notes = "手机端访问密码";
                Help.Helper.PhonePWD.SystemInfoId = Guid.NewGuid().ToString();
                Help.Helper.PhonePWD.SystemType = Model.SystemInfoType.PhonePWD;
                Help.Helper.PhonePWD.SystemValue = "123";
                Help.Helper.PhonePWD.UpdateTime = Help.Helper.PhonePWD.CreateTime;
                new Assistant.BLL.systeminfo().Add(Help.Helper.PhonePWD);
            }
            if (Help.Helper.IsPhone == null)
            {
                //没有此配置信息
                Help.Helper.IsPhone = new Model.systeminfo();
                Help.Helper.IsPhone.CreateTime = DateTime.Now;
                Help.Helper.IsPhone.Notes = "是否开启手机端远程访问";
                Help.Helper.IsPhone.SystemInfoId = Guid.NewGuid().ToString();
                Help.Helper.IsPhone.SystemType = Model.SystemInfoType.IsPhone;
                Help.Helper.IsPhone.SystemValue = "1";
                Help.Helper.IsPhone.UpdateTime = Help.Helper.IsPhone.CreateTime;
                new Assistant.BLL.systeminfo().Add(Help.Helper.IsPhone);
            }
            if (Help.Helper.IsTopMost == null)
            {
                //没有此配置信息
                Help.Helper.IsTopMost = new Model.systeminfo();
                Help.Helper.IsTopMost.CreateTime = DateTime.Now;
                Help.Helper.IsTopMost.Notes = "是否置顶";
                Help.Helper.IsTopMost.SystemInfoId = Guid.NewGuid().ToString();
                Help.Helper.IsTopMost.SystemType = Model.SystemInfoType.IsTopMost;
                Help.Helper.IsTopMost.SystemValue = "0";
                Help.Helper.IsTopMost.UpdateTime = Help.Helper.IsTopMost.CreateTime;
                new Assistant.BLL.systeminfo().Add(Help.Helper.IsTopMost);
            }
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);

                sb.AppendLine("【异常方法】：" + ex.TargetSite);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            Log.WriteLog("系统异常：\n" + sb.ToString());
            return sb.ToString();
        }

        private static void DeleLog()
        {
            DirectoryInfo d = new DirectoryInfo(Log.LogPath);
            if (!d.Exists) return;

            List<string> dels = new List<string>();
            foreach (var item in d.GetFiles())
            {
                if ((DateTime.Now - item.CreationTime).TotalDays > 15)
                {
                    dels.Add(item.FullName);
                }
            }

            dels.ForEach(c =>
            {
                try
                {
                    File.Delete(c);
                }
                catch { }
            });
        }
    }
}
