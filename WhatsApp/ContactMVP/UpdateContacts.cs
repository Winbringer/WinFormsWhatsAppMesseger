using System;
using System.Windows.Forms;

namespace WhatsApp.ContactMVP
{
    public partial class UpdateContacts : Form
    {
        private int id;
        public UpdateContacts()
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

        public UpdateContacts(int id,AddContakt.MyStruct m)
        {
            InitializeComponent();
            this.id = id;
            using (DataMy dt = DataMy.GetInstanse())
            {
                this.comboBox1.DataSource = dt.GetTable("group_contacts");
                comboBox1.DisplayMember = "name_group";
                comboBox1.ValueMember = "id_group";
            }
            comboBox1.SelectedValue = m.id_group;
            dateTimePicker1.Value = LogicMy.unixToDT(m.Date_reg);
            textBox1.Text = m.Name;
            textBox2.Text = m.Familiya;
            dateTimePicker2.Value = LogicMy.unixToDT(m.Date_of_birth);
            textBox3.Text = m.Phone;
            textBox4.Text = m.Email;
            comboBox2.Text = m.Sex;
            comboBox3.Text = m.Is_whats_app.ToString();
            comboBox4.Text = m.Subscrible.ToString();
            textBox5.Text = m.coment;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddContakt.MyStruct m = new AddContakt.MyStruct();
            m.id_group = Convert.ToInt32(comboBox1.SelectedValue);
            m.Date_reg = LogicMy.dtToUnix(dateTimePicker1.Value);
            m.Name = textBox1.Text;
            m.Familiya = textBox2.Text;
            m.Date_of_birth = LogicMy.dtToUnix(dateTimePicker2.Value);
            m.Phone = textBox3.Text;
            m.Email = textBox4.Text;
            m.Sex = comboBox2.Text;
            m.Is_whats_app = comboBox3.Text == "Yes" ? AddContakt.MyEnum.Yes : AddContakt.MyEnum.No;
            m.Subscrible = comboBox4.Text == "Yes" ? AddContakt.MyEnum.Yes : AddContakt.MyEnum.No;
            m.coment = textBox5.Text;
            using (DataMy dt = DataMy.GetInstanse())
            {
                dt.UpdateContacts(id,m.id_group, m.Date_reg, m.Familiya, m.Name, m.Date_of_birth, m.Phone, m.Email, m.Sex, (int)m.Is_whats_app, (int)m.Subscrible, m.coment);
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
