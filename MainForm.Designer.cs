namespace DouSlackingMonitor
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label1_out = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2_out = new System.Windows.Forms.Label();
            this.label3_out = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label4_out = new System.Windows.Forms.Label();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.MinButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "配置状态：";
            // 
            // label1_out
            // 
            this.label1_out.AutoEllipsis = true;
            this.label1_out.AutoSize = true;
            this.label1_out.Location = new System.Drawing.Point(111, 9);
            this.label1_out.Name = "label1_out";
            this.label1_out.Size = new System.Drawing.Size(13, 17);
            this.label1_out.TabIndex = 1;
            this.label1_out.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "API 地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "同步密钥：";
            // 
            // label2_out
            // 
            this.label2_out.AutoEllipsis = true;
            this.label2_out.AutoSize = true;
            this.label2_out.Location = new System.Drawing.Point(111, 36);
            this.label2_out.Name = "label2_out";
            this.label2_out.Size = new System.Drawing.Size(13, 17);
            this.label2_out.TabIndex = 4;
            this.label2_out.Text = "-";
            // 
            // label3_out
            // 
            this.label3_out.AutoEllipsis = true;
            this.label3_out.AutoSize = true;
            this.label3_out.Location = new System.Drawing.Point(111, 63);
            this.label3_out.Name = "label3_out";
            this.label3_out.Size = new System.Drawing.Size(13, 17);
            this.label3_out.TabIndex = 5;
            this.label3_out.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "白名单数量：";
            // 
            // label4_out
            // 
            this.label4_out.AutoEllipsis = true;
            this.label4_out.AutoSize = true;
            this.label4_out.Location = new System.Drawing.Point(111, 90);
            this.label4_out.Name = "label4_out";
            this.label4_out.Size = new System.Drawing.Size(13, 17);
            this.label4_out.TabIndex = 7;
            this.label4_out.Text = "-";
            // 
            // ReloadButton
            // 
            this.ReloadButton.Location = new System.Drawing.Point(12, 113);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(106, 26);
            this.ReloadButton.TabIndex = 8;
            this.ReloadButton.Text = "重载配置";
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButton_Click);
            // 
            // MinButton
            // 
            this.MinButton.Location = new System.Drawing.Point(134, 113);
            this.MinButton.Name = "MinButton";
            this.MinButton.Size = new System.Drawing.Size(106, 26);
            this.MinButton.TabIndex = 9;
            this.MinButton.Text = "最小化到托盘";
            this.MinButton.UseVisualStyleBackColor = true;
            this.MinButton.Click += new System.EventHandler(this.MinButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExitButton.ForeColor = System.Drawing.Color.IndianRed;
            this.ExitButton.Location = new System.Drawing.Point(256, 113);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(106, 26);
            this.ExitButton.TabIndex = 10;
            this.ExitButton.Text = "完全退出";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 151);
            this.ControlBox = false;
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.MinButton);
            this.Controls.Add(this.ReloadButton);
            this.Controls.Add(this.label4_out);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3_out);
            this.Controls.Add(this.label2_out);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1_out);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(390, 190);
            this.MinimumSize = new System.Drawing.Size(390, 190);
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Dou Slacking Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label1_out;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2_out;
        private System.Windows.Forms.Label label3_out;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label4_out;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.Button MinButton;
        private System.Windows.Forms.Button ExitButton;
    }
}

