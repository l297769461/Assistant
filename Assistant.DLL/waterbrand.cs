
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DBUtility;
using System.Collections.Generic;
namespace Assistant.DAL
{
    /// <summary>
    /// 数据访问类:waterbrand
    /// </summary>
    public partial class waterbrand
    {
        public waterbrand()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("id", "waterbrand");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from waterbrand");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Assistant.Model.waterbrand model)
        {
            //重复判断
            if (GetModelByBrandName(model.BrandName) != null) return false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into waterbrand(");
            strSql.Append("WaterBrandId,BrandName,Notes,CreateTime,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@WaterBrandId,@BrandName,@Notes,@CreateTime,@UpdateTime)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@WaterBrandId", MySqlDbType.VarChar,50),
					new MySqlParameter("@BrandName", MySqlDbType.VarChar,50),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime)};
            parameters[0].Value = model.WaterBrandId;
            parameters[1].Value = model.BrandName;
            parameters[2].Value = model.Notes;
            parameters[3].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[4].Value = model.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");

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
        public bool Update(Assistant.Model.waterbrand model)
        {
            DataSet set = GetList(" WaterBrandId != '" + model.WaterBrandId + "' and BrandName='" + model.BrandName + "' ");
            if (set != null && set.Tables.Count > 0 && set.Tables[0].Rows.Count > 0)
                return false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update waterbrand set ");
            strSql.Append("BrandName=@BrandName,");
            strSql.Append("Notes=@Notes,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where WaterBrandId=@WaterBrandId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@WaterBrandId", MySqlDbType.VarChar,50),
					new MySqlParameter("@BrandName", MySqlDbType.VarChar,50),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime)};
            parameters[0].Value = model.WaterBrandId;
            parameters[1].Value = model.BrandName;
            parameters[2].Value = model.Notes;
            parameters[3].Value = model.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");

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
        public bool Delete(string WaterBrandId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from waterbrand ");
            strSql.Append(" where WaterBrandId=@WaterBrandId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@WaterBrandId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = WaterBrandId;

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
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.waterbrand GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,WaterBrandId,BrandName,Notes,CreateTime,UpdateTime from waterbrand ");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            Assistant.Model.waterbrand model = new Assistant.Model.waterbrand();
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
        public Assistant.Model.waterbrand GetModel(string WaterBrandId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,WaterBrandId,BrandName,Notes,CreateTime,UpdateTime from waterbrand ");
            strSql.Append(" where WaterBrandId=@WaterBrandId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@WaterBrandId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = WaterBrandId;

            Assistant.Model.waterbrand model = new Assistant.Model.waterbrand();
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
        public Assistant.Model.waterbrand GetModelByBrandName(string BrandName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,WaterBrandId,BrandName,Notes,CreateTime,UpdateTime from waterbrand ");
            strSql.Append(" where BrandName=@BrandName ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@BrandName", MySqlDbType.VarChar,50)			};
            parameters[0].Value = BrandName;

            Assistant.Model.waterbrand model = new Assistant.Model.waterbrand();
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
        public Assistant.Model.waterbrand DataRowToModel(DataRow row)
        {
            Assistant.Model.waterbrand model = new Assistant.Model.waterbrand();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["WaterBrandId"] != null && row["WaterBrandId"].ToString() != "")
                {
                    model.WaterBrandId = row["WaterBrandId"].ToString();
                }
                if (row["BrandName"] != null && row["BrandName"].ToString() != "")
                {
                    model.BrandName = row["BrandName"].ToString();
                }
                if (row["Notes"] != null && row["Notes"].ToString() != "")
                {
                    model.Notes = row["Notes"].ToString();
                }
                if (row["CreateTime"] != null && row["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(row["CreateTime"].ToString());
                }
                if (row["UpdateTime"] != null && row["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(row["UpdateTime"].ToString());
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
            strSql.Append("select id,WaterBrandId,BrandName,Notes,CreateTime,UpdateTime ");
            strSql.Append(" FROM waterbrand ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by CreateTime desc");
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
            strSql.Append(" FROM waterbrand ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            total = obj == null ? 0 : Convert.ToInt32(obj);

            strSql = new StringBuilder();
            strSql.Append("select id,WaterBrandId,BrandName,Notes,CreateTime,UpdateTime ");
            strSql.Append(" FROM waterbrand ");
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
            strSql.Append("select count(1) FROM waterbrand ");
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


        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.waterbrand> DataTableToList(DataTable dt)
        {
            List<Assistant.Model.waterbrand> modelList = new List<Assistant.Model.waterbrand>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Assistant.Model.waterbrand model;
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

