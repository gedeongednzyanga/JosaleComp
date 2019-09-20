using System;
using Connexion_manager;
using Parametre;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Prets_Lib
{
    class Gage
    {
        public int Id { get; set; }
        public string Designation { get; set; }
        public float Valeur { get; set; }
        public int Refpret { get; set; }

        public int Nouveau()
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(code_gage) as last_id from gage";
                IDataReader dr = cmd.ExecuteReader();
                if(dr.Read()){
                    if (dr["last_id"] == DBNull.Value)
                        Id = 1;
                    else
                        Id = Convert.ToInt32(dr["last_id"].ToString());
                }
                dr.Dispose();
            }
            return Id;
        }

        public void Save(Gage gage)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand()) {
                cmd.CommandText = "";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 5, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 50, DbType.String, Designation));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 10, DbType.Double, Valeur));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "", 10, DbType.Int32, Refpret));
                cmd.ExecuteNonQuery();
            }
        }
    }
}
