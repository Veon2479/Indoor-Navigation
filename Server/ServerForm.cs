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

            if (SettingsModel.DownloadSettings(pbMapImage) != 0)
            {
                BlockSettingsControls();
            }
            UpdatePointText();
            UpdateRealValuesText();

            //set up server settings
            CheckForIllegalCrossThreadCalls = false;
            ServerManage.SetUpServer(LogMessage, ref lvQRList, pbQRLocation);
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
            tbRealLength.Text = SettingsModel.RealLength.ToString("N3");
            tbRealWidth.Text = SettingsModel.RealWidth.ToString("N3");
        }

        // update coordinate text with current values of points coordinates
        private void UpdatePointText()
        {
            tbCoordinateX1.Text = SettingsModel.PointX1.ToString();
            tbCoordinateX2.Text = SettingsModel.PointX2.ToString();
            tbCoordinateY1.Text = SettingsModel.PointY1.ToString();
            tbCoordinateY2.Text = SettingsModel.PointY2.ToString();
        }

        // downloading map image from file, 
        // preparing controls for work
        private void btnDownloadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg,*.jpeg,*.jpe,*.bmp,*.png)|*.jpg;*.jpeg;*.jpe;*.bmp;*.png|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                MessageView(SettingsModel.DownloadImage(pbMapImage, openFileDialog.FileName));
            }
            if (0 == MessageView(SettingsModel.FramePointsView(pbMapImage)))
            {
                UnblockSettingsControls();
                UpdatePointText();
            }
        }

        //  display message according to message or error code
        private int MessageView(int errorCode)
        {
            switch (errorCode)
            {
                case SettingsModel.MESSAGE.IMAGE_DOWNLOAD_ERROR:
                    {
                        MessageBox.Show("Image loading error", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case SettingsModel.MESSAGE.DROW_FRAME_POINTS_ERROR:
                    {
                        MessageBox.Show("Error drawing additional elements", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case SettingsModel.MESSAGE.DOWNLOAD_USER_SETTINGS_ERROR:
                    {
                        MessageBox.Show("Error loading user settings", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case SettingsModel.MESSAGE.USER_SETTINGS_SAVED_SUCCESSFULLY:
                    {
                        MessageBox.Show("Settings saved successfully", "Message", MessageBoxButtons.OK);
                    }
                    break;
                case SettingsModel.MESSAGE.SAVE_USER_SETTINGS_ERROR:
                    {
                        MessageBox.Show("Error saving user settings", "Error", MessageBoxButtons.OK);
                    }
                    break;
            }
            return errorCode;
        }

        // changing coordinate value and redrowing frame according to changed coordinate
        private void tbCoordinateX1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCoordinateX1.Text, out SettingsModel.PointX1))
            {
                MessageView(SettingsModel.FramePointsView(pbMapImage));
            }
        }
        private void tbCoordinateX2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCoordinateX2.Text, out SettingsModel.PointX2))
            {
                MessageView(SettingsModel.FramePointsView(pbMapImage));
            }
        }
        private void tbCoordinateY1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCoordinateY1.Text, out SettingsModel.PointY1))
            {
                MessageView(SettingsModel.FramePointsView(pbMapImage));
            }
        }
        private void tbCoordinateY2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCoordinateY2.Text, out SettingsModel.PointY2))
            {
                MessageView(SettingsModel.FramePointsView(pbMapImage));
            }
        }

        // entering valid characters in text fields
        private void tbCoordinate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
        private void tbRealValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar)
                && !char.IsDigit(e.KeyChar)
                && e.KeyChar != Convert.ToChar(8)
                && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        // save current settings to file 
        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageView(SettingsModel.SaveSettings());
        }

        // changing real length and changing real width according to new value of real length
        private void tbRealLength_TextChanged(object sender, EventArgs e)
        {
            if (tbRealLength.Focused)
            {
                tbRealWidth.Text = SettingsModel.RealLengthChanged(tbRealLength.Text);
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
            if (SettingsModel.MoveFramePoint(pbMapImage, e.X, e.Y) != SettingsModel.NO_POINT)
            {
                //UpdatePointText();
                //UpdateRealValuesText();
            }
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
            if (Result == 0)
            {
                QRLocation.DrawQRPoint(pbQRLocation, Color.Green, int.Parse(tbQRx.Text), int.Parse(tbQRy.Text));
                QRLocation.adding = false;
            }

        }

        //edit QR in a config file
        private void btnEditQR_Click(object sender, EventArgs e)
        {
            if (selectedItem.Count == 0)
                return;
            string Result = QRLocation.EditQR(selectedItem[0].Text, tbQRID.Text, tbQRName.Text, tbQRx.Text, tbQRy.Text, ref lvQRList, pbQRLocation);
            tbError.Text = Result;
        }

        //delete QR from a config file
        private void btnDeleteQR_Click(object sender, EventArgs e)
        {
            if (selectedItem.Count == 0)
                return;
            string Result = QRLocation.DeleteQR(tbQRID.Text, ref lvQRList, pbQRLocation);
            tbError.Text = Result;
        }

        //on select item in QR list
        ListView.SelectedListViewItemCollection selectedItem = null;
        private void lvQRList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedItem = lvQRList.SelectedItems.Count > 0 ? lvQRList.SelectedItems : null;

            if (selectedItem != null)
            {
                tbQRID.Text = selectedItem[0].Text;
                tbQRName.Text = selectedItem[0].SubItems[1].Text;
                tbQRx.Text = selectedItem[0].SubItems[2].Text;
                tbQRy.Text = selectedItem[0].SubItems[3].Text;
            }
        }

        private void pbQRLocation_MouseMove(object sender, MouseEventArgs e)
        {
            if (!QRLocation.adding)
            {
                tbQRx.Text = e.X.ToString();
                tbQRy.Text = e.Y.ToString();
            }
        }

        //repaint QR location map
        private void tcMain_Selected(object sender, TabControlEventArgs e)
        {
            QRLocation.PaintQRMap(pbQRLocation);
        }

        //add QR point
        private void pbQRLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            QRLocation.DrawQRPoint(pbQRLocation, Color.Red, e.X, e.Y);
            QRLocation.adding = true;
        }
    }
}
