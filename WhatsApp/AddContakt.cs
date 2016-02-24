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
    public partial class AddContakt : Form
    {
        public AddContakt()
        {
            InitializeComponent();
            using (DataMy dt = DataMy.GetInstanse())
            {
                this.comboBox1.DataSource = dt.GetTable("group_contacts");
                comboBox1.DisplayMember = "name_group";
                comboBox1.ValueMember = "id_group";
            }
            this.comboBox2.Text = "Неопределен";
            this.comboBox3.Text = "Yes";
            this.comboBox4.Text = "Yes";
        }
        /// <summary>
        /// Структура для хранения данных из формы для добавления контактов.
        /// </summary>
      public  struct MyStruct
        {
            public int id_group;
            public int Date_reg;
            public string Familiya;
            public string Name;
            public int Date_of_birth;
            public string Phone;
            public string Email;
            public string Sex;
            public MyEnum Is_whats_app;
            public MyEnum Subscrible;
            public string coment;
        }
        /// <summary>
        /// Перечисление для хранения варианта ответов.
        /// </summary>
      public  enum MyEnum
        {
            Yes=1,
            No=0
        }

        private void button1_Click(object sender, EventArgs e)
        {
           MyStruct m = new MyStruct();
            m.id_group = Convert.ToInt32(comboBox1.SelectedValue);
            m.Date_reg = LogicMy.dtToUnix(dateTimePicker1.Value);
           m.Name = textBox1.Text;
            m.Familiya   = textBox2.Text;
            m.Date_of_birth = LogicMy.dtToUnix(dateTimePicker2.Value);
            m.Phone = textBox3.Text;
            m.Email = textBox4.Text;
            m.Sex = comboBox2.Text;
            m.Is_whats_app = comboBox3.Text == "Yes" ? MyEnum.Yes : MyEnum.No;
            m.Subscrible = comboBox4.Text == "Yes" ? MyEnum.Yes : MyEnum.No;
            m.coment = textBox5.Text;
            using (DataMy dt = DataMy.GetInstanse())
            {
                dt.InsertContacts(m.id_group,m.Date_reg,m.Familiya,m.Name,m.Date_of_birth,m.Phone,m.Email,m.Sex,(int)m.Is_whats_app,(int)m.Subscrible,m.coment);
            }
            this.DialogResult=DialogResult.OK;
        }
    }
}
