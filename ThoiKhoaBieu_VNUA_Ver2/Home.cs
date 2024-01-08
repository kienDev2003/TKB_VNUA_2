using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace ThoiKhoaBieu_VNUA_Ver2
{
    public partial class Home : Form
    {
        HomeController controller = new HomeController();

        public Home()
        {
            InitializeComponent();
            controller.LayPanel(Panel_1, Panel_2, Panel_3, Panel_4, Panel_5, Panel_6, Panel_7);
        }

        private void LoadTuanHoc(Panel panelTuanHoc)
        {
            controller.CreateTuanHoc(panelTuanHoc);
        }

        private void Home_Load(object sender, EventArgs e)
        {
            if (controller.KiemTraDuLieu() == false) lbThietLapDuLieu_Click(null, EventArgs.Empty);
            LoadTuanHoc(panelTuanHoc);
            lbThongTinSV.Text = controller.ThongTinSinhVien();
        }

        private void vScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void vScrollBar2_Scroll_1(object sender, ScrollEventArgs e)
        {
            Panel_4.AutoScrollPosition = new System.Drawing.Point(0, e.NewValue);
        }

        private void lbThietLapDuLieu_MouseEnter(object sender, EventArgs e)
        {
            lbThietLapDuLieu.Cursor = Cursors.Hand;
            lbThietLapDuLieu.ForeColor = Color.Red;
        }

        private void lbThietLapDuLieu_MouseLeave(object sender, EventArgs e)
        {
            lbThietLapDuLieu.ForeColor = Color.Blue;
        }

        private void lbThietLapDuLieu_Click(object sender, EventArgs e)
        {
            Form thietLapDuLieu = new ThietLapDuLieu();
            this.Hide();
            thietLapDuLieu.ShowDialog();
            this.Show();
        }
    }
}
