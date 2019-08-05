
using System;
using System.Data;
using System.Collections.Generic;
using Assistant.Model;
namespace Assistant.BLL
{
    /// <summary>
    /// callrecordWEB
    /// </summary>
    public partial class callrecordWEB : callrecord
    {
        private readonly Assistant.DAL.callrecordWEB dal = new Assistant.DAL.callrecordWEB();
        public callrecordWEB()
        { }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add2(Assistant.Model.callrecord model)
        {
            return dal.Add2(model);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.callrecord> GetList2(string strWhere)
        {
            return dal.DataTableToList2(dal.GetList2(strWhere).Tables[0]);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update2(string CallRecordId, string CreateTime, int handlingType, string keyvalue)
        {
            return dal.Update2(CallRecordId, CreateTime, handlingType, keyvalue);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.callrecord GetModel2(string id)
        {
            return dal.GetModel2(id);
        }
    }
}

