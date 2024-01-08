using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThoiKhoaBieu_VNUA_Ver2
{
    public partial class ThietLapDuLieu : Form
    {
        ThietLapDuLieuController controller = new ThietLapDuLieuController();
        long maSV;
        string hocKy;
        string url;
        public ThietLapDuLieu()
        {
            InitializeComponent();
        }

        private void btnThietLap_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;

            if (long.TryParse(txtMaSV.Text, out maSV) == false)
            {
                MessageBox.Show("Vui lòng nhập kiểm tra lại mã sinh viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (radHocKy1.Checked)
            {
                if (date.Month >= 1 && date.Month <= 6)
                {
                    hocKy = $"{date.AddYears(-1).ToString("yyyy")}1";
                }
                if (date.Month >= 7 && date.Month <= 12)
                {
                    hocKy = $"{date.AddYears(0).ToString("yyyy")}1";
                }
            }
            else if (radHocKy2.Checked)
            {
                if (date.Month >= 1 && date.Month <= 6)
                {
                    hocKy = $"{date.AddYears(-1).ToString("yyyy")}2";
                }
                if (date.Month >= 7 && date.Month <= 12)
                {
                    hocKy = $"{date.AddYears(0).ToString("yyyy")}2";
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn học kỳ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            controller.XoaDuLieuCu();

            url = ConfigurationManager.AppSettings["urlTkb"].ToString() + maSV.ToString();

            UpdateAppSettings("hocKy", hocKy);

            this.Hide();
            Form loading = new loading();
            loading.Show();
            bool kq = controller.ThemDuLieu(url,hocKy);
            if (kq)
            {
                MessageBox.Show("Thiết lập dữ liệu thành công !"+"\nApp sẽ mở lại với dữ liệu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("Reboot.bat");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Thiết lập dữ liệu KHÔNG thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                loading.Close();
                this.Show();
            }
            
        }

        private void UpdateAppSettings(string key, string value)
        {
            // Mở cấu hình cho việc sửa đổi
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Kiểm tra xem khóa có tồn tại không
            if (config.AppSettings.Settings.AllKeys.Contains(key))
            {
                // Nếu tồn tại, thay đổi giá trị
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                // Nếu không tồn tại, thêm khóa mới
                config.AppSettings.Settings.Add(key, value);
            }

            // Lưu các thay đổi
            config.Save(ConfigurationSaveMode.Modified);

            // Tải lại cấu hình để áp dụng thay đổi ngay lập tức
            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}
