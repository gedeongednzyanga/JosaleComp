namespace JosaleApp.Forms
{
    partial class Update_rembou_credit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Update_rembou_credit));
            this.label1 = new System.Windows.Forms.Label();
            this.lab_name = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lab_dete = new System.Windows.Forms.Label();
            this.lab_reste = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.text_mount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 38;
            this.label1.Text = "Customer :";
            // 
            // lab_name
            // 
            this.lab_name.AutoSize = true;
            this.lab_name.BackColor = System.Drawing.Color.Transparent;
            this.lab_name.Font = new System.Drawing.Font("Segoe UI", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.lab_name.Location = new System.Drawing.Point(96, 5);
            this.lab_name.Name = "lab_name";
            this.lab_name.Size = new System.Drawing.Size(58, 20);
            this.lab_name.TabIndex = 36;
            this.lab_name.Text = "Names";
            this.lab_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lab_dete);
            this.groupBox4.Controls.Add(this.lab_reste);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnSave);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.text_mount);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(9, 31);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(380, 158);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            // 
            // lab_dete
            // 
            this.lab_dete.BackColor = System.Drawing.Color.White;
            this.lab_dete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_dete.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lab_dete.Location = new System.Drawing.Point(91, 21);
            this.lab_dete.Name = "lab_dete";
            this.lab_dete.Size = new System.Drawing.Size(273, 25);
            this.lab_dete.TabIndex = 9;
            this.lab_dete.Text = "0";
            this.lab_dete.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lab_reste
            // 
            this.lab_reste.BackColor = System.Drawing.Color.White;
            this.lab_reste.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_reste.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lab_reste.Location = new System.Drawing.Point(91, 80);
            this.lab_reste.Name = "lab_reste";
            this.lab_reste.Size = new System.Drawing.Size(273, 25);
            this.lab_reste.TabIndex = 8;
            this.lab_reste.Text = "0";
            this.lab_reste.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(232, 113);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 33);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "    Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(114, 113);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 33);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "    Update";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Loan";
            // 
            // text_mount
            // 
            this.text_mount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_mount.Location = new System.Drawing.Point(91, 51);
            this.text_mount.Name = "text_mount";
            this.text_mount.Size = new System.Drawing.Size(273, 24);
            this.text_mount.TabIndex = 5;
            this.text_mount.TextChanged += new System.EventHandler(this.text_mount_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 21);
            this.label6.TabIndex = 4;
            this.label6.Text = "Rest";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Mount";
            // 
            // Update_rembou_credit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 195);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lab_name);
            this.Controls.Add(this.groupBox4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Update_rembou_credit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update_rembou_credit";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lab_name;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Label lab_dete;
        public System.Windows.Forms.Label lab_reste;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox text_mount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
    }
}