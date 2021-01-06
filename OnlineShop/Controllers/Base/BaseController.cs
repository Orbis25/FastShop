using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        public void SendNotification(string title = "", string text = "" , NotificationEnum type = NotificationEnum.Success)
        {
            TempData["Notification"] = $"Swal.fire('{title}','{text}','{type.ToString().ToLower()}')";
        }
    }
}
