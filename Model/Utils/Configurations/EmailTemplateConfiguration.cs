using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Utils.Configuration
{
    public class EmailTemplateConfiguration
    {
        public string AccountConfirmed { get; set; }
        public string LockedUser { get; set; }
        public string PasswordRecovery { get; set; }
        public string Bill { get; set; }
    }
}
