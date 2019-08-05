
using System;
namespace Assistant.Model
{
    /// <summary>
    /// 客户信息表
    /// </summary>
    [Serializable]
    public partial class customerinfo
    {
        public customerinfo()
        { }
        #region Model
        private int _id;
        private string _customerinfoid;
        private string _number;
        private decimal _bottledwaterprice;
        private string _waterbrandid;
        private string _address;
        private string _notes;
        private DateTime _CreateTime;
        private DateTime _UpdateTime;
        private string _BrandName;
        private string _Phons;
        /// <summary>
        /// 饮水品牌名称
        /// </summary>
        public string BrandName
        {
            get
            {
                return _BrandName;
            }
            set
            {
                _BrandName = value;
            }
        }
        /// <summary>
        /// 改客户信息下的所有电话
        /// </summary>
        public string Phons
        {
            get
            {
                return _Phons;
            }
            set
            {
                _Phons = value;
            }
        }

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
        /// 客户信息GUID
        /// </summary>
        public string CustomerInfoId
        {
            set { _customerinfoid = value; }
            get { return _customerinfoid; }
        }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 桶装水单价
        /// </summary>
        public decimal BottledWaterPrice
        {
            set { _bottledwaterprice = value; }
            get { return _bottledwaterprice; }
        }
        /// <summary>
        /// 饮水品牌
        /// </summary>
        public string WaterBrandId
        {
            set { _waterbrandid = value; }
            get { return _waterbrandid; }
        }
        /// <summary>
        /// 送水地址 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 备注信息 
        /// </summary>
        public string Notes
        {
            set { _notes = value; }
            get { return _notes; }
        }
        #endregion Model

        /// <summary>
        /// 客户编号的索引
        /// </summary>
        public int NumberNo
        {
            get
            {
                //读取 Number
                string _int = "0123456789";
                string _no = "";
                for (int i = 0; i < _number.Length; i++)
                {
                    string str = _number.Substring(i, 1);
                    if (_int.Contains(str))
                        _no += str;
                }
                return int.Parse(string.IsNullOrEmpty(_no) ? "0" : _no);
            }
        }
    }
}

