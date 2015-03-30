using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;

namespace SignApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ClientCompany = string.Empty;
            MacAddress = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MacAddress = GetMacAddress();
        }

        /// <summary>
        /// Finds the MAC address of the NIC with maximum speed.
        /// </summary>
        /// <returns>The MAC address.</returns>
        static string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                Console.WriteLine("Found MAC Address: " + nic.GetPhysicalAddress() + " Type: " + nic.NetworkInterfaceType);

                string tempMac = nic.GetPhysicalAddress().ToString();

                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    Console.WriteLine("New Max Speed = " + nic.Speed + ", MAC: " + tempMac);
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            return macAddress;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var mac = GetMacAddress();
                
                using (StreamWriter sw = new StreamWriter(sfd.FileName))
                {
                    sw.WriteLine(StringEncryption.EncryptString(ClientCompany, mac));
                    sw.WriteLine(StringEncryption.EncryptString(mac, mac));
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            var mac = GetMacAddress();
            MacAddress = mac;
            var clientCrypt = string.Empty;
            var macCrypt = string.Empty;

            OpenFileDialog sfd = new OpenFileDialog();

            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sw = new StreamReader(sfd.FileName))
                {
                    clientCrypt = sw.ReadLine();
                    macCrypt = sw.ReadLine();
                }
            }

            ClientDecrypt = StringEncryption.DecryptString(clientCrypt, mac);
            MacDecrypt = StringEncryption.DecryptString(macCrypt, mac);
        }

        string MacAddress
        {
            get { return lblMacAddress.Text; }
            set { lblMacAddress.Text = value; }
        }

        string ClientCompany
        {
            get { return txtClientCompany.Text; }
            set { txtClientCompany.Text = value; }
        }

        string MacDecrypt
        {
            get { return lblMacDecrypt.Text; }
            set { lblMacDecrypt.Text = value; }
        }

        string ClientDecrypt
        {
            get { return lblClientDecrypt.Text; }
            set { lblClientDecrypt.Text = value; }
        }
    }
}
