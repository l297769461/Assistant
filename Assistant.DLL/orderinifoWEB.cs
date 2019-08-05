
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DBUtility;
using System.Collections.Generic;
namespace Assistant.DAL
{
    /// <summary>
    /// 数据访问类:orderinifoWEB
    /// </summary>
    public partial class orderinifoWEB : orderinifo
    {
        public orderinifoWEB()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add2(Assistant.Model.orderinifo model)
        {
            int length = 30;
            List<Assistant.Model.orderinifo> lcs = DataTableToList2(GetList2(" KeyValue='" + model.KeyValue + "'").Tables[0]);
            if (lcs != null && lcs.Count >= length)
            {
                //总数大于 length 条，将删除多余的数据
                for (int i = lcs.Count - 1; i >= length; i--)
                {
                    base.Delete(lcs[i].OrderInfoId);
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into orderinifo(");
            strSql.Append("id,OrderInfoId,Phone,CustomerInfoId,BottledWaterPrice,BottledNumber,OrderHandlingType,Notes,CreateTime,UpdateTime,BrandName,Address,Number,KeyValue)");
            strSql.Append(" values (");
            strSql.Append("@id,@OrderInfoId,@Phone,@CustomerInfoId,@BottledWaterPrice,@BottledNumber,@OrderHandlingType,@Notes,@CreateTime,@UpdateTime,@BrandName,@Address,@Number,@KeyValue)");
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
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime),
                    new MySqlParameter("@BrandName",MySqlDbType.VarChar,50),
                    new MySqlParameter("@Address",MySqlDbType.VarChar,255),
                    new MySqlParameter("@Number",MySqlDbType.VarChar,50),
                    new MySqlParameter("@KeyValue",MySqlDbType.VarChar,255)};
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
            parameters[10].Value = model.BrandName;
            parameters[11].Value = model.Address;
            parameters[12].Value = model.Number;
            parameters[13].Value = model.KeyValue;

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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList2(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from orderinifo  ");
            strSql.Append(" where");
            strSql.Append(" " + strWhere + " ");
            strSql.Append(" order by id desc");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.orderinifo DataRowToModel2(DataRow row)
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
                if (row["BrandName"] != null && row["BrandName"].ToString() != "")
                {
                    model.BrandName = row["BrandName"].ToString();
                }
                if (row["Address"] != null && row["Address"].ToString() != "")
                {
                    model.Address = row["Address"].ToString();
                }
                if (row["Number"] != null && row["Number"].ToString() != "")
                {
                    model.Number = row["Number"].ToString();
                }
                if (row["KeyValue"] != null && row["KeyValue"].ToString() != "")
                {
                    model.KeyValue = row["KeyValue"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Assistant.Model.orderinifo> DataTableToList2(DataTable dt)
        {
            List<Assistant.Model.orderinifo> modelList = new List<Assistant.Model.orderinifo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Assistant.Model.orderinifo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = DataRowToModel2(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update2(Assistant.Model.orderinifo model)
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
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("BrandName=@BrandName,");
            strSql.Append("Address=@Address,");
            strSql.Append("Number=@Number");
            strSql.Append(" where OrderInfoId=@OrderInfoId and KeyValue=@KeyValue");
            MySqlParameter[] parameters = {
					new MySqlParameter("@OrderInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Phone", MySqlDbType.VarChar,50),
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@BottledWaterPrice", MySqlDbType.Decimal,10),
					new MySqlParameter("@BottledNumber", MySqlDbType.Int32,11),
					new MySqlParameter("@OrderHandlingType", MySqlDbType.Int32,11),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime),
                    new MySqlParameter("@BrandName",MySqlDbType.VarChar,50),
                    new MySqlParameter("@Address",MySqlDbType.VarChar,255),
                    new MySqlParameter("@Number",MySqlDbType.VarChar,50),
                    new MySqlParameter("@KeyValue",MySqlDbType.VarChar,255)};
            parameters[0].Value = model.OrderInfoId;
            parameters[1].Value = model.Phone;
            parameters[2].Value = model.CustomerInfoId;
            parameters[3].Value = model.BottledWaterPrice;
            parameters[4].Value = model.BottledNumber;
            parameters[5].Value = model.OrderHandlingType;
            parameters[6].Value = model.Notes;
            parameters[7].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[8].Value = DateTime.Parse(model.UpdateTime.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            parameters[9].Value = model.BrandName;
            parameters[10].Value = model.Address;
            parameters[11].Value = model.Number;
            parameters[12].Value = model.KeyValue;

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
        public Assistant.Model.orderinifo GetModel2(string id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from orderinifo ");
            strSql.Append(" where 1=1 ");
            strSql.Append(" and OrderInfoId=@OrderInfoId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@OrderInfoId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = id;

            Assistant.Model.orderinifo model = new Assistant.Model.orderinifo();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel2(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
    }
}

