using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PS_QuotaZero
{
    public class GsysSQL
    {
        #region //Execute Data
        public static DataTable fncGetQueryData(string lvSQL, DataTable dt)
        {
            string query = lvSQL;
            DataTable DTReturn = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(query, System.Configuration.ConfigurationManager.ConnectionStrings["PSConnectionSQL"].ToString());
            DA.Fill(DTReturn);

            return DTReturn;
        }

        public static string fncExecuteQueryData(string lvSQL)
        {
            string lvReturn = "";
            try
            {
                string query = lvSQL;
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnectionSQL"].ToString());
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                con.Close();
                con.Dispose();

                lvReturn = "Success";
                return lvReturn;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        
        public static string fncCheckLogin(string lvUser, string lvPass)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT * FROM SysUser WHERE us_UserID = '" + lvUser + "' AND us_Password = '" + lvPass + "' And us_Active = '1' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["us_UserID"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncGetFullName(string lvQuota)
        {
            #region //Connect Database 
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnectionSQL"].ToString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "Select Q_FirstName, Q_LastName From Cane_Quota WHERE Q_ID = '" + lvQuota + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Q_FirstName"].ToString() + " " + dr["Q_LastName"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }
    }
}