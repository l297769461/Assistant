
using System;
namespace Assistant.Model
{
    /// <summary>
    /// 来电记录表 
    /// </summary>
    [Serializable]
    public partial class callrecord
    {
        public callrecord()
        { }
        #region Model
        private int _id;
        private string _callrecordid;
        private string _phone;
        private string _customerinfoid;
        private int? _handlingtype;
        private DateTime _CreateTime;
        private DateTime _UpdateTime;

        public DateTime CreateTime
        {

            get
            {
                return _CreateTime;
            }
            set
            {
                _CreateTime = value;
            }
        }

        public DateTime UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                _UpdateTime = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 来电记录GUID
        /// </summary>
        public string CallRecordId
        {
            set { _callrecordid = value; }
            get { return _callrecordid; }
        }
        /// <summary>
        /// 来电号码
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 客户信息GUID
        /// </summary>
        public string CustomerInfoId
        {
            set { _customerinfoid = value; }
            get { return _customerinfoid; }
        }
        /// <summary>
        /// 处理方式
        /// 0：未接；1：手抄；2：打印
        /// </summary>
        public int? handlingType
        {
            set { _handlingtype = value; }
            get { return _handlingtype; }
        }

        /// <summary>
        /// 客户编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 桶装水单价
        /// </summary>
        public decimal BottledWaterPrice { get; set; }
        /// <summary>
        /// 饮用水名称
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 送水地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 客户备注信息
        /// </summary>
        public string Notes { get; set; }
        /// <summary>
        /// key
        /// </summary>
        public string KeyValue { get; set; }
        #endregion Model

    }
}

