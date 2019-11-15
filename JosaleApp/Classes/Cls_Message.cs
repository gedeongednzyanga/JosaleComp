using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using Connexion_manager;
using Parametre;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace JosaleApp.Classes
{
    class Cls_Message
    {
        public static int port, baudRate, timeout;
        public GsmCommMain comm;
        public static Cls_Message cMessage = null;

        public string _Message { get; set; }
        public int Reftier { get; set; }

        public static Cls_Message Insatnce()
        {
            if (cMessage == null)
                cMessage = new Cls_Message();
            return cMessage;
        }
       
        //Méthode pour recuperer le port du modem

        public string  GetAllPorts(ComboBox port)
        {
            string modems = "";
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_POTSModem ");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if ((string)queryObj["Status"] == "OK")
                    {
                        string pr = "";
                        string por = queryObj["AttachedTo"].ToString(); // + " - " + System.Convert.ToString(queryObj["Description"]);
                        for (int i = 0; i < por.Length; i++)
                        {
                            if (por[i] != 'C' && por[i] != 'O' && por[i] != 'M')
                                pr = pr + por[i];
                        }
                        port.Items.Add(pr);
                    }
                    if (port.Items.Count > 0)
                    {
                        port.SelectedIndex = 0;
                    }
                }
                return modems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la requette", "Erreur de" + ex.Message);
                return "";
            }
        }
         
        //Méthode pour recuperer les ports

        public void SetData(int por, int baudRat, int timeoute)
        {
            port = por;
            baudRate = baudRat;
            timeout = timeoute;
        }

        //Méthode pour tester la configuration des ports

        public void Test_port()
        {
            Cursor.Current = Cursors.WaitCursor;
            comm = new GsmCommMain(port, baudRate, timeout);
            try
            {
                comm.Open();
                while (!comm.IsConnected())
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("La connexion au peripherique mobile a echoué\nREESAYER?", "TEST DE CONNEXION");

                    if (MessageBox.Show("La connexion au peripherique mobile a echoué\nREESAYER?", "TEST DE CONNEXION") != DialogResult.Cancel)
                    {
                        comm.Close();
                        return;
                    }
                    Cursor.Current = Cursors.WaitCursor;
                }
                MessageBox.Show("Successfully connected to the phone.", "Connection setup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection error: " + ex.Message, "Connection setup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        //Méthode pour envoyer un message
        public void Send(string message ,string number, string nothing)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                SmsSubmitPdu pdu;
                pdu = new SmsSubmitPdu(message, number, nothing);  // "" indicate SMSC No
                if (!comm.IsOpen())
                    comm.Open();
                comm.SendMessage(pdu);
                MessageBox.Show("message envoyé", "ENVOIE SIMPLE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comm.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("L'envoie a échoué", "ENVOIE SIMPLE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cursor.Current = Cursors.Default;
        }  

        public void Save(Cls_Message message)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using(IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "INSERT_MESSAGE_T";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@message", 255, DbType.String, message._Message));
                cmd.Parameters.Add(Parametres.Instance.AjouterParametre(cmd, "@ref_tier", 10, DbType.Int32, message.Reftier));
                cmd.ExecuteNonQuery();
   
            }
        }


        public void Get_Message(FlowLayoutPanel container, PictureBox img)
        {
            if (ImplementeConnexion.Instance.Conn.State == ConnectionState.Closed)
                ImplementeConnexion.Instance.Conn.Open();
            using (IDbCommand cmd = ImplementeConnexion.Instance.Conn.CreateCommand())
            {
                cmd.CommandText = "select top (3) * from Affichage_Message order by datemessage desc";
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //Creation des object
                    GroupBox groupe = new GroupBox();
                    PictureBox image = new PictureBox();
                    Label name = new Label();
                    Label message = new Label();
                    Label datemessage = new Label();

                    image.Image = img.Image;

                    //Localisation des objects
                    image.Location = new Point(5, 11);
                    name.Location= new Point(30, 11);
                    message.Location = new Point(2, 33);
                    datemessage.Location = new Point(250, 84);

                    //Taille des objets
                    image.Size = new Size(18, 19);
                    name.AutoSize = true;
                    message.Size = new Size(367, 45);
                    datemessage.Size = new Size(119, 13);

                    //Font
                    name.Font = new Font("Segoe UI Semibold", 10, FontStyle.Underline);

                    //Ajout des objets sur le panel
                    groupe.Controls.Add(image);
                    groupe.Controls.Add(name);
                    groupe.Controls.Add(message);
                    groupe.Controls.Add(datemessage);
                    groupe.Location.X.Equals(3);
                    groupe.Location.Y.Equals(3);

                    //Affectation des données de la Base aux objets
                    name.Text = dr["Tiers"].ToString();
                    message.Text = dr["message_env"].ToString();
                    datemessage.Text = dr["datemessage"].ToString();
                    groupe.Size = new Size(378, 105);
                    container.Controls.Add(groupe);
                }
            }
        }
    }
}
