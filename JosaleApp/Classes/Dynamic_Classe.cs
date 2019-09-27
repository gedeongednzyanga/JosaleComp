using System;
using Connexion_manager;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JosaleApp.Classes
{
    class Dynamic_Classe
    {
        SqlDataAdapter da = null;
        SqlConnection con;
        DataSet ds;
        public static Dynamic_Classe dynC;

        public static  Dynamic_Classe Instance()
        {
            if (dynC == null)
                dynC = new Dynamic_Classe();
            return dynC;
        }

        public DataTable Get_Data(string tablename)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                con = (SqlConnection)ImplementeConnexion.Instance.Conn;
                da = new SqlDataAdapter("SELECT * FROM " + tablename + "", con);
                ds = new DataSet();
                da.Fill(ds, tablename);
                con.Close();
                return ds.Tables[0];

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable Get_Data_combo(string tablename, string champ, string champ2)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                con = (SqlConnection)ImplementeConnexion.Instance.Conn;
                da = new SqlDataAdapter("SELECT * FROM " + tablename + "", con);
                ds = new DataSet();
                da.Fill(ds, tablename);
                con.Close();
                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
