
using System;
namespace Assistant.Model
{
    /// <summary>
    ///饮水品牌
    /// </summary>
    [Serializable]
    public partial class waterbrand
    {
        public waterbrand()
        { }
        #region Model
        private int _id;
        private string _waterbrandid;
        private string _brandname;
        private string _notes;
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
        /// 品牌GUID
        /// </summary>
        public string WaterBrandId
        {
            set { _waterbrandid = value; }
            get { return _waterbrandid; }
        }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName
        {
            set { _brandname = value; }
            get { return _brandname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Notes
        {
            set { _notes = value; }
            get { return _notes; }
        }
        #endregion Model

    }
}

