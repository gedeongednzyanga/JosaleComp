using System;
using Connexion_manager;
using Parametre;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
namespace Trier_Lib
{
    public class Tiers : ITiers
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Postnom { get; set; }
        public string Prenom { get; set; }
        public string Contact { get; set; }
        public string Mail { get; set; }
        public string Addresse { get; set; }

        private ITiers GetTiers(IDataReader dr)
        {
            ITiers tiers = new Tiers();
            tiers.Id = Convert.ToInt32(dr["N°"].ToString());
            tiers.Nom = dr["First name"].ToString();
            tiers.Postnom = dr["Last name"].ToString();
            tiers.Prenom = dr["Surname"].ToString();
            tiers.Contact = dr["Telephone"].ToString();
            tiers.Mail = dr["E-mail"].ToString();
            tiers.Addresse = dr["Adresse"].ToString();
            return tiers;
        }

        public List<ITiers> AllTiers()
        {
            List<ITiers> List = new List<ITiers>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT_TIER";
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    List.Add(GetTiers(dr));
                }
                cmd.Dispose();
                dr.Close();
            }
            return List;
        }

        public int Nouveau()
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(code_tier) AS last_id FROM tier ";
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
            return Id;
        }

        public void Save(ITiers tiers)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "INSERT_TIER";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 5, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, Nom));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@postnom", 50, DbType.String, Postnom));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@prenom", 50, DbType.String, Prenom));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@contact", 50, DbType.String, Contact));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@email", 50, DbType.String, Mail));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@adresse", 100, DbType.String, Addresse));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved successfully !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Delete(int id)
        {
            try
            {
                if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                    ImplementeConnexion.Instance.Conn.Open();
                using(IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE_TIER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@id", 5, DbType.Int32, id));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Deleted successfully !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message,"Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ImplementeConnexion.Instance.Conn.Close();
            }
        }

        public List<ITiers> Search(string recherche)
        {
            List<ITiers> lst = new List<ITiers>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Affichage_Tier WHERE [First name] LIKE '%" + recherche + "%' or [Last name] LIKE '%" + recherche + "%' ";
                IDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    lst.Add(GetTiers(rd));
                }
                rd.Dispose();
                rd.Close();
            }
            return lst;
        }

        public ITiers OneTier(int i)
        {
            ITiers tier = new Tiers();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "code_tier", 5, DbType.Int32, Id));
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    tier = GetTiers(dr);
                }
                cmd.Dispose();
                dr.Dispose();
            }
            return tier;
        }
    }
}
