namespace ConfigManager
{
    partial class ConfigManage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_istop = new System.Windows.Forms.CheckBox();
            this.checkBox_isnew = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_pront = new System.Windows.Forms.TextBox();
            this.checkBox_isphone = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox_update = new System.Windows.Forms.GroupBox();
            this.textBox_update_url = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_save = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDown_time = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel_title.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox_update.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_time)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.button_save);
            this.panel2.Controls.Add(this.groupBox_update);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Size = new System.Drawing.Size(493, 315);
            // 
            // panel_title
            // 
            this.panel_title.Size = new System.Drawing.Size(493, 32);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_istop);
            this.groupBox1.Controls.Add(this.checkBox_isnew);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(491, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基础";
            // 
            // checkBox_istop
            // 
            this.checkBox_istop.AutoSize = true;
            this.checkBox_istop.Location = new System.Drawing.Point(232, 20);
            this.checkBox_istop.Name = "checkBox_istop";
            this.checkBox_istop.Size = new System.Drawing.Size(228, 16);
            this.checkBox_istop.TabIndex = 1;
            this.checkBox_istop.Text = "是否将“来电助手”置于所有窗口之上";
            this.checkBox_istop.UseVisualStyleBackColor = true;
            // 
            // checkBox_isnew
            // 
            this.checkBox_isnew.AutoSize = true;
            this.checkBox_isnew.Checked = true;
            this.checkBox_isnew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_isnew.Enabled = false;
            this.checkBox_isnew.Location = new System.Drawing.Point(49, 20);
            this.checkBox_isnew.Name = "checkBox_isnew";
            this.checkBox_isnew.Size = new System.Drawing.Size(144, 16);
            this.checkBox_isnew.TabIndex = 1;
            this.checkBox_isnew.Text = "是否实施检测最新版本";
            this.checkBox_isnew.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox_pront);
            this.groupBox2.Controls.Add(this.checkBox_isphone);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox_url);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox_ip);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(491, 107);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "手机终端管理";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(231, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "节省服务器流量，暂时关闭";
            // 
            // textBox_pront
            // 
            this.textBox_pront.Location = new System.Drawing.Point(395, 44);
            this.textBox_pront.Name = "textBox_pront";
            this.textBox_pront.Size = new System.Drawing.Size(69, 21);
            this.textBox_pront.TabIndex = 1;
            // 
            // checkBox_isphone
            // 
            this.checkBox_isphone.AutoSize = true;
            this.checkBox_isphone.Enabled = false;
            this.checkBox_isphone.Location = new System.Drawing.Point(49, 20);
            this.checkBox_isphone.Name = "checkBox_isphone";
            this.checkBox_isphone.Size = new System.Drawing.Size(144, 16);
            this.checkBox_isphone.TabIndex = 1;
            this.checkBox_isphone.Text = "是否开启手机远程管理";
            this.checkBox_isphone.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "服务器端口号：";
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(116, 74);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(348, 21);
            this.textBox_url.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "服务器访问地址：";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(116, 44);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(163, 21);
            this.textBox_ip.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器IP地址：";
            // 
            // groupBox_update
            // 
            this.groupBox_update.Controls.Add(this.textBox_update_url);
            this.groupBox_update.Controls.Add(this.label5);
            this.groupBox_update.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox_update.Location = new System.Drawing.Point(0, 155);
            this.groupBox_update.Name = "groupBox_update";
            this.groupBox_update.Size = new System.Drawing.Size(491, 62);
            this.groupBox_update.TabIndex = 4;
            this.groupBox_update.TabStop = false;
            this.groupBox_update.Text = "版本更新管理";
            // 
            // textBox_update_url
            // 
            this.textBox_update_url.Location = new System.Drawing.Point(116, 25);
            this.textBox_update_url.Name = "textBox_update_url";
            this.textBox_update_url.Size = new System.Drawing.Size(348, 21);
            this.textBox_update_url.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "版本号获取地址：";
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(198, 270);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(97, 36);
            this.button_save.TabIndex = 5;
            this.button_save.Text = "保存";
            this.button_save.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown_time);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 217);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(491, 47);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "来电弹窗自动关闭";
            // 
            // numericUpDown_time
            // 
            this.numericUpDown_time.Location = new System.Drawing.Point(153, 17);
            this.numericUpDown_time.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_time.Name = "numericUpDown_time";
            this.numericUpDown_time.Size = new System.Drawing.Size(49, 21);
            this.numericUpDown_time.TabIndex = 1;
            this.numericUpDown_time.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(285, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "，0 将不自动关闭（重启生效）";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "分钟自动关闭";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "弹出框将于";
            // 
            // ConfigManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 347);
            this.Name = "ConfigManage";
            this.Text = "系统配置管理";
            this.panel2.ResumeLayout(false);
            this.panel_title.ResumeLayout(false);
            this.panel_title.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox_update.ResumeLayout(false);
            this.groupBox_update.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_time)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_isnew;
        private System.Windows.Forms.TextBox textBox_pront;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox_update;
        private System.Windows.Forms.TextBox textBox_update_url;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.CheckBox checkBox_istop;
        private System.Windows.Forms.CheckBox checkBox_isphone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numericUpDown_time;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;

    }
}