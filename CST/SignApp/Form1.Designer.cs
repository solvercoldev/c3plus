namespace SignApp
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtClientCompany = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMacAddress = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.lblMacDecrypt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblClientDecrypt = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Generar MAC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtClientCompany
            // 
            this.txtClientCompany.Location = new System.Drawing.Point(108, 51);
            this.txtClientCompany.Name = "txtClientCompany";
            this.txtClientCompany.Size = new System.Drawing.Size(260, 20);
            this.txtClientCompany.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(108, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Encriptar y Generar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Empresa Cliente:";
            // 
            // lblMacAddress
            // 
            this.lblMacAddress.AutoSize = true;
            this.lblMacAddress.Location = new System.Drawing.Point(105, 82);
            this.lblMacAddress.Name = "lblMacAddress";
            this.lblMacAddress.Size = new System.Drawing.Size(67, 13);
            this.lblMacAddress.TabIndex = 11;
            this.lblMacAddress.Text = "mac address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "MAC Address:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(236, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Check DeCrypt";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblMacDecrypt
            // 
            this.lblMacDecrypt.AutoSize = true;
            this.lblMacDecrypt.Location = new System.Drawing.Point(105, 116);
            this.lblMacDecrypt.Name = "lblMacDecrypt";
            this.lblMacDecrypt.Size = new System.Drawing.Size(67, 13);
            this.lblMacDecrypt.TabIndex = 14;
            this.lblMacDecrypt.Text = "mac address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "MAC Decrypt:";
            // 
            // lblClientDecrypt
            // 
            this.lblClientDecrypt.AutoSize = true;
            this.lblClientDecrypt.Location = new System.Drawing.Point(105, 144);
            this.lblClientDecrypt.Name = "lblClientDecrypt";
            this.lblClientDecrypt.Size = new System.Drawing.Size(32, 13);
            this.lblClientDecrypt.TabIndex = 16;
            this.lblClientDecrypt.Text = "client";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Client Decrypt:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 292);
            this.Controls.Add(this.lblClientDecrypt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMacDecrypt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lblMacAddress);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtClientCompany);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "SignApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtClientCompany;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMacAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label lblMacDecrypt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClientDecrypt;
        private System.Windows.Forms.Label label5;
    }
}

