using System;
using Connexion_manager;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connexion_manager
{
    public class Cls_Connexion
    {
        Sgbd sgbd = new Sgbd();
        public static Cls_Connexion _instance = null;

        public static Cls_Connexion Instance()
        {
            if (_instance == null)
                _instance = new Cls_Connexion();
            return _instance;
        }

        public void Connect()
        {
            try
            {
                Connexion connexion = new Connexion();
                connexion.Server = File.ReadAllText(Chemin_Connexion.Table.server.ToString()).Trim();
                connexion.Database = File.ReadAllText(Chemin_Connexion.Table.database.ToString()).Trim();
                connexion.User = File.ReadAllText(Chemin_Connexion.Table.user.ToString()).Trim();
                connexion.Password = File.ReadAllText(Chemin_Connexion.Table.password.ToString()).Trim();
                ImplementeConnexion.Instance.Initialize(connexion, sgbd);
            }catch(Exception ex)
            {
                MessageBox.Show("Erreur => " + ex.Message, "Message...");
            }
            finally
            {
                if (ImplementeConnexion.Instance.Conn != null)
                {
                    if (ImplementeConnexion.Instance.Conn.State == System.Data.ConnectionState.Open)
                    {
                        ImplementeConnexion.Instance.Conn.Close();
                    }
                }
            }
        }
    }
}
