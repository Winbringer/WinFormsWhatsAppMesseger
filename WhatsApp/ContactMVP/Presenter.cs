using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsApp.ContactMVP
{
    class Presenter
    {
        public Presenter(AddContakt ac)
        {
            this.ac = ac;
        }

        private AddContakt ac;
        struct MyStruct
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

        enum MyEnum
        {
            Yes = 1,
            No = 0
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyStruct m = new MyStruct();
            ac.DialogResult = DialogResult.OK;
        }
    }
}
