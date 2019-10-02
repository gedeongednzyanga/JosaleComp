using System;
using Connexion_manager;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Parametre;

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

       public void Load_gage(int id, ListView liste)
        {

            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT_ONE_GAGE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListViewItem list = new ListViewItem(dr["Code"].ToString());
                        list.SubItems.Add(dr["Designation"].ToString());
                        list.SubItems.Add(dr["Valeur"].ToString());
                        list.SubItems.Add(dr["Nombre"].ToString());
                        list.SubItems.Add(dr["Nombre"].ToString());
                        liste.Items.Add(list);
                    }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur " + ex.Message, "Error");
            }
            finally
            {
                ImplementeConnexion.Instance.Conn.Close();
            }
        }
        public void Load_Credit(DataGridView liste)
        {

            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT_CREDIT_FOR_CUSTOMER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        liste.Rows.Add(dr["N°"].ToString(), dr["Customer"].ToString(), dr["Mount"].ToString());
                       
                    }
                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur " + ex.Message, "Error");
            }
            finally
            {
                ImplementeConnexion.Instance.Conn.Close();
            }
        }
    }
}
