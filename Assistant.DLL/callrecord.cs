
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DBUtility;
using System.Collections.Generic;
namespace Assistant.DAL
{
    /// <summary>
    /// 数据访问类:callrecord
    /// </summary>
    public partial class callrecord
    {
        public callrecord()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("id", "callrecord");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from callrecord");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Assistant.Model.callrecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into callrecord(");
            strSql.Append("CallRecordId,Phone,CustomerInfoId,handlingType,CreateTime,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@CallRecordId,@Phone,@CustomerInfoId,@handlingType,@CreateTime,@UpdateTime)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CallRecordId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Phone", MySqlDbType.VarChar,50),
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@handlingType", MySqlDbType.Int32,7),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime)};
            parameters[0].Value = model.CallRecordId;
            parameters[1].Value = model.Phone;
            parameters[2].Value = model.CustomerInfoId;
            parameters[3].Value = model.handlingType;
            parameters[4].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[5].Value = model.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Assistant.Model.callrecord model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update callrecord set ");
            strSql.Append("Phone=@Phone,");
            strSql.Append("CustomerInfoId=@CustomerInfoId,");
            strSql.Append("handlingType=@handlingType,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where CallRecordId=@CallRecordId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CallRecordId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Phone", MySqlDbType.VarChar,50),
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@handlingType", MySqlDbType.Int32,7),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime)};
            parameters[0].Value = model.CallRecordId;
            parameters[1].Value = model.Phone;
            parameters[2].Value = model.CustomerInfoId;
            parameters[3].Value = model.handlingType;
            parameters[4].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[5].Value = model.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(string CallRecordId, string CreateTime, int handlingType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update callrecord set ");
            strSql.Append("handlingType=" + handlingType);
            strSql.Append(" where CallRecordId='" + CallRecordId + "' and CreateTime='" + CreateTime + "' ");

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string CallRecordId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from callrecord ");
            strSql.Append(" where CallRecordId=@CallRecordId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CallRecordId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = CallRecordId;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByTime(string time)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from callrecord ");
            strSql.Append(" where CreateTime < @time ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@time", MySqlDbType.DateTime)			};
            parameters[0].Value = time;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from callrecord ");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from callrecord ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.callrecord GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,CallRecordId,Phone,CustomerInfoId,handlingType,CreateTime,UpdateTime from callrecord ");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            Assistant.Model.callrecord model = new Assistant.Model.callrecord();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.callrecord GetModel(string CallRecordId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.*,cu.Number,cu.BottledWaterPrice,w.BrandName,cu.Address,cu.Notes ");
            strSql.Append(" FROM callrecord c  ");
            strSql.Append("LEFT JOIN customerinfo cu on c.CustomerInfoId=cu.CustomerInfoId ");
            strSql.Append("LEFT JOIN waterbrand w on w.WaterBrandId=cu.WaterBrandId ");
            strSql.Append(" where CallRecordId=@CallRecordId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CallRecordId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = CallRecordId;

            Assistant.Model.callrecord model = new Assistant.Model.callrecord();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.callrecord DataRowToModel(DataRow row)
        {
            Assistant.Model.callrecord model = new Assistant.Model.callrecord();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["CallRecordId"] != null && row["CallRecordId"].ToString() != "")
                {
                    model.CallRecordId = row["CallRecordId"].ToString();
                }
                if (row["Phone"] != null && row["Phone"].ToString() != "")
                {
                    model.Phone = row["Phone"].ToString();
                }
                if (row["CustomerInfoId"] != null && row["CustomerInfoId"].ToString() != "")
                {
                    model.CustomerInfoId = row["CustomerInfoId"].ToString();
                }
                if (row["handlingType"] != null && row["handlingType"].ToString() != "")
                {
                    model.handlingType = int.Parse(row["handlingType"].ToString());
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
                }
                if (row["Number"] != null && row["Number"].ToString() != "")
                {
                    model.Number = row["Number"].ToString();
                }
                if (row["BottledWaterPrice"] != null && row["BottledWaterPrice"].ToString() != "")
                {
                    model.BottledWaterPrice = decimal.Parse(row["BottledWaterPrice"].ToString());
                }
                if (row["BrandName"] != null && row["BrandName"].ToString() != "")
                {
                    model.BrandName = row["BrandName"].ToString();
                }
                if (row["Address"] != null && row["Address"].ToString() != "")
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["Notes"] != null && row["Notes"].ToString() != "")
                {
                    model.Notes = row["Notes"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.*,cu.Number,cu.BottledWaterPrice,w.BrandName,cu.Address,cu.Notes ");
            strSql.Append(" FROM callrecord c  ");
            strSql.Append("LEFT JOIN customerinfo cu on c.CustomerInfoId=cu.CustomerInfoId ");
            strSql.Append("LEFT JOIN waterbrand w on w.WaterBrandId=cu.WaterBrandId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by id desc");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, int pageindex, int pagesize, string orderby, bool orderbytype, out int total)
        {
            total = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) ");
            strSql.Append(" FROM callrecord  ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            total = obj == null ? 0 : Convert.ToInt32(obj);

            strSql = new StringBuilder();
            strSql.Append("select c.*,cu.Number,cu.BottledWaterPrice,w.BrandName,cu.Address,cu.Notes ");
            strSql.Append(" FROM callrecord c  ");
            strSql.Append("LEFT JOIN customerinfo cu on c.CustomerInfoId=cu.CustomerInfoId ");
            strSql.Append("LEFT JOIN waterbrand w on w.WaterBrandId=cu.WaterBrandId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + (string.IsNullOrEmpty(orderby) ? "id" : orderby) + " " + (orderbytype ? " desc " : " asc "));

            int start = (pageindex - 1) * pagesize;
            start = start < 0 ? 0 : start;
            strSql.Append(" limit " + start + "," + pagesize);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM callrecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from callrecord T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@PageSize", MySqlDbType.Int32),
                    new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                    new MySqlParameter("@IsReCount", MySqlDbType.Bit),
                    new MySqlParameter("@OrderType", MySqlDbType.Bit),
                    new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "callrecord";
            parameters[1].Value = "id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.callrecord> DataTableToList(DataTable dt)
        {
            List<Assistant.Model.callrecord> modelList = new List<Assistant.Model.callrecord>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Assistant.Model.callrecord model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }
        #endregion  ExtensionMethod
    }
}

