using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ThoiKhoaBieu_VNUA_Ver2
{
    internal class ThietLapDuLieuController
    {
        Model model = new Model();
        string hocKy = ConfigurationManager.AppSettings["hocKy"].ToString();

        public bool ThemDuLieu(string url,string hocKy)
        {
            string html = LayChuoiDataHtml(url,hocKy);
            bool themDuLieu = PhanTichHtmlVaInsertDuLieu(html);

            if (themDuLieu)
            {
                return true;
            }
            else 
            {
                return false; 
            }
        }

        public void XoaDuLieuCu()
        {
            string query = "DELETE FROM tblDataTkb;DELETE FROM tblTTSV;DELETE FROM tblKiemTra;";
            if(model.KiemTraCoDuLieuChua("SELECT * FROM tblTTSV"))
            {
                if(DialogResult.Yes == MessageBox.Show("Đã có dữ liệu. Bạn thật sự muốn thiết lập lại ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    int check = model.Command(query);
                }

            }
        }
        
        private bool PhanTichHtmlVaInsertDuLieu(string html)
        {
            try
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);

                //Thêm Thông Tin Sinh Viên
                var ttsv = document.DocumentNode.Descendants("div")
                .Where(d => d.Attributes.Contains("id") && d.Attributes["id"].Value.Contains("ctl00_ContentPlaceHolder1_ctl00_pnlTKB")).FirstOrDefault();
                var tables = ttsv.Descendants("table");
                foreach (var table in tables)
                {
                    var span = table.Descendants("span").ToList();

                    string hoTen = span[3].InnerText.Trim();
                    string lop = span[5].InnerText.Trim();

                    string queryInsertTTSV = $"INSERT INTO tblTTSV (TenSV,Lop) VALUES ('{hoTen}','{lop}')";
                    int check = model.Command(queryInsertTTSV);
                }
                //----------------------------------

                var tuanBDHoc = document.DocumentNode.Descendants("span")
                    .Where(span => span.Attributes.Contains("id") && span.Attributes["id"].Value == "ctl00_ContentPlaceHolder1_ctl00_lblNote").FirstOrDefault();
                string _tuanBDHoc = TachLayNgayDauTienBDTuan(tuanBDHoc.InnerText.Trim());

                string queryInsertNBDTH = $"UPDATE tblTTSV SET NBDHK = '{_tuanBDHoc}'";
                int _check = model.Command(queryInsertNBDTH);

                // Thêm Dữ Liệu TKB
                string tenMH = "";
                string NgayHoc = "";
                string tietBD = "";
                string soTiet = "";
                string Phong = "";
                string tuanHoc = "";
                string _ngayHocTong = "";
                string ngayBDHoc = "";
                string ngayKTHoc = "";

                var TKB = document.DocumentNode.Descendants("div")
                .Where(d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("grid-roll2")).FirstOrDefault();

                ThemChuoiHTMLTKBVaoCSDL(TKB.OuterHtml.Trim());

                var tableS = TKB.Descendants("table");
                foreach (var table in tableS)
                {
                    var tdS = table.Descendants("td").ToList();
                    foreach (var _TKB in tdS)
                    {

                        tenMH = tdS.ElementAt(1).InnerText.Trim();
                        NgayHoc = tdS.ElementAt(8).InnerText.Trim();
                        tietBD = tdS.ElementAt(9).InnerText.Trim();
                        soTiet = tdS.ElementAt(10).InnerText.Trim();
                        Phong = tdS.ElementAt(11).InnerText.Trim();
                        tuanHoc = tdS.ElementAt(13).InnerText.Trim();

                        var NgayHocTong = tdS.ElementAt(13).SelectSingleNode("div");
                        _ngayHocTong = NgayHocTong.GetAttributeValue("onmouseover", "").ToString();
                        //Cắt chuỗi lấy dữ liệu ngày học tổng
                        int diemBDCat = _ngayHocTong.IndexOf("('");
                        int diemDungCat = _ngayHocTong.LastIndexOf("')");
                        if (diemBDCat >= 0 && diemDungCat >= 0)
                        {
                            string desiredText = _ngayHocTong.Substring(diemBDCat + 2, diemDungCat - diemBDCat - 2);
                            string originalText = desiredText;
                            string[] parts = originalText.Split(new string[] { "--" }, StringSplitOptions.None);

                            if (parts.Length >= 2)
                            {
                                ngayBDHoc = parts[0];
                                ngayKTHoc = parts[1];
                                ngayBDHoc = DateTime.ParseExact(ngayBDHoc, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                                ngayKTHoc = DateTime.ParseExact(ngayKTHoc, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");
                            }

                            string query = $"INSERT INTO tblDataTkb (TenMH,NgayHoc,TietBD,SoTiet,PhongHoc,TuanHoc,NgayBatDauHoc,NgayKetThucHoc) VALUES " +
                                                $"('{tenMH}','{NgayHoc}','{tietBD}','{soTiet}','{Phong}','{tuanHoc}','{ngayBDHoc}','{ngayKTHoc}')";
                            int check = model.Command(query);
                            if(check > 0)
                            {
                                break;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }

        private string LayChuoiDataHtml(string url,string hocKy)
        {
            string html = "";

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");

            IWebDriver chrome = new ChromeDriver(service, options);
            try
            {
                chrome.Navigate().GoToUrl(url);
                string capcha = chrome.PageSource;
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(capcha);

                var _capcha = doc.DocumentNode.Descendants("span").Where(span => span.Attributes.Contains("id") && span.Attributes["id"].Value == "ctl00_ContentPlaceHolder1_ctl00_lblCapcha").FirstOrDefault();
                if (_capcha != null)
                {
                    string __capcha = _capcha.InnerText.Trim();
                    var txtCapcha = chrome.FindElement(By.Id("ctl00_ContentPlaceHolder1_ctl00_txtCaptcha"));
                    txtCapcha.SendKeys(__capcha);
                    chrome.FindElement(By.Id("ctl00_ContentPlaceHolder1_ctl00_btnXacNhan")).Click();
                    Thread.Sleep(1000);
                    chrome.Navigate().GoToUrl(url);
                }
                Thread.Sleep(1000);
                if (CheckAlert(chrome))
                {
                    IAlert alert = chrome.SwitchTo().Alert();
                    alert.Accept();
                }
                IWebElement element = chrome.FindElement(By.Id("ctl00_ContentPlaceHolder1_ctl00_rad_ThuTiet"));
                element.Click();
                Thread.Sleep(1000);

                IWebElement _selectHocKy = chrome.FindElement(By.Id("ctl00_ContentPlaceHolder1_ctl00_ddlChonNHHK"));
                SelectElement select = new SelectElement(_selectHocKy);
                select.SelectByValue(hocKy);
                Thread.Sleep(1000);

                if (CheckAlert(chrome))
                {
                    IAlert alert = chrome.SwitchTo().Alert();
                    alert.Accept();
                }

                html = chrome.PageSource;
                chrome.Quit();

                return html;
            }
            catch (Exception ex)
            {
                chrome.Quit();
                return html;
            }

        }

        private bool CheckAlert(IWebDriver driver)
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string TachLayNgayDauTienBDTuan(string input)
        {
            // Tìm vị trí bắt đầu của chuỗi ngày
            int startIndex = input.IndexOf("ngày ") + "ngày ".Length;

            // Tìm vị trí kết thúc của chuỗi ngày (là vị trí ký tự đầu tiên không phải là số hoặc "/")
            int endIndex = startIndex;
            while (endIndex < input.Length && (char.IsDigit(input[endIndex]) || input[endIndex] == '/'))
            {
                endIndex++;
            }

            // Tách và in ra chuỗi ngày
            if (startIndex != -1 && endIndex > startIndex)
            {
                string ngay = input.Substring(startIndex, endIndex - startIndex);
                return ngay;
            }
            return "";
        }

        private void ThemChuoiHTMLTKBVaoCSDL(string htmlTKB)
        {
            string query = $"INSERT INTO tblKiemTra (Tkb) VALUES (@Tkb)";
            DBConnection dBConnection = new DBConnection();
            dBConnection.GetConn();
            using (SQLiteCommand cmd = new SQLiteCommand(query, dBConnection.conn))
            {
                cmd.Parameters.AddWithValue("@Tkb", htmlTKB);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
