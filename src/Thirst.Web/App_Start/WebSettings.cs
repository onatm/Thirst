using System.Configuration.Abstractions;

namespace Thirst.Web
{
    public class WebSettings
    {
        private static WebSettings settings;

        public string BaseUrls { get; set; }

        public static WebSettings Current()
        {
            return settings ?? (settings = ConfigurationManager.Instance.AppSettings.Map<WebSettings>());
        }
    }
}
