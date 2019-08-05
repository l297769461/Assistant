
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DBUtility;
using System.Collections.Generic;
using Assistant.Model;
namespace Assistant.DAL
{
    /// <summary>
    /// 数据访问类:systeminfo
    /// </summary>
    public partial class systeminfo
    {
        public systeminfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("id", "systeminfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from systeminfo");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Assistant.Model.systeminfo model)
        {
            //配置重复判断，同一类型，值不能相同
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from systeminfo");
            strSql.Append(" where SystemType='" + model.SystemType + "' and SystemValue = '" + model.SystemValue + "' ");
            if (DbHelperMySQL.Exists(strSql.ToString())) return false;

            strSql = new StringBuilder();
            strSql.Append("insert into systeminfo(");
            strSql.Append("id,SystemInfoId,SystemType,SystemValue,Notes,CreateTime,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@id,@SystemInfoId,@SystemType,@SystemValue,@Notes,@CreateTime,@UpdateTime)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11),
					new MySqlParameter("@SystemInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@SystemType", MySqlDbType.Int32,11),
					new MySqlParameter("@SystemValue", MySqlDbType.VarChar,255),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.SystemInfoId;
            parameters[2].Value = model.SystemType;
            parameters[3].Value = model.SystemValue;
            parameters[4].Value = model.Notes;
            parameters[5].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[6].Value = model.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");

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
        public bool Update(Assistant.Model.systeminfo model)
        {
            //配置重复判断，同一类型，值不能相同
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from systeminfo");
            strSql.Append(" where SystemInfoId !='" + model.SystemInfoId + "' and SystemType = '" + model.SystemType + "' and SystemValue = '" + model.SystemValue + "' ");
            if (DbHelperMySQL.Exists(strSql.ToString())) return false;

            strSql = new StringBuilder();
            strSql.Append("update systeminfo set ");
            strSql.Append("SystemType=@SystemType,");
            strSql.Append("SystemValue=@SystemValue,");
            strSql.Append("Notes=@Notes,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where SystemInfoId=@SystemInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@SystemInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@SystemType", MySqlDbType.Int32,11),
					new MySqlParameter("@SystemValue", MySqlDbType.VarChar,255),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime)};
            parameters[0].Value = model.SystemInfoId;
            parameters[1].Value = model.SystemType;
            parameters[2].Value = model.SystemValue;
            parameters[3].Value = model.Notes;
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from systeminfo ");
            strSql.Append(" where SystemInfoId=@SystemInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@SystemInfoId", MySqlDbType.VarChar,50)			};
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
            strSql.Append("delete from systeminfo ");
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
        public Assistant.Model.systeminfo GetModel(string SystemInfoId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,SystemInfoId,SystemType,SystemValue,Notes,CreateTime,UpdateTime from systeminfo ");
            strSql.Append(" where SystemInfoId=@SystemInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@SystemInfoId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = SystemInfoId;

            Assistant.Model.systeminfo model = new Assistant.Model.systeminfo();
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
        public Assistant.Model.systeminfo GetModel(Assistant.Model.SystemInfoType type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,SystemInfoId,SystemType,SystemValue,Notes,CreateTime,UpdateTime from systeminfo ");
            strSql.Append(" where SystemType=@SystemType limit 0,1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@SystemType", MySqlDbType.Int32,11)			};
            parameters[0].Value = type;

            Assistant.Model.systeminfo model = new Assistant.Model.systeminfo();
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
        public Assistant.Model.systeminfo DataRowToModel(DataRow row)
        {
            Assistant.Model.systeminfo model = new Assistant.Model.systeminfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["SystemInfoId"] != null)
                {
                    model.SystemInfoId = row["SystemInfoId"].ToString();
                }
                if (row["SystemType"] != null && row["SystemType"].ToString() != "")
                {
                    model.SystemType = (SystemInfoType)int.Parse(row["SystemType"].ToString());
                }
                if (row["SystemValue"] != null)
                {
                    model.SystemValue = row["SystemValue"].ToString();
                }
                if (row["Notes"] != null)
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
            strSql.Append("select id,SystemInfoId,SystemType,SystemValue,Notes,CreateTime,UpdateTime ");
            strSql.Append(" FROM systeminfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by SystemType,updatetime ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM systeminfo ");
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
            strSql.Append(")AS Row, T.*  from systeminfo T ");
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
            parameters[0].Value = "systeminfo";
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
        public List<Assistant.Model.systeminfo> DataTableToList(DataTable dt)
        {
            List<Assistant.Model.systeminfo> modelList = new List<Assistant.Model.systeminfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Assistant.Model.systeminfo model;
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

