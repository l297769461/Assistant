namespace AddPhone
{
    partial class AddPhone
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_query = new System.Windows.Forms.Button();
            this.textBox_bh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_bz = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_phone = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView_kh = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn_kh_bh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加到选中客户信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看选中项详情ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pager_kh = new BaseForm.Pager();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_adds = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panel_title.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_kh)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView_kh);
            this.panel2.Controls.Add(this.pager_kh);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Size = new System.Drawing.Size(920, 376);
            // 
            // panel_title
            // 
            this.panel_title.Size = new System.Drawing.Size(920, 32);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(918, 96);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_adds);
            this.groupBox2.Controls.Add(this.textBox_bh);
            this.groupBox2.Controls.Add(this.button_query);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(918, 48);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "客户信息检索";
            // 
            // button_query
            // 
            this.button_query.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_query.Location = new System.Drawing.Point(589, 15);
            this.button_query.Name = "button_query";
            this.button_query.Size = new System.Drawing.Size(75, 23);
            this.button_query.TabIndex = 10;
            this.button_query.Text = "查询";
            this.button_query.UseVisualStyleBackColor = true;
            // 
            // textBox_bh
            // 
            this.textBox_bh.Location = new System.Drawing.Point(213, 17);
            this.textBox_bh.Name = "textBox_bh";
            this.textBox_bh.Size = new System.Drawing.Size(114, 21);
            this.textBox_bh.TabIndex = 9;
            this.toolTip1.SetToolTip(this.textBox_bh, "不填获取所有客户信息");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "请输入客户编号：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_bz);
            this.groupBox1.Controls.Add(this.textBox_name);
            this.groupBox1.Controls.Add(this.label_phone);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(918, 48);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "需要添加的客户联系信息";
            // 
            // textBox_bz
            // 
            this.textBox_bz.Location = new System.Drawing.Point(540, 17);
            this.textBox_bz.Name = "textBox_bz";
            this.textBox_bz.Size = new System.Drawing.Size(124, 21);
            this.textBox_bz.TabIndex = 6;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(300, 17);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(124, 21);
            this.textBox_name.TabIndex = 6;
            // 
            // label_phone
            // 
            this.label_phone.AutoSize = true;
            this.label_phone.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_phone.ForeColor = System.Drawing.Color.Red;
            this.label_phone.Location = new System.Drawing.Point(100, 20);
            this.label_phone.Name = "label_phone";
            this.label_phone.Size = new System.Drawing.Size(107, 16);
            this.label_phone.TabIndex = 3;
            this.label_phone.Text = "18628285768";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(496, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "备注：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "姓名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "联系号码：";
            // 
            // dataGridView_kh
            // 
            this.dataGridView_kh.AllowUserToAddRows = false;
            this.dataGridView_kh.AllowUserToDeleteRows = false;
            this.dataGridView_kh.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_kh.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_kh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn_kh_bh,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.Column7,
            this.Column8,
            this.dataGridViewTextBoxColumn6});
            this.dataGridView_kh.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView_kh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_kh.Location = new System.Drawing.Point(0, 96);
            this.dataGridView_kh.Name = "dataGridView_kh";
            this.dataGridView_kh.ReadOnly = true;
            this.dataGridView_kh.RowTemplate.Height = 23;
            this.dataGridView_kh.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_kh.Size = new System.Drawing.Size(918, 245);
            this.dataGridView_kh.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn_kh_bh
            // 
            this.dataGridViewTextBoxColumn_kh_bh.HeaderText = "编号";
            this.dataGridViewTextBoxColumn_kh_bh.Name = "dataGridViewTextBoxColumn_kh_bh";
            this.dataGridViewTextBoxColumn_kh_bh.ReadOnly = true;
            this.dataGridViewTextBoxColumn_kh_bh.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "客户编号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "桶装水单价";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "饮水品牌";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "送水地址";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "备注信息";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "联系电话摘要";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.ToolTipText = "查看详细，请右键查看详情";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "guid";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加到选中客户信息ToolStripMenuItem,
            this.查看选中项详情ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(189, 48);
            // 
            // 添加到选中客户信息ToolStripMenuItem
            // 
            this.添加到选中客户信息ToolStripMenuItem.Name = "添加到选中客户信息ToolStripMenuItem";
            this.添加到选中客户信息ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.添加到选中客户信息ToolStripMenuItem.Text = "添加到选中 客户信息";
            this.添加到选中客户信息ToolStripMenuItem.Click += new System.EventHandler(this.添加到选中客户信息ToolStripMenuItem_Click);
            // 
            // 查看选中项详情ToolStripMenuItem
            // 
            this.查看选中项详情ToolStripMenuItem.Name = "查看选中项详情ToolStripMenuItem";
            this.查看选中项详情ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.查看选中项详情ToolStripMenuItem.Text = "查看选中项详情";
            this.查看选中项详情ToolStripMenuItem.Click += new System.EventHandler(this.查看选中项详情ToolStripMenuItem_Click);
            // 
            // pager_kh
            // 
            this.pager_kh.Count = 0;
            this.pager_kh.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pager_kh.Enabled = false;
            this.pager_kh.GoIndex = 0;
            this.pager_kh.Location = new System.Drawing.Point(0, 341);
            this.pager_kh.Name = "pager_kh";
            this.pager_kh.PageCount = 0;
            this.pager_kh.PageIndex = 1;
            this.pager_kh.PageSize = 20;
            this.pager_kh.Size = new System.Drawing.Size(918, 33);
            this.pager_kh.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(350, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "请输入送水地址：";
            // 
            // textBox_adds
            // 
            this.textBox_adds.Location = new System.Drawing.Point(446, 17);
            this.textBox_adds.Name = "textBox_adds";
            this.textBox_adds.Size = new System.Drawing.Size(114, 21);
            this.textBox_adds.TabIndex = 9;
            // 
            // AddPhone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 408);
            this.Name = "AddPhone";
            this.Text = "添加号码到指定客户";
            this.Controls.SetChildIndex(this.panel_title, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.panel2.ResumeLayout(false);
            this.panel_title.ResumeLayout(false);
            this.panel_title.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_kh)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_phone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_kh;
        private BaseForm.Pager pager_kh;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 添加到选中客户信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看选中项详情ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_query;
        private System.Windows.Forms.TextBox textBox_bh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_bz;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn_kh_bh;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.TextBox textBox_adds;
        private System.Windows.Forms.Label label5;

    }
}