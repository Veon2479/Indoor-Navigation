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
            ServerManage.SetUpServer(LogMessage);
            
        }

        private void BlockSettingsControls()
        {
            foreach (Control control in pSettings.Controls)
            {
                control.Enabled = false;
            }
            btnDownloadImage.Enabled = true;
        }
        private void UnblockSettingsControls()
        {
            foreach (Control control in pSettings.Controls)
            {
                control.Enabled = true;
            }
            tbRealWidth.Enabled = false;
        }
        private void UpdateRealValuesText()
        {
            tbRealLength.Text = SettingsModel.RealLength.ToString("N3");
            tbRealWidth.Text = SettingsModel.RealWidth.ToString("N3");
        }
        private void UpdatePointText()
        {
            tbCoordinateX1.Text = SettingsModel.PointX1.ToString();
            tbCoordinateX2.Text = SettingsModel.PointX2.ToString();
            tbCoordinateY1.Text = SettingsModel.PointY1.ToString();
            tbCoordinateY2.Text = SettingsModel.PointY2.ToString();
        }
        private void btnDownloadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                //openFileDialog.Filter = "Image files (*.jpg,*.jpeg,*.jpe,*.jfif,*.png)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png";
                MessageView(SettingsModel.DownloadImage(pbMapImage, openFileDialog.FileName));

            }
            //bitmap = new Bitmap(MapImageFilename);
            if (0 == MessageView(SettingsModel.FramePointsView(pbMapImage)))
            {
                UnblockSettingsControls();
                UpdatePointText();
            }
        }

        private int MessageView(int errorCode)
        {
            switch (errorCode)
            {
                case SettingsModel.IMAGE_DOWNLOAD_ERROR:
                    {
                        MessageBox.Show("Image loading error", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case SettingsModel.DROW_FRAME_POINTS_ERROR:
                    {
                        MessageBox.Show("Error drawing additional elements", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case SettingsModel.DOWNLOAD_USER_SETTINGS_ERROR:
                    {
                        MessageBox.Show("Error loading user settings", "Error", MessageBoxButtons.OK);
                    }
                    break;
                case SettingsModel.USER_SETTINGS_SAVED_SUCCESSFULLY:
                    {
                        MessageBox.Show("Settings saved successfully", "Message", MessageBoxButtons.OK);
                    }
                    break;
                case SettingsModel.SAVE_USER_SETTINGS_ERROR:
                    {
                        MessageBox.Show("Error saving user settings", "Error", MessageBoxButtons.OK);
                    }
                    break;
            }
            return errorCode;
        }

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageView(SettingsModel.SaveSettings());
        }

        private void tbRealLength_TextChanged(object sender, EventArgs e)
        {
            if (tbRealLength.Focused)
            {
                double realLength = SettingsModel.RealLength;
                if (double.TryParse(tbRealLength.Text, out realLength))
                {
                    SettingsModel.SetRealLength(realLength);
                }
                tbRealWidth.Text = SettingsModel.RealWidth.ToString("N3");
            }
        }

        private void pbMapImage_MouseDown(object sender, MouseEventArgs e)
        {
            SettingsModel.SelectFramePoint(e.X, e.Y);
            UpdatePointText();
            UpdateRealValuesText();
        }

        private void pbMapImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (SettingsModel.MoveFramePoint(pbMapImage, e.X, e.Y) != SettingsModel.NO_POINT)
            {
                UpdatePointText();
                UpdateRealValuesText();
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
