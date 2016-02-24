using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhatsApp.ContactMVP;
using WhatsAppApi;

namespace WhatsApp
{
    public partial class Form1 : Form
    {
        bool direction = false;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
#region Работа с контроллами. На всякий случай
            //Поиск нужных контроллов по имени и изменение их свойств
            //for (int i = 7; i <= 12; i++)
            //{
            //    foreach (Control c in this.Controls.Find("button" + i.ToString(), true))
            //    {
            //        c.Enabled = false;
            //        c.Visible = false;
            //    }
            //}
            //foreach (TextBox tb in Controls.Cast<Control>().Where(x => x is TextBox).Select(x => x as TextBox))
            //{

            //}
#endregion
            this.UpdateGrid();
            this.UpdateContactsGrid();
            using (DataMy dt = DataMy.GetInstanse())
            {
                this.comboBox1.DataSource = dt.GetTable("group_contacts");
                comboBox1.DisplayMember = "name_group";
                comboBox1.ValueMember = "id_group";
            }

          
               
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogicMy.DelFromGridInDb(dataGridView1, "Cannels");
            UpdateGrid();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            using (LoadingInFromFile ld = LoadingInFromFile.Instance)
            {
                ld.SaveChannelsToFile();
            }
            #region  Создание дт
            //using (DataMy dt = new DataMy())
            //{
            //    using (DataTable dtt = dt.GetTable("group_contacts"))
            //    {
            //        dtt.Columns[0].Unique = true;
            //        dtt.Columns[0].AutoIncrement = true;
            //        dtt.Columns[0].AutoIncrementSeed = (long)dtt.Rows[(dtt.Rows.Count - 1)][0] + 1;
            //        dtt.Columns[0].AutoIncrementStep = 1;
            //        DataRow dr = dtt.NewRow();
            //        dr[0] = 11;
            //        dr[1] = "fffffv";
            //        dr[2] = "fffffv";
            //        DataRow dr1 = dtt.NewRow();
            //        dr1[1] = "fffff";
            //        dr1[2] = "fffff";
            //       // dtt.Rows.Add(dr);
            //       // dtt.Rows.Add(dr1);
            //       // dtt.Rows.RemoveAt(1);
            //      // dtt.Rows.RemoveAt(0);
            //        dtt.Rows.Remove(dtt.Rows[0]);
            //        //DataMy.LoadGroupsToDT(dtt);
            //        dataGridView3.DataSource = dtt;//dt.GetTable("group_contacts");
            //    }
            //}
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (LoadingInFromFile ld = LoadingInFromFile.Instance)
            {
                ld.LoadChannelsFromFile();
                UpdateGrid();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (AddContakt aC = new AddContakt())
            {
                if (aC.ShowDialog() == DialogResult.OK) this.UpdateContactsGrid();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SaveLoadFile sf = new SaveLoadFile())
            {
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    UpdateGrid();
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (ActivateDeAChannel sf = new ActivateDeAChannel())
            {
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    UpdateGrid();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LogicMy.DelFromGridInDb(dataGridView2, "Contacts");
            UpdateContactsGrid();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AddContakt.MyStruct m = new AddContakt.MyStruct();
            if (dataGridView2.CurrentRow != null)
            {
                DataTable dtm = null;
                using (DataMy dt = DataMy.GetInstanse())
                {
                    dtm = dt.GetTable("group_contacts");

                }
             int idContact = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
             var s = dtm.AsEnumerable();
             var mm = from ss in s
                     where ss["name_group"].ToString() == dataGridView2.CurrentRow.Cells[1].Value.ToString()
                     select ss["id_group"];
             var k = mm.FirstOrDefault();
                if (k == null) k = "0";
             m.id_group = Convert.ToInt32(k.ToString());
             m.Date_reg = LogicMy.dtToUnix(Convert.ToDateTime(dataGridView2.CurrentRow.Cells[2].Value.ToString()));
             m.Name = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            m.Familiya = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            m.Date_of_birth = LogicMy.dtToUnix(Convert.ToDateTime(dataGridView2.CurrentRow.Cells[5].Value.ToString()));
            m.Phone = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            m.Email = dataGridView2.CurrentRow.Cells[7].Value.ToString();
            m.Sex = dataGridView2.CurrentRow.Cells[8].Value.ToString();
            m.Is_whats_app = dataGridView2.CurrentRow.Cells[9].Value.ToString().Trim()== "1" ? AddContakt.MyEnum.Yes : AddContakt.MyEnum.No;
            m.Subscrible = dataGridView2.CurrentRow.Cells[10].Value.ToString().Trim() == "1" ? AddContakt.MyEnum.Yes : AddContakt.MyEnum.No;
            m.coment = dataGridView2.CurrentRow.Cells[11].Value.ToString().Trim();
            using (UpdateContacts uc = new UpdateContacts(idContact,m))
            {
                if (uc.ShowDialog() == DialogResult.OK)
                {
                    this.UpdateContactsGrid();
                }
            }
        }
            else
            {
                MessageBox.Show("Выберите строку для редактирования!");
            }
    }

        private void button12_Click(object sender, EventArgs e)
        {
            //using (DataMy dm = DataMy.GetInstanse())
            //{
            //    dm.InsertGrops("ddd", "ddt");
            //    dataGridView3.DataSource = dm.GetTable("group_contacts");
            //    this.group_contactsTableAdapter.Fill(this.sQLiteBDDataSet.group_contacts);
            //}
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //LogicMy.DelFromGridInDb(dataGridView3, "group_contacts");
            //this.group_contactsTableAdapter.Fill(this.sQLiteBDDataSet.group_contacts);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //using (DataMy dm = DataMy.GetInstanse())
            //{
            //    dm.UpdateGrops(2, "vvv", "ddt");
            //    dataGridView3.DataSource = dm.GetTable("group_contacts");
            //    this.group_contactsTableAdapter.Fill(this.sQLiteBDDataSet.group_contacts);
            //}
        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.Columns[0].HeaderCell.SortGlyphDirection = SortOrder.None;
            if (e.ColumnIndex == 0)
            {
                LogicMy.SortChannelsIdColumn(dataGridView1, direction);
                direction = !direction;
            } //Иф нужный столбец.       
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var h = dataGridView1.HitTest(e.X, e.Y);
                if (h.Type == DataGridViewHitTestType.Cell)
                {

                    dataGridView1.Rows[h.RowIndex].Selected = true;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                int i = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Айди"].Value);

                if (((CheckState)dataGridView1.Rows[e.RowIndex].Cells["Активный"].Value) == CheckState.Checked)
                {
                    using (DataMy dt = DataMy.GetInstanse())
                    {
                        dt.UpdateIsActive(i, 0);
                    }
                    UpdateGrid();
                }
                else if (((CheckState)dataGridView1.Rows[e.RowIndex].Cells["Активный"].Value) == CheckState.Unchecked)
                {
                    using (DataMy dt = DataMy.GetInstanse())
                    {
                        dt.UpdateIsActive(i, 1);
                    }
                    UpdateGrid();
                }
            }

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            using (RegisterChannel sf = new RegisterChannel())
            {
                if (sf.ShowDialog() == DialogResult.OK)
                { }
                UpdateGrid();

            }
        }
        int k = 0;
        private void button13_Click(object sender, EventArgs e)
        {
            LogicMy.CheckChannels(progressBar1,dataGridView1);
            UpdateGrid();
           progressBar1.Visible = false;
        }

        private void UpdateGrid()
        {
            using (DataMy dt = DataMy.GetInstanse())
            using (ViewMyDataTableToGridView vm = new ViewMyDataTableToGridView(dt.GetTable("Cannels"), dataGridView1))
                vm.ChannelsGridFiller(label1, label2, label3, checkBox1);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            using (Grupps sf = new Grupps())
            {
                if (sf.ShowDialog() == DialogResult.OK)
                { }
                UpdateGrid();
                using (DataMy dt = DataMy.GetInstanse())
                {
                    this.comboBox1.DataSource = dt.GetTable("group_contacts");
                    comboBox1.DisplayMember = "name_group";
                    comboBox1.ValueMember = "id_group";
                }
            }
        }

       
        /// <summary>
        /// Метод для фильтрации контактов по группам.
        /// </summary>
        void UpdateContactsGrid()
        {
            using (DataMy dt = DataMy.GetInstanse())
            {
                using (DataTable dtt = dt.GetTable("Contacts"))
                {
                    using (ViewMyDataTableToGridView vm = new ViewMyDataTableToGridView(dtt, dataGridView2))
                    {
                        vm.ContactsGridFiller(checkBox2, comboBox1);
                    }
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.UpdateContactsGrid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateContactsGrid();
        }

        private void логинToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            using (DataMy dm =DataMy.GetInstanse())
            {
                using (DataTable dtt = dm.GetTable("Cannels"))
                {
                    DataRow dr = dtt.Select("id_cannels=" + dataGridView1.CurrentRow.Cells[0].Value.ToString()).FirstOrDefault();
                    LogicMy.ChekWALoginMy(dr, dm);
                   UpdateGrid();
                }
            }
            
        }

    }//Класс Форм1
}

