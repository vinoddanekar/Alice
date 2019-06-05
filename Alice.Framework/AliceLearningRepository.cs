using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Alice.Common;

namespace Alice.Framework
{
    public class AliceLearningRepository
    {
        private string DataFile
        {
            get
            {
                string virtualPath = "~/app_data/bot-learning.json";
                return HttpContext.Current.Server.MapPath(virtualPath);
            }
        }

        public IList<Command> List()
        {
            IList<Command> communications;
            if (!File.Exists(DataFile))
                return new List<Command>();

            using (StreamReader r = new StreamReader(DataFile))
            {
                string json = r.ReadToEnd();
                if (string.IsNullOrWhiteSpace(json))
                    communications = new List<Command>();
                else
                    communications = JsonConvert.DeserializeObject<List<Command>>(json);
            }

            return communications;
        }
    }
}