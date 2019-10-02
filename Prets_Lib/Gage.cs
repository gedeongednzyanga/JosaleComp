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
    public class Gage
    {

        public int Id { get; set; }
        public string Designation { get; set; }
        public float Valeur { get; set; }
        public int Refpret { get; set; }
        public int Nombre { get; set; }

        public int Nouveau()
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT MAX(code_remb) as last_id from gage";
                IDataReader dr = cmd.ExecuteReader();
                if(dr.Read()){
                    if (dr["last_id"] == DBNull.Value)
                        Id = 1;
                    else
                        Id = Convert.ToInt32(dr["last_id"].ToString()) + 1;
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
                cmd.CommandText = "INSERT_GAGE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 10, DbType.Int32, Id));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@designation", 50, DbType.String, Designation));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@valeur", 10, DbType.Double, Valeur));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@refpret", 10, DbType.Int32, Refpret));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@nombre", 10, DbType.Int32, Nombre));
                cmd.ExecuteNonQuery();
            }
        }
        private DataTable GetAll(IDataReader dr)
        {
            DataTable gage = new DataTable();
            gage.Columns.Add("Code");
            gage.Columns.Add("Designation");
            gage.Columns.Add("Valeur");
            gage.Columns.Add("Refpre");
            gage.Columns.Add("Nombre");
            return gage;

        }
        public List<DataTable> AllGage(int id)
        {
            List<DataTable> list = new List<DataTable>();
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "SELECT_ONE_GAGE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@code", 10, DbType.Int32, id));
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
    }
}
