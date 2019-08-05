
using System;
namespace Assistant.Model
{
    /// <summary>
    /// systeminfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class systeminfo
    {
        public systeminfo()
        { }
        #region Model
        private int _id;
        private string _systeminfoid;
        private SystemInfoType _systemtype;
        private string _systemvalue;
        private string _notes;
        private DateTime _createtime;
        private DateTime _updatetime;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 配置guid
        /// </summary>
        public string SystemInfoId
        {
            set { _systeminfoid = value; }
            get { return _systeminfoid; }
        }
        /// <summary>
        /// 配置类型
        /// </summary>
        public SystemInfoType SystemType
        {
            set { _systemtype = value; }
            get { return _systemtype; }
        }
        /// <summary>
        /// 配置值
        /// </summary>
        public string SystemValue
        {
            set { _systemvalue = value; }
            get { return _systemvalue; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Notes
        {
            set { _notes = value; }
            get { return _notes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model

        /// <summary>
        /// 获取当前配置类型名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            switch (SystemType)
            {
                case SystemInfoType.Default:
                    return "暂无";
                case SystemInfoType.JB:
                    return "号码加拨";
                default:
                    return "错误";
            }
        }
    }

    public enum SystemInfoType
    {

        /// <summary>
        /// 默认，无
        /// </summary>
        Default = 0,
        /// <summary>
        /// 号码加拨前缀
        /// </summary>
        JB,
        /// <summary>
        /// 是否自动清理
        /// </summary>
        IS_Clear,
        /// <summary>
        /// 来电清理时间
        /// </summary>
        LD_Clear,
        /// <summary>
        /// 订单清理时间
        /// </summary>
        DD_Clear,
        /// <summary>
        /// 手机端访问密码
        /// </summary>
        PhonePWD,
        /// <summary>
        /// 是否置顶
        /// </summary>
        IsTopMost,
        /// <summary>
        /// 是否开启手机端远程访问
        /// </summary>
        IsPhone
    }
}

