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
    public partial class SaveLoadFile : Form
    {
        public SaveLoadFile()
        {
            InitializeComponent();
            checkBox1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(LogicMy.AddChannel(textBox3,maskedTextBox2,maskedTextBox3,dateTimePicker1,
               dateTimePicker2,dateTimePicker3,checkBox1,textBox1,textBox2,textBox5))           
            this.DialogResult = DialogResult.OK;
        }      

    }
}
