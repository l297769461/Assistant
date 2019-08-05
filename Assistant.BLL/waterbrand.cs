
using System;
using System.Data;
using System.Collections.Generic;
using Assistant.Model;
namespace Assistant.BLL
{
    /// <summary>
    /// waterbrand
    /// </summary>
    public partial class waterbrand
    {
        private readonly Assistant.DAL.waterbrand dal = new Assistant.DAL.waterbrand();
        public waterbrand()
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
        public bool Add(Assistant.Model.waterbrand model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Assistant.Model.waterbrand model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string WaterBrandId)
        {

            return dal.Delete(WaterBrandId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.waterbrand GetModel(int id)
        {
            return dal.GetModel(id);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.waterbrand GetModel(string WaterBrandId)
        {
            return dal.GetModel(WaterBrandId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.waterbrand> GetList(string strWhere)
        {
            return dal.DataTableToList(dal.GetList(strWhere).Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.waterbrand> GetList(string strWhere, int pageindex, int pagesize, string orderby, bool orderbytype, out int total)
        {
            return dal.DataTableToList(dal.GetList(strWhere, pageindex, pagesize, orderby, orderbytype, out total).Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.waterbrand> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return dal.DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.waterbrand> GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

