﻿using GsmComm.GsmCommunication;
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
    class Cls_Message
    {
        public static int port, baudRate, timeout;
        public GsmCommMain comm;
        public static Cls_Message cMessage = null;

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
    }
}
