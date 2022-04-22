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
                // возможно стоит копировать картинку куда-то к себе на сервер
                // не работают фильтры
                //openFileDialog.Filter = "Image files (*.jpg,*.jpeg,*.jpe,*.jfif,*.png)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png";
                SettingsModel.InitSettings();
                ErrorMessageView(SettingsModel.SaveImage(openFileDialog.FileName));
            }
            //bitmap = new Bitmap(MapImageFilename);
            if (0 == ErrorMessageView(SettingsModel.FramePointsView(pbMapImage)))
            {
                UnblockSettingsControls();
                UpdatePointText();
            }
        }

        private int ErrorMessageView(int errorCode)
        {
            if (errorCode == SettingsModel.IMAGE_DOWNLOAD_ERROR)
            {
                MessageBox.Show("Ошибка", "Не удалось загрузить изображение", MessageBoxButtons.OK);
                return SettingsModel.IMAGE_DOWNLOAD_ERROR;
            }
            else if (errorCode == SettingsModel.DROW_FRAME_POINTS_ERROR)
            {
                MessageBox.Show("Ошибка", "Не удалось отрисовать дополнительные элементы изображения", MessageBoxButtons.OK);
                return SettingsModel.DROW_FRAME_POINTS_ERROR;
            }
            else if (errorCode == SettingsModel.IMAGE_SAVE_ERROR)
            {
                MessageBox.Show("Ошибка", "Не удалось осохранить изображение", MessageBoxButtons.OK);
                return SettingsModel.IMAGE_SAVE_ERROR;
            }
            else return 0;
        }
        private void tbCoordinateX1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCoordinateX1.Text, out SettingsModel.PointX1))
            {
                ErrorMessageView(SettingsModel.FramePointsView(pbMapImage));
            }
        }
        private void tbCoordinateX2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCoordinateX2.Text, out SettingsModel.PointX2))
            {
                ErrorMessageView(SettingsModel.FramePointsView(pbMapImage));
            }
        }
        private void tbCoordinateY1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCoordinateY1.Text, out SettingsModel.PointY1))
            {
                ErrorMessageView(SettingsModel.FramePointsView(pbMapImage));
            }
        }
        private void tbCoordinateY2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(tbCoordinateY2.Text, out SettingsModel.PointY2))
            {
                ErrorMessageView(SettingsModel.FramePointsView(pbMapImage));
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

        private void tbRealWidth_TextChanged(object sender, EventArgs e)
        {
            if (tbRealWidth.Focused)
            {
                double realWidth = SettingsModel.RealWidth;
                if (double.TryParse(tbRealWidth.Text, out realWidth))
                {
                    SettingsModel.SetRealLength(realWidth);
                }
                tbRealLength.Text = SettingsModel.RealLength.ToString("N3");
            }
        }

        private void pbMapImage_MouseDown(object sender, MouseEventArgs e)
        {
            SettingsModel.SelectFramePoint(pbMapImage, e.X, e.Y);
            UpdatePointText();
            UpdateRealValuesText();
        }

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
