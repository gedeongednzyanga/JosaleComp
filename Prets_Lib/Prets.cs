using System;
using Connexion_manager;
using Parametre;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prets_Lib
{
    class Prets : IPrets
    {
        public DateTime DateRembour {get; set; }
        public int Id{ get; set; }
        public float Montant { get; set; }
        public int RefCli { get; set; }
        public int RefInteret { get; set; }
        public string Nom { get; set; }
        public string Postnom { get; set; }
        public string Prenom { get; set; }
        public float Interet { get; set; }
        public float Montantpaye { get; set; }
        public DateTime DatePret { get; set; }

        private IPrets GetAll(IDataReader dr)
        {
            IPrets prets = new Prets();
            prets.Id = Convert.ToInt32(dr[""].ToString());
            prets.Nom = dr[""].ToString();
            prets.Postnom = dr[""].ToString();
            prets.Prenom = dr[""].ToString();
            prets.Montant = float.Parse(dr[""].ToString());
            prets.Interet = float.Parse(dr[""].ToString());
            prets.Montantpaye = float.Parse(dr[""].ToString());
            prets.DatePret = Convert.ToDateTime(dr[""].ToString());
            prets.DateRembour = Convert.ToDateTime(dr[""].ToString()).Date;
            return prets;

        }
        public List<IPrets> Allcredit()
        {
            List<IPrets> list = new List<IPrets>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "";
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(GetAll(dr));
                }
                cmd.Dispose();
                dr.Close();
            }
            return list;
        }

        public void Delete(int id)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "code_pret", 5, DbType.Int32, id));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { ImplementeConnexion.Instance.Conn.Close(); }
           
        }

        public int Nouveau()
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(code_pret) AS last_id FROM prets";
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["last_id"] == DBNull.Value)
                        Id = 1;
                    else
                        Id = Convert.ToInt32(dr["last_id"].ToString()) + 1;
                }
                dr.Close();
                cmd.Dispose();
            }
            return Id;
        }

        public IPrets OneCredit(int id)
        {
            IPrets prets = new Prets();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "";
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    prets = GetAll(dr);
                }
                dr.Close();
                cmd.Dispose();
            }
            ImplementeConnexion.Instance.Conn.Close();
            return prets;
        }

        public void Save(IPrets prets)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "INSERT_PRETS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 10, DbType.Double, Montant));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Int32, RefCli));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Int32, RefInteret));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Date, DateRembour));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved successfully !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
