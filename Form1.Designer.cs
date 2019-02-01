namespace Brower_v1
{
    partial class Browser
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
            this.test_lb = new System.Windows.Forms.Label();
            this.tp_ = new System.Windows.Forms.TabPage();
            this.tabController = new System.Windows.Forms.TabControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tp_.SuspendLayout();
            this.tabController.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // test_lb
            // 
            this.test_lb.AutoSize = true;
            this.test_lb.Location = new System.Drawing.Point(1281, 13);
            this.test_lb.Name = "test_lb";
            this.test_lb.Size = new System.Drawing.Size(35, 13);
            this.test_lb.TabIndex = 3;
            this.test_lb.Text = "label1";
            // 
            // tp_
            // 
            this.tp_.Controls.Add(this.splitContainer1);
            this.tp_.Location = new System.Drawing.Point(4, 22);
            this.tp_.Name = "tp_";
            this.tp_.Padding = new System.Windows.Forms.Padding(3);
            this.tp_.Size = new System.Drawing.Size(1526, 862);
            this.tp_.TabIndex = 0;
            this.tp_.Text = "dsgjhfgs";
            this.tp_.UseVisualStyleBackColor = true;
            this.tp_.Click += new System.EventHandler(this.tp__Click);
            // 
            // tabController
            // 
            this.tabController.Controls.Add(this.tp_);
            this.tabController.Location = new System.Drawing.Point(12, 12);
            this.tabController.Name = "tabController";
            this.tabController.SelectedIndex = 0;
            this.tabController.Size = new System.Drawing.Size(1534, 888);
            this.tabController.TabIndex = 8;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(1520, 856);
            this.splitContainer1.SplitterDistance = 396;
            this.splitContainer1.TabIndex = 0;
            // 
            // Browser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1558, 912);
            this.Controls.Add(this.tabController);
            this.Controls.Add(this.test_lb);
            this.Name = "Browser";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tp_.ResumeLayout(false);
            this.tabController.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label test_lb;
        private System.Windows.Forms.TabPage tp_;
        private System.Windows.Forms.TabControl tabController;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

