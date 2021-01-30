using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels.Base.ImageServer
{


    /// <summary>
    /// Image model response of ingur api in endpoint for uploads
    /// </summary>
    public class ImageServerResponseVM
    {
        public ImageServerVM Data { get; set; }
    }

    /// <summary>
    /// model response of upload image
    /// </summary>
    public class ImageServerVM
    {
        public string Id { get; set; }
        public string Link { get; set; }
    }
}
