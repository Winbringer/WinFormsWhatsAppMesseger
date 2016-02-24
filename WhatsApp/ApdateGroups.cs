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
    public partial class ApdateGroups : Form
    {
        private int id;
        public ApdateGroups()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Конструктор формы для обновления групп.
        /// </summary>
        /// <param name="id">Айди группы</param>
        /// <param name="n">Имя группы</param>
        /// <param name="k">Коментарий группы</param>
        public ApdateGroups(int id,string n,string k)
        {
            InitializeComponent();
            this.id = id;
            textBox1.Text = n;
            textBox2.Text = k;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (DataMy dt = DataMy.GetInstanse())
            {
                dt.UpdateGrops(this.id,textBox1.Text,textBox2.Text);
                DialogResult=DialogResult.OK;
            }
        }
    }
}
