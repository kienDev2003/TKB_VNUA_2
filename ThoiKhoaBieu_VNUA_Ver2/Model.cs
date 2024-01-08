using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ThoiKhoaBieu_VNUA_Ver2
{
    internal class Model
    {
        DBConnection DBConn = new DBConnection();

        public int Command(string query)
        {
            DBConn.GetConn();
            using (SQLiteCommand cmd = new SQLiteCommand(query, DBConn.conn))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        public string GetDataTkb(int tuanHT, string query)
        {
            string data = "";

            DBConn.GetConn();
            using (SQLiteCommand cmd = new SQLiteCommand(query, DBConn.conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tenMH = reader["TenMH"].ToString();
                        string tietBD = reader["TietBD"].ToString();
                        tietBD = ChuyenDoiTietBatDau(tietBD);
                        string soTiet = reader["soTiet"].ToString();
                        string phongHoc = reader["PhongHoc"].ToString();
                        string tuanHoc = reader["TuanHoc"].ToString();

                        int[] _tuanHoc = ExtractNumbers(tuanHoc);
                        bool check = false;
                        for (int i = 0; i < _tuanHoc.Length; i++)
                        {
                            if (_tuanHoc[i] == tuanHT)
                            {
                                check = true;
                            }
                        }
                        if (check)
                        {
                            data += $"Tên MH: {tenMH}\n" + $"Tiết BD: {tietBD}\n" + $"Số tiết: {soTiet}\n" + $"Phòng: {phongHoc}\n\n";
                        }
                    }
                }
            }
            if (data.Length == 0)
            {
                return "Không có môn học nào";
            }
            return data.Replace("&amp;", " và ");
        }

        public string GetThongTinSV(string query)
        {
            string data = "";

            DBConn.GetConn();
            using (SQLiteCommand cmd = new SQLiteCommand(query, DBConn.conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tenSV = reader["TenSV"].ToString();
                        string lop = reader["Lop"].ToString();

                        data = $"Sinh viên: {tenSV.Replace("&amp;", " và ")} Lớp: {lop.Replace("&amp;", " và ")}";
                    }
                }
            }
            if (data.Length == 0)
            {
                return "Chưa có dữ liệu";
            }
            return data;
        }

        public bool KiemTraCoDuLieuChua(string query)
        {
            DBConn.GetConn();
            using (SQLiteCommand cmd = new SQLiteCommand(query, DBConn.conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return true;
                }
            }
            return false;
        }

        public string GetNgayBDHK(string query)
        {
            DBConn.GetConn();
            using (SQLiteCommand cmd = new SQLiteCommand(query, DBConn.conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string ngayBDHK = reader["NBDHK"].ToString();

                        return ngayBDHK;
                    }
                }
            }
            return "";
        }

        private string ChuyenDoiTietBatDau(string tietBD)
        {
            switch (tietBD)
            {
                case "1":
                    return "1 ( 7:00 )";
                case "2":
                    return "2 ( 7:55 )";
                case "3":
                    return "3 ( 8:50 )";
                case "4":
                    return "4 ( 9:55 )";
                case "5":
                    return "5 ( 10:50 )";
                case "6":
                    return "6 ( 12:45 )";
                case "7":
                    return "7 ( 13:40 )";
                case "8":
                    return "8 ( 14:35 )";
                case "9":
                    return "9 ( 15:40 )";
                case "10":
                    return "10 ( 16:35 )";
                case "11":
                    return "11 ( 18:00 )";
                case "12":
                    return "12 ( 18:55 )";
                case "13":
                    return "13 ( 19:50 )";
                default:
                    return string.Empty;
            }
        }

        private int[] ExtractNumbers(string tuanHoc)
        {
            List<int> numbers = new List<int>();
            int x = 1;

            for (int i = 0; i < tuanHoc.Length; i++)
            {
                if (tuanHoc[i] == '-')
                {
                    numbers.Add(x);
                    x += 1;
                }
                else
                {
                    if (int.Parse(tuanHoc[i].ToString()) < x)
                    {
                        numbers.Add(x);
                    }
                    else
                    {
                        numbers.Add(int.Parse(tuanHoc[i].ToString()));
                    }
                    x += 1;
                }
            }

            for (int i = 0; i < tuanHoc.Length; i++)
            {
                if (tuanHoc[i] == '-')
                {
                    numbers.RemoveAll(item => item == i + 1);
                }
            }

            return numbers.ToArray();
        }
    }
}
