using System;
using System.Configuration.Abstractions;
using System.Linq;

namespace Thirst.Web.Configuration
{
    public class WebSettings
    {
        private static WebSettings settings;

        public string BaseUrlList { get; set; }

        public static WebSettings Current()
        {
            return settings ?? (settings = ConfigurationManager.Instance.AppSettings.Map<WebSettings>());
        }

        public Uri[] BaseUrlListArray()
        {
            return settings.BaseUrlList.Split(',').Select(url => new Uri(url)).ToArray();
        }
    }
}
