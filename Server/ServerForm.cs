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
            ServerManage.SetUpServer(LogMessage);
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
                MapInfo.SetDefoult();
                UpdateRealValuesText();
                tbAzimuth.Text = MapInfo.Azimuth.ToString("N12");
                if (SettingsModel.MessageView(msg) == SettingsModel.MESSAGE.DOWNLOAD_USER_SETTINGS)
                {
                    MapInfo.isMapChanged = false;
                }
                else if(msg == SettingsModel.MESSAGE.DOWNLOAD_DEFOULT_SETTINGS)
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
            SettingsModel.MessageView(SettingsModel.SaveSettings());
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
            SettingsModel.CheckSaving(tcMain);
        }


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

        //flush the server temp storage
        private void btnFlush_Click(object sender, EventArgs e)
        {
            Server.Flush();
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

        //open QR config file
        private void btnOpenQRConf_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog() == DialogResult.Cancel)
                return;
            QRLocation.OpenQRConfig(dlgOpenFile.FileName, ref lvQRList);
        }

        //create new QR config file
        private void btnCreateQRConf_Click(object sender, EventArgs e)
        {
            if (dlgSaveFile.ShowDialog() == DialogResult.Cancel)
                return;
            QRLocation.OpenQRConfig(dlgSaveFile.FileName, ref lvQRList);
        }

        //add QR into a config file
        private void btnAddQR_Click(object sender, EventArgs e)
        {
            string Result = QRLocation.AddQR(tbQRID.Text, tbQRName.Text, tbQRx.Text, tbQRy.Text, ref lvQRList);
            tbError.Text = Result;
        }

        //edit QR in a config file
        private void btnEditQR_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
                return;
            //string Result = QRLocation.EditQR();
            //tbError.Text = Result;
        }

        //delete QR from a config file
        private void btnDeleteQR_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
                return;
            //string Result = QRLocation.DeleteQR();
            //tbError.Text = Result;
        }

        //on select item in QR list
        ListView.SelectedListViewItemCollection selectedItem = null;
        private void lvQRList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedItem = lvQRList.SelectedItems.Count> 0 ? lvQRList.SelectedItems : null;

            if (selectedItem != null)
            {
                tbQRID.Text = selectedItem[0].Text;
                tbQRName.Text = selectedItem[0].SubItems[1].Text;
                tbQRx.Text = selectedItem[0].SubItems[2].Text;
                tbQRy.Text = selectedItem[0].SubItems[3].Text;
            }
        }
    }
}
