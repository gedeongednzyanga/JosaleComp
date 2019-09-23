using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JosaleApp.Classes
{
    public class Cls_Message
    {
        private static int port, baudRate, timeout;
        private GsmCommMain comm;
       
        public string GetAllPorts(ComboBox port)
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

        public void SetData(int por, int baudRat, int timeoute)
        {
            port = por;
            baudRate = baudRat;
            timeout = timeoute;
        }

        public bool EnterNewSettings(string port1, string port2, string port3)
        {
            int newPort;
            int newBaudRate;
            int newTimeout;

            try
            {
                newPort = int.Parse(port1);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //comboBox1.Focus();
                return false;
            }

            try
            {
                newBaudRate = int.Parse(port2);
            }
            catch (Exception)
            {

                MessageBox.Show("Invalid baud rate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               // comboBox2.Focus();
                return false;
            }

            try
            {
                newTimeout = int.Parse(port3);
            }
            catch (Exception)
            {

                MessageBox.Show("Invalid timeout value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //comboBox3.Focus();
                return false;
            }

            SetData(newPort, newBaudRate, newTimeout);

            return true;
        }

        public void Send(string number, string message)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {

                SmsSubmitPdu pdu;
                pdu = new SmsSubmitPdu(message, number, "");  // "" indicate SMSC No

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

        private void Test_port()
        {
            if (!EnterNewSettings("", "", ""))
                return;

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
                //x.conf(Int32.Parse(comboBox1.Text), Int32.Parse(comboBox2.Text), Int32.Parse(comboBox3.Text));
                comm.Close();
            }
            catch (Exception ex)
            {
                //Output("ERREUR : " + ex.Message);
                //Output("");

                MessageBox.Show("Connection error: " + ex.Message, "Connection setup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
