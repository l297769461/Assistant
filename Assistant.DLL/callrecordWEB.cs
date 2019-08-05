
using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DBUtility;
using System.Collections.Generic;
namespace Assistant.DAL
{
    /// <summary>
    /// 数据访问类:callrecordWEB
    /// </summary>
    public partial class callrecordWEB : callrecord
    {
        public callrecordWEB()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add2(Assistant.Model.callrecord model)
        {
            int length = 30;
            List<Assistant.Model.callrecord> lcs = DataTableToList2(GetList2(" KeyValue='" + model.KeyValue + "'").Tables[0]);
            if (lcs != null && lcs.Count >= length)
            {

                //总数大于 length 条，将删除多余的数据
                for (int i = lcs.Count - 1; i >= length; i--)
                {
                    base.Delete(lcs[i].CallRecordId);
                }
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into callrecord(");
            strSql.Append("CallRecordId,Phone,CustomerInfoId,handlingType,CreateTime,UpdateTime,Number,BottledWaterPrice,BrandName,Address,Notes,KeyValue)");
            strSql.Append(" values (");
            strSql.Append("@CallRecordId,@Phone,@CustomerInfoId,@handlingType,@CreateTime,@UpdateTime,@Number,@BottledWaterPrice,@BrandName,@Address,@Notes,@KeyValue)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CallRecordId", MySqlDbType.VarChar,50),
					new MySqlParameter("@Phone", MySqlDbType.VarChar,50),
					new MySqlParameter("@CustomerInfoId", MySqlDbType.VarChar,50),
					new MySqlParameter("@handlingType", MySqlDbType.Int32,7),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@UpdateTime", MySqlDbType.DateTime),
					new MySqlParameter("@Number", MySqlDbType.VarChar,50),
					new MySqlParameter("@BottledWaterPrice", MySqlDbType.Decimal,10),
					new MySqlParameter("@BrandName", MySqlDbType.VarChar,50),
					new MySqlParameter("@Address", MySqlDbType.VarChar,255),
					new MySqlParameter("@Notes", MySqlDbType.VarChar,255),
                    new MySqlParameter("@KeyValue",MySqlDbType.VarChar,255)};
            parameters[0].Value = model.CallRecordId;
            parameters[1].Value = model.Phone;
            parameters[2].Value = model.CustomerInfoId;
            parameters[3].Value = model.handlingType;
            parameters[4].Value = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[5].Value = model.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
            parameters[6].Value = model.Number;
            parameters[7].Value = model.BottledWaterPrice;
            parameters[8].Value = model.BrandName;
            parameters[9].Value = model.Address;
            parameters[10].Value = model.Notes;
            parameters[11].Value = model.KeyValue;

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
            strSql.Append("select * ");
            strSql.Append(" FROM callrecord ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by id desc");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Assistant.Model.callrecord DataRowToModel2(DataRow row)
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
        public List<Assistant.Model.callrecord> DataTableToList2(DataTable dt)
        {
            List<Assistant.Model.callrecord> modelList = new List<Assistant.Model.callrecord>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Assistant.Model.callrecord model;
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
        public bool Update2(string CallRecordId, string CreateTime, int handlingType, string keyvalue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update callrecord set ");
            strSql.Append("handlingType=" + handlingType);
            strSql.Append(" where CallRecordId='" + CallRecordId + "' and CreateTime='" + CreateTime + "' and keyvalue='" + keyvalue + "'");

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
        public Assistant.Model.callrecord GetModel2(string CallRecordId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM callrecord  ");
            strSql.Append(" where CallRecordId=@CallRecordId ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@CallRecordId", MySqlDbType.VarChar,50)			};
            parameters[0].Value = CallRecordId;

            Assistant.Model.callrecord model = new Assistant.Model.callrecord();
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

