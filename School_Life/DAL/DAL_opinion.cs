using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data.SqlClient;
using System.Data;
using Common;

namespace DAL
{
    public class DAL_opinion
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(Opinions model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into opinions(");
            strSql.Append("opntime,opncontent,userid)");
            strSql.Append(" values (");
            strSql.Append("@opntime,@opncontent,@userid)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@opntime", SqlDbType.VarChar,50),
					new SqlParameter("@opncontent", SqlDbType.Text),
					new SqlParameter("@userid", SqlDbType.VarChar,20)};
            parameters[0].Value = model.opntime;
            parameters[1].Value = model.opncontent;
            parameters[2].Value = model.userid;

            object obj = SQLDBHelper.GetSingle(strSql.ToString(), parameters);
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
        /// 删除一条数据
        /// </summary>
        public static bool Delete(int opnid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from opinions ");
            strSql.Append(" where opnid=@opnid");
            SqlParameter[] parameters = {
					new SqlParameter("@opnid", SqlDbType.Int,4)
			};
            parameters[0].Value = opnid;

            int rows = SQLDBHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
