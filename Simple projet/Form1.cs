using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS3Lib; 
using System.Threading;
using Simple_projet.Properties;
using System.IO;

namespace Simple_projet
{
    public partial class Form1 : Form
    {
        static PS3API PS3 = new PS3API();
        private List<CCAPI.ConsoleInfo> Consoles;
        private void UpdateConsoles()
        {
            Consoles = PS3.CCAPI.GetConsoleList();
            foreach (CCAPI.ConsoleInfo Console in Consoles)
                targetList.Items.Add(String.Format("{0} : {1}", Console.Name, Console.Ip));
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PS3.ChangeAPI(SelectAPI.ControlConsole);
            targetList.Items.Clear();
            if (targetList.Items.Count > 0)
                targetList.SelectedIndex = 0;
            targetList.Visible = true;
            UpdateConsoles();
        }
        static string teamstring = "\n\n\nCCAPI By H7K3R Team";
        private void cButton1_Click(object sender, EventArgs e)
        {
            CCAPI.ConsoleInfo Console = Consoles.ElementAt(targetList.SelectedIndex);
            try
            {
                if (PS3.ConnectTarget(Console.Ip))
                {
                    label5.Text = PS3.CCAPI.GetFirmwareVersion();
                    label6.Text = PS3.CCAPI.GetFirmwareType();
                    label9.Text = PS3.CCAPI.GetTemperatureCELL() + " °";
                    label4.Text = PS3.CCAPI.GetTemperatureRSX() + " °";
                    MessageBox.Show("Votre PS3 est bien connectée !" + teamstring, "Connectée", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //PS3.CCAPI.RingBuzzer(CCAPI.BuzzerMode.Single);
                    PS3.CCAPI.Notify(CCAPI.NotifyIcon.INFO, "Connectee !" + teamstring);
                }
                else
                {
                    MessageBox.Show("Une erreur est survenue", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Une erreur est survenue", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cButton2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Votre PS3 est bien déconnectée !" + teamstring, "Déconnectée", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PS3.CCAPI.DisconnectTarget();
        }

        private void cButton3_Click(object sender, EventArgs e)
        {
            label5.Text = PS3.CCAPI.GetFirmwareVersion();
            label6.Text = PS3.CCAPI.GetFirmwareType();
            label9.Text = PS3.CCAPI.GetTemperatureCELL() + " °";
            label4.Text = PS3.CCAPI.GetTemperatureRSX() + " °";
        }

        private void cCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cCheckBox1.Checked)
            {
                cCheckBox2.Checked = false;
            }
        }

        private void cCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (cCheckBox2.Checked)
            {
                cCheckBox1.Checked = false;
            }
        }

        private void cCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (cCheckBox4.Checked)
            {
                cCheckBox3.Checked = false;
                cCheckBox5.Checked = false;
            }
        }

        private void cCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (cCheckBox3.Checked)
            {
                cCheckBox4.Checked = false;
                cCheckBox5.Checked = false;
            }
        }

        private void cCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (cCheckBox5.Checked)
            {
                cCheckBox3.Checked = false;
                cCheckBox4.Checked = false;
            }
        }

        private void cButton4_Click(object sender, EventArgs e)
        {
                CCAPI.LedColor Color = CCAPI.LedColor.Green;
                CCAPI.LedMode Mode = CCAPI.LedMode.On;

                if (cCheckBox4.Checked)
                    Mode = CCAPI.LedMode.On;
                else if (cCheckBox3.Checked)
                    Mode = CCAPI.LedMode.Off;
                else if (cCheckBox5.Checked)
                    Mode = CCAPI.LedMode.Blink;

                if (cCheckBox1.Checked)
                    Color = CCAPI.LedColor.Green;

                if (cCheckBox2.Checked)
                    Color = CCAPI.LedColor.Red;

                PS3.CCAPI.SetConsoleLed(Color, Mode);
        }

        private void cButton5_Click(object sender, EventArgs e)
        {
            if (comboBuzzer.SelectedIndex >= 0)
            {
                if (comboBuzzer.SelectedIndex == 0)
                    PS3.CCAPI.RingBuzzer(CCAPI.BuzzerMode.Continuous);
                else if (comboBuzzer.SelectedIndex == 1)
                    PS3.CCAPI.RingBuzzer(CCAPI.BuzzerMode.Single);
                else if (comboBuzzer.SelectedIndex == 2)
                    PS3.CCAPI.RingBuzzer(CCAPI.BuzzerMode.Double);
                else if (comboBuzzer.SelectedIndex == 2)
                    PS3.CCAPI.RingBuzzer(CCAPI.BuzzerMode.Triple);
            }
            else MessageBox.Show("S'il vous plait, séléctionnez un buzzer !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cButton6_Click(object sender, EventArgs e)
        {
            if (boosterComboBox1.SelectedIndex == 0)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.ARROW, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 1)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.ARROWRIGHT, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 2)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.CAUTION, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 3)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.DIALOG, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 4)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.DIALOGSHADOW, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 5)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.FINGER, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 6)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.FRIEND, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 7)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.GRAB, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 8)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.HAND, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 9)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.INFO, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 10)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.PEN, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 11)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.POINTER, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 12)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.PROGRESS, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 13)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.SLIDER, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 14)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.TEXT, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 15)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.TROPHY1, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 16)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.TROPHY2, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 17)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.TROPHY3, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 18)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.TROPHY4, cTextBox1.Text);
            else if (boosterComboBox1.SelectedIndex == 19)
                PS3.CCAPI.Notify(CCAPI.NotifyIcon.WRONGWAY, cTextBox1.Text);
        }

        private void cButton7_Click(object sender, EventArgs e)
        {
            PS3.CCAPI.ResetBootConsoleID();
        }

        private void cButton8_Click(object sender, EventArgs e)
        {
            if (boot.Checked)
            {
                PS3.CCAPI.SetBootConsoleID(cTextBox2.Text);
            }
            if (now.Checked)
            {
                PS3.CCAPI.SetConsoleID(cTextBox2.Text);
            }
        }

        private void boot_CheckedChanged(object sender, EventArgs e)
        {
            if (boot.Checked)
            {
                now.Checked = false;
            }
        }

        private void now_CheckedChanged(object sender, EventArgs e)
        {
            if (now.Checked)
            {
                boot.Checked = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/groups/h7k3r.officiel/");
        }
    }
}
