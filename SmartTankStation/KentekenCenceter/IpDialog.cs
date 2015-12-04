using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarCenter
{
    public partial class IpDialog : Form
    {
        public IpDialog()
        {
            InitializeComponent();
            CenterToScreen();
            AcceptButton = btnOk;
            KeyPreview = true;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            bool checkbool = true;
            string checkstring = tbIP.Text;
            string[] check = checkstring.Split('.');
            foreach (string stringitem in check)
            {
                if (stringitem.Length <= 0)
                {
                    checkbool = false;
                    break;
                }
            }
            if (check.Count() == 4 && checkbool)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Voer een geldig IP adress in", "Warning");
            }        
        }

        private void IpDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void IpDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                System.Environment.Exit(0);
            }
        }
    }
}
