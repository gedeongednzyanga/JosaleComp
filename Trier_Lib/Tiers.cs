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

        private ITiers GetTiers(IDataReader dr)
        {
            ITiers tiers = new Tiers();
            tiers.Id = Convert.ToInt32(dr[""].ToString());
            tiers.Nom = dr[""].ToString();
            tiers.Postnom = dr[""].ToString();
            tiers.Prenom = dr[""].ToString();
            tiers.Contact = dr[""].ToString();
            return tiers;
        }
        public List<ITiers> AllTiers()
        {
            List<ITiers> List = new List<ITiers>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "";
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
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 50, DbType.String, Nom));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 50, DbType.String, Postnom));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 50, DbType.String, Prenom));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 50, DbType.String, Contact));
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
                    cmd.CommandText = "";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "code", 5, DbType.Int32, id));
                    cmd.ExecuteNonQuery();
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
