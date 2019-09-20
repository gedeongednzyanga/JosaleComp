using System;
using Connexion_manager;
using Parametre;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Remboursemment_Lib
{
    class Remboursser : IRemboursser
    {
        public int Id { get; set; }
        public float Montant { get; set; }
        public int RefCli { get; set; }
        public string Nom{ get; set; }
        public string Postnom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateRembour { get; set; }


        private IRemboursser GetAll(IDataReader dr)
        {
            IRemboursser rembouresser = new Remboursser();
            rembouresser.Id = Convert.ToInt32(dr[""].ToString());
            rembouresser.Nom = dr[""].ToString();
            rembouresser.Postnom = dr[""].ToString();
            rembouresser.Prenom = dr[""].ToString();
            rembouresser.Montant = float.Parse(dr[""].ToString());
            rembouresser.DateRembour = Convert.ToDateTime(dr[""].ToString());
            return rembouresser;

        }
        public int Nouveau()
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(code_rembu) as last_id FROM rembourssement";
                IDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["last_id"] == DBNull.Value)
                        Id = 1;
                    else
                        Id = Convert.ToInt32(dr["last_id"].ToString());
                }
                dr.Dispose();
                cmd.Dispose();
            }
            return Id;
        }

        public void Save (IRemboursser rembours)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 10, DbType.Double, Montant));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Int32, RefCli));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved successfully !!!", "Messsage...", MessageBoxButtons.OK, MessageBoxIcon.Information)
;            }
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
                    cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "code_rembu", 5, DbType.Int32, id));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { ImplementeConnexion.Instance.Conn.Close(); }
        }

        public List<IRemboursser> AllRembour()
        {
            List<IRemboursser> list = new List<IRemboursser>();
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

        public  IRemboursser OneRembour(int id)
        {
            IRemboursser prets = new Remboursser();
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
    }
}
