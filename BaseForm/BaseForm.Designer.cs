namespace BaseForm
{
    partial class BaseForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.panel_title = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button_close = new System.Windows.Forms.Button();
            this.button_max = new System.Windows.Forms.Button();
            this.button_min = new System.Windows.Forms.Button();
            this.label_name = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip();
            this.panel_title.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_title
            // 
            this.panel_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.panel_title.Controls.Add(this.panel3);
            this.panel_title.Controls.Add(this.label_name);
            this.panel_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_title.ForeColor = System.Drawing.Color.White;
            this.panel_title.Location = new System.Drawing.Point(0, 0);
            this.panel_title.Name = "panel_title";
            this.panel_title.Size = new System.Drawing.Size(330, 32);
            this.panel_title.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_close);
            this.panel3.Controls.Add(this.button_max);
            this.panel3.Controls.Add(this.button_min);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(238, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(92, 32);
            this.panel3.TabIndex = 1;
            // 
            // button_close
            // 
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_close.Font = new System.Drawing.Font("黑体", 6F);
            this.button_close.Location = new System.Drawing.Point(63, 7);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(23, 17);
            this.button_close.TabIndex = 0;
            this.button_close.Text = "X";
            this.toolTip1.SetToolTip(this.button_close, "关闭");
            this.button_close.UseVisualStyleBackColor = true;
            // 
            // button_max
            // 
            this.button_max.Enabled = false;
            this.button_max.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_max.Font = new System.Drawing.Font("宋体", 6F);
            this.button_max.Location = new System.Drawing.Point(33, 7);
            this.button_max.Name = "button_max";
            this.button_max.Size = new System.Drawing.Size(23, 17);
            this.button_max.TabIndex = 0;
            this.button_max.Text = "□";
            this.toolTip1.SetToolTip(this.button_max, "最大化/还原");
            this.button_max.UseVisualStyleBackColor = true;
            // 
            // button_min
            // 
            this.button_min.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_min.Location = new System.Drawing.Point(4, 7);
            this.button_min.Name = "button_min";
            this.button_min.Size = new System.Drawing.Size(22, 17);
            this.button_min.TabIndex = 0;
            this.button_min.Text = "-";
            this.toolTip1.SetToolTip(this.button_min, "最小化");
            this.button_min.UseVisualStyleBackColor = true;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Location = new System.Drawing.Point(12, 10);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(29, 12);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "标题";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 281);
            this.panel2.TabIndex = 1;
            // 
            // BaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 313);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "基础窗体";
            this.panel_title.ResumeLayout(false);
            this.panel_title.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        protected System.Windows.Forms.Label label_name;
        protected System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolTip toolTip1;
        protected System.Windows.Forms.Button button_max;
        protected System.Windows.Forms.Button button_min;
        protected System.Windows.Forms.Button button_close;
        protected System.Windows.Forms.Panel panel_title;

    }
}