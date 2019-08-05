
using System;
using System.Data;
using System.Collections.Generic;
using Assistant.Model;
namespace Assistant.BLL
{
    /// <summary>
    /// customerinfo
    /// </summary>
    public partial class customerinfo
    {
        private readonly Assistant.DAL.customerinfo dal = new Assistant.DAL.customerinfo();
        public customerinfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Assistant.Model.customerinfo model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Assistant.Model.customerinfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string id)
        {
            return dal.Delete(id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.customerinfo GetModel(int id)
        {
            return dal.GetModel(id);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.customerinfo GetModel(string id)
        {
            return dal.GetModel(id);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.customerinfo> GetList(string strWhere, int pageindex, int pagesize, string orderby, bool orderbytype, out int total)
        {
            return dal.DataTableToList(dal.GetList(strWhere, pageindex, pagesize, orderby, orderbytype, out total).Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.customerinfo> GetList(string strWhere)
        {
            return dal.DataTableToList(dal.GetList(strWhere).Tables[0]);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

