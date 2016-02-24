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
    public partial class AddGroups : Form
    {
        public AddGroups()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (DataMy dt = DataMy.GetInstanse())
            {
                dt.InsertGrops(textBox1.Text,textBox2.Text);
                DialogResult=DialogResult.OK;
            }
        }
    }
}
