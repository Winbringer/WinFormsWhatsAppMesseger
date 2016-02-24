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
    public partial class Grupps : Form
    {
        public Grupps()
        {
            InitializeComponent();
            UpdateGridGr();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Обработчик кнопки добавить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click_1(object sender, EventArgs e)
        {
            using (AddGroups ag = new AddGroups())
            {
                if (ag.ShowDialog() == DialogResult.OK)
                {
                    UpdateGridGr();
                }
            }
        }
        /// <summary>
        /// Обработчик кнопки удалить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click_1(object sender, EventArgs e)
        {

            LogicMy.DelFromGridInDb(dataGridView2, "group_contacts");
            UpdateGridGr();
        }
        /// <summary>
        /// Метод для обновления таблицы и группами.
        /// </summary>
        private void UpdateGridGr()
        {
            using (DataMy dm = DataMy.GetInstanse())
            {
                using (
                    ViewMyDataTableToGridView vm = new ViewMyDataTableToGridView(dm.GetTable("group_contacts"),
                        dataGridView2))
                {
                    vm.GroupsGridFiller();
                }
            }
        }
        /// <summary>
        /// Обработчик кнопки обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click_1(object sender, EventArgs e)
        {
            using (DataMy dt = DataMy.GetInstanse())
            {
                if (dataGridView2.CurrentRow != null)
                {
                    int id =Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
                    string nm=dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    string kom=dataGridView2.CurrentRow.Cells[2].Value.ToString();
                    using (ApdateGroups ap = new ApdateGroups(id,nm,kom))
                    {
                        if (ap.ShowDialog() == DialogResult.OK)
                        {
                            UpdateGridGr();
                        }
                    }
                }
            }
        }
    }
}
