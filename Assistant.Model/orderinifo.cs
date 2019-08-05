
using System;
namespace Assistant.Model
{
    /// <summary>
    /// orderinifo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class orderinifo
    {
        public orderinifo()
        { }
        #region Model
        private int _id;
        private string _orderinfoid;
        private string _phone;
        private string _CustomerInfoId;
        private decimal? _bottledwaterprice;
        private int _bottlednumber = 1;
        private int? _orderhandlingtype;
        private string _notes;
        private DateTime _createtime;
        private DateTime? _updatetime;
        /// <summary>
        /// 饮用水品牌
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// 送水地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单id
        /// </summary>
        public string OrderInfoId
        {
            set { _orderinfoid = value; }
            get { return _orderinfoid; }
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
        /// 客户信息guid
        /// </summary>
        public string CustomerInfoId
        {
            set { _CustomerInfoId = value; }
            get { return _CustomerInfoId; }
        }
        /// <summary>
        /// 桶装水单价
        /// </summary>
        public decimal? BottledWaterPrice
        {
            set { _bottledwaterprice = value; }
            get { return _bottledwaterprice; }
        }
        /// <summary>
        /// 桶装水数量
        /// </summary>
        public int BottledNumber
        {
            set { _bottlednumber = value; }
            get { return _bottlednumber; }
        }
        /// <summary>
        /// 订单处理方式
        /// </summary>
        public int? OrderHandlingType
        {
            set { _orderhandlingtype = value; }
            get { return _orderhandlingtype; }
        }
        /// <summary>
        /// 订单备注信息
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
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// key
        /// </summary>
        public string KeyValue { get; set; }
        #endregion Model

    }
}

