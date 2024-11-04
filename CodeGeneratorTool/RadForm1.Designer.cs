namespace CodeGenerator
{
    partial class RadForm1
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
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radBrowseEditor1 = new Telerik.WinControls.UI.RadBrowseEditor();
            this.radBrowseEditor2 = new Telerik.WinControls.UI.RadBrowseEditor();
            this.radBrowseEditor3 = new Telerik.WinControls.UI.RadBrowseEditor();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBrowseEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBrowseEditor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBrowseEditor3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(67, 245);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(270, 24);
            this.radButton1.TabIndex = 0;
            this.radButton1.Text = "Generate all snippets";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(67, 25);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(82, 18);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "Samples Folder";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(67, 73);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(76, 18);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Output Folder";
            // 
            // radBrowseEditor1
            // 
            this.radBrowseEditor1.Location = new System.Drawing.Point(166, 23);
            this.radBrowseEditor1.Name = "radBrowseEditor1";
            this.radBrowseEditor1.Size = new System.Drawing.Size(392, 20);
            this.radBrowseEditor1.TabIndex = 3;
            this.radBrowseEditor1.Text = "radBrowseEditor1";
            this.radBrowseEditor1.ValueChanged += new System.EventHandler(this.radBrowseEditor1_ValueChanged);
            // 
            // radBrowseEditor2
            // 
            this.radBrowseEditor2.Location = new System.Drawing.Point(166, 73);
            this.radBrowseEditor2.Name = "radBrowseEditor2";
            this.radBrowseEditor2.Size = new System.Drawing.Size(392, 20);
            this.radBrowseEditor2.TabIndex = 4;
            this.radBrowseEditor2.Text = "radBrowseEditor2";
            this.radBrowseEditor2.ValueChanged += new System.EventHandler(this.radBrowseEditor2_ValueChanged);
            // 
            // radBrowseEditor3
            // 
            this.radBrowseEditor3.Location = new System.Drawing.Point(166, 126);
            this.radBrowseEditor3.Name = "radBrowseEditor3";
            this.radBrowseEditor3.Size = new System.Drawing.Size(392, 20);
            this.radBrowseEditor3.TabIndex = 5;
            this.radBrowseEditor3.Text = "radBrowseEditor3";
            this.radBrowseEditor3.ValueChanged += new System.EventHandler(this.radBrowseEditor3_ValueChanged);
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(67, 126);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(67, 18);
            this.radLabel3.TabIndex = 6;
            this.radLabel3.Text = "Repo Folder";
            // 
            // RadForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 300);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radBrowseEditor3);
            this.Controls.Add(this.radBrowseEditor2);
            this.Controls.Add(this.radBrowseEditor1);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radButton1);
            this.Name = "RadForm1";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "RadForm1";
            this.ThemeName = "ControlDefault";
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBrowseEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBrowseEditor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radBrowseEditor3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadBrowseEditor radBrowseEditor1;
        private Telerik.WinControls.UI.RadBrowseEditor radBrowseEditor2;
        private Telerik.WinControls.UI.RadBrowseEditor radBrowseEditor3;
        private Telerik.WinControls.UI.RadLabel radLabel3;
    }
}