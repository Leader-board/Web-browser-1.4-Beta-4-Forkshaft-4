namespace RibbonGenerator.Console
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
            this.qRibbon1 = new Qios.DevSuite.Components.Ribbon.QRibbon();
            this.qRibbonCaption1 = new Qios.DevSuite.Components.Ribbon.QRibbonCaption();
            this.qTranslucentWindowComponent1 = new Qios.DevSuite.Components.QTranslucentWindowComponent(this.components);
            this.qRibbonPage1 = new Qios.DevSuite.Components.Ribbon.QRibbonPage();
            this.qRibbonPanel1 = new Qios.DevSuite.Components.Ribbon.QRibbonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.qRibbon1)).BeginInit();
            this.qRibbon1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qRibbonCaption1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qRibbonPage1)).BeginInit();
            this.SuspendLayout();
            // 
            // qRibbon1
            // 
            this.qRibbon1.ActiveTabPage = this.qRibbonPage1;
            this.qRibbon1.Controls.Add(this.qRibbonCaption1);
            this.qRibbon1.Controls.Add(this.qRibbonPage1);
            this.qRibbon1.Dock = System.Windows.Forms.DockStyle.Top;
            this.qRibbon1.Location = new System.Drawing.Point(0, 0);
            this.qRibbon1.Name = "qRibbon1";
            this.qRibbon1.PersistGuid = new System.Guid("15f4c7d7-f2b8-4bdd-90a3-e9a7dcdf8d98");
            this.qRibbon1.Size = new System.Drawing.Size(687, 94);
            this.qRibbon1.TabIndex = 0;
            this.qRibbon1.Text = "qRibbonCaption1";
            // 
            // qRibbonCaption1
            // 
            this.qRibbonCaption1.Location = new System.Drawing.Point(0, 0);
            this.qRibbonCaption1.Name = "qRibbonCaption1";
            this.qRibbonCaption1.Size = new System.Drawing.Size(687, 23);
            this.qRibbonCaption1.TabIndex = 0;
            this.qRibbonCaption1.Text = "qRibbonCaption1";
            // 
            // qRibbonPage1
            // 
            this.qRibbonPage1.ButtonOrder = 0;
            this.qRibbonPage1.Items.Add(this.qRibbonPanel1);
            this.qRibbonPage1.Location = new System.Drawing.Point(2, 31);
            this.qRibbonPage1.Name = "qRibbonPage1";
            this.qRibbonPage1.PersistGuid = new System.Guid("6d4a32c6-adda-4911-a875-8a44165433fe");
            this.qRibbonPage1.Size = new System.Drawing.Size(681, 59);
            this.qRibbonPage1.Text = "qRibbonPage1";
            // 
            // qRibbonPanel1
            // 
            this.qRibbonPanel1.Title = "qRibbonPanel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 323);
            this.Controls.Add(this.qRibbon1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.qRibbon1)).EndInit();
            this.qRibbon1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qRibbonCaption1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qRibbonPage1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Qios.DevSuite.Components.Ribbon.QRibbon qRibbon1;
        private Qios.DevSuite.Components.Ribbon.QRibbonPage qRibbonPage1;
        private Qios.DevSuite.Components.Ribbon.QRibbonPanel qRibbonPanel1;
        private Qios.DevSuite.Components.Ribbon.QRibbonCaption qRibbonCaption1;
        private Qios.DevSuite.Components.QTranslucentWindowComponent qTranslucentWindowComponent1;
    }
}