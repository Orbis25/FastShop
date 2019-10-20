using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Settings
{
    public class EmailSetting
    {
        public string User { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool DefaultCredentials { get; set; }
        public string Smtp { get; set; }
    }
}
