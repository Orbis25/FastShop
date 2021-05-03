using DataLayer.Enums.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShop.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        public void SendNotification(string title = "", string text = "", NotificationEnum type = NotificationEnum.Success)
        {
            TempData["Notification"] = $"Swal.fire('{title}','{text}','{type.ToString().ToLower()}')";
        }


        public string GetLoggedIdUser() => User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public string GetModelStateErrorSummary(ModelStateDictionary modelState) 
        {
            string result = string.Empty;
            foreach (var Model in modelState.Values)
            {
                foreach (var item in Model.Errors) result += $"<b>{item.ErrorMessage} <br/></b>";
            }
            return result;
        }
    }
}
