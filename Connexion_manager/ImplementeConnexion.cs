using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Threading.Tasks;

namespace Connexion_manager
{
    public class ImplementeConnexion
    {
        private ImplementeConnexion(){}

        private IDbConnection _conn = null;
        private static ImplementeConnexion _instance = null;

        public IDbConnection Conn{
            get { return _conn; }
            set { _conn = value; }
        }

        public static ImplementeConnexion Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ImplementeConnexion();
                return _instance;
            }
        }

        public IDbConnection Initialize(Connexion connexion, Sgbd sgbd)
        {
            switch (sgbd)
            {
                case Sgbd.SQLServer:
                    _conn = new SqlConnection(string.Format("Data source ={ 0 }; Initial catalog = { 1 }; User ID = { 2 }; Password ={ 3}",
                        connexion.Server, connexion.Database, connexion.User, connexion.Password));
                    break;
                    //case ConnexionType.MySQL:
                    //    _conn = new MySqlConnection(string.Format("Server={0};Database={1};UserID={2};Password={3}",
                    //        connexion.Serveur, connexion.Database, connexion.User, connexion.Password));
                    //    break;
                    //case ConnexionType.PostGrsSQL:
                    //    _conn = new NpgsqlConnection(string.Format("Server={0};Database={1};Uid={2};Pwd={3};Port={4}",
                    //        connexion.Serveur, connexion.Database, connexion.User, connexion.Password, connexion.Port));
                    //    break;
                 case Sgbd.Oracle:
                    return null;
                case Sgbd.Access:
                    _conn = new OleDbConnection(string.Format("Data Source ={ 0 }; Initial catalog ={ 1 }; User ID ={ 2 }; Password ={ 3 }",
                        connexion.Server, connexion.Database, connexion.User, connexion.Password));
                    break;
            }
            return _conn;
        }
    }
}
