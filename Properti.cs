﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace PemesananTiket
{
    internal class Properti
    {

        public static int id_penumpang = 0;

        public static SqlConnection conn = new SqlConnection("data source = DESKTOP-18L8S2S; initial catalog = BookingTiket; integrated security = true");
        public static bool validasi(Control.ControlCollection container, TextBoxBase bolehkosong= null)
        {
            foreach (Control control in container)
            {
                if(control is TextBoxBase textBox && string.IsNullOrWhiteSpace(textBox.Text) && textBox != bolehkosong)
                {
                    return true;
                }
            }

            return false;
        }


        public static bool Number(string number)
        {
            return double.TryParse(number, out _);
        }

        public static bool Email(string email)
        {

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            } catch { return false; }   

        }

        public static string enkripsi(string input)
        {
            MD5 md5= MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i<bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
