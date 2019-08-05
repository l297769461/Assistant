
using System;
namespace Assistant.Model
{
    /// <summary>
    /// 客户联系表
    /// </summary>
    [Serializable]
    public partial class customercontact
    {
        public customercontact()
        { }
        #region Model
        private int _id;
        private string _customercontactid;
        private string _customerinfoid;
        private string _phone;
        private string _name;
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
        /// 客户联系GUID
        /// </summary>
        public string CustomerContactId
        {
            set { _customercontactid = value; }
            get { return _customercontactid; }
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
        /// 联系电话
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
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

