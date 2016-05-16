using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nancy;
using Newtonsoft.Json;
using Thirst.Core.Messages;

namespace Thirst.Web.Modules
{
    public class ConfigModule : NancyModule
    {
        private static InspectProcesses inspectProcesses = new InspectProcesses(new List<string>());

        public ConfigModule()
        {
            Get["/config"] = _ => Response.AsJson(inspectProcesses);

            Post["/config"] = _ =>
            {
                var file = Request.Files.FirstOrDefault();

                if (file == null)
                {
                    return HttpStatusCode.BadRequest;
                }

                string fileContent;

                using (var sr = new StreamReader(file.Value, Encoding.UTF8))
                {
                    fileContent = sr.ReadToEnd();
                }

                inspectProcesses = JsonConvert.DeserializeObject<InspectProcesses>(fileContent);

                return HttpStatusCode.OK;
            };
        }
    }
}
