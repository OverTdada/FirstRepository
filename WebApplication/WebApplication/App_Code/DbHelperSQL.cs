using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication.App_Code
{
    public class DbHelperSQL
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString;
        public DbHelperSQL() { }
        /// <summary>
        /// 执行SQL语句如update,delete,insert等
        /// </summary>
        /// <param name="sqlText">SQL语句</param>
        /// <returns>影响的行数</returns>
        public static int RunSqlText(string sqlText)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlText, con);
                int rows = cmd.ExecuteNonQuery();
                con.Close();
                return rows;
            }
        }
        /// <summary>
        /// 运行SQL语句返回字符串
        /// </summary>
        /// <param name="sqlText">SQL语句</param>
        /// <returns>返回字符串</returns>
        public static string RunSqlStr(string sqlText)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sqlText, con);
                object returnStr = cmd.ExecuteScalar();
                con.Close();
                if (returnStr == null)
                    return "";
                else
                    return returnStr.ToString();
            }
        }
        /// <summary>
        /// 运行SQL语句返回DataTable
        /// </summary>
        /// <param name="sqlText">SQL语句</param>
        /// <returns>返回DataTable</returns>
        public static DataTable RunSqlDt(string sqlText)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(sqlText, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                return dt;
            }
        }
        /// <summary>
        /// 运行SQL语句返回DataTable分页
        /// </summary>
        /// <param name="sqlText">SQL语句</param>
        /// <param name="Page">页码</param>
        /// <param name="PageSize">分页大小</param>
        /// <returns>返回DataTable</returns>
        public static DataTable RunSqlPage(string sqlText, int Page, int PageSize)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlDataAdapter MyAdapter = new SqlDataAdapter(sqlText, con);
                MyAdapter.Fill(ds, Page, PageSize, "pro_name");
                con.Close();
                dt = ds.Tables["pro_name"];
                return dt;
            }
        }
    }
}