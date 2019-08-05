
using System;
using System.Data;
using System.Collections.Generic;
using Assistant.Model;
namespace Assistant.BLL
{
    /// <summary>
    /// orderinifoWEB
    /// </summary>
    public partial class orderinifoWEB : orderinifo
    {
        private readonly Assistant.DAL.orderinifoWEB dal = new Assistant.DAL.orderinifoWEB();
        public orderinifoWEB()
        { }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add2(Assistant.Model.orderinifo model)
        {
            return dal.Add2(model);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.orderinifo> GetList2(string strWhere)
        {
            return dal.DataTableToList2(dal.GetList2(strWhere).Tables[0]);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update2(Assistant.Model.orderinifo model)
        {
            return dal.Update2(model);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.orderinifo GetModel2(string id)
        {
            return dal.GetModel2(id);
        }
    }
}

