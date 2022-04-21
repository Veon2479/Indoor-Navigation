
namespace Server
{
    partial class frmServer
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TabPage tbSettings;
            this.pImage = new System.Windows.Forms.Panel();
            this.pbMapImage = new System.Windows.Forms.PictureBox();
            this.pSettings = new System.Windows.Forms.Panel();
            this.tbRealWidth = new System.Windows.Forms.TextBox();
            this.tbRealLength = new System.Windows.Forms.TextBox();
            this.tbCoordinateY2 = new System.Windows.Forms.TextBox();
            this.tbCoordinateX2 = new System.Windows.Forms.TextBox();
            this.tbCoordinateY1 = new System.Windows.Forms.TextBox();
            this.tbCoordinateX1 = new System.Windows.Forms.TextBox();
            this.lblRealLength = new System.Windows.Forms.Label();
            this.lblRealWidth = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblCoordinateY2 = new System.Windows.Forms.Label();
            this.lblCoordinateX2 = new System.Windows.Forms.Label();
            this.lblCoordinateY1 = new System.Windows.Forms.Label();
            this.lblCoordinateX1 = new System.Windows.Forms.Label();
            this.btnDownloadImage = new System.Windows.Forms.Button();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            tbSettings = new System.Windows.Forms.TabPage();
            tbSettings.SuspendLayout();
            this.pImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapImage)).BeginInit();
            this.pSettings.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSettings
            // 
            tbSettings.Controls.Add(this.pImage);
            tbSettings.Controls.Add(this.pSettings);
            tbSettings.Location = new System.Drawing.Point(4, 22);
            tbSettings.Name = "tbSettings";
            tbSettings.Padding = new System.Windows.Forms.Padding(3);
            tbSettings.Size = new System.Drawing.Size(1120, 680);
            tbSettings.TabIndex = 0;
            tbSettings.Text = "Settings";
            tbSettings.UseVisualStyleBackColor = true;
            // 
            // pImage
            // 
            this.pImage.BackColor = System.Drawing.Color.DarkGray;
            this.pImage.Controls.Add(this.pbMapImage);
            this.pImage.Location = new System.Drawing.Point(231, 3);
            this.pImage.Name = "pImage";
            this.pImage.Padding = new System.Windows.Forms.Padding(5);
            this.pImage.Size = new System.Drawing.Size(886, 674);
            this.pImage.TabIndex = 1;
            // 
            // pbMapImage
            // 
            this.pbMapImage.BackColor = System.Drawing.Color.DarkGray;
            this.pbMapImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbMapImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMapImage.Location = new System.Drawing.Point(5, 5);
            this.pbMapImage.Name = "pbMapImage";
            this.pbMapImage.Size = new System.Drawing.Size(876, 664);
            this.pbMapImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMapImage.TabIndex = 0;
            this.pbMapImage.TabStop = false;
            this.pbMapImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMapImage_MouseDown);
            this.pbMapImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMapImage_MouseMove);
            // 
            // pSettings
            // 
            this.pSettings.Controls.Add(this.tbRealWidth);
            this.pSettings.Controls.Add(this.tbRealLength);
            this.pSettings.Controls.Add(this.tbCoordinateY2);
            this.pSettings.Controls.Add(this.tbCoordinateX2);
            this.pSettings.Controls.Add(this.tbCoordinateY1);
            this.pSettings.Controls.Add(this.tbCoordinateX1);
            this.pSettings.Controls.Add(this.lblRealLength);
            this.pSettings.Controls.Add(this.lblRealWidth);
            this.pSettings.Controls.Add(this.btnSave);
            this.pSettings.Controls.Add(this.lblCoordinateY2);
            this.pSettings.Controls.Add(this.lblCoordinateX2);
            this.pSettings.Controls.Add(this.lblCoordinateY1);
            this.pSettings.Controls.Add(this.lblCoordinateX1);
            this.pSettings.Controls.Add(this.btnDownloadImage);
            this.pSettings.Location = new System.Drawing.Point(3, 3);
            this.pSettings.Name = "pSettings";
            this.pSettings.Size = new System.Drawing.Size(225, 674);
            this.pSettings.TabIndex = 0;
            // 
            // tbRealWidth
            // 
            this.tbRealWidth.Location = new System.Drawing.Point(14, 206);
            this.tbRealWidth.Name = "tbRealWidth";
            this.tbRealWidth.Size = new System.Drawing.Size(97, 20);
            this.tbRealWidth.TabIndex = 23;
            this.tbRealWidth.TextChanged += new System.EventHandler(this.tbRealWidth_TextChanged);
            this.tbRealWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRealValue_KeyPress);
            // 
            // tbRealLength
            // 
            this.tbRealLength.Location = new System.Drawing.Point(14, 143);
            this.tbRealLength.Name = "tbRealLength";
            this.tbRealLength.Size = new System.Drawing.Size(97, 20);
            this.tbRealLength.TabIndex = 22;
            this.tbRealLength.TextChanged += new System.EventHandler(this.tbRealLength_TextChanged);
            // 
            // tbCoordinateY2
            // 
            this.tbCoordinateY2.Location = new System.Drawing.Point(144, 72);
            this.tbCoordinateY2.Name = "tbCoordinateY2";
            this.tbCoordinateY2.Size = new System.Drawing.Size(52, 20);
            this.tbCoordinateY2.TabIndex = 21;
            this.tbCoordinateY2.TextChanged += new System.EventHandler(this.tbCoordinateY2_TextChanged);
            this.tbCoordinateY2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCoordinate_KeyPress);
            // 
            // tbCoordinateX2
            // 
            this.tbCoordinateX2.Location = new System.Drawing.Point(144, 46);
            this.tbCoordinateX2.Name = "tbCoordinateX2";
            this.tbCoordinateX2.Size = new System.Drawing.Size(52, 20);
            this.tbCoordinateX2.TabIndex = 20;
            this.tbCoordinateX2.TextChanged += new System.EventHandler(this.tbCoordinateX2_TextChanged);
            this.tbCoordinateX2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCoordinate_KeyPress);
            // 
            // tbCoordinateY1
            // 
            this.tbCoordinateY1.Location = new System.Drawing.Point(44, 72);
            this.tbCoordinateY1.Name = "tbCoordinateY1";
            this.tbCoordinateY1.Size = new System.Drawing.Size(52, 20);
            this.tbCoordinateY1.TabIndex = 19;
            this.tbCoordinateY1.TextChanged += new System.EventHandler(this.tbCoordinateY1_TextChanged);
            this.tbCoordinateY1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCoordinate_KeyPress);
            // 
            // tbCoordinateX1
            // 
            this.tbCoordinateX1.Location = new System.Drawing.Point(44, 46);
            this.tbCoordinateX1.Name = "tbCoordinateX1";
            this.tbCoordinateX1.Size = new System.Drawing.Size(52, 20);
            this.tbCoordinateX1.TabIndex = 18;
            this.tbCoordinateX1.TextChanged += new System.EventHandler(this.tbCoordinateX1_TextChanged);
            this.tbCoordinateX1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCoordinate_KeyPress);
            // 
            // lblRealLength
            // 
            this.lblRealLength.AutoSize = true;
            this.lblRealLength.Location = new System.Drawing.Point(12, 111);
            this.lblRealLength.Name = "lblRealLength";
            this.lblRealLength.Size = new System.Drawing.Size(150, 26);
            this.lblRealLength.TabIndex = 16;
            this.lblRealLength.Text = "Длина отмеченной области \r\n(в метрах):";
            // 
            // lblRealWidth
            // 
            this.lblRealWidth.AutoSize = true;
            this.lblRealWidth.Location = new System.Drawing.Point(12, 174);
            this.lblRealWidth.Name = "lblRealWidth";
            this.lblRealWidth.Size = new System.Drawing.Size(153, 26);
            this.lblRealWidth.TabIndex = 14;
            this.lblRealWidth.Text = "Ширина отмеченной области\r\n(в метрах):";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 289);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(195, 25);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblCoordinateY2
            // 
            this.lblCoordinateY2.AutoSize = true;
            this.lblCoordinateY2.Location = new System.Drawing.Point(115, 75);
            this.lblCoordinateY2.Name = "lblCoordinateY2";
            this.lblCoordinateY2.Size = new System.Drawing.Size(23, 13);
            this.lblCoordinateY2.TabIndex = 11;
            this.lblCoordinateY2.Text = "Y2:";
            // 
            // lblCoordinateX2
            // 
            this.lblCoordinateX2.AutoSize = true;
            this.lblCoordinateX2.Location = new System.Drawing.Point(115, 49);
            this.lblCoordinateX2.Name = "lblCoordinateX2";
            this.lblCoordinateX2.Size = new System.Drawing.Size(23, 13);
            this.lblCoordinateX2.TabIndex = 9;
            this.lblCoordinateX2.Text = "X2:";
            // 
            // lblCoordinateY1
            // 
            this.lblCoordinateY1.AutoSize = true;
            this.lblCoordinateY1.Location = new System.Drawing.Point(15, 75);
            this.lblCoordinateY1.Name = "lblCoordinateY1";
            this.lblCoordinateY1.Size = new System.Drawing.Size(23, 13);
            this.lblCoordinateY1.TabIndex = 7;
            this.lblCoordinateY1.Text = "Y1:";
            // 
            // lblCoordinateX1
            // 
            this.lblCoordinateX1.AutoSize = true;
            this.lblCoordinateX1.Location = new System.Drawing.Point(15, 49);
            this.lblCoordinateX1.Name = "lblCoordinateX1";
            this.lblCoordinateX1.Size = new System.Drawing.Size(23, 13);
            this.lblCoordinateX1.TabIndex = 5;
            this.lblCoordinateX1.Text = "X1:";
            // 
            // btnDownloadImage
            // 
            this.btnDownloadImage.Location = new System.Drawing.Point(14, 11);
            this.btnDownloadImage.Name = "btnDownloadImage";
            this.btnDownloadImage.Size = new System.Drawing.Size(195, 25);
            this.btnDownloadImage.TabIndex = 0;
            this.btnDownloadImage.Text = "Загрузить карту помещения";
            this.btnDownloadImage.UseVisualStyleBackColor = true;
            this.btnDownloadImage.Click += new System.EventHandler(this.btnDownloadImage_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(tbSettings);
            this.tcMain.Controls.Add(this.tabPage2);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1128, 706);
            this.tcMain.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1120, 680);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1128, 706);
            this.Controls.Add(this.tcMain);
            this.Name = "frmServer";
            this.Text = "Server";
            tbSettings.ResumeLayout(false);
            this.pImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMapImage)).EndInit();
            this.pSettings.ResumeLayout(false);
            this.pSettings.PerformLayout();
            this.tcMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.Panel pSettings;
        private System.Windows.Forms.Panel pImage;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lblCoordinateY2;
        private System.Windows.Forms.Label lblCoordinateX2;
        private System.Windows.Forms.Label lblCoordinateY1;
        private System.Windows.Forms.Button btnDownloadImage;
        private System.Windows.Forms.Label lblRealLength;
        private System.Windows.Forms.Label lblRealWidth;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblCoordinateX1;
        private System.Windows.Forms.PictureBox pbMapImage;
        private System.Windows.Forms.TextBox tbCoordinateY2;
        private System.Windows.Forms.TextBox tbCoordinateX2;
        private System.Windows.Forms.TextBox tbCoordinateY1;
        private System.Windows.Forms.TextBox tbCoordinateX1;
        private System.Windows.Forms.TextBox tbRealWidth;
        private System.Windows.Forms.TextBox tbRealLength;
    }
}

