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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TabPage tbSettings;
            this.pImage = new System.Windows.Forms.Panel();
            this.pbMapImage = new System.Windows.Forms.PictureBox();
            this.pSettings = new System.Windows.Forms.Panel();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.tbRealWidth = new System.Windows.Forms.TextBox();
            this.lblRealWidth = new System.Windows.Forms.Label();
            this.tbAzimuth = new System.Windows.Forms.TextBox();
            this.lblRealLength = new System.Windows.Forms.Label();
            this.lAzimuth = new System.Windows.Forms.Label();
            this.tbRealLength = new System.Windows.Forms.TextBox();
            this.dbFrame = new System.Windows.Forms.GroupBox();
            this.tbCoordinateY2 = new System.Windows.Forms.TextBox();
            this.lblCoordinateX1 = new System.Windows.Forms.Label();
            this.lblCoordinateY1 = new System.Windows.Forms.Label();
            this.lblCoordinateX2 = new System.Windows.Forms.Label();
            this.lblCoordinateY2 = new System.Windows.Forms.Label();
            this.tbCoordinateX1 = new System.Windows.Forms.TextBox();
            this.tbCoordinateX2 = new System.Windows.Forms.TextBox();
            this.tbCoordinateY1 = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDownloadImage = new System.Windows.Forms.Button();
            this.tmrListUpdate = new System.Windows.Forms.Timer(this.components);
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.ttInfo = new System.Windows.Forms.ToolTip(this.components);
            this.tmrOnlineViewUpdate = new System.Windows.Forms.Timer(this.components);
            this.ttOnlineUser = new System.Windows.Forms.ToolTip(this.components);
            this.tbHeatMap = new System.Windows.Forms.TabPage();
            this.pHeatMap = new System.Windows.Forms.Panel();
            this.pbHeatMap = new System.Windows.Forms.PictureBox();
            this.pnlHeatMap = new System.Windows.Forms.Panel();
            this.gbHeatMapDate = new System.Windows.Forms.GroupBox();
            this.mtbBegin = new System.Windows.Forms.MaskedTextBox();
            this.mtbEnd = new System.Windows.Forms.MaskedTextBox();
            this.lFrom = new System.Windows.Forms.Label();
            this.lTo = new System.Windows.Forms.Label();
            this.bGenerate = new System.Windows.Forms.Button();
            this.tbWIFILocation = new System.Windows.Forms.TabPage();
            this.tbWIFI = new System.Windows.Forms.TabControl();
            this.tbWIFIMap = new System.Windows.Forms.TabPage();
            this.pbWIFIMap = new System.Windows.Forms.PictureBox();
            this.tbWIFIList = new System.Windows.Forms.TabPage();
            this.lvWIFIList = new System.Windows.Forms.ListView();
            this.colWIFIID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWIFIName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWIFIStrength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWIFIX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWIFIY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblWIFIPower = new System.Windows.Forms.Label();
            this.lblWIFIMAC = new System.Windows.Forms.Label();
            this.tbMAC = new System.Windows.Forms.TextBox();
            this.tbWIFIPower = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblWIFIY = new System.Windows.Forms.Label();
            this.lblWIFIX = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbWIFIName = new System.Windows.Forms.TextBox();
            this.tbWIFIX = new System.Windows.Forms.TextBox();
            this.tbWIFIY = new System.Windows.Forms.TextBox();
            this.tbWIFIID = new System.Windows.Forms.TextBox();
            this.btnWIFIDelete = new System.Windows.Forms.Button();
            this.btnWIFIEdit = new System.Windows.Forms.Button();
            this.btnWIFIAdd = new System.Windows.Forms.Button();
            this.btnWIFICreate = new System.Windows.Forms.Button();
            this.btnWIFIOpen = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblWIFIName = new System.Windows.Forms.Label();
            this.lblWIFIID = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnCreateWIFI = new System.Windows.Forms.Button();
            this.btnOpenWIFI = new System.Windows.Forms.Button();
            this.tbQRLocation = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tcQRLocation = new System.Windows.Forms.TabControl();
            this.tbQRMap = new System.Windows.Forms.TabPage();
            this.pbQRLocation = new System.Windows.Forms.PictureBox();
            this.tbQRList = new System.Windows.Forms.TabPage();
            this.lvQRList = new System.Windows.Forms.ListView();
            this.colQRID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colQRName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlQRLocation = new System.Windows.Forms.Panel();
            this.pbQR = new System.Windows.Forms.PictureBox();
            this.lblQRy = new System.Windows.Forms.Label();
            this.lblQRx = new System.Windows.Forms.Label();
            this.lblQRName = new System.Windows.Forms.Label();
            this.lblQRID = new System.Windows.Forms.Label();
            this.tbQRName = new System.Windows.Forms.TextBox();
            this.tbQRx = new System.Windows.Forms.TextBox();
            this.tbQRy = new System.Windows.Forms.TextBox();
            this.tbQRID = new System.Windows.Forms.TextBox();
            this.btnDeleteQR = new System.Windows.Forms.Button();
            this.btnEditQR = new System.Windows.Forms.Button();
            this.btnAddQR = new System.Windows.Forms.Button();
            this.btnCreateQRConf = new System.Windows.Forms.Button();
            this.btnOpenQRConf = new System.Windows.Forms.Button();
            this.tbManage = new System.Windows.Forms.TabPage();
            this.tbServerManage = new System.Windows.Forms.TabControl();
            this.tbLog = new System.Windows.Forms.TabPage();
            this.txtbLog = new System.Windows.Forms.TextBox();
            this.tbOnlineMap = new System.Windows.Forms.TabPage();
            this.pOnline = new System.Windows.Forms.Panel();
            this.pbOnline = new System.Windows.Forms.PictureBox();
            this.tbOnlineList = new System.Windows.Forms.TabPage();
            this.lvOnline = new System.Windows.Forms.ListView();
            this.userID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lastOnline = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tcMain = new System.Windows.Forms.TabControl();
            tbSettings = new System.Windows.Forms.TabPage();
            tbSettings.SuspendLayout();
            this.pImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapImage)).BeginInit();
            this.pSettings.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.dbFrame.SuspendLayout();
            this.tbHeatMap.SuspendLayout();
            this.pHeatMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHeatMap)).BeginInit();
            this.pnlHeatMap.SuspendLayout();
            this.gbHeatMapDate.SuspendLayout();
            this.tbWIFILocation.SuspendLayout();
            this.tbWIFI.SuspendLayout();
            this.tbWIFIMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWIFIMap)).BeginInit();
            this.tbWIFIList.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tbQRLocation.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tcQRLocation.SuspendLayout();
            this.tbQRMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRLocation)).BeginInit();
            this.tbQRList.SuspendLayout();
            this.pnlQRLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQR)).BeginInit();
            this.tbManage.SuspendLayout();
            this.tbServerManage.SuspendLayout();
            this.tbLog.SuspendLayout();
            this.tbOnlineMap.SuspendLayout();
            this.pOnline.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOnline)).BeginInit();
            this.tbOnlineList.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbSettings
            // 
            tbSettings.Controls.Add(this.pImage);
            tbSettings.Controls.Add(this.pSettings);
            tbSettings.Location = new System.Drawing.Point(4, 25);
            tbSettings.Margin = new System.Windows.Forms.Padding(4);
            tbSettings.Name = "tbSettings";
            tbSettings.Padding = new System.Windows.Forms.Padding(4);
            tbSettings.Size = new System.Drawing.Size(1496, 840);
            tbSettings.TabIndex = 0;
            tbSettings.Text = "Settings";
            tbSettings.UseVisualStyleBackColor = true;
            // 
            // pImage
            // 
            this.pImage.BackColor = System.Drawing.Color.DarkGray;
            this.pImage.Controls.Add(this.pbMapImage);
            this.pImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pImage.Location = new System.Drawing.Point(304, 4);
            this.pImage.Margin = new System.Windows.Forms.Padding(4);
            this.pImage.Name = "pImage";
            this.pImage.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.pImage.Size = new System.Drawing.Size(1188, 832);
            this.pImage.TabIndex = 1;
            // 
            // pbMapImage
            // 
            this.pbMapImage.BackColor = System.Drawing.Color.DarkGray;
            this.pbMapImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbMapImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMapImage.Location = new System.Drawing.Point(7, 6);
            this.pbMapImage.Margin = new System.Windows.Forms.Padding(4);
            this.pbMapImage.Name = "pbMapImage";
            this.pbMapImage.Size = new System.Drawing.Size(1174, 820);
            this.pbMapImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMapImage.TabIndex = 0;
            this.pbMapImage.TabStop = false;
            this.pbMapImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMapImage_MouseDown);
            this.pbMapImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMapImage_MouseMove);
            // 
            // pSettings
            // 
            this.pSettings.Controls.Add(this.gbSettings);
            this.pSettings.Controls.Add(this.dbFrame);
            this.pSettings.Controls.Add(this.btnSave);
            this.pSettings.Controls.Add(this.btnDownloadImage);
            this.pSettings.Dock = System.Windows.Forms.DockStyle.Left;
            this.pSettings.Location = new System.Drawing.Point(4, 4);
            this.pSettings.Margin = new System.Windows.Forms.Padding(4);
            this.pSettings.Name = "pSettings";
            this.pSettings.Size = new System.Drawing.Size(300, 832);
            this.pSettings.TabIndex = 0;
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.tbRealWidth);
            this.gbSettings.Controls.Add(this.lblRealWidth);
            this.gbSettings.Controls.Add(this.tbAzimuth);
            this.gbSettings.Controls.Add(this.lblRealLength);
            this.gbSettings.Controls.Add(this.lAzimuth);
            this.gbSettings.Controls.Add(this.tbRealLength);
            this.gbSettings.Location = new System.Drawing.Point(15, 109);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(265, 214);
            this.gbSettings.TabIndex = 27;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // tbRealWidth
            // 
            this.tbRealWidth.Enabled = false;
            this.tbRealWidth.Location = new System.Drawing.Point(7, 113);
            this.tbRealWidth.Margin = new System.Windows.Forms.Padding(4);
            this.tbRealWidth.Name = "tbRealWidth";
            this.tbRealWidth.Size = new System.Drawing.Size(243, 22);
            this.tbRealWidth.TabIndex = 6;
            this.tbRealWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDoubleValue_KeyPress);
            // 
            // lblRealWidth
            // 
            this.lblRealWidth.AutoSize = true;
            this.lblRealWidth.Location = new System.Drawing.Point(5, 93);
            this.lblRealWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRealWidth.Name = "lblRealWidth";
            this.lblRealWidth.Size = new System.Drawing.Size(183, 16);
            this.lblRealWidth.TabIndex = 14;
            this.lblRealWidth.Text = "Height of the marked area (m)";
            // 
            // tbAzimuth
            // 
            this.tbAzimuth.Location = new System.Drawing.Point(6, 171);
            this.tbAzimuth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbAzimuth.Name = "tbAzimuth";
            this.tbAzimuth.Size = new System.Drawing.Size(244, 22);
            this.tbAzimuth.TabIndex = 7;
            this.tbAzimuth.TextChanged += new System.EventHandler(this.tbAzimuth_TextChanged);
            this.tbAzimuth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDoubleValue_KeyPress);
            // 
            // lblRealLength
            // 
            this.lblRealLength.AutoSize = true;
            this.lblRealLength.Location = new System.Drawing.Point(7, 34);
            this.lblRealLength.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRealLength.Name = "lblRealLength";
            this.lblRealLength.Size = new System.Drawing.Size(182, 16);
            this.lblRealLength.TabIndex = 16;
            this.lblRealLength.Text = "Width of the marked area (m)";
            // 
            // lAzimuth
            // 
            this.lAzimuth.AutoSize = true;
            this.lAzimuth.Location = new System.Drawing.Point(7, 153);
            this.lAzimuth.Name = "lAzimuth";
            this.lAzimuth.Size = new System.Drawing.Size(88, 16);
            this.lAzimuth.TabIndex = 24;
            this.lAzimuth.Text = "Azimuth (deg)";
            // 
            // tbRealLength
            // 
            this.tbRealLength.Location = new System.Drawing.Point(8, 54);
            this.tbRealLength.Margin = new System.Windows.Forms.Padding(4);
            this.tbRealLength.Name = "tbRealLength";
            this.tbRealLength.Size = new System.Drawing.Size(242, 22);
            this.tbRealLength.TabIndex = 5;
            this.tbRealLength.TextChanged += new System.EventHandler(this.tbRealLength_TextChanged);
            this.tbRealLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDoubleValue_KeyPress);
            // 
            // dbFrame
            // 
            this.dbFrame.Controls.Add(this.tbCoordinateY2);
            this.dbFrame.Controls.Add(this.lblCoordinateX1);
            this.dbFrame.Controls.Add(this.lblCoordinateY1);
            this.dbFrame.Controls.Add(this.lblCoordinateX2);
            this.dbFrame.Controls.Add(this.lblCoordinateY2);
            this.dbFrame.Controls.Add(this.tbCoordinateX1);
            this.dbFrame.Controls.Add(this.tbCoordinateX2);
            this.dbFrame.Controls.Add(this.tbCoordinateY1);
            this.dbFrame.Location = new System.Drawing.Point(15, 3);
            this.dbFrame.Name = "dbFrame";
            this.dbFrame.Size = new System.Drawing.Size(266, 100);
            this.dbFrame.TabIndex = 26;
            this.dbFrame.TabStop = false;
            this.dbFrame.Text = "Frame ponts";
            // 
            // tbCoordinateY2
            // 
            this.tbCoordinateY2.Location = new System.Drawing.Point(182, 60);
            this.tbCoordinateY2.Margin = new System.Windows.Forms.Padding(4);
            this.tbCoordinateY2.Name = "tbCoordinateY2";
            this.tbCoordinateY2.Size = new System.Drawing.Size(68, 22);
            this.tbCoordinateY2.TabIndex = 4;
            this.tbCoordinateY2.TextChanged += new System.EventHandler(this.tbCoordinateY2_TextChanged);
            this.tbCoordinateY2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIntValue_KeyPress);
            // 
            // lblCoordinateX1
            // 
            this.lblCoordinateX1.AutoSize = true;
            this.lblCoordinateX1.Location = new System.Drawing.Point(10, 31);
            this.lblCoordinateX1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCoordinateX1.Name = "lblCoordinateX1";
            this.lblCoordinateX1.Size = new System.Drawing.Size(25, 16);
            this.lblCoordinateX1.TabIndex = 5;
            this.lblCoordinateX1.Text = "X1:";
            // 
            // lblCoordinateY1
            // 
            this.lblCoordinateY1.AutoSize = true;
            this.lblCoordinateY1.Location = new System.Drawing.Point(10, 63);
            this.lblCoordinateY1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCoordinateY1.Name = "lblCoordinateY1";
            this.lblCoordinateY1.Size = new System.Drawing.Size(26, 16);
            this.lblCoordinateY1.TabIndex = 7;
            this.lblCoordinateY1.Text = "Y1:";
            // 
            // lblCoordinateX2
            // 
            this.lblCoordinateX2.AutoSize = true;
            this.lblCoordinateX2.Location = new System.Drawing.Point(143, 31);
            this.lblCoordinateX2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCoordinateX2.Name = "lblCoordinateX2";
            this.lblCoordinateX2.Size = new System.Drawing.Size(25, 16);
            this.lblCoordinateX2.TabIndex = 9;
            this.lblCoordinateX2.Text = "X2:";
            // 
            // lblCoordinateY2
            // 
            this.lblCoordinateY2.AutoSize = true;
            this.lblCoordinateY2.Location = new System.Drawing.Point(143, 63);
            this.lblCoordinateY2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCoordinateY2.Name = "lblCoordinateY2";
            this.lblCoordinateY2.Size = new System.Drawing.Size(26, 16);
            this.lblCoordinateY2.TabIndex = 11;
            this.lblCoordinateY2.Text = "Y2:";
            // 
            // tbCoordinateX1
            // 
            this.tbCoordinateX1.Location = new System.Drawing.Point(48, 28);
            this.tbCoordinateX1.Margin = new System.Windows.Forms.Padding(4);
            this.tbCoordinateX1.Name = "tbCoordinateX1";
            this.tbCoordinateX1.Size = new System.Drawing.Size(68, 22);
            this.tbCoordinateX1.TabIndex = 1;
            this.tbCoordinateX1.TextChanged += new System.EventHandler(this.tbCoordinateX1_TextChanged);
            this.tbCoordinateX1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIntValue_KeyPress);
            // 
            // tbCoordinateX2
            // 
            this.tbCoordinateX2.Location = new System.Drawing.Point(182, 28);
            this.tbCoordinateX2.Margin = new System.Windows.Forms.Padding(4);
            this.tbCoordinateX2.Name = "tbCoordinateX2";
            this.tbCoordinateX2.Size = new System.Drawing.Size(68, 22);
            this.tbCoordinateX2.TabIndex = 3;
            this.tbCoordinateX2.TextChanged += new System.EventHandler(this.tbCoordinateX2_TextChanged);
            this.tbCoordinateX2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIntValue_KeyPress);
            // 
            // tbCoordinateY1
            // 
            this.tbCoordinateY1.Location = new System.Drawing.Point(48, 60);
            this.tbCoordinateY1.Margin = new System.Windows.Forms.Padding(4);
            this.tbCoordinateY1.Name = "tbCoordinateY1";
            this.tbCoordinateY1.Size = new System.Drawing.Size(68, 22);
            this.tbCoordinateY1.TabIndex = 2;
            this.tbCoordinateY1.TextChanged += new System.EventHandler(this.tbCoordinateY1_TextChanged);
            this.tbCoordinateY1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbIntValue_KeyPress);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(14, 369);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(266, 31);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDownloadImage
            // 
            this.btnDownloadImage.Location = new System.Drawing.Point(14, 330);
            this.btnDownloadImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnDownloadImage.Name = "btnDownloadImage";
            this.btnDownloadImage.Size = new System.Drawing.Size(266, 31);
            this.btnDownloadImage.TabIndex = 8;
            this.btnDownloadImage.Text = "Open Map";
            this.btnDownloadImage.UseVisualStyleBackColor = true;
            this.btnDownloadImage.Click += new System.EventHandler(this.btnDownloadImage_Click);
            // 
            // tmrListUpdate
            // 
            this.tmrListUpdate.Interval = 1000;
            this.tmrListUpdate.Tick += new System.EventHandler(this.tmrListUpdate_Tick);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.DefaultExt = "xml";
            // 
            // dlgSaveFile
            // 
            this.dlgSaveFile.DefaultExt = "xml";
            // 
            // ttInfo
            // 
            this.ttInfo.AutomaticDelay = 0;
            this.ttInfo.ShowAlways = true;
            // 
            // tmrOnlineViewUpdate
            // 
            this.tmrOnlineViewUpdate.Interval = 1000;
            this.tmrOnlineViewUpdate.Tick += new System.EventHandler(this.tmrOnlineViewUpdate_Tick);
            // 
            // tbHeatMap
            // 
            this.tbHeatMap.Controls.Add(this.pHeatMap);
            this.tbHeatMap.Controls.Add(this.pnlHeatMap);
            this.tbHeatMap.Location = new System.Drawing.Point(4, 25);
            this.tbHeatMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbHeatMap.Name = "tbHeatMap";
            this.tbHeatMap.Size = new System.Drawing.Size(1496, 840);
            this.tbHeatMap.TabIndex = 2;
            this.tbHeatMap.Text = "Heat Map";
            this.tbHeatMap.UseVisualStyleBackColor = true;
            // 
            // pHeatMap
            // 
            this.pHeatMap.BackColor = System.Drawing.Color.DarkGray;
            this.pHeatMap.Controls.Add(this.pbHeatMap);
            this.pHeatMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pHeatMap.Location = new System.Drawing.Point(300, 0);
            this.pHeatMap.Margin = new System.Windows.Forms.Padding(4);
            this.pHeatMap.Name = "pHeatMap";
            this.pHeatMap.Padding = new System.Windows.Forms.Padding(4);
            this.pHeatMap.Size = new System.Drawing.Size(1196, 840);
            this.pHeatMap.TabIndex = 3;
            // 
            // pbHeatMap
            // 
            this.pbHeatMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbHeatMap.Location = new System.Drawing.Point(4, 4);
            this.pbHeatMap.Margin = new System.Windows.Forms.Padding(4);
            this.pbHeatMap.Name = "pbHeatMap";
            this.pbHeatMap.Size = new System.Drawing.Size(1188, 832);
            this.pbHeatMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHeatMap.TabIndex = 0;
            this.pbHeatMap.TabStop = false;
            // 
            // pnlHeatMap
            // 
            this.pnlHeatMap.Controls.Add(this.gbHeatMapDate);
            this.pnlHeatMap.Controls.Add(this.bGenerate);
            this.pnlHeatMap.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlHeatMap.Location = new System.Drawing.Point(0, 0);
            this.pnlHeatMap.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeatMap.Name = "pnlHeatMap";
            this.pnlHeatMap.Size = new System.Drawing.Size(300, 840);
            this.pnlHeatMap.TabIndex = 7;
            // 
            // gbHeatMapDate
            // 
            this.gbHeatMapDate.Controls.Add(this.mtbBegin);
            this.gbHeatMapDate.Controls.Add(this.mtbEnd);
            this.gbHeatMapDate.Controls.Add(this.lFrom);
            this.gbHeatMapDate.Controls.Add(this.lTo);
            this.gbHeatMapDate.Location = new System.Drawing.Point(8, 4);
            this.gbHeatMapDate.Name = "gbHeatMapDate";
            this.gbHeatMapDate.Size = new System.Drawing.Size(276, 152);
            this.gbHeatMapDate.TabIndex = 7;
            this.gbHeatMapDate.TabStop = false;
            this.gbHeatMapDate.Text = "Time interval";
            // 
            // mtbBegin
            // 
            this.mtbBegin.Location = new System.Drawing.Point(7, 53);
            this.mtbBegin.Margin = new System.Windows.Forms.Padding(4);
            this.mtbBegin.Mask = "00/00/0000 90:00:00";
            this.mtbBegin.Name = "mtbBegin";
            this.mtbBegin.Size = new System.Drawing.Size(262, 22);
            this.mtbBegin.TabIndex = 0;
            this.mtbBegin.Text = "26042022000000";
            this.mtbBegin.ValidatingType = typeof(System.DateTime);
            // 
            // mtbEnd
            // 
            this.mtbEnd.Location = new System.Drawing.Point(7, 109);
            this.mtbEnd.Margin = new System.Windows.Forms.Padding(4);
            this.mtbEnd.Mask = "00/00/0000 90:00:00";
            this.mtbEnd.Name = "mtbEnd";
            this.mtbEnd.Size = new System.Drawing.Size(262, 22);
            this.mtbEnd.TabIndex = 1;
            this.mtbEnd.Text = "30042022000000";
            this.mtbEnd.ValidatingType = typeof(System.DateTime);
            // 
            // lFrom
            // 
            this.lFrom.AutoSize = true;
            this.lFrom.Location = new System.Drawing.Point(4, 33);
            this.lFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lFrom.Name = "lFrom";
            this.lFrom.Size = new System.Drawing.Size(67, 16);
            this.lFrom.TabIndex = 5;
            this.lFrom.Text = "Time from";
            // 
            // lTo
            // 
            this.lTo.AutoSize = true;
            this.lTo.Location = new System.Drawing.Point(4, 89);
            this.lTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTo.Name = "lTo";
            this.lTo.Size = new System.Drawing.Size(52, 16);
            this.lTo.TabIndex = 6;
            this.lTo.Text = "Time to";
            // 
            // bGenerate
            // 
            this.bGenerate.Location = new System.Drawing.Point(8, 163);
            this.bGenerate.Margin = new System.Windows.Forms.Padding(4);
            this.bGenerate.Name = "bGenerate";
            this.bGenerate.Size = new System.Drawing.Size(276, 34);
            this.bGenerate.TabIndex = 2;
            this.bGenerate.Text = "Generate heat map";
            this.bGenerate.UseVisualStyleBackColor = true;
            this.bGenerate.Click += new System.EventHandler(this.bGenerate_Click);
            // 
            // tbWIFILocation
            // 
            this.tbWIFILocation.Controls.Add(this.tbWIFI);
            this.tbWIFILocation.Controls.Add(this.panel3);
            this.tbWIFILocation.Location = new System.Drawing.Point(4, 25);
            this.tbWIFILocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFILocation.Name = "tbWIFILocation";
            this.tbWIFILocation.Size = new System.Drawing.Size(1496, 840);
            this.tbWIFILocation.TabIndex = 5;
            this.tbWIFILocation.Text = "Wi-Fi location";
            this.tbWIFILocation.UseVisualStyleBackColor = true;
            // 
            // tbWIFI
            // 
            this.tbWIFI.Controls.Add(this.tbWIFIMap);
            this.tbWIFI.Controls.Add(this.tbWIFIList);
            this.tbWIFI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbWIFI.Location = new System.Drawing.Point(300, 0);
            this.tbWIFI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFI.Name = "tbWIFI";
            this.tbWIFI.SelectedIndex = 0;
            this.tbWIFI.Size = new System.Drawing.Size(1196, 840);
            this.tbWIFI.TabIndex = 11;
            // 
            // tbWIFIMap
            // 
            this.tbWIFIMap.Controls.Add(this.pbWIFIMap);
            this.tbWIFIMap.Location = new System.Drawing.Point(4, 25);
            this.tbWIFIMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFIMap.Name = "tbWIFIMap";
            this.tbWIFIMap.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFIMap.Size = new System.Drawing.Size(1188, 811);
            this.tbWIFIMap.TabIndex = 0;
            this.tbWIFIMap.Text = "Map";
            this.tbWIFIMap.UseVisualStyleBackColor = true;
            // 
            // pbWIFIMap
            // 
            this.pbWIFIMap.BackColor = System.Drawing.Color.DarkGray;
            this.pbWIFIMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbWIFIMap.Location = new System.Drawing.Point(3, 2);
            this.pbWIFIMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbWIFIMap.Name = "pbWIFIMap";
            this.pbWIFIMap.Size = new System.Drawing.Size(1182, 807);
            this.pbWIFIMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbWIFIMap.TabIndex = 0;
            this.pbWIFIMap.TabStop = false;
            this.pbWIFIMap.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbWIFIMap_MouseDoubleClick);
            this.pbWIFIMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbWIFIMap_MouseDown);
            this.pbWIFIMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbWIFIMap_MouseMove);
            this.pbWIFIMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbWIFIMap_MouseUp);
            // 
            // tbWIFIList
            // 
            this.tbWIFIList.Controls.Add(this.lvWIFIList);
            this.tbWIFIList.Location = new System.Drawing.Point(4, 25);
            this.tbWIFIList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFIList.Name = "tbWIFIList";
            this.tbWIFIList.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFIList.Size = new System.Drawing.Size(1188, 811);
            this.tbWIFIList.TabIndex = 1;
            this.tbWIFIList.Text = "Wi-Fi List";
            this.tbWIFIList.UseVisualStyleBackColor = true;
            // 
            // lvWIFIList
            // 
            this.lvWIFIList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colWIFIID,
            this.colWIFIName,
            this.colMAC,
            this.colWIFIStrength,
            this.colWIFIX,
            this.colWIFIY});
            this.lvWIFIList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvWIFIList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvWIFIList.FullRowSelect = true;
            this.lvWIFIList.GridLines = true;
            this.lvWIFIList.HideSelection = false;
            this.lvWIFIList.Location = new System.Drawing.Point(3, 2);
            this.lvWIFIList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvWIFIList.Name = "lvWIFIList";
            this.lvWIFIList.Size = new System.Drawing.Size(1182, 807);
            this.lvWIFIList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvWIFIList.TabIndex = 0;
            this.lvWIFIList.UseCompatibleStateImageBehavior = false;
            this.lvWIFIList.View = System.Windows.Forms.View.Details;
            this.lvWIFIList.SelectedIndexChanged += new System.EventHandler(this.lvWIFIList_SelectedIndexChanged);
            // 
            // colWIFIID
            // 
            this.colWIFIID.Text = "Wi-Fi ID";
            this.colWIFIID.Width = 100;
            // 
            // colWIFIName
            // 
            this.colWIFIName.Text = "Wi-FI Name";
            this.colWIFIName.Width = 200;
            // 
            // colMAC
            // 
            this.colMAC.Text = "MAC-address";
            this.colMAC.Width = 180;
            // 
            // colWIFIStrength
            // 
            this.colWIFIStrength.Text = "Strength";
            this.colWIFIStrength.Width = 100;
            // 
            // colWIFIX
            // 
            this.colWIFIX.Text = "X";
            this.colWIFIX.Width = 100;
            // 
            // colWIFIY
            // 
            this.colWIFIY.Text = "Y";
            this.colWIFIY.Width = 100;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lblWIFIName);
            this.panel3.Controls.Add(this.lblWIFIID);
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.textBox2);
            this.panel3.Controls.Add(this.textBox3);
            this.panel3.Controls.Add(this.textBox4);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.btnCreateWIFI);
            this.panel3.Controls.Add(this.btnOpenWIFI);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(300, 840);
            this.panel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblWIFIPower);
            this.panel4.Controls.Add(this.lblWIFIMAC);
            this.panel4.Controls.Add(this.tbMAC);
            this.panel4.Controls.Add(this.tbWIFIPower);
            this.panel4.Controls.Add(this.pictureBox3);
            this.panel4.Controls.Add(this.lblWIFIY);
            this.panel4.Controls.Add(this.lblWIFIX);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.tbWIFIName);
            this.panel4.Controls.Add(this.tbWIFIX);
            this.panel4.Controls.Add(this.tbWIFIY);
            this.panel4.Controls.Add(this.tbWIFIID);
            this.panel4.Controls.Add(this.btnWIFIDelete);
            this.panel4.Controls.Add(this.btnWIFIEdit);
            this.panel4.Controls.Add(this.btnWIFIAdd);
            this.panel4.Controls.Add(this.btnWIFICreate);
            this.panel4.Controls.Add(this.btnWIFIOpen);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(300, 840);
            this.panel4.TabIndex = 14;
            // 
            // lblWIFIPower
            // 
            this.lblWIFIPower.AutoSize = true;
            this.lblWIFIPower.Location = new System.Drawing.Point(149, 181);
            this.lblWIFIPower.Name = "lblWIFIPower";
            this.lblWIFIPower.Size = new System.Drawing.Size(84, 16);
            this.lblWIFIPower.TabIndex = 17;
            this.lblWIFIPower.Text = "Power (dBm)";
            // 
            // lblWIFIMAC
            // 
            this.lblWIFIMAC.AutoSize = true;
            this.lblWIFIMAC.Location = new System.Drawing.Point(21, 181);
            this.lblWIFIMAC.Name = "lblWIFIMAC";
            this.lblWIFIMAC.Size = new System.Drawing.Size(90, 16);
            this.lblWIFIMAC.TabIndex = 16;
            this.lblWIFIMAC.Text = "MAC-address";
            // 
            // tbMAC
            // 
            this.tbMAC.Location = new System.Drawing.Point(24, 199);
            this.tbMAC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbMAC.Name = "tbMAC";
            this.tbMAC.Size = new System.Drawing.Size(120, 22);
            this.tbMAC.TabIndex = 4;
            // 
            // tbWIFIPower
            // 
            this.tbWIFIPower.Location = new System.Drawing.Point(151, 199);
            this.tbWIFIPower.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFIPower.Name = "tbWIFIPower";
            this.tbWIFIPower.Size = new System.Drawing.Size(120, 22);
            this.tbWIFIPower.TabIndex = 5;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(11, 530);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(274, 274);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // lblWIFIY
            // 
            this.lblWIFIY.AutoSize = true;
            this.lblWIFIY.Location = new System.Drawing.Point(148, 240);
            this.lblWIFIY.Name = "lblWIFIY";
            this.lblWIFIY.Size = new System.Drawing.Size(50, 16);
            this.lblWIFIY.TabIndex = 12;
            this.lblWIFIY.Text = "Wi-Fi Y";
            // 
            // lblWIFIX
            // 
            this.lblWIFIX.AutoSize = true;
            this.lblWIFIX.Location = new System.Drawing.Point(21, 240);
            this.lblWIFIX.Name = "lblWIFIX";
            this.lblWIFIX.Size = new System.Drawing.Size(49, 16);
            this.lblWIFIX.TabIndex = 11;
            this.lblWIFIX.Text = "Wi-Fi X";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(148, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Wi-Fi Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "Wi-Fi ID";
            // 
            // tbWIFIName
            // 
            this.tbWIFIName.Location = new System.Drawing.Point(151, 126);
            this.tbWIFIName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFIName.Name = "tbWIFIName";
            this.tbWIFIName.Size = new System.Drawing.Size(120, 22);
            this.tbWIFIName.TabIndex = 3;
            // 
            // tbWIFIX
            // 
            this.tbWIFIX.Location = new System.Drawing.Point(24, 258);
            this.tbWIFIX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFIX.Name = "tbWIFIX";
            this.tbWIFIX.Size = new System.Drawing.Size(120, 22);
            this.tbWIFIX.TabIndex = 6;
            // 
            // tbWIFIY
            // 
            this.tbWIFIY.Location = new System.Drawing.Point(151, 258);
            this.tbWIFIY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFIY.Name = "tbWIFIY";
            this.tbWIFIY.Size = new System.Drawing.Size(120, 22);
            this.tbWIFIY.TabIndex = 7;
            // 
            // tbWIFIID
            // 
            this.tbWIFIID.Location = new System.Drawing.Point(24, 126);
            this.tbWIFIID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbWIFIID.Name = "tbWIFIID";
            this.tbWIFIID.Size = new System.Drawing.Size(120, 22);
            this.tbWIFIID.TabIndex = 2;
            // 
            // btnWIFIDelete
            // 
            this.btnWIFIDelete.Location = new System.Drawing.Point(24, 375);
            this.btnWIFIDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWIFIDelete.Name = "btnWIFIDelete";
            this.btnWIFIDelete.Size = new System.Drawing.Size(247, 28);
            this.btnWIFIDelete.TabIndex = 10;
            this.btnWIFIDelete.Text = "Delete Wi-Fi";
            this.btnWIFIDelete.UseVisualStyleBackColor = true;
            this.btnWIFIDelete.Click += new System.EventHandler(this.btnWIFIDelete_Click);
            // 
            // btnWIFIEdit
            // 
            this.btnWIFIEdit.Location = new System.Drawing.Point(24, 341);
            this.btnWIFIEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWIFIEdit.Name = "btnWIFIEdit";
            this.btnWIFIEdit.Size = new System.Drawing.Size(247, 28);
            this.btnWIFIEdit.TabIndex = 9;
            this.btnWIFIEdit.Text = "Edit Wi-Fi";
            this.btnWIFIEdit.UseVisualStyleBackColor = true;
            this.btnWIFIEdit.Click += new System.EventHandler(this.btnWIFIEdit_Click);
            // 
            // btnWIFIAdd
            // 
            this.btnWIFIAdd.Location = new System.Drawing.Point(24, 306);
            this.btnWIFIAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWIFIAdd.Name = "btnWIFIAdd";
            this.btnWIFIAdd.Size = new System.Drawing.Size(247, 28);
            this.btnWIFIAdd.TabIndex = 8;
            this.btnWIFIAdd.Text = "Add Wi-Fi";
            this.btnWIFIAdd.UseVisualStyleBackColor = true;
            this.btnWIFIAdd.Click += new System.EventHandler(this.btnWIFIAdd_Click);
            // 
            // btnWIFICreate
            // 
            this.btnWIFICreate.Location = new System.Drawing.Point(24, 59);
            this.btnWIFICreate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWIFICreate.Name = "btnWIFICreate";
            this.btnWIFICreate.Size = new System.Drawing.Size(247, 28);
            this.btnWIFICreate.TabIndex = 1;
            this.btnWIFICreate.Text = "Create Wi-Fi config";
            this.btnWIFICreate.UseVisualStyleBackColor = true;
            this.btnWIFICreate.Click += new System.EventHandler(this.btnWIFICreate_Click);
            // 
            // btnWIFIOpen
            // 
            this.btnWIFIOpen.Location = new System.Drawing.Point(24, 25);
            this.btnWIFIOpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWIFIOpen.Name = "btnWIFIOpen";
            this.btnWIFIOpen.Size = new System.Drawing.Size(247, 28);
            this.btnWIFIOpen.TabIndex = 0;
            this.btnWIFIOpen.Text = "Open Wi-Fi config";
            this.btnWIFIOpen.UseVisualStyleBackColor = true;
            this.btnWIFIOpen.Click += new System.EventHandler(this.btnWIFIOpen_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(11, 530);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(274, 274);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "QR Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "QR X";
            // 
            // lblWIFIName
            // 
            this.lblWIFIName.AutoSize = true;
            this.lblWIFIName.Location = new System.Drawing.Point(148, 107);
            this.lblWIFIName.Name = "lblWIFIName";
            this.lblWIFIName.Size = new System.Drawing.Size(78, 16);
            this.lblWIFIName.TabIndex = 10;
            this.lblWIFIName.Text = "Wi-Fi Name";
            // 
            // lblWIFIID
            // 
            this.lblWIFIID.AutoSize = true;
            this.lblWIFIID.Location = new System.Drawing.Point(21, 107);
            this.lblWIFIID.Name = "lblWIFIID";
            this.lblWIFIID.Size = new System.Drawing.Size(54, 16);
            this.lblWIFIID.TabIndex = 9;
            this.lblWIFIID.Text = "Wi-Fi ID";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(151, 126);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 22);
            this.textBox1.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(24, 258);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(120, 22);
            this.textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(151, 258);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(120, 22);
            this.textBox3.TabIndex = 6;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(24, 126);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(120, 22);
            this.textBox4.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 375);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(247, 28);
            this.button1.TabIndex = 4;
            this.button1.Text = "Delete QR";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(24, 341);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(247, 28);
            this.button2.TabIndex = 3;
            this.button2.Text = "Edit QR";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(24, 306);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(247, 28);
            this.button3.TabIndex = 2;
            this.button3.Text = "Add QR";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // btnCreateWIFI
            // 
            this.btnCreateWIFI.Location = new System.Drawing.Point(24, 59);
            this.btnCreateWIFI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateWIFI.Name = "btnCreateWIFI";
            this.btnCreateWIFI.Size = new System.Drawing.Size(247, 28);
            this.btnCreateWIFI.TabIndex = 1;
            this.btnCreateWIFI.Text = "Create Wi-Fi config";
            this.btnCreateWIFI.UseVisualStyleBackColor = true;
            // 
            // btnOpenWIFI
            // 
            this.btnOpenWIFI.Location = new System.Drawing.Point(24, 25);
            this.btnOpenWIFI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenWIFI.Name = "btnOpenWIFI";
            this.btnOpenWIFI.Size = new System.Drawing.Size(247, 28);
            this.btnOpenWIFI.TabIndex = 0;
            this.btnOpenWIFI.Text = "Open Wi-Fi config";
            this.btnOpenWIFI.UseVisualStyleBackColor = true;
            // 
            // tbQRLocation
            // 
            this.tbQRLocation.Controls.Add(this.panel2);
            this.tbQRLocation.Controls.Add(this.pnlQRLocation);
            this.tbQRLocation.Location = new System.Drawing.Point(4, 25);
            this.tbQRLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbQRLocation.Name = "tbQRLocation";
            this.tbQRLocation.Size = new System.Drawing.Size(1496, 840);
            this.tbQRLocation.TabIndex = 3;
            this.tbQRLocation.Text = "QR location";
            this.tbQRLocation.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tcQRLocation);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(300, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1196, 840);
            this.panel2.TabIndex = 2;
            // 
            // tcQRLocation
            // 
            this.tcQRLocation.Controls.Add(this.tbQRMap);
            this.tcQRLocation.Controls.Add(this.tbQRList);
            this.tcQRLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcQRLocation.Location = new System.Drawing.Point(0, 0);
            this.tcQRLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tcQRLocation.Name = "tcQRLocation";
            this.tcQRLocation.SelectedIndex = 0;
            this.tcQRLocation.Size = new System.Drawing.Size(1196, 840);
            this.tcQRLocation.TabIndex = 9;
            // 
            // tbQRMap
            // 
            this.tbQRMap.Controls.Add(this.pbQRLocation);
            this.tbQRMap.Location = new System.Drawing.Point(4, 25);
            this.tbQRMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbQRMap.Name = "tbQRMap";
            this.tbQRMap.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbQRMap.Size = new System.Drawing.Size(1188, 811);
            this.tbQRMap.TabIndex = 0;
            this.tbQRMap.Text = "Map";
            this.tbQRMap.UseVisualStyleBackColor = true;
            // 
            // pbQRLocation
            // 
            this.pbQRLocation.BackColor = System.Drawing.Color.DarkGray;
            this.pbQRLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbQRLocation.Location = new System.Drawing.Point(3, 2);
            this.pbQRLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbQRLocation.Name = "pbQRLocation";
            this.pbQRLocation.Size = new System.Drawing.Size(1182, 807);
            this.pbQRLocation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbQRLocation.TabIndex = 0;
            this.pbQRLocation.TabStop = false;
            this.pbQRLocation.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbQRLocation_MouseDoubleClick);
            this.pbQRLocation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbQRLocation_MouseDown);
            this.pbQRLocation.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbQRLocation_MouseMove);
            this.pbQRLocation.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbQRLocation_MouseUp);
            // 
            // tbQRList
            // 
            this.tbQRList.Controls.Add(this.lvQRList);
            this.tbQRList.Location = new System.Drawing.Point(4, 25);
            this.tbQRList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbQRList.Name = "tbQRList";
            this.tbQRList.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbQRList.Size = new System.Drawing.Size(1188, 811);
            this.tbQRList.TabIndex = 1;
            this.tbQRList.Text = "QR List";
            this.tbQRList.UseVisualStyleBackColor = true;
            // 
            // lvQRList
            // 
            this.lvQRList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colQRID,
            this.colQRName,
            this.colX,
            this.colY});
            this.lvQRList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lvQRList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvQRList.FullRowSelect = true;
            this.lvQRList.GridLines = true;
            this.lvQRList.HideSelection = false;
            this.lvQRList.Location = new System.Drawing.Point(3, 2);
            this.lvQRList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvQRList.Name = "lvQRList";
            this.lvQRList.Size = new System.Drawing.Size(1182, 807);
            this.lvQRList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvQRList.TabIndex = 0;
            this.lvQRList.UseCompatibleStateImageBehavior = false;
            this.lvQRList.View = System.Windows.Forms.View.Details;
            this.lvQRList.SelectedIndexChanged += new System.EventHandler(this.lvQRList_SelectedIndexChanged);
            // 
            // colQRID
            // 
            this.colQRID.Text = "QR ID";
            this.colQRID.Width = 100;
            // 
            // colQRName
            // 
            this.colQRName.Text = "QR Name";
            this.colQRName.Width = 300;
            // 
            // colX
            // 
            this.colX.Text = "X";
            this.colX.Width = 100;
            // 
            // colY
            // 
            this.colY.Text = "Y";
            this.colY.Width = 100;
            // 
            // pnlQRLocation
            // 
            this.pnlQRLocation.Controls.Add(this.pbQR);
            this.pnlQRLocation.Controls.Add(this.lblQRy);
            this.pnlQRLocation.Controls.Add(this.lblQRx);
            this.pnlQRLocation.Controls.Add(this.lblQRName);
            this.pnlQRLocation.Controls.Add(this.lblQRID);
            this.pnlQRLocation.Controls.Add(this.tbQRName);
            this.pnlQRLocation.Controls.Add(this.tbQRx);
            this.pnlQRLocation.Controls.Add(this.tbQRy);
            this.pnlQRLocation.Controls.Add(this.tbQRID);
            this.pnlQRLocation.Controls.Add(this.btnDeleteQR);
            this.pnlQRLocation.Controls.Add(this.btnEditQR);
            this.pnlQRLocation.Controls.Add(this.btnAddQR);
            this.pnlQRLocation.Controls.Add(this.btnCreateQRConf);
            this.pnlQRLocation.Controls.Add(this.btnOpenQRConf);
            this.pnlQRLocation.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlQRLocation.Location = new System.Drawing.Point(0, 0);
            this.pnlQRLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlQRLocation.Name = "pnlQRLocation";
            this.pnlQRLocation.Size = new System.Drawing.Size(300, 840);
            this.pnlQRLocation.TabIndex = 0;
            // 
            // pbQR
            // 
            this.pbQR.Location = new System.Drawing.Point(11, 530);
            this.pbQR.Margin = new System.Windows.Forms.Padding(4);
            this.pbQR.Name = "pbQR";
            this.pbQR.Size = new System.Drawing.Size(274, 274);
            this.pbQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbQR.TabIndex = 13;
            this.pbQR.TabStop = false;
            // 
            // lblQRy
            // 
            this.lblQRy.AutoSize = true;
            this.lblQRy.Location = new System.Drawing.Point(148, 169);
            this.lblQRy.Name = "lblQRy";
            this.lblQRy.Size = new System.Drawing.Size(39, 16);
            this.lblQRy.TabIndex = 12;
            this.lblQRy.Text = "QR Y";
            // 
            // lblQRx
            // 
            this.lblQRx.AutoSize = true;
            this.lblQRx.Location = new System.Drawing.Point(21, 169);
            this.lblQRx.Name = "lblQRx";
            this.lblQRx.Size = new System.Drawing.Size(38, 16);
            this.lblQRx.TabIndex = 11;
            this.lblQRx.Text = "QR X";
            // 
            // lblQRName
            // 
            this.lblQRName.AutoSize = true;
            this.lblQRName.Location = new System.Drawing.Point(148, 107);
            this.lblQRName.Name = "lblQRName";
            this.lblQRName.Size = new System.Drawing.Size(67, 16);
            this.lblQRName.TabIndex = 10;
            this.lblQRName.Text = "QR Name";
            // 
            // lblQRID
            // 
            this.lblQRID.AutoSize = true;
            this.lblQRID.Location = new System.Drawing.Point(21, 107);
            this.lblQRID.Name = "lblQRID";
            this.lblQRID.Size = new System.Drawing.Size(43, 16);
            this.lblQRID.TabIndex = 9;
            this.lblQRID.Text = "QR ID";
            // 
            // tbQRName
            // 
            this.tbQRName.Location = new System.Drawing.Point(151, 126);
            this.tbQRName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbQRName.Name = "tbQRName";
            this.tbQRName.Size = new System.Drawing.Size(120, 22);
            this.tbQRName.TabIndex = 3;
            // 
            // tbQRx
            // 
            this.tbQRx.Location = new System.Drawing.Point(24, 188);
            this.tbQRx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbQRx.Name = "tbQRx";
            this.tbQRx.Size = new System.Drawing.Size(120, 22);
            this.tbQRx.TabIndex = 4;
            // 
            // tbQRy
            // 
            this.tbQRy.Location = new System.Drawing.Point(151, 188);
            this.tbQRy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbQRy.Name = "tbQRy";
            this.tbQRy.Size = new System.Drawing.Size(120, 22);
            this.tbQRy.TabIndex = 5;
            // 
            // tbQRID
            // 
            this.tbQRID.Location = new System.Drawing.Point(24, 126);
            this.tbQRID.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbQRID.Name = "tbQRID";
            this.tbQRID.Size = new System.Drawing.Size(120, 22);
            this.tbQRID.TabIndex = 2;
            // 
            // btnDeleteQR
            // 
            this.btnDeleteQR.Location = new System.Drawing.Point(24, 304);
            this.btnDeleteQR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeleteQR.Name = "btnDeleteQR";
            this.btnDeleteQR.Size = new System.Drawing.Size(247, 28);
            this.btnDeleteQR.TabIndex = 8;
            this.btnDeleteQR.Text = "Delete QR";
            this.btnDeleteQR.UseVisualStyleBackColor = true;
            this.btnDeleteQR.Click += new System.EventHandler(this.btnDeleteQR_Click);
            // 
            // btnEditQR
            // 
            this.btnEditQR.Location = new System.Drawing.Point(24, 270);
            this.btnEditQR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEditQR.Name = "btnEditQR";
            this.btnEditQR.Size = new System.Drawing.Size(247, 28);
            this.btnEditQR.TabIndex = 7;
            this.btnEditQR.Text = "Edit QR";
            this.btnEditQR.UseVisualStyleBackColor = true;
            this.btnEditQR.Click += new System.EventHandler(this.btnEditQR_Click);
            // 
            // btnAddQR
            // 
            this.btnAddQR.Location = new System.Drawing.Point(24, 236);
            this.btnAddQR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAddQR.Name = "btnAddQR";
            this.btnAddQR.Size = new System.Drawing.Size(247, 28);
            this.btnAddQR.TabIndex = 6;
            this.btnAddQR.Text = "Add QR";
            this.btnAddQR.UseVisualStyleBackColor = true;
            this.btnAddQR.Click += new System.EventHandler(this.btnAddQR_Click);
            // 
            // btnCreateQRConf
            // 
            this.btnCreateQRConf.Location = new System.Drawing.Point(24, 59);
            this.btnCreateQRConf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreateQRConf.Name = "btnCreateQRConf";
            this.btnCreateQRConf.Size = new System.Drawing.Size(247, 28);
            this.btnCreateQRConf.TabIndex = 1;
            this.btnCreateQRConf.Text = "Create QR config";
            this.btnCreateQRConf.UseVisualStyleBackColor = true;
            this.btnCreateQRConf.Click += new System.EventHandler(this.btnCreateQRConf_Click);
            // 
            // btnOpenQRConf
            // 
            this.btnOpenQRConf.Location = new System.Drawing.Point(24, 25);
            this.btnOpenQRConf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenQRConf.Name = "btnOpenQRConf";
            this.btnOpenQRConf.Size = new System.Drawing.Size(247, 28);
            this.btnOpenQRConf.TabIndex = 0;
            this.btnOpenQRConf.Text = "Open QR config";
            this.btnOpenQRConf.UseVisualStyleBackColor = true;
            this.btnOpenQRConf.Click += new System.EventHandler(this.btnOpenQRConf_Click);
            // 
            // tbManage
            // 
            this.tbManage.Controls.Add(this.tbServerManage);
            this.tbManage.Controls.Add(this.panel1);
            this.tbManage.Location = new System.Drawing.Point(4, 25);
            this.tbManage.Margin = new System.Windows.Forms.Padding(4);
            this.tbManage.Name = "tbManage";
            this.tbManage.Padding = new System.Windows.Forms.Padding(4);
            this.tbManage.Size = new System.Drawing.Size(1496, 840);
            this.tbManage.TabIndex = 1;
            this.tbManage.Text = "Management";
            this.tbManage.UseVisualStyleBackColor = true;
            // 
            // tbServerManage
            // 
            this.tbServerManage.Controls.Add(this.tbLog);
            this.tbServerManage.Controls.Add(this.tbOnlineMap);
            this.tbServerManage.Controls.Add(this.tbOnlineList);
            this.tbServerManage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbServerManage.Location = new System.Drawing.Point(304, 4);
            this.tbServerManage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbServerManage.Name = "tbServerManage";
            this.tbServerManage.SelectedIndex = 0;
            this.tbServerManage.Size = new System.Drawing.Size(1188, 832);
            this.tbServerManage.TabIndex = 1;
            this.tbServerManage.Selected += new System.Windows.Forms.TabControlEventHandler(this.tbServerManage_Selected);
            // 
            // tbLog
            // 
            this.tbLog.Controls.Add(this.txtbLog);
            this.tbLog.Location = new System.Drawing.Point(4, 25);
            this.tbLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLog.Name = "tbLog";
            this.tbLog.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbLog.Size = new System.Drawing.Size(1180, 803);
            this.tbLog.TabIndex = 0;
            this.tbLog.Text = "Log";
            this.tbLog.UseVisualStyleBackColor = true;
            // 
            // txtbLog
            // 
            this.txtbLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbLog.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbLog.Location = new System.Drawing.Point(3, 2);
            this.txtbLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtbLog.Multiline = true;
            this.txtbLog.Name = "txtbLog";
            this.txtbLog.ReadOnly = true;
            this.txtbLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtbLog.Size = new System.Drawing.Size(1174, 799);
            this.txtbLog.TabIndex = 0;
            this.txtbLog.TabStop = false;
            // 
            // tbOnlineMap
            // 
            this.tbOnlineMap.Controls.Add(this.pOnline);
            this.tbOnlineMap.Location = new System.Drawing.Point(4, 25);
            this.tbOnlineMap.Margin = new System.Windows.Forms.Padding(4);
            this.tbOnlineMap.Name = "tbOnlineMap";
            this.tbOnlineMap.Size = new System.Drawing.Size(1180, 803);
            this.tbOnlineMap.TabIndex = 5;
            this.tbOnlineMap.Text = "Online Map";
            this.tbOnlineMap.UseVisualStyleBackColor = true;
            // 
            // pOnline
            // 
            this.pOnline.BackColor = System.Drawing.Color.DarkGray;
            this.pOnline.Controls.Add(this.pbOnline);
            this.pOnline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pOnline.Location = new System.Drawing.Point(0, 0);
            this.pOnline.Margin = new System.Windows.Forms.Padding(4);
            this.pOnline.Name = "pOnline";
            this.pOnline.Padding = new System.Windows.Forms.Padding(4);
            this.pOnline.Size = new System.Drawing.Size(1180, 803);
            this.pOnline.TabIndex = 0;
            // 
            // pbOnline
            // 
            this.pbOnline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOnline.Location = new System.Drawing.Point(4, 4);
            this.pbOnline.Margin = new System.Windows.Forms.Padding(4);
            this.pbOnline.Name = "pbOnline";
            this.pbOnline.Size = new System.Drawing.Size(1172, 795);
            this.pbOnline.TabIndex = 0;
            this.pbOnline.TabStop = false;
            this.pbOnline.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbOnline_MouseDown);
            // 
            // tbOnlineList
            // 
            this.tbOnlineList.Controls.Add(this.lvOnline);
            this.tbOnlineList.Location = new System.Drawing.Point(4, 25);
            this.tbOnlineList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbOnlineList.Name = "tbOnlineList";
            this.tbOnlineList.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbOnlineList.Size = new System.Drawing.Size(1180, 803);
            this.tbOnlineList.TabIndex = 1;
            this.tbOnlineList.Text = "Online List";
            this.tbOnlineList.UseVisualStyleBackColor = true;
            // 
            // lvOnline
            // 
            this.lvOnline.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.userID,
            this.lastOnline});
            this.lvOnline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvOnline.GridLines = true;
            this.lvOnline.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvOnline.HideSelection = false;
            this.lvOnline.Location = new System.Drawing.Point(3, 2);
            this.lvOnline.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lvOnline.MultiSelect = false;
            this.lvOnline.Name = "lvOnline";
            this.lvOnline.Size = new System.Drawing.Size(1174, 799);
            this.lvOnline.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvOnline.TabIndex = 0;
            this.lvOnline.UseCompatibleStateImageBehavior = false;
            this.lvOnline.View = System.Windows.Forms.View.Details;
            // 
            // userID
            // 
            this.userID.Text = "User ID";
            this.userID.Width = 300;
            // 
            // lastOnline
            // 
            this.lastOnline.Text = "Last Online";
            this.lastOnline.Width = 500;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnStop);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 832);
            this.panel1.TabIndex = 0;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(25, 78);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(247, 37);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop server";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(25, 36);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(247, 37);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start server";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(tbSettings);
            this.tcMain.Controls.Add(this.tbManage);
            this.tcMain.Controls.Add(this.tbQRLocation);
            this.tcMain.Controls.Add(this.tbWIFILocation);
            this.tcMain.Controls.Add(this.tbHeatMap);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Margin = new System.Windows.Forms.Padding(4);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(1504, 869);
            this.tcMain.TabIndex = 0;
            this.tcMain.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcMain_Selecting);
            this.tcMain.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcMain_Selected);
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1504, 869);
            this.Controls.Add(this.tcMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmServer_FormClosing);
            tbSettings.ResumeLayout(false);
            this.pImage.ResumeLayout(false);
            this.pImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapImage)).EndInit();
            this.pSettings.ResumeLayout(false);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.dbFrame.ResumeLayout(false);
            this.dbFrame.PerformLayout();
            this.tbHeatMap.ResumeLayout(false);
            this.pHeatMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHeatMap)).EndInit();
            this.pnlHeatMap.ResumeLayout(false);
            this.gbHeatMapDate.ResumeLayout(false);
            this.gbHeatMapDate.PerformLayout();
            this.tbWIFILocation.ResumeLayout(false);
            this.tbWIFI.ResumeLayout(false);
            this.tbWIFIMap.ResumeLayout(false);
            this.tbWIFIMap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWIFIMap)).EndInit();
            this.tbWIFIList.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tbQRLocation.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tcQRLocation.ResumeLayout(false);
            this.tbQRMap.ResumeLayout(false);
            this.tbQRMap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQRLocation)).EndInit();
            this.tbQRList.ResumeLayout(false);
            this.pnlQRLocation.ResumeLayout(false);
            this.pnlQRLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbQR)).EndInit();
            this.tbManage.ResumeLayout(false);
            this.tbServerManage.ResumeLayout(false);
            this.tbLog.ResumeLayout(false);
            this.tbLog.PerformLayout();
            this.tbOnlineMap.ResumeLayout(false);
            this.pOnline.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOnline)).EndInit();
            this.tbOnlineList.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrListUpdate;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.ToolTip ttInfo;
        private System.Windows.Forms.Timer tmrOnlineViewUpdate;
        private System.Windows.Forms.ToolTip ttOnlineUser;
        private System.Windows.Forms.TabPage tbHeatMap;
        private System.Windows.Forms.TabPage tbWIFILocation;
        private System.Windows.Forms.TabControl tbWIFI;
        private System.Windows.Forms.TabPage tbWIFIMap;
        private System.Windows.Forms.PictureBox pbWIFIMap;
        private System.Windows.Forms.TabPage tbWIFIList;
        private System.Windows.Forms.ListView lvWIFIList;
        private System.Windows.Forms.ColumnHeader colWIFIID;
        private System.Windows.Forms.ColumnHeader colWIFIName;
        private System.Windows.Forms.ColumnHeader colMAC;
        private System.Windows.Forms.ColumnHeader colWIFIStrength;
        private System.Windows.Forms.ColumnHeader colWIFIX;
        private System.Windows.Forms.ColumnHeader colWIFIY;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblWIFIPower;
        private System.Windows.Forms.Label lblWIFIMAC;
        private System.Windows.Forms.TextBox tbMAC;
        private System.Windows.Forms.TextBox tbWIFIPower;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblWIFIY;
        private System.Windows.Forms.Label lblWIFIX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbWIFIName;
        private System.Windows.Forms.TextBox tbWIFIX;
        private System.Windows.Forms.TextBox tbWIFIY;
        private System.Windows.Forms.TextBox tbWIFIID;
        private System.Windows.Forms.Button btnWIFIDelete;
        private System.Windows.Forms.Button btnWIFIEdit;
        private System.Windows.Forms.Button btnWIFIAdd;
        private System.Windows.Forms.Button btnWIFICreate;
        private System.Windows.Forms.Button btnWIFIOpen;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblWIFIName;
        private System.Windows.Forms.Label lblWIFIID;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnCreateWIFI;
        private System.Windows.Forms.Button btnOpenWIFI;
        private System.Windows.Forms.TabPage tbQRLocation;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tcQRLocation;
        private System.Windows.Forms.TabPage tbQRMap;
        private System.Windows.Forms.PictureBox pbQRLocation;
        private System.Windows.Forms.TabPage tbQRList;
        private System.Windows.Forms.ListView lvQRList;
        private System.Windows.Forms.ColumnHeader colQRID;
        private System.Windows.Forms.ColumnHeader colQRName;
        private System.Windows.Forms.ColumnHeader colX;
        private System.Windows.Forms.ColumnHeader colY;
        private System.Windows.Forms.Panel pnlQRLocation;
        private System.Windows.Forms.PictureBox pbQR;
        private System.Windows.Forms.Label lblQRy;
        private System.Windows.Forms.Label lblQRx;
        private System.Windows.Forms.Label lblQRName;
        private System.Windows.Forms.Label lblQRID;
        private System.Windows.Forms.TextBox tbQRName;
        private System.Windows.Forms.TextBox tbQRx;
        private System.Windows.Forms.TextBox tbQRy;
        private System.Windows.Forms.TextBox tbQRID;
        private System.Windows.Forms.Button btnDeleteQR;
        private System.Windows.Forms.Button btnEditQR;
        private System.Windows.Forms.Button btnAddQR;
        private System.Windows.Forms.Button btnCreateQRConf;
        private System.Windows.Forms.Button btnOpenQRConf;
        private System.Windows.Forms.TabPage tbManage;
        private System.Windows.Forms.TabControl tbServerManage;
        private System.Windows.Forms.TabPage tbLog;
        private System.Windows.Forms.TextBox txtbLog;
        private System.Windows.Forms.TabPage tbOnlineMap;
        private System.Windows.Forms.Panel pOnline;
        private System.Windows.Forms.PictureBox pbOnline;
        private System.Windows.Forms.TabPage tbOnlineList;
        private System.Windows.Forms.ListView lvOnline;
        private System.Windows.Forms.ColumnHeader userID;
        private System.Windows.Forms.ColumnHeader lastOnline;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pImage;
        private System.Windows.Forms.PictureBox pbMapImage;
        private System.Windows.Forms.Panel pSettings;
        private System.Windows.Forms.TextBox tbAzimuth;
        private System.Windows.Forms.Label lAzimuth;
        private System.Windows.Forms.TextBox tbRealWidth;
        private System.Windows.Forms.TextBox tbRealLength;
        private System.Windows.Forms.TextBox tbCoordinateY2;
        private System.Windows.Forms.TextBox tbCoordinateX2;
        private System.Windows.Forms.TextBox tbCoordinateY1;
        private System.Windows.Forms.TextBox tbCoordinateX1;
        private System.Windows.Forms.Label lblRealLength;
        private System.Windows.Forms.Label lblRealWidth;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblCoordinateY2;
        private System.Windows.Forms.Label lblCoordinateX2;
        private System.Windows.Forms.Label lblCoordinateY1;
        private System.Windows.Forms.Label lblCoordinateX1;
        private System.Windows.Forms.Button btnDownloadImage;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.Button bGenerate;
        private System.Windows.Forms.MaskedTextBox mtbEnd;
        private System.Windows.Forms.MaskedTextBox mtbBegin;
        private System.Windows.Forms.Panel pHeatMap;
        private System.Windows.Forms.PictureBox pbHeatMap;
        private System.Windows.Forms.Label lTo;
        private System.Windows.Forms.Label lFrom;
        private System.Windows.Forms.Panel pnlHeatMap;
        private System.Windows.Forms.GroupBox dbFrame;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.GroupBox gbHeatMapDate;
    }
}
