
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DBUtility;
using System.Collections.Generic;
namespace Assistant.DAL
{
    /// <summary>
    /// 数据访问类:orderinifo
    /// </summary>
    public partial class orderinifo
    {
        public orderinifo()
        { }
        #region  BasicMethod
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("id", "orderinifo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from orderinifo");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Assistant.Model.orderinifo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into orderinifo(");
            strSql.Append("id,OrderInfoId,Phone,CustomerInfoId,BottledWaterPrice,BottledNumber,OrderHandlingType,Notes,CreateTime,UpdateTime)");
            strSql.Append(" values (");
            strSql.Append("@id,@OrderInfoId,@Phone,@CustomerInfoId,@BottledWaterPrice,@BottledNumber,@OrderHandlingType,@Notes,@CreateTime,@UpdateTime)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11),
					new MySqlParameter("@OrderInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Phone", MySqlDbType.VarChar,50),
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@BottledWaterPrice", MySqlDbType.Decimal,10),
					new MySqlParameter("@BottledNumber", MySqlDbType.Int32,11),
					new MySqlParameter("@OrderHandlingType", MySqlDbType.Int32,11),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.OrderInfoId;
            parameters[2].Value = model.Phone;
            parameters[3].Value = model.CustomerInfoId;
            parameters[4].Value = model.BottledWaterPrice;
            parameters[5].Value = model.BottledNumber;
            parameters[6].Value = model.OrderHandlingType;
            parameters[7].Value = model.Notes;
            parameters[8].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[9].Value = DateTime.Parse(model.UpdateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

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
        public bool Update(Assistant.Model.orderinifo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update orderinifo set ");
            strSql.Append("Phone=@Phone,");
            strSql.Append("CustomerInfoId=@CustomerInfoId,");
            strSql.Append("BottledWaterPrice=@BottledWaterPrice,");
            strSql.Append("BottledNumber=@BottledNumber,");
            strSql.Append("OrderHandlingType=@OrderHandlingType,");
            strSql.Append("Notes=@Notes,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where OrderInfoId=@OrderInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@OrderInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Phone", MySqlDbType.VarChar,50),
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@BottledWaterPrice", MySqlDbType.Decimal,10),
					new MySqlParameter("@BottledNumber", MySqlDbType.Int32,11),
					new MySqlParameter("@OrderHandlingType", MySqlDbType.Int32,11),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime) };
            parameters[0].Value = model.OrderInfoId;
            parameters[1].Value = model.Phone;
            parameters[2].Value = model.CustomerInfoId;
            parameters[3].Value = model.BottledWaterPrice;
            parameters[4].Value = model.BottledNumber;
            parameters[5].Value = model.OrderHandlingType;
            parameters[6].Value = model.Notes;
            parameters[7].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[8].Value = DateTime.Parse(model.UpdateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

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
            strSql.Append("delete from orderinifo ");
            strSql.Append(" where OrderInfoId=@OrderInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@OrderInfoId", MySqlDbType.VarChar,50)			};
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
        /// 删除一条数据
        /// </summary>
        public bool DeleteByTime(string time)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from orderinifo ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from orderinifo ");
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
        public Assistant.Model.orderinifo GetModel(string id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select o.*,w.BrandName,c.Address,c.Number from orderinifo o ");
            strSql.Append("LEFT JOIN customerinfo c on o.CustomerInfoId=c.CustomerInfoId ");
            strSql.Append("LEFT JOIN waterbrand w on w.WaterBrandId=c.WaterBrandId where 1=1 ");
            strSql.Append(" and OrderInfoId=@OrderInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@OrderInfoId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = id;

            Assistant.Model.orderinifo model = new Assistant.Model.orderinifo();
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
        public Assistant.Model.orderinifo DataRowToModel(DataRow row)
        {
            Assistant.Model.orderinifo model = new Assistant.Model.orderinifo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["OrderInfoId"] != null)
                {
                    model.OrderInfoId = row["OrderInfoId"].ToString();
                }
                if (row["Phone"] != null)
                {
                    model.Phone = row["Phone"].ToString();
                }
                if (row["CustomerInfoId"] != null)
                {
                    model.CustomerInfoId = row["CustomerInfoId"].ToString();
                }
                if (row["BottledWaterPrice"] != null && row["BottledWaterPrice"].ToString() != "")
                {
                    model.BottledWaterPrice = decimal.Parse(row["BottledWaterPrice"].ToString());
                }
                if (row["BottledNumber"] != null && row["BottledNumber"].ToString() != "")
                {
                    model.BottledNumber = int.Parse(row["BottledNumber"].ToString());
                }
                if (row["OrderHandlingType"] != null && row["OrderHandlingType"].ToString() != "")
                {
                    model.OrderHandlingType = int.Parse(row["OrderHandlingType"].ToString());
                }
                if (row["Notes"] != null)
                {
                    model.Notes = row["Notes"].ToString();
                }
                if (row["BrandName"] != null)
                {
                    model.BrandName = row["BrandName"].ToString();
                }
                if (row["Address"] != null)
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["Number"] != null)
                {
                    model.Number = row["Number"].ToString();
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
        public DataSet GetList(string strWhere, int pageindex, int pagesize, string orderby, bool orderbytype, out int total)
        {
            total = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) ");
            strSql.Append(" FROM orderinifo o where 1=1 ");
            strSql.Append(" " + strWhere + " ");

            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            total = obj == null ? 0 : Convert.ToInt32(obj);

            strSql = new StringBuilder();
            strSql.Append("select o.*,w.BrandName,c.Address,c.Number from orderinifo o ");
            strSql.Append("LEFT JOIN customerinfo c on o.CustomerInfoId=c.CustomerInfoId ");
            strSql.Append("LEFT JOIN waterbrand w on w.WaterBrandId=c.WaterBrandId where 1=1 ");
            strSql.Append(" " + strWhere + " ");
            strSql.Append(" order by " + (string.IsNullOrEmpty(orderby) ? "id" : orderby) + " " + (orderbytype ? " desc " : " asc "));
            return DbHelperMySQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select o.*,w.BrandName,c.Address,c.Number from orderinifo o ");
            strSql.Append("LEFT JOIN customerinfo c on o.CustomerInfoId=c.CustomerInfoId ");
            strSql.Append("LEFT JOIN waterbrand w on w.WaterBrandId=c.WaterBrandId where 1=1 ");
            strSql.Append(" " + strWhere + " ");
            strSql.Append(" order by id desc");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM orderinifo ");
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
            strSql.Append(")AS Row, T.*  from orderinifo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.orderinifo> DataTableToList(DataTable dt)
        {
            List<Assistant.Model.orderinifo> modelList = new List<Assistant.Model.orderinifo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Assistant.Model.orderinifo model;
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

