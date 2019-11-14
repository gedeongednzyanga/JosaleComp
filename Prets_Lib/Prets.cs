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
    public class Prets : IPrets
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
            prets.Id = Convert.ToInt32(dr["Numéro"].ToString());
            prets.Nom = dr["Nom"].ToString();
            prets.Postnom = dr["Postnom"].ToString();
            prets.Prenom = dr["Prénom"].ToString();
            prets.Montant = float.Parse(dr["Montant prêté"].ToString());
            prets.Interet = float.Parse(dr["Intéret"].ToString());
            prets.Montantpaye = float.Parse(dr["Montant à payé"].ToString());
            prets.DatePret = Convert.ToDateTime(dr["Date Prêt"].ToString());
            prets.DateRembour = Convert.ToDateTime(dr["Date Remb."].ToString()).Date;
            return prets;

        }

        private IPrets GetAll_Rembou(IDataReader dr)
        {
            IPrets prets = new Prets();
            prets.Id = Convert.ToInt32(dr["Numéro"].ToString());
            prets.Nom = dr["Nom"].ToString();
            prets.Postnom = dr["Postnom"].ToString();
            prets.Prenom = dr["Prénom"].ToString();
            prets.Montant = float.Parse(dr["montant"].ToString());
            prets.Montantpaye = float.Parse(dr["reste"].ToString());
            prets.DatePret = Convert.ToDateTime(dr["DateRemb."].ToString());
            prets.RefCli = Convert.ToInt32(dr["refcli"].ToString());
    
            return prets;
        }

        public List<IPrets> Allcredit()
        {
            List<IPrets> list = new List<IPrets>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT_PRET";
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

        public List<IPrets> Allcredit_Tri(int annee)
        {
            List<IPrets> list = new List<IPrets>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "TRI_PRETS_ANNEE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@anne", 10, DbType.Int32, annee));
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

        public List<IPrets> Allcredit_Tri_moi(int annee, string mois)
        {
            List<IPrets> list = new List<IPrets>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "TRI_PRETS_ANNE_MOI";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@anne", 10, DbType.Int32, annee));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@mois", 50, DbType.String, mois));
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

        public List<IPrets> AllRembou()
        {
            List<IPrets> list = new List<IPrets>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT_REMBOU";
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(GetAll_Rembou(dr));
                }
                cmd.Dispose();
                dr.Close();
            }
            return list;
        }

        public List<IPrets> Search (string recherche)
        {
            List<IPrets> list = new List<IPrets>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Affichage_Client_Prets WHERE nom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR Prénom LIKE '%" + recherche + "%'";
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

        public List<IPrets> Search_Rembou(string recherche)
        {
            List<IPrets> list = new List<IPrets>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using(IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Affichage_Client_Rembourssement WHERE nom LIKE '%" + recherche + "%' OR postnom LIKE '%" + recherche + "%' OR Prénom LIKE '%" + recherche + "%'";
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(GetAll_Rembou(dr));
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
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 10, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@montant", 10, DbType.Double, Montant));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@refcli", 10, DbType.Int32, RefCli));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@refinter", 10, DbType.Int32, RefInteret));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@daterembour", 20, DbType.Date, DateRembour));
                cmd.ExecuteNonQuery();
                //MessageBox.Show("Saved successfully !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
