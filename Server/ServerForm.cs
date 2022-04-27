using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class frmServer : Form
    {
        public frmServer()
        {
            InitializeComponent();

            BlockSettingsControls();

            //set up server settings
            CheckForIllegalCrossThreadCalls = false;
            ServerManage.SetUpServer(LogMessage, ref lvQRList, pbQRLocation, ref lvWIFIList, pbWIFIMap);
        }

        // blocking settings controls except for the btnDownloadImage
        private void BlockSettingsControls()
        {
            foreach (Control control in pSettings.Controls)
            {
                control.Enabled = false;
            }
            btnDownloadImage.Enabled = true;
        }

        // unblocking settings controls except for the tbRealWidth
        private void UnblockSettingsControls()
        {
            foreach (Control control in pSettings.Controls)
            {
                control.Enabled = true;
            }
            tbRealWidth.Enabled = false;
        }

        // update tbRealLength and tbRealWidth with current values of RealLength and RealWidth
        private void UpdateRealValuesText()
        {
            tbRealLength.Text = MapInfo.RealLength.ToString("N3");
            tbRealWidth.Text = MapInfo.RealWidth.ToString("N3");
        }

        // update coordinate text with current values of points coordinates
        private void UpdatePointText()
        {
            tbCoordinateX1.Text = MapInfo.PointX1.ToString();
            tbCoordinateX2.Text = MapInfo.PointX2.ToString();
            tbCoordinateY1.Text = MapInfo.PointY1.ToString();
            tbCoordinateY2.Text = MapInfo.PointY2.ToString();
        }

        // downloading map image from file, preparing controls for work
        private void btnDownloadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter =
                    "Indoor-navigation map files (*.inm)|*.inm|" +
                    "Image files (*.jpg,*.jpeg,*.jpe,*.bmp,*.png)|*.jpg;*.jpeg;*.jpe;*.bmp;*.png|" +
                    "All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                int msg = SettingsModel.DownloadMap(pbMapImage, openFileDialog.FileName);
                UnblockSettingsControls();
                UpdatePointText();
                UpdateRealValuesText();
                tbAzimuth.Text = MapInfo.Azimuth.ToString("N12");
                if (SettingsModel.MessageView(msg) == SettingsModel.MESSAGE.DOWNLOAD_USER_SETTINGS)
                {
                    MapInfo.isMapChanged = false;
                }
                else if (msg == SettingsModel.MESSAGE.DOWNLOAD_DEFOULT_SETTINGS)
                {
                    MapInfo.isMapChanged = true;
                }
            }
        }

        // changing values of coordinates
        private void tbCoordinateX1_TextChanged(object sender, EventArgs e)
        {
            MapInfo.SetX1(SettingsModel.CoordTextChanged(tbCoordinateX1.Text, MapInfo.PointX1));
            SettingsModel.FramePointsView(pbMapImage);
            UpdateRealValuesText();
        }
        private void tbCoordinateX2_TextChanged(object sender, EventArgs e)
        {
            MapInfo.SetX2(SettingsModel.CoordTextChanged(tbCoordinateX2.Text, MapInfo.PointX2));
            SettingsModel.FramePointsView(pbMapImage);
            UpdateRealValuesText();
        }
        private void tbCoordinateY1_TextChanged(object sender, EventArgs e)
        {
            MapInfo.SetY1(SettingsModel.CoordTextChanged(tbCoordinateY1.Text, MapInfo.PointY1));
            SettingsModel.FramePointsView(pbMapImage);
            UpdateRealValuesText();
        }
        private void tbCoordinateY2_TextChanged(object sender, EventArgs e)
        {
            MapInfo.SetY2(SettingsModel.CoordTextChanged(tbCoordinateY2.Text, MapInfo.PointY2));
            SettingsModel.FramePointsView(pbMapImage);
            UpdateRealValuesText();
        }

        // changing real length and changing real width according to new value of real length
        private void tbRealLength_TextChanged(object sender, EventArgs e)
        {
            if (tbRealLength.Focused)
            {
                tbRealWidth.Text = SettingsModel.RealLengthChanged(tbRealLength.Text);
            }
        }

        // changing value of azimuth
        private void tbAzimuth_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(tbAzimuth.Text, out double value))
            {
                MapInfo.Azimuth = value;
            }
            SettingsModel.RedrowAzimuth();
            MapInfo.isMapChanged = true;
        }

        // entering valid characters in text fields
        private void tbIntValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void tbDoubleValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!char.IsNumber(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != Convert.ToChar(8)
                && (e.KeyChar == '.' && (tb.Text.Contains("."))))
            {
                e.Handled = true;
            }
        }

        // save current settings to file 
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SettingsModel.MessageView(SettingsModel.SaveSettings()) == SettingsModel.MESSAGE.USER_SETTINGS_SAVED_SUCCESSFULLY)
            {
                UpdatePointText();
                MapInfo.isMapChanged = false;
            }
        }

        // save select point 
        private void pbMapImage_MouseDown(object sender, MouseEventArgs e)
        {
            SettingsModel.SelectFramePoint(e.X, e.Y);
            UpdatePointText();
            UpdateRealValuesText();
        }

        // moving frame point
        private void pbMapImage_MouseMove(object sender, MouseEventArgs e)
        {
            SettingsModel.MoveFramePoint(pbMapImage, e.X, e.Y);
        }

        // checking the save before switching to another tab
        private void tcMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (SettingsModel.CheckSaving(tcMain) == SettingsModel.MESSAGE.USER_SETTINGS_SAVED_SUCCESSFULLY)
            {
                UpdatePointText();
                MapInfo.isMapChanged = false;
            }
        }

        /*
         * Management tab
         */

        //start the server
        private void btnStart_Click(object sender, EventArgs e)
        {
            ServerManage.StartServer(ref tmrListUpdate);
        }

        //stop the server
        private void btnStop_Click(object sender, EventArgs e)
        {
            ServerManage.StopServer(ref tmrListUpdate);
        }

        //print log message
        private void LogMessage(string msg)
        {
            txtbLog.AppendText(msg + "\r\n");
        }

        //close main form and stop server
        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Server.Stop();
        }

        //updating online user list
        private void tmrListUpdate_Tick(object sender, EventArgs e)
        {
            if (!Server.Run)
                return;
            ServerManage.UpdateOnlineUsersView(ref lvOnline, Server.userIDModel);
        }

        /*
         * QR location tab
         */

        //open QR config file
        private void btnOpenQRConf_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.Cancel)
                return;
            QRLocation.OpenQRConfig(dlgOpenFile.FileName, ref lvQRList, pbQRLocation);
        }

        //create new QR config file
        private void btnCreateQRConf_Click(object sender, EventArgs e)
        {
            if (dlgSaveFile.ShowDialog() == DialogResult.Cancel)
                return;
            QRLocation.OpenQRConfig(dlgSaveFile.FileName, ref lvQRList, pbQRLocation);
        }

        //add QR into a config file
        private void btnAddQR_Click(object sender, EventArgs e)
        {
            int Result = QRLocation.AddQR(tbQRID.Text, tbQRName.Text, tbQRx.Text, tbQRy.Text, ref lvQRList, pbQRLocation);

            //processing Result

            if (Result == 0)
            {
                QRLocation.DrawQRPoint(pbQRLocation, Color.Green, double.Parse(tbQRx.Text), double.Parse(tbQRy.Text));
                QRLocation.adding = false;
            }
        }

        //edit QR in a config file
        private void btnEditQR_Click(object sender, EventArgs e)
        {
            int Result = -1;

            //select in list view
            if (QRLocation.selectedItem != null && QRLocation.selectedItem.Count > 0)
            {
                Result = QRLocation.EditQR(QRLocation.selectedItem[0].Text, tbQRID.Text, tbQRName.Text, tbQRx.Text, tbQRy.Text, ref lvQRList, pbQRLocation);
            }

            //select in QR map
            else if (QRLocation.selectedPoint.QRID != null)
                Result = QRLocation.EditQR(QRLocation.selectedPoint.QRID, tbQRID.Text, tbQRName.Text, tbQRx.Text, tbQRy.Text, ref lvQRList, pbQRLocation);
            
            //processing Result
        }

        //delete QR from a config file
        private void btnDeleteQR_Click(object sender, EventArgs e)
        {
            int Result = -1; 

            //select in list view
            if (QRLocation.selectedItem != null)
            {
                Result = QRLocation.DeleteQR(tbQRID.Text, ref lvQRList, pbQRLocation);
            }

            //select in QR map
            else if (QRLocation.selectedPoint.QRID != null)
                Result = QRLocation.DeleteQR(QRLocation.selectedPoint.QRID, ref lvQRList, pbQRLocation);

            //processing Result
        }

        //on select item in QR list
        private void lvQRList_SelectedIndexChanged(object sender, EventArgs e)
        {
            QRLocation.selectedItem = lvQRList.SelectedItems.Count > 0 ? lvQRList.SelectedItems : null;

            if (QRLocation.selectedItem != null)
            {
                tbQRID.Text = QRLocation.selectedItem[0].Text;
                tbQRName.Text = QRLocation.selectedItem[0].SubItems[1].Text;
                tbQRx.Text = QRLocation.selectedItem[0].SubItems[2].Text;
                tbQRy.Text = QRLocation.selectedItem[0].SubItems[3].Text;
                QRLocation.ShowQRImg(QRLocation.selectedItem[0].Text, pbQR);
            }
        }

        //cursore move
        private void pbQRLocation_MouseMove(object sender, MouseEventArgs e)
        {
            if (QRLocation.adding)
                return;

            if (QRLocation.selecting && e.Button == MouseButtons.None)
            {
                tbQRx.Text = QRLocation.selectedPoint.X;
                tbQRy.Text = QRLocation.selectedPoint.Y;
            }
            else
            {
                tbQRx.Text = ((e.X - MapInfo.PointX1) * MapInfo.SizeCoefficient).ToString("0.00");
                tbQRy.Text = ((e.Y - MapInfo.PointY1) * MapInfo.SizeCoefficient).ToString("0.00");
            }
        }

        //repaint QR location map
        private void tcMain_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tbQRLocation)
            {
                QRLocation.DrawQRMap(pbQRLocation);
            }
            else if (e.TabPage == tbWIFILocation)
            {
                WIFILocation.DrawWIFIMap(pbWIFIMap);
            }
        }

        //add QR point
        private void pbQRLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!QRLocation.adding)
            {
                QRLocation.DrawQRMap(pbQRLocation);
                QRLocation.DrawQRPoint(pbQRLocation, Color.Red, (e.X - MapInfo.PointX1) * MapInfo.SizeCoefficient, (e.Y - MapInfo.PointY1) * MapInfo.SizeCoefficient);
                tbQRx.Text = ((e.X - MapInfo.PointX1) * MapInfo.SizeCoefficient).ToString("0.00");
                tbQRy.Text = ((e.Y - MapInfo.PointY1) * MapInfo.SizeCoefficient).ToString("0.00");
                QRLocation.adding = true;
            }
        }

        //on select QR point
        private void pbQRLocation_MouseDown(object sender, MouseEventArgs e)
        {
            //check hitting
            QRLocation.selectedPoint = QRLocation.HitPoint(e.X, e.Y);

            //update text boxes
            tbQRID.Text = QRLocation.selectedPoint.QRID;
            tbQRName.Text = QRLocation.selectedPoint.QRName;
            tbQRx.Text = QRLocation.selectedPoint.X;
            tbQRy.Text = QRLocation.selectedPoint.Y;

            if (QRLocation.selectedPoint.QRID != null)
            {
                //show tip, select point
                QRLocation.SelectPoint(QRLocation.selectedPoint, ttInfo, pbQRLocation, pbQR);
            }
            else
            {
                //no hitting
                QRLocation.DrawQRMap(pbQRLocation);
                QRLocation.adding = false;
                QRLocation.selecting = false;
            }
        }

        //end drag
        private void pbQRLocation_MouseUp(object sender, MouseEventArgs e)
        {
            if (QRLocation.selectedPoint.QRID != null)
            {
                double x = double.Parse(QRLocation.selectedPoint.X, System.Globalization.CultureInfo.InvariantCulture);
                double y = double.Parse(QRLocation.selectedPoint.Y, System.Globalization.CultureInfo.InvariantCulture);

                //new location of a QR point
                if (QRLocation.selecting && Math.Abs(e.X - (x / MapInfo.SizeCoefficient + MapInfo.PointX1)) > QRLocation.QRPointRadius && Math.Abs(e.Y - (y /  MapInfo.SizeCoefficient + MapInfo.PointY1)) > QRLocation.QRPointRadius)
                {
                    QRLocation.EditQR(QRLocation.selectedPoint.QRID, tbQRID.Text, tbQRName.Text, ((e.X - MapInfo.PointX1) * MapInfo.SizeCoefficient).ToString("0.00"), ((e.Y - MapInfo.PointY1) * MapInfo.SizeCoefficient).ToString("0.00"), ref lvQRList, pbQRLocation);
                    QRLocation.DrawQRMap(pbQRLocation);
                    QRLocation.selecting = false;
                }
            }
        }

        /*
         * Online user tab
         */

        private void tmrOnlineViewUpdate_Tick(object sender, EventArgs e)
        {
            OnlineView.UpdateOnlineView(pbOnline);
        }

        private void pbOnline_MouseDown(object sender, MouseEventArgs e)
        {
            OnlineView.RealUserData uData = OnlineView.GetUserInfo(e.X, e.Y);
            if (uData.ID > 0)
            {
                string uInfo = $"ID: {uData.ID}\n" +
                    $"X: " + (uData.x).ToString("N3") + "\n" +
                    $"Y: " + (uData.y).ToString("N3");
                ttOnlineUser.SetToolTip(pbOnline, uInfo);
            }
        }

        private void tbServerManage_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tbOnlineMap)
            {
                tmrOnlineViewUpdate.Interval = OnlineView.UPDATE_ONLINE_VIEW_INTERVAL;
                if (MapInfo.bitmap == null)
                {
                    return;
                }
                else
                {
                    OnlineView.DrawMap(pbOnline);
                    tmrOnlineViewUpdate.Start();
                }
            }
            else
            {
                tmrOnlineViewUpdate.Stop();
            }
        }

        /*
        * Wi-Fi location tab
        */

        //open Wi-Fi congig
        private void btnWIFIOpen_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.Cancel)
                return;
            WIFILocation.OpenWIFIConfig(dlgOpenFile.FileName, ref lvWIFIList, pbWIFIMap);
        }

        //create Wi-Fi config
        private void btnWIFICreate_Click(object sender, EventArgs e)
        {
            if (dlgSaveFile.ShowDialog() == DialogResult.Cancel)
                return;
            WIFILocation.OpenWIFIConfig(dlgSaveFile.FileName, ref lvWIFIList, pbWIFIMap);
        }

        //select Wi-Fi in list view
        private void lvWIFIList_SelectedIndexChanged(object sender, EventArgs e)
        {
            WIFILocation.selectedItem = lvWIFIList.SelectedItems.Count > 0 ? lvWIFIList.SelectedItems : null;

            if (WIFILocation.selectedItem != null)
            {
                tbWIFIID.Text = WIFILocation.selectedItem[0].Text;
                tbWIFIName.Text = WIFILocation.selectedItem[0].SubItems[1].Text;
                tbMAC.Text = WIFILocation.selectedItem[0].SubItems[2].Text;
                tbWIFIPower.Text = WIFILocation.selectedItem[0].SubItems[3].Text;
                tbWIFIX.Text = WIFILocation.selectedItem[0].SubItems[4].Text;
                tbWIFIY.Text = WIFILocation.selectedItem[0].SubItems[5].Text;
            }
        }

        //add Wi-Fi spot to config
        private void btnWIFIAdd_Click(object sender, EventArgs e)
        {
            int Result = WIFILocation.AddWIFI(tbWIFIID.Text, tbWIFIName.Text, tbMAC.Text, tbWIFIPower.Text, tbWIFIX.Text, tbWIFIY.Text, ref lvWIFIList, pbWIFIMap);

            //processing Result

            if (Result >= 0)
            {
                WIFILocation.DrawWIFIMap(pbWIFIMap);
                WIFILocation.adding = false;
            }
        }

        //edit Wi-Fi spot in config
        private void btnWIFIEdit_Click(object sender, EventArgs e)
        {
            int Result = -1;

            //select in list view
            if (WIFILocation.selectedItem != null && WIFILocation.selectedItem.Count > 0)
            {
                Result = WIFILocation.EditWIFI(WIFILocation.selectedItem[0].Text, tbWIFIID.Text, tbWIFIName.Text, tbMAC.Text, tbWIFIPower.Text, tbWIFIX.Text, tbWIFIY.Text, ref lvWIFIList, pbWIFIMap);
            }

            //select in QR map
            else if (WIFILocation.selectedPoint.WIFISpotID != null)
                Result = WIFILocation.EditWIFI(WIFILocation.selectedPoint.WIFISpotID, tbWIFIID.Text, tbWIFIName.Text, tbMAC.Text, tbWIFIPower.Text, tbWIFIX.Text, tbWIFIY.Text, ref lvWIFIList, pbWIFIMap);

            //processing Result
        }

        //delete Wi-Fi spot from config
        private void btnWIFIDelete_Click(object sender, EventArgs e)
        {
            int Result = -1;

            //select in list view
            if (WIFILocation.selectedItem != null)
            {
                Result = WIFILocation.DeleteWIFI(WIFILocation.selectedItem[0].Text, ref lvWIFIList, pbWIFIMap);
            }

            //select in QR map
            else if (WIFILocation.selectedPoint.WIFISpotID!= null)
                Result = WIFILocation.DeleteWIFI(WIFILocation.selectedPoint.WIFISpotID, ref lvWIFIList, pbWIFIMap);

            //processing Result
        }

        //adding Wi-Fi spot by mouse
        private void pbWIFIMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!WIFILocation.adding)
            {
                WIFILocation.DrawWIFIMap(pbWIFIMap);
                WIFILocation.DrawWIFIPoint(pbWIFIMap, Color.Red, (e.X - MapInfo.PointX1) * MapInfo.SizeCoefficient, (e.Y - MapInfo.PointY1) * MapInfo.SizeCoefficient);

                tbWIFIX.Text = ((e.X - MapInfo.PointX1) * MapInfo.SizeCoefficient).ToString("0.00");
                tbWIFIY.Text = ((e.Y - MapInfo.PointY1) * MapInfo.SizeCoefficient).ToString("0.00");
                WIFILocation.adding = true;
            }
        }

        //follow mouse coordinates
        private void pbWIFIMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (WIFILocation.adding)
                return;

            if (WIFILocation.selecting && e.Button == MouseButtons.None)
            {
                tbWIFIX.Text = WIFILocation.selectedPoint.X;
                tbWIFIY.Text = WIFILocation.selectedPoint.Y;
            }
            else
            {
                tbWIFIX.Text = ((e.X - MapInfo.PointX1) * MapInfo.SizeCoefficient).ToString("0.00");
                tbWIFIY.Text = ((e.Y - MapInfo.PointY1) * MapInfo.SizeCoefficient).ToString("0.00");
            }
        }

        //select Wi-Fi spot by mouse
        private void pbWIFIMap_MouseDown(object sender, MouseEventArgs e)
        {
            //check hitting
            WIFILocation.selectedPoint = WIFILocation.HitPoint(e.X, e.Y);

            //update text boxes
            tbWIFIID.Text = WIFILocation.selectedPoint.WIFISpotID;
            tbWIFIName.Text = WIFILocation.selectedPoint.WIFISpotName;
            tbMAC.Text = WIFILocation.selectedPoint.WIFISpotMAC;
            tbWIFIPower.Text = WIFILocation.selectedPoint.WIFISpotPower;
            tbWIFIX.Text = WIFILocation.selectedPoint.X;
            tbWIFIY.Text = WIFILocation.selectedPoint.Y;

            if (WIFILocation.selectedPoint.WIFISpotID != null)
            {
                //show tip, select point
                WIFILocation.SelectPoint(WIFILocation.selectedPoint, ttInfo, pbWIFIMap);
            }
            else
            {
                //no hitting
                WIFILocation.DrawWIFIMap(pbWIFIMap);
                WIFILocation.adding = false;
                WIFILocation.selecting = false;
            }
        }

        //drag Wi-Fi spot by mouse
        private void pbWIFIMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (WIFILocation.selectedPoint.WIFISpotID!= null)
            {
                double x = double.Parse(WIFILocation.selectedPoint.X, System.Globalization.CultureInfo.InvariantCulture);
                double y = double.Parse(WIFILocation.selectedPoint.Y, System.Globalization.CultureInfo.InvariantCulture);

                //new location of a QR point
                if (WIFILocation.selecting && Math.Abs(e.X - (x / MapInfo.SizeCoefficient + MapInfo.PointX1)) > QRLocation.QRPointRadius && Math.Abs(e.Y - (y / MapInfo.SizeCoefficient + MapInfo.PointY1)) > WIFILocation.WIFIRadius)
                {
                    WIFILocation.EditWIFI(WIFILocation.selectedPoint.WIFISpotID, tbWIFIID.Text, tbWIFIName.Text, tbMAC.Text, tbWIFIPower.Text, ((e.X - MapInfo.PointX1) * MapInfo.SizeCoefficient).ToString("0.00"), ((e.Y - MapInfo.PointY1) * MapInfo.SizeCoefficient).ToString("0.00"), ref lvWIFIList, pbWIFIMap);
                    WIFILocation.DrawWIFIMap(pbQRLocation);
                    WIFILocation.selecting = false;
                }
            }
        }

        private void bGenerate_Click(object sender, EventArgs e)
        {
            string BeginDateTime;
            string EndDateTime;
            mtbBegin.ValidatingType = typeof(DateTime);

            if (mtbBegin.ValidateText() == null)
            {
                return;
            }
            if (mtbEnd.ValidateText() == null)
            {
                return;
            }

            BeginDateTime = mtbBegin.ValidateText().ToString();
            EndDateTime = mtbEnd.ValidateText().ToString();
            if (BeginDateTime != null && EndDateTime != null)
            {
                if (MapInfo.bitmap != null)
                {
                    pbHeatMap.Image = (Image)MapInfo.bitmap.Clone();
                    long BeginTime = DateTimeOffset.Parse(BeginDateTime).ToUnixTimeSeconds();
                    long EndTime = DateTimeOffset.Parse(EndDateTime).ToUnixTimeSeconds();
                    if (BeginTime > EndTime) return;
                    HeatMap.Generate(pbHeatMap, BeginTime, EndTime);
                }
            }
        }
    }
}