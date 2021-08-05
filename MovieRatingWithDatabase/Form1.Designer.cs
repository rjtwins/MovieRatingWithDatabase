
namespace MovieRatingWithDatabase
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
            this.components = new System.ComponentModel.Container();
            this.PublicDisplayListView = new System.Windows.Forms.ListView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.PrivateDisplayListView = new System.Windows.Forms.ListView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.waterMarkTextBox1 = new MovieRatingWithDatabase.WaterMarkTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PublicDisplayListView
            // 
            this.PublicDisplayListView.BackColor = System.Drawing.Color.Black;
            this.PublicDisplayListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PublicDisplayListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PublicDisplayListView.ForeColor = System.Drawing.Color.White;
            this.PublicDisplayListView.HideSelection = false;
            this.PublicDisplayListView.Location = new System.Drawing.Point(20, 20);
            this.PublicDisplayListView.MultiSelect = false;
            this.PublicDisplayListView.Name = "PublicDisplayListView";
            this.PublicDisplayListView.Size = new System.Drawing.Size(886, 442);
            this.PublicDisplayListView.TabIndex = 0;
            this.PublicDisplayListView.UseCompatibleStateImageBehavior = false;
            this.PublicDisplayListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.PublicDisplayListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PublicDisplayListView_MouseDoubleClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.ItemSize = new System.Drawing.Size(80, 20);
            this.tabControl1.Location = new System.Drawing.Point(0, 70);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(5, 5);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(934, 510);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.PublicDisplayListView);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tabPage1.ForeColor = System.Drawing.Color.FloralWhite;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(10);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(20);
            this.tabPage1.Size = new System.Drawing.Size(926, 482);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Search";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.PrivateDisplayListView);
            this.tabPage2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(10);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(20);
            this.tabPage2.Size = new System.Drawing.Size(926, 482);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bookmarks";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // PrivateDisplayListView
            // 
            this.PrivateDisplayListView.BackColor = System.Drawing.Color.Black;
            this.PrivateDisplayListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PrivateDisplayListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrivateDisplayListView.ForeColor = System.Drawing.Color.White;
            this.PrivateDisplayListView.HideSelection = false;
            this.PrivateDisplayListView.Location = new System.Drawing.Point(20, 20);
            this.PrivateDisplayListView.MultiSelect = false;
            this.PrivateDisplayListView.Name = "PrivateDisplayListView";
            this.PrivateDisplayListView.Size = new System.Drawing.Size(886, 442);
            this.PrivateDisplayListView.TabIndex = 1;
            this.PrivateDisplayListView.UseCompatibleStateImageBehavior = false;
            this.PrivateDisplayListView.SelectedIndexChanged += new System.EventHandler(this.PrivateDisplayListView_SelectedIndexChanged);
            this.PrivateDisplayListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PrivateDisplayListView_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // waterMarkTextBox1
            // 
            this.waterMarkTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.waterMarkTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.waterMarkTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.waterMarkTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.waterMarkTextBox1.ForeColor = System.Drawing.Color.White;
            this.waterMarkTextBox1.Location = new System.Drawing.Point(12, 12);
            this.waterMarkTextBox1.Margin = new System.Windows.Forms.Padding(10);
            this.waterMarkTextBox1.Name = "waterMarkTextBox1";
            this.waterMarkTextBox1.Size = new System.Drawing.Size(900, 23);
            this.waterMarkTextBox1.TabIndex = 2;
            this.waterMarkTextBox1.WaterMarkColor = System.Drawing.Color.LightGray;
            this.waterMarkTextBox1.WaterMarkText = "Type here to search";
            this.waterMarkTextBox1.TextChanged += new System.EventHandler(this.waterMarkTextBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(934, 580);
            this.Controls.Add(this.waterMarkTextBox1);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView PublicDisplayListView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView PrivateDisplayListView;
        private WaterMarkTextBox waterMarkTextBox1;
        private System.Windows.Forms.Timer timer1;
    }
}

