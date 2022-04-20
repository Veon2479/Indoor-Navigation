
namespace Server_Interfase_Prototype
{
    partial class FMapSettings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.bSave = new System.Windows.Forms.Button();
            this.tbRealHeight = new System.Windows.Forms.TextBox();
            this.tbRealWidth = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbY2 = new System.Windows.Forms.TextBox();
            this.tbX2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbY1 = new System.Windows.Forms.TextBox();
            this.tbX1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bDownloadMap = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbMapImage = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.bSave);
            this.panel1.Controls.Add(this.tbRealHeight);
            this.panel1.Controls.Add(this.tbRealWidth);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbY2);
            this.panel1.Controls.Add(this.tbX2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbY1);
            this.panel1.Controls.Add(this.tbX1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.bDownloadMap);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 717);
            this.panel1.TabIndex = 1;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(10, 280);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(236, 30);
            this.bSave.TabIndex = 15;
            this.bSave.Text = "Сохранить";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // tbRealHeight
            // 
            this.tbRealHeight.Location = new System.Drawing.Point(17, 242);
            this.tbRealHeight.Name = "tbRealHeight";
            this.tbRealHeight.Size = new System.Drawing.Size(92, 22);
            this.tbRealHeight.TabIndex = 14;
            this.tbRealHeight.Text = "0";
            this.tbRealHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbCoordinate_KeyPress);
            // 
            // tbRealWidth
            // 
            this.tbRealWidth.Location = new System.Drawing.Point(17, 164);
            this.tbRealWidth.Name = "tbRealWidth";
            this.tbRealWidth.Size = new System.Drawing.Size(92, 22);
            this.tbRealWidth.TabIndex = 13;
            this.tbRealWidth.Text = "0";
            this.tbRealWidth.TextChanged += new System.EventHandler(this.Form1_Load);
            this.tbRealWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRealWidth_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 34);
            this.label6.TabIndex = 12;
            this.label6.Text = "Длина выделенной области\r\n(в метрах)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 34);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ширина отмеченной области \r\n(в метрах)";
            // 
            // tbY2
            // 
            this.tbY2.Location = new System.Drawing.Point(168, 88);
            this.tbY2.Name = "tbY2";
            this.tbY2.Size = new System.Drawing.Size(60, 22);
            this.tbY2.TabIndex = 10;
            this.tbY2.Text = "0";
            this.tbY2.TextChanged += new System.EventHandler(this.tbY2_TextChanged);
            this.tbY2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbCoordinate_KeyPress);
            // 
            // tbX2
            // 
            this.tbX2.Location = new System.Drawing.Point(168, 60);
            this.tbX2.Name = "tbX2";
            this.tbX2.Size = new System.Drawing.Size(60, 22);
            this.tbX2.TabIndex = 9;
            this.tbX2.Text = "0";
            this.tbX2.TextChanged += new System.EventHandler(this.tbX2_TextChanged);
            this.tbX2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbCoordinate_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Y2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "X2:";
            // 
            // tbY1
            // 
            this.tbY1.Location = new System.Drawing.Point(49, 88);
            this.tbY1.Name = "tbY1";
            this.tbY1.Size = new System.Drawing.Size(60, 22);
            this.tbY1.TabIndex = 5;
            this.tbY1.Text = "0";
            this.tbY1.TextChanged += new System.EventHandler(this.tbY1_TextChanged);
            this.tbY1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbCoordinate_KeyPress);
            // 
            // tbX1
            // 
            this.tbX1.Location = new System.Drawing.Point(49, 60);
            this.tbX1.Name = "tbX1";
            this.tbX1.Size = new System.Drawing.Size(60, 22);
            this.tbX1.TabIndex = 4;
            this.tbX1.Text = "0";
            this.tbX1.TextChanged += new System.EventHandler(this.tbX1_TextChanged);
            this.tbX1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbCoordinate_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y1:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "X1:";
            // 
            // bDownloadMap
            // 
            this.bDownloadMap.Location = new System.Drawing.Point(10, 12);
            this.bDownloadMap.Name = "bDownloadMap";
            this.bDownloadMap.Size = new System.Drawing.Size(236, 30);
            this.bDownloadMap.TabIndex = 0;
            this.bDownloadMap.Text = "Загрузить карту помещения";
            this.bDownloadMap.UseVisualStyleBackColor = true;
            this.bDownloadMap.Click += new System.EventHandler(this.bDownloadMap_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.pbMapImage);
            this.panel2.Location = new System.Drawing.Point(279, 12);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(1093, 717);
            this.panel2.TabIndex = 2;
            // 
            // pbMapImage
            // 
            this.pbMapImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbMapImage.Location = new System.Drawing.Point(6, 6);
            this.pbMapImage.Margin = new System.Windows.Forms.Padding(1);
            this.pbMapImage.Name = "pbMapImage";
            this.pbMapImage.Padding = new System.Windows.Forms.Padding(1);
            this.pbMapImage.Size = new System.Drawing.Size(1080, 704);
            this.pbMapImage.TabIndex = 1;
            this.pbMapImage.TabStop = false;
            // 
            // fMapSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 741);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "fMapSettings";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMapImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbY1;
        private System.Windows.Forms.TextBox tbX1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bDownloadMap;
        private System.Windows.Forms.TextBox tbY2;
        private System.Windows.Forms.TextBox tbX2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pbMapImage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.TextBox tbRealHeight;
        private System.Windows.Forms.TextBox tbRealWidth;
    }
}

