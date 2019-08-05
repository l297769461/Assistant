using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Help
{
    public class Helper
    {
        /// <summary>
        /// 气泡信息
        /// </summary>
        /// <param name="txt"></param>
        public delegate void w_qp(string txt, bool islong = false);

        /// <summary>
        /// 设置气泡信息委托
        /// </summary>
        public static w_qp f_qp;

        /// <summary>
        /// 系统唯一标示
        /// </summary>
        public static string KeyValue { get; set; }

        /// <summary>
        /// 数据管理
        /// </summary>
        /// <param name="type"></param>
        public delegate void w_manager_bind(int type);
        /// <summary>
        /// 数据管理委托
        /// </summary>
        public static w_manager_bind f_manager_bind;
         
        /// <summary>
        /// 实时来电委托
        /// </summary>
        public delegate void w_bind_lddd();
        /// <summary>
        /// 实时来电、订单回调
        /// </summary>
        public static w_bind_lddd f_bind_lddd;

        /// <summary>
        /// 系统配置 手机访问密码
        /// </summary>
        public static Assistant.Model.systeminfo PhonePWD = null;

        public delegate void w_closeld(string phone, int handlingType);
        /// <summary>
        /// 匹配来电号码，关闭来电弹窗
        /// </summary>
        public static w_closeld f_closeld;

        /// <summary>
        /// 更新所有的来电信息
        /// </summary>
        public static void Bind_ld(string phone, int handlingType)
        {
            //处理来电的弹窗
            f_closeld(phone, handlingType);
        }

        public delegate void w_orderprint(Assistant.Model.orderinifo order);
        /// <summary>
        /// 订单打印
        /// </summary>
        public static w_orderprint f_orderprint;

        public delegate void w_topmost(bool pd);
        /// <summary>
        /// 主界面设置是否置顶回调
        /// </summary>
        public static w_topmost f_TopMost;

        /// <summary>
        /// 系统配置 是否置顶
        /// </summary>
        public static Assistant.Model.systeminfo IsTopMost = null;
        /// <summary>
        /// 系统配置 是否开启手机端
        /// </summary>
        public static Assistant.Model.systeminfo IsPhone = null;

        public delegate void w_Set_IsPhone();
        /// <summary>
        /// 开启或者关闭手机远程访问功能
        /// </summary>
        public static w_Set_IsPhone f_Set_IsPhone;
    }
}
