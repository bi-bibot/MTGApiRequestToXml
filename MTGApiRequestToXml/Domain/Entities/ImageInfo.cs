using MTGApiRequestToXml.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGApiRequestToXml.Domain.Entities
{
    public class ImageInfo
    {
        public string Url { get; set; }
        public string FileName { get; set; }

        public ImageInfo() 
        {
            //
        }

        public ImageInfo(string url, string name)
        {
            Url = url;
            FileName = RegExUtil.FormatCardName(name) + ".jpg";
        }

    }
}
