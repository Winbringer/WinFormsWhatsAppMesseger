using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsApp
{
    public partial class ActivateDeAChannel : Form
    {
        public ActivateDeAChannel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = (int)numericUpDown1.Value;
            int b = (int)numericUpDown2.Value;
            if (radioButton1.Checked)
            {
                using (DataMy dt = DataMy.GetInstanse())
                {
                    for (int i = a; i <= b; ++i)
                    {
                        dt.UpdateIsActive(i, 1);
                    }
                }
            }
            if (radioButton2.Checked)
            {
                using (DataMy dt = DataMy.GetInstanse())
                {
                    for (int i = a; i <= b; ++i)
                    {
                        dt.UpdateIsActive(i, 0);
                    }
                }
            }
            this.DialogResult = DialogResult.OK; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;           
        }
    }
}
