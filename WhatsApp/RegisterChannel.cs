using System;
using System.Diagnostics;
using System.Windows.Forms;
using WhatsAppApi.Parser;
using WhatsAppApi.Register;

namespace WhatsApp
{
    public partial class RegisterChannel : Form
    {
        protected string identity = string.Empty;
        protected string number = string.Empty;
        protected string cc = string.Empty;
        protected string phone = string.Empty;
        protected string password = string.Empty;
        protected string code = string.Empty;
        protected bool raw = false;
        public string method = "sms";
        private bool debug = true;
       
        public RegisterChannel()
        {
            InitializeComponent();
        }
        private void btnCodeRequest_Click(object sender, EventArgs e)
        {
            if (this.parseNumber())
            {
                //try sms
                this.method = "sms";
                string resp1 = string.Empty;
                if (!this._requestCode(out resp1))
                {
                   
                        this.Notify(string.Format(@"Could not request code using either sms or voice.
SMS:    {0}", resp1));
                    
                }
            }
        }
        private bool parseNumber()
        {
            this.debug =true;
            if (!string.IsNullOrEmpty(this.txtPhoneNumber.Text))
            {
                try
                {
                    this.number = this.txtPhoneNumber.Text;
                    this.TrimNumber();
                    PhoneNumber phoneNumber = new PhoneNumber(this.number);
                    this.identity = WhatsRegisterV2.GenerateIdentity((string)phoneNumber.Number,"");
                    this.cc = (string)phoneNumber.CC;
                    this.phone = (string)phoneNumber.Number;
                    CountryHelper countryHelper = new CountryHelper();
                    string country = string.Empty;
                    if (countryHelper.CheckFormat(this.cc, this.phone, out country))
                        return true;
                    this.Notify(string.Format("Provided number does not match any known patterns for {0}", (object)country));
                    return false;
                }
                catch (Exception ex)
                {
                    this.Notify(string.Format("Error: {0}", (object)ex.Message));
                }
            }
            else
                this.Notify("Please enter a phone number");
            return false;
        }
        private bool _requestCode(out string response)
        {
            string str1 = (string)null;
            bool flag = WhatsRegisterV2.RequestCode(this.number, out this.password, out str1, out response, this.method, this.identity);
            MessageBox.Show(str1 + "!!!!" + response);
            textBox2.Text = str1;
            if (this.debug)
            {
                string format = "Code Request:\nToken = {0}\nIdentity = {1}\nUser Agent = {2}\nRequest = {3}\nResponse = {4}";
                object[] objArray = new object[5];
                int index1 = 0;
                string token = WhatsRegisterV2.GetToken(this.phone);
                objArray[index1] = (object)token;
                int index2 = 1;
                string str2 = this.identity;
                objArray[index2] = (object)str2;
                int index3 = 2;
                string str3 = "WhatsApp/2.12.81 S40Version/14.26 Device/Nokia302";
                objArray[index3] = (object)str3;
                int index4 = 3;
                string str4 = str1;
                objArray[index4] = (object)str4;
                int index5 = 4;
                string str5 = response;
                objArray[index5] = (object)str5;
                this.Notify(string.Format(format, objArray));
            }
            if (flag)
            {
                if (!string.IsNullOrEmpty(this.password))
                {
                    this.OnReceivePassword();
                }
                else
                {
                    this.grpStep1.Enabled = false;
                    this.grpStep2.Enabled = true;
                    this.Notify(string.Format("Code sent by {0} to {1}", (object)this.method, (object)this.number));
                }
            }
            return flag;
        }

        private void btnRegisterCode_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtCode.Text) || this.txtCode.Text.Length != 6)
                return;
            this.code = this.txtCode.Text;
            string str1 = string.Empty;
            this.password = WhatsRegisterV2.RegisterCode(this.number, this.code, out str1, this.identity);
            MessageBox.Show(str1);
            if (this.debug)
            {
                string format = "Code register:\nCode = {0}\nNumber = {1}\nIdentity = {2}\nResponse = {3}";
                object[] objArray = new object[4];
                int index1 = 0;
                string str2 = this.code;
                objArray[index1] = (object)str2;
                int index2 = 1;
                string str3 = this.number;
                objArray[index2] = (object)str3;
                int index3 = 2;
                string str4 = this.identity;
                objArray[index3] = (object)str4;
                int index4 = 3;
                string str5 = str1;
                objArray[index4] = (object)str5;
                this.Notify(string.Format(format, objArray));
            }
            if (!string.IsNullOrEmpty(this.password))
                this.OnReceivePassword();
            else
                this.Notify("Verification code not accepted");
        }

        private void Notify(string msg)
        {
            this.txtOutput.Text = msg;
            MessageBox.Show(msg);
        }

        private void OnReceivePassword()
        {
            this.Notify(String.Format("Got password:\r\n{0}\r\n\r\nWrite it down and exit the program", this.password));
            this.textBox1.Text = this.password;
            this.grpStep1.Enabled = false;
            this.grpStep2.Enabled = false;
            this.grpResult.Enabled = true;
        }  

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCode.Text.Length == 6)
            {
                this.btnRegisterCode.Enabled = true;
            }
            else
            {
                this.btnRegisterCode.Enabled = false;
            }
        }



        private void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.txtPhoneNumber.Text))
            {

                this.btnCodeRequest.Enabled = true;

            }
            else
            {

                this.btnCodeRequest.Enabled = false;

            }
        }

        protected void TrimNumber()
        {
            this.number = this.number.TrimStart(new char[] { '+', '0' });
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (DataMy dt = DataMy.GetInstanse())
            {
                dt.InsertChannels(txtPhoneNumber.Text.Trim(), textBox1.Text, "", 0, 0, LogicMy.dtToUnix(DateTime.Now), 0, 0, "", 1);
            }
            grpResult.Enabled = true;
            grpStep1.Enabled = true;
            grpStep2.Enabled = true;
            txtOutput.Text = String.Empty;
            txtCode.Text = String.Empty;

            txtPhoneNumber.Text = String.Empty;
            textBox1.Text = String.Empty;
            button3.Enabled = true;
            button4.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            using (DataMy dt = DataMy.GetInstanse())
            {
                dt.InsertChannels(txtPhoneNumber.Text.Trim(), textBox1.Text, "", 0, 0, LogicMy.dtToUnix(DateTime.Now), 0, 0, "", 1);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegisterChannel_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string m = WhatsAppApi.Settings.WhatsConstants.UserAgent;
            string mm = WhatsAppApi.Settings.WhatsConstants.WhatsAppVer;
            string mmm = WhatsAppApi.Register.WhatsRegisterV2.GetToken("79635255413");
            MessageBox.Show("UserAgent:" + m + "!!!!" + "WhatsAppVer:" + mm + "!!!!" + "Token" + mmm);
            //    textBox1.Text = mmm;
            //    //WhatsAppApi.WhatsApp.SYSEncoding = Encoding.UTF8;
            //    //WhatsAppApi.WhatsApp wa = new WhatsAppApi.WhatsApp("79635255413", "0k1qIbBvYS90q6s=", "ЦЦ");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.StartupPath + @"\Resources\WART-1.8.2.0.exe");
            Process.Start(Application.StartupPath+@"\Resources\WART-1.8.2.0.exe");

            //Assembly.Load((byte[])new ResourceManager(typeof(Properties.Resources)).GetObject("WART-1.8.2.0.exe")).EntryPoint.Invoke(null, null);
        }
    }//Конец класса RegisterChannel
}
