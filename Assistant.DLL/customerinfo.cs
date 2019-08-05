
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DBUtility;
using System.Collections.Generic;
namespace Assistant.DAL
{
    /// <summary>
    /// 数据访问类:customerinfo
    /// </summary>
    public partial class customerinfo
    {
        public customerinfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperMySQL.GetMaxID("id", "customerinfo");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from customerinfo");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Assistant.Model.customerinfo model)
        {
            //客户编号重复判断
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from customerinfo");
            strSql.Append(" where Number = '" + model.Number + "' ");
            if (DbHelperMySQL.Exists(strSql.ToString())) return false;

            strSql = new StringBuilder();
            strSql.Append("insert into customerinfo(");
            strSql.Append("CustomerInfoId,Number,BottledWaterPrice,WaterBrandId,Address,Notes,CreateTime,UpdateTime,NumberNo)");
            strSql.Append(" values (");
            strSql.Append("@CustomerInfoId,@Number,@BottledWaterPrice,@WaterBrandId,@Address,@Notes,@CreateTime,@UpdateTime,@NumberNo)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Number", MySqlDbType.VarChar,50),
					new MySqlParameter("@BottledWaterPrice", MySqlDbType.Decimal,10),
					new MySqlParameter("@WaterBrandId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Address", MySqlDbType.VarChar,255),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime),
					new MySqlParameter("@NumberNo", MySqlDbType.Int32,11)};
            parameters[0].Value = model.CustomerInfoId;
            parameters[1].Value = model.Number;
            parameters[2].Value = model.BottledWaterPrice;
            parameters[3].Value = model.WaterBrandId;
            parameters[4].Value = model.Address;
            parameters[5].Value = model.Notes;
            parameters[6].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[7].Value = model.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[8].Value = model.NumberNo;

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
        public bool Update(Assistant.Model.customerinfo model)
        {

            //客户编号重复判断
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from customerinfo");
            strSql.Append(" where Number = '" + model.Number + "' and CustomerInfoId != '" + model.CustomerInfoId + "'");
            if (DbHelperMySQL.Exists(strSql.ToString())) return false;

            strSql = new StringBuilder();
            strSql.Append("update customerinfo set ");
            strSql.Append("Number=@Number,");
            strSql.Append("BottledWaterPrice=@BottledWaterPrice,");
            strSql.Append("WaterBrandId=@WaterBrandId,");
            strSql.Append("Address=@Address,");
            strSql.Append("Notes=@Notes,");
            strSql.Append("CreateTime=@CreateTime,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("NumberNo=@NumberNo");
            strSql.Append(" where CustomerInfoId=@CustomerInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Number", MySqlDbType.VarChar,50),
					new MySqlParameter("@BottledWaterPrice", MySqlDbType.Decimal,10),
					new MySqlParameter("@WaterBrandId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Address", MySqlDbType.VarChar,255),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255), 
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime),
					new MySqlParameter("@NumberNo", MySqlDbType.Int32,11)};
            parameters[0].Value = model.CustomerInfoId;
            parameters[1].Value = model.Number;
            parameters[2].Value = model.BottledWaterPrice;
            parameters[3].Value = model.WaterBrandId;
            parameters[4].Value = model.Address;
            parameters[5].Value = model.Notes;
            parameters[6].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[7].Value = model.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[8].Value = model.NumberNo;

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
            strSql.Append("delete from customerinfo ");
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(string CustomerInfoId)
        {
            //删除来电记录
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from callrecord ");
            strSql.Append(" where CustomerInfoId='" + CustomerInfoId + "' ");
            DbHelperMySQL.ExecuteSql(strSql.ToString());

            //删除联系人信息
            strSql = new StringBuilder();
            strSql.Append("delete from customercontact ");
            strSql.Append(" where CustomerInfoId='" + CustomerInfoId + "' ");
            DbHelperMySQL.ExecuteSql(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("delete from customerinfo ");
            strSql.Append(" where CustomerInfoId=@CustomerInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = CustomerInfoId;

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
            strSql.Append("delete from customerinfo ");
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
        public Assistant.Model.customerinfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.*,w.BrandName ");
            strSql.Append(" FROM customerinfo c ");
            strSql.Append(" LEFT JOIN waterbrand w on(c.WaterBrandId=w.WaterBrandId) ");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,11)			};
            parameters[0].Value = id;

            Assistant.Model.customerinfo model = new Assistant.Model.customerinfo();
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
        public Assistant.Model.customerinfo GetModel(string CustomerInfoId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.*,w.BrandName ");
            strSql.Append(" FROM customerinfo c ");
            strSql.Append(" LEFT JOIN waterbrand w on(c.WaterBrandId=w.WaterBrandId) ");
            strSql.Append(" where CustomerInfoId=@CustomerInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = CustomerInfoId;

            Assistant.Model.customerinfo model = new Assistant.Model.customerinfo();
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
        public Assistant.Model.customerinfo DataRowToModel(DataRow row)
        {
            Assistant.Model.customerinfo model = new Assistant.Model.customerinfo();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["CustomerInfoId"] != null && row["CustomerInfoId"].ToString() != "")
                {
                    model.CustomerInfoId = row["CustomerInfoId"].ToString();
                }
                if (row["Number"] != null && row["Number"].ToString() != "")
                {
                    model.Number = row["Number"].ToString();
                }
                if (row["BottledWaterPrice"] != null && row["BottledWaterPrice"].ToString() != "")
                {
                    model.BottledWaterPrice = decimal.Parse(row["BottledWaterPrice"].ToString());
                }
                if (row["WaterBrandId"] != null && row["WaterBrandId"].ToString() != "")
                {
                    model.WaterBrandId = row["WaterBrandId"].ToString();
                }
                if (row["Address"] != null && row["Address"].ToString() != "")
                {
                    model.Address = row["Address"].ToString();
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
                if (row["BrandName"] != null && row["BrandName"].ToString() != "")
                {
                    model.BrandName = row["BrandName"].ToString();
                }
                //获取联系方式
                foreach (var item in new customercontact().DataTableToList(new customercontact().GetList(" CustomerInfoId='" + model.CustomerInfoId + "' ").Tables[0]))
                {
                    model.Phons += item.Phone + ",";
                }
                if (!string.IsNullOrEmpty(model.Phons))
                    model.Phons = model.Phons.Substring(0, model.Phons.Length - 1);
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.*,w.BrandName ");
            strSql.Append(" FROM customerinfo c ");
            strSql.Append(" LEFT JOIN waterbrand w on(c.WaterBrandId=w.WaterBrandId) ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" FROM customerinfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            total = obj == null ? 0 : Convert.ToInt32(obj);

            strSql = new StringBuilder();
            strSql.Append("select c.*,w.BrandName ");
            strSql.Append(" FROM customerinfo c ");
            strSql.Append(" LEFT JOIN waterbrand w on(c.WaterBrandId=w.WaterBrandId) ");
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
            strSql.Append("select count(1) FROM customerinfo ");
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
            strSql.Append(")AS Row, T.*  from customerinfo T ");
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
            parameters[0].Value = "customerinfo";
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
        public List<Assistant.Model.customerinfo> DataTableToList(DataTable dt)
        {
            List<Assistant.Model.customerinfo> modelList = new List<Assistant.Model.customerinfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Assistant.Model.customerinfo model;
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

