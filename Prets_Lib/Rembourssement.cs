using Connexion_manager;
using Parametre;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prets_Lib
{
    public class Rembourssement
    {
        public int Id { get; set; }
        public double Montant { get; set; }
        public int RefCli { get; set; }

        public int Nouveau()
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(code_gage) as last_id from gage";
                IDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["last_id"] == DBNull.Value)
                        Id = 1;
                    else
                        Id = Convert.ToInt32(dr["last_id"].ToString()) + 1;
                }
                dr.Dispose();
            }
            return Id;
        }

        public void Save(Rembourssement rembu)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "INSERT_REMBOURSSEMENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 10, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@montant", 10, DbType.Double, Montant));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@refcli", 10, DbType.Int32, RefCli));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Saved successfully !!!", "Message...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.Dispose();
            }
            ImplementeConnexion.Instance.Conn.Close();
        }
    }
}
