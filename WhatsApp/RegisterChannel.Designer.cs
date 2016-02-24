namespace WhatsApp
{
    partial class RegisterChannel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnCodeRequest = new System.Windows.Forms.Button();
            this.btnRegisterCode = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.grpStep1 = new System.Windows.Forms.GroupBox();
            this.grpStep2 = new System.Windows.Forms.GroupBox();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.grpStep1.SuspendLayout();
            this.grpStep2.SuspendLayout();
            this.grpResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Телефон:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Щестизначный SMS код :\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Пароль:";
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(23, 38);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(158, 20);
            this.txtPhoneNumber.TabIndex = 3;
            this.txtPhoneNumber.TextChanged += new System.EventHandler(this.txtPhoneNumber_TextChanged);
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(23, 42);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(158, 20);
            this.txtCode.TabIndex = 4;
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(23, 32);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(158, 97);
            this.txtOutput.TabIndex = 5;
            this.txtOutput.TextChanged += new System.EventHandler(this.txtOutput_TextChanged);
            // 
            // btnCodeRequest
            // 
            this.btnCodeRequest.Location = new System.Drawing.Point(23, 71);
            this.btnCodeRequest.Name = "btnCodeRequest";
            this.btnCodeRequest.Size = new System.Drawing.Size(158, 23);
            this.btnCodeRequest.TabIndex = 6;
            this.btnCodeRequest.Text = "Получить SMS код";
            this.btnCodeRequest.UseVisualStyleBackColor = true;
            this.btnCodeRequest.Click += new System.EventHandler(this.btnCodeRequest_Click);
            // 
            // btnRegisterCode
            // 
            this.btnRegisterCode.Location = new System.Drawing.Point(23, 71);
            this.btnRegisterCode.Name = "btnRegisterCode";
            this.btnRegisterCode.Size = new System.Drawing.Size(158, 23);
            this.btnRegisterCode.TabIndex = 7;
            this.btnRegisterCode.Text = "Подтвердить SMS код";
            this.btnRegisterCode.UseVisualStyleBackColor = true;
            this.btnRegisterCode.Click += new System.EventHandler(this.btnRegisterCode_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(247, 176);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(158, 47);
            this.button3.TabIndex = 8;
            this.button3.Text = "Сохранить и продолжить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(247, 229);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(158, 40);
            this.button4.TabIndex = 9;
            this.button4.Text = "Сохранить и выйти";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // grpStep1
            // 
            this.grpStep1.Controls.Add(this.txtPhoneNumber);
            this.grpStep1.Controls.Add(this.label1);
            this.grpStep1.Controls.Add(this.btnCodeRequest);
            this.grpStep1.Location = new System.Drawing.Point(12, 12);
            this.grpStep1.Name = "grpStep1";
            this.grpStep1.Size = new System.Drawing.Size(200, 100);
            this.grpStep1.TabIndex = 10;
            this.grpStep1.TabStop = false;
            this.grpStep1.Text = "Шаг 1:";
            // 
            // grpStep2
            // 
            this.grpStep2.Controls.Add(this.label2);
            this.grpStep2.Controls.Add(this.btnRegisterCode);
            this.grpStep2.Controls.Add(this.txtCode);
            this.grpStep2.Location = new System.Drawing.Point(224, 12);
            this.grpStep2.Name = "grpStep2";
            this.grpStep2.Size = new System.Drawing.Size(200, 100);
            this.grpStep2.TabIndex = 11;
            this.grpStep2.TabStop = false;
            this.grpStep2.Text = "Шаг 2:";
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.label3);
            this.grpResult.Controls.Add(this.txtOutput);
            this.grpResult.Location = new System.Drawing.Point(12, 118);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(200, 162);
            this.grpResult.TabIndex = 12;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "Результат:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(224, 150);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(200, 20);
            this.textBox1.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 360);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(118, 360);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(199, 360);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 16;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 330);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(277, 20);
            this.textBox2.TabIndex = 17;
            this.textBox2.Visible = false;
            // 
            // RegisterChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 291);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.grpStep2);
            this.Controls.Add(this.grpStep1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegisterChannel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegisterChannel";
            this.Load += new System.EventHandler(this.RegisterChannel_Load);
            this.grpStep1.ResumeLayout(false);
            this.grpStep1.PerformLayout();
            this.grpStep2.ResumeLayout(false);
            this.grpStep2.PerformLayout();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnCodeRequest;
        private System.Windows.Forms.Button btnRegisterCode;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox grpStep1;
        private System.Windows.Forms.GroupBox grpStep2;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox2;
    }
}