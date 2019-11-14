using System;
using Connexion_manager;
using Parametre;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emprunts_Lib
{
    public class Emprunt : IEmprunt
    {
        public int Id { get; set; }
        public float Montant { get; set; }
        public float MontantRemb { get; set; }
        public float Reste { get; set; }
        public int Reftier { get; set; }
        public DateTime DateEmprunt { get; set; }
        public DateTime DateRembu { get; set; }
        public string Tier { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
   

        public int Nouveau()
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(code_emprunt) AS last_id FROM emprunts ";
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

        public void Save(IEmprunt emprunt)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "INSERT_EMPRUNT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 5, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@montant", 10, DbType.Double, Montant));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@date_remb", 20, DbType.Date, DateRembu));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@montant_remb", 20, DbType.Double, MontantRemb));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@reftier", 20, DbType.Int32, Reftier));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved Successfully !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();
            }
        }

        public void Delete(int id)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "DELETE_EMPRUNT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 10, DbType.Int32, id));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Deleted Successfully !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();
            }
        }

        public IEmprunt GetEmprunt (IDataReader dr)
        {
            IEmprunt emprunt = new Emprunt();
            emprunt.Id = Convert.ToInt32(dr["code_emprunt"].ToString());
            emprunt.Name = dr["nom_t"].ToString();
            emprunt.LastName = dr["postnom_t"].ToString();
            emprunt.SurName = dr["prenom_t"].ToString();
            emprunt.Montant = float.Parse(dr["montant"].ToString());
            emprunt.DateEmprunt = DateTime.Parse(dr["date_emprunt"].ToString());
            emprunt.MontantRemb = float.Parse(dr["montant_remb"].ToString());
            emprunt.DateRembu = DateTime.Parse(dr["date_rembu"].ToString());
            return emprunt;
        }

        public IEmprunt GetEmprunt_Remb(IDataReader dr)
        {
            IEmprunt emprunt = new Emprunt();
            emprunt.Id = Convert.ToInt32(dr["N°"].ToString());
            emprunt.Name = dr["Noms"].ToString();
            emprunt.Montant = float.Parse( dr["Emprunt"].ToString());
            emprunt.MontantRemb = float.Parse(dr["Montant"].ToString());
            emprunt.Reste = float.Parse(dr["Reste"].ToString());
            emprunt.DateEmprunt = DateTime.Parse(dr["Date_op"].ToString());
            emprunt.Reftier =Convert.ToInt32(dr["ref_emp"].ToString());
            return emprunt;
        }

        public IEmprunt GetNomId(IDataReader dr)
        {
            IEmprunt tier = new Emprunt();
            tier.Reftier = Convert.ToInt32(dr["code_tier"].ToString());
            tier.Tier = dr["noms"].ToString();
            return tier;
        }

        public List<IEmprunt> Get_TierNomId()
        {
            List<IEmprunt> list = new List<IEmprunt>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "select * from Affichage_TierNID";
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(GetNomId(dr));
                }
                dr.Close();
                cmd.Dispose();
            }
            return list;
        }

        public List<IEmprunt> AllEmprunt()
        {
            List<IEmprunt> list = new List<IEmprunt>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT_EMPRUNT";
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(GetEmprunt(dr));
                }
                cmd.Dispose();
                dr.Close();
            }
            return list;
        }

        public List<IEmprunt> AllEmprunt_Remb()
        {
            List<IEmprunt> list = new List<IEmprunt>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT_REMBOU_TIER";
                cmd.CommandType = CommandType.StoredProcedure;
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(GetEmprunt_Remb(dr));
                }
                cmd.Dispose();
                dr.Close();
            }
            return list;
        }

        public IEmprunt OneEmprunt(int id)
        {
            IEmprunt tier = new Emprunt();
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
                    tier = GetEmprunt(dr); 
                }
                cmd.Dispose();
                dr.Dispose();
            }
            return tier;
        }

        public List<IEmprunt> Search(string recherche)
        {
            List<IEmprunt> list = new List<IEmprunt>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Affichage_Emprunt WHERE nom_t LIKE '%"+recherche+ "%' OR postnom_t LIKE '%"+recherche+ "%' OR prenom_t LIKE '%"+recherche+"%'";
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(GetEmprunt(dr));
                }
                cmd.Dispose();
                dr.Close();
            }
            return list;
        }
        public List<IEmprunt> Search_remb(string recherche)
        {
            List<IEmprunt> list = new List<IEmprunt>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Affichage_Tier_Rembou WHERE Noms LIKE '%" + recherche + "%'";
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(GetEmprunt_Remb(dr));
                }
                cmd.Dispose();
                dr.Close();
            }
            return list;
        }
    }
}
