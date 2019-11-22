namespace JosaleApp.Forms
{
    partial class Create_User
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Create_User));
            this.panel1 = new System.Windows.Forms.Panel();
            this.comb_niveau = new System.Windows.Forms.ComboBox();
            this.text_pass = new System.Windows.Forms.TextBox();
            this.text_username = new System.Windows.Forms.TextBox();
            this.text_user = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.comb_niveau);
            this.panel1.Controls.Add(this.text_pass);
            this.panel1.Controls.Add(this.text_username);
            this.panel1.Controls.Add(this.text_user);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(383, 412);
            this.panel1.TabIndex = 0;
            // 
            // comb_niveau
            // 
            this.comb_niveau.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comb_niveau.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.comb_niveau.FormattingEnabled = true;
            this.comb_niveau.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comb_niveau.Location = new System.Drawing.Point(33, 311);
            this.comb_niveau.Name = "comb_niveau";
            this.comb_niveau.Size = new System.Drawing.Size(317, 28);
            this.comb_niveau.TabIndex = 9;
            // 
            // text_pass
            // 
            this.text_pass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_pass.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.text_pass.Location = new System.Drawing.Point(34, 252);
            this.text_pass.Multiline = true;
            this.text_pass.Name = "text_pass";
            this.text_pass.Size = new System.Drawing.Size(317, 27);
            this.text_pass.TabIndex = 8;
            // 
            // text_username
            // 
            this.text_username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_username.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.text_username.Location = new System.Drawing.Point(34, 195);
            this.text_username.Multiline = true;
            this.text_username.Name = "text_username";
            this.text_username.Size = new System.Drawing.Size(317, 27);
            this.text_username.TabIndex = 7;
            // 
            // text_user
            // 
            this.text_user.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_user.Font = new System.Drawing.Font("Arial Narrow", 12F);
            this.text_user.Location = new System.Drawing.Point(33, 136);
            this.text_user.Multiline = true;
            this.text_user.Name = "text_user";
            this.text_user.Size = new System.Drawing.Size(317, 27);
            this.text_user.TabIndex = 6;
            this.text_user.TextChanged += new System.EventHandler(this.text_user_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(357, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(126, 352);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Create_User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 420);
            this.Controls.Add(this.panel1);
            this.Name = "Create_User";
            this.ShowInTaskbar = false;
            this.Style = MetroFramework.MetroColorStyle.White;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_pass;
        private System.Windows.Forms.TextBox text_username;
        private System.Windows.Forms.TextBox text_user;
        private System.Windows.Forms.ComboBox comb_niveau;
    }
}