using Connexion_manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JosaleApp.Classes
{
    public class Test_Configuration
    {
        public static void Test_Flies()
        {
            if (Directory.Exists(Chemin_Connexion.Table.InitialDiarictory)) { }
            else { Directory.CreateDirectory(Chemin_Connexion.Table.InitialDiarictory); }

            if (File.Exists(Chemin_Connexion.Table.server)==true && File.Exists(Chemin_Connexion.Table.database)==true && 
                File.Exists(Chemin_Connexion.Table.user)==true && File.Exists(Chemin_Connexion.Table.password)== true)
            { Cls_Connexion.Instance().Connect(); }
            else{
                FormConfigurationServer frmconf = new FormConfigurationServer();
                frmconf.ShowDialog();
                Cls_Connexion.Instance().Connect();
            }
        }
    }
}
