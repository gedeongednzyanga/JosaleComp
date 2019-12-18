using System;
using Connexion_manager;
using Parametre;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace User_lib
{
    public class User : IUsers
    {
        public int Id { get; set; }
        public string Nom_user { get; set; }
        public string User_name { get; set; }
        public string Pass_user { get; set; }
        public int Niveau { get; set; }

       public  void Save(IUsers user)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "INSERT_USER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@nom_user", 255, DbType.String, Nom_user));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@usename", 255, DbType.String, User_name));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@pass", 255, DbType.String, Pass_user));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@niveau", 10, DbType.Int32, Niveau));
                MessageBox.Show("Saved Successfully...", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.ExecuteNonQuery();
            }
        }

        public int Test_user(string username, string pass)
        {
            int count = 0;
            string user_name = "";
            int niveau = 0;
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "TEST_USER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@usename", 255, DbType.String, username));
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@pass", 255, DbType.String, pass));
                    IDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        user_name = dr["name_user"].ToString();
                        niveau = Convert.ToInt32(dr["niveau"].ToString());
                        count += 1;
                    }
                    if (count == 1)
                    {
                        MessageBox.Show("Connected successfully !!!", "Server Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UserSession.getInstance().UserNom = user_name;
                        UserSession.getInstance().UserNiveau = niveau;
     
                    }
                    else
                    {
                        MessageBox.Show("Faild to connect database", "Message Serveur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
            return count;
        }








        public void Test_user(string username, string pass, bool test)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "TEST_USER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@usename", 255, DbType.String, username));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@pass", 255, DbType.String, pass));
                IDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    test = true;
                }
                else
                {
                    test = false;
                }    
            }
        }
    }
}
                                                  