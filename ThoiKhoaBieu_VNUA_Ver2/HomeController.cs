using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace ThoiKhoaBieu_VNUA_Ver2
{
    internal class HomeController
    {
        Model model = new Model();
        string hocKy = ConfigurationManager.AppSettings["hocKy"].ToString();

        Panel _PanelTuanHoc;
        Panel _Panel_1;
        Panel _Panel_2;
        Panel _Panel_3;
        Panel _Panel_4;
        Panel _Panel_5;
        Panel _Panel_6;
        Panel _Panel_7;

        public void LayPanel(Panel Panel_1, Panel Panel_2, Panel Panel_3, Panel Panel_4, Panel Panel_5, Panel Panel_6, Panel Panel_7)
        {
            _Panel_1 = Panel_1;
            _Panel_2 = Panel_2;
            _Panel_3 = Panel_3;
            _Panel_4 = Panel_4;
            _Panel_5 = Panel_5;
            _Panel_6 = Panel_6;
            _Panel_7 = Panel_7;
        }
        
        public void CreateTuanHoc(Panel PanelTuanHoc)
        {
            _PanelTuanHoc = PanelTuanHoc;

            string queryNBDHK = "SELECT NBDHK FROM tblTTSV";
            string NBDHK = model.GetNgayBDHK(queryNBDHK);

            char _hocKy = hocKy.Last();
            if (_hocKy == '1')
            {
                DateTime _NBDHK = DateTime.ParseExact(NBDHK, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                for (int i = 0; i < 20; i++)
                {
                    if (i != 0)
                    {
                        _NBDHK = _NBDHK.AddDays(7);

                        Label tuanHoc = new Label();
                        string name = $"tuan{i + 1}";
                        tuanHoc.Name = name;

                        tuanHoc.Text = $"Tuần {i + 1} ({_NBDHK.ToString("dd-MM-yy")})";
                        tuanHoc.Dock = DockStyle.Bottom;
                        tuanHoc.Font = new Font(tuanHoc.Font.FontFamily, 12, tuanHoc.Font.Style);
                        tuanHoc.Click += TuanHoc_Click;

                        if(DateTime.Now >= _NBDHK && DateTime.Now < _NBDHK.AddDays(7))
                        {
                            TuanHoc_Click(tuanHoc, EventArgs.Empty);
                        }

                        _PanelTuanHoc.Controls.Add(tuanHoc);
                    }
                    else
                    {
                        Label tuanHoc = new Label();
                        string name = $"tuan{i + 1}";
                        tuanHoc.Name = name;
                        tuanHoc.Text = $"Tuần {i + 1} ({_NBDHK.ToString("dd-MM-yy")})";
                        tuanHoc.Dock = DockStyle.Bottom;
                        tuanHoc.Font = new Font(tuanHoc.Font.FontFamily, 12, tuanHoc.Font.Style);
                        tuanHoc.Click += TuanHoc_Click;

                        if (DateTime.Now >= _NBDHK && DateTime.Now < _NBDHK.AddDays(7))
                        {
                            TuanHoc_Click(tuanHoc, EventArgs.Empty);
                        }

                        _PanelTuanHoc.Controls.Add(tuanHoc);
                    }
                }
            }
            else if (_hocKy == '2')
            {
                DateTime _NBDHK = DateTime.ParseExact(NBDHK, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                for (int i = 0; i < 22; i++)
                {
                    if (i != 0)
                    {
                        _NBDHK = _NBDHK.AddDays(7);

                        Label tuanHoc = new Label();
                        string name = $"tuan{i + 1}";
                        tuanHoc.Name = name;

                        tuanHoc.Text = $"Tuần {i + 1} ({_NBDHK.ToString("dd-MM-yy")})";
                        tuanHoc.Dock = DockStyle.Bottom;
                        tuanHoc.Font = new Font(tuanHoc.Font.FontFamily, 12, tuanHoc.Font.Style);
                        tuanHoc.Click += TuanHoc_Click;

                        if (DateTime.Now >= _NBDHK && DateTime.Now < _NBDHK.AddDays(7))
                        {
                            TuanHoc_Click(tuanHoc, EventArgs.Empty);
                        }

                        _PanelTuanHoc.Controls.Add(tuanHoc);
                    }
                    else
                    {
                        Label tuanHoc = new Label();
                        string name = $"tuan{i + 1}";
                        tuanHoc.Name = name;
                        tuanHoc.Text = $"Tuần {i + 1} ({_NBDHK.ToString("dd-MM-yy")})";
                        tuanHoc.Dock = DockStyle.Bottom;
                        tuanHoc.Font = new Font(tuanHoc.Font.FontFamily, 12, tuanHoc.Font.Style);
                        tuanHoc.Click += TuanHoc_Click;

                        if (DateTime.Now >= _NBDHK && DateTime.Now < _NBDHK.AddDays(7))
                        {
                            TuanHoc_Click(tuanHoc, EventArgs.Empty);
                        }

                        _PanelTuanHoc.Controls.Add(tuanHoc);
                    }
                }
            }
            else
            {
                Label tuanHoc = new Label();
                tuanHoc.Text = "Chưa có dữ liệu";

                tuanHoc.Dock = DockStyle.Top;
                tuanHoc.Font = new Font(tuanHoc.Font.FontFamily, 12, tuanHoc.Font.Style);

                _PanelTuanHoc.Controls.Add(tuanHoc);
            }
        }

        private void TuanHoc_Click(object sender, EventArgs e)
        {
            foreach(Control control in _PanelTuanHoc.Controls)
            {
                if (control is Label)
                {
                    Label label = control as Label;
                    label.ForeColor = Color.Black;
                }
            }

            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                clickedLabel.ForeColor = Color.Red;
                int tuanHoc = Convert.ToInt32(clickedLabel.Name.Substring(4));
                string ngayBDTuan = ChuyenTuanSangNgay(tuanHoc);
                LoadNgayHoc(tuanHoc,ngayBDTuan);
            }

        }

        private void LoadNgayHoc(int tuanHoc,string ngayBDTuan)
        {
            _Panel_1.Controls.Clear();
            _Panel_2.Controls.Clear();
            _Panel_3.Controls.Clear();
            _Panel_4.Controls.Clear();
            _Panel_5.Controls.Clear();
            _Panel_6.Controls.Clear();
            _Panel_7.Controls.Clear();

            for(int i =0;i<7;i++)
            {
                DateTime _ngayBDTuan = DateTime.ParseExact(ngayBDTuan, "yyyy-MM-dd", CultureInfo.CurrentCulture);
                string thu = TranslateToVietnamese(_ngayBDTuan.AddDays(i).DayOfWeek);

                string query = $"SELECT * FROM tblDataTKB WHERE NgayHoc = '{thu}' AND NgayBatDauHoc <= '{ngayBDTuan}' AND NgayKetThucHoc >= '{ngayBDTuan}' ORDER BY TietBD ASC";
                string data = model.GetDataTkb(tuanHoc, query);

                Label _data = new Label();
                _data.Text = data;
                _data.Dock = DockStyle.Fill;
                _data.Font = new Font(_data.Font.FontFamily,12, _data.Font.Style);

                if (thu == "Hai") _Panel_1.Controls.Add(_data);
                if (thu == "Ba") _Panel_2.Controls.Add(_data);
                if (thu == "Tư") _Panel_3.Controls.Add(_data);
                if (thu == "Năm") _Panel_4.Controls.Add(_data);
                if (thu == "Sáu") _Panel_5.Controls.Add(_data);
                if(thu == "Bảy") _Panel_6.Controls.Add(_data);
                if(thu == "CN") _Panel_7.Controls.Add(_data);
            }
        }

        public string ThongTinSinhVien()
        {
            return model.GetThongTinSV("SELECT * FROM tblTTSV");
        }

        private string TranslateToVietnamese(DayOfWeek dayOfWeekInEnglish)
        {
            switch (dayOfWeekInEnglish)
            {
                case DayOfWeek.Monday:
                    return "Hai";
                case DayOfWeek.Tuesday:
                    return "Ba";
                case DayOfWeek.Wednesday:
                    return "Tư";
                case DayOfWeek.Thursday:
                    return "Năm";
                case DayOfWeek.Friday:
                    return "Sáu";
                case DayOfWeek.Saturday:
                    return "Bảy";
                case DayOfWeek.Sunday:
                    return "CN";
                default:
                    return string.Empty;
            }
        }

        private string ChuyenTuanSangNgay(int tuanHoc)
        {
            string queryNBDHK = "SELECT NBDHK FROM tblTTSV";
            string NBDHK = model.GetNgayBDHK(queryNBDHK);

            char _hocKy = hocKy.Last();

            if(_hocKy == '1')
            {
                DateTime _NBDHK = DateTime.ParseExact(NBDHK, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                for (int i = 1; i <= 20; i++)
                {
                    if(i != tuanHoc)
                    {
                        _NBDHK = _NBDHK.AddDays(7);
                    }
                    if(i == tuanHoc)
                    {
                        return _NBDHK.ToString("yyyy-MM-dd");
                    }
                }
            }
            else if(_hocKy == '2')
            {
                DateTime _NBDHK = DateTime.ParseExact(NBDHK, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                for (int i = 1; i <= 22; i++)
                {
                    if (i != tuanHoc)
                    {
                        _NBDHK = _NBDHK.AddDays(7);
                    }
                    if (i == tuanHoc)
                    {
                        return _NBDHK.ToString("yyyy-MM-dd");
                    }
                }
            }
            return "";
        }

        public bool KiemTraDuLieu()
        {
            string query = "SELECT * FROM tblTTSV";
            if (model.KiemTraCoDuLieuChua(query)) return true;
            else return false;
        }
    }
}
