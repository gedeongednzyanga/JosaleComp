using System;
using Parametre;
using Connexion_manager;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Lib
{
   public  class Client : IClient
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Postnom { get; set; }
        public string Prenom { get; set; }
        public string Contact { get; set; }
        public string Mail { get; set; }
        public string Addresse { get; set; }

        private IClient GetClient(IDataReader dr)
        {
            IClient clients = new Client();
            clients.Id = Convert.ToInt32(dr["N°"].ToString());
            clients.Nom = dr["First name"].ToString();
            clients.Postnom = dr["Last name"].ToString();
            clients.Prenom = dr["Surname"].ToString();
            clients.Contact = dr["Telephone"].ToString();
            clients.Mail = dr["E-mail"].ToString();
            clients.Addresse = dr["Physical address"].ToString();
            return clients;
        }
        public List<IClient> AllClient()
        {
            List<IClient> list = new List<IClient>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT_CLIENT";
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(GetClient(dr));
                }
                cmd.Dispose();
                dr.Dispose();
            }
            return list;
        }

        public List<IClient> Search(string recherche)
        {
            List<IClient> lst = new List<IClient>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Affichage_Client WHERE [First name] LIKE '%" + recherche + "%' or [Last name] LIKE '%" + recherche + "%' ";
                IDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    lst.Add(GetClient(rd));
                }
                rd.Dispose();
                rd.Close();
            }
            return lst;
        }
        public void Delete(int id)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Int32, id));
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            ImplementeConnexion.Instance.Conn.Close();
        }

        public int Nouveau()
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(code_cli) as last_id FROM client";
                IDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["last_id"] == DBNull.Value)
                        Id = 1;
                    else
                        Id = Convert.ToInt32(dr["last_id"].ToString()) + 1;
                }
                dr.Dispose();
                cmd.Dispose();
            }
            ImplementeConnexion.Instance.Conn.Close();
            return Id;
        }

        public IClient OneClient(int id)
        {
            IClient client = new Client();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Int32, Id));
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    client = GetClient(dr);
                }
                cmd.Dispose();
                dr.Dispose();
            }
            ImplementeConnexion.Instance.Conn.Close();
            return client;
        }
        public void Save(IClient client)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "INSERT_CLIENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 10, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, Nom));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@postnom", 50, DbType.String, Postnom));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@prenom", 50, DbType.String, Prenom));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@contact", 50, DbType.String, Contact));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@mail", 50, DbType.String, Mail));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@adresse", 100, DbType.String, Addresse));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved successfully !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();
            }
            ImplementeConnexion.Instance.Conn.Close();
        }
    }
}
