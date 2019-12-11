using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineShop.ExtensionMethods
{
    public class NotFoundView : ViewResult
    {
        /// <summary>
        /// This method call the page 404 custom
        /// </summary>
        /// <param name="name"></param>
        public NotFoundView(string name = "NotFound")
        {
            ViewName = name;
            StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
