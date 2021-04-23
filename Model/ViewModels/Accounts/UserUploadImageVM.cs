using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Accounts
{
    public class UserUploadImageVM
    {
        public IFormFile Img { get; set; }
        public string Id { get; set; }
    }
}
