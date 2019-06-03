using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TestWebApp.Alice.Fx
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

        public IList<Communication> List()
        {
            IList<Communication> communications;
            if (!File.Exists(DataFile))
                return new List<Communication>();

            using (StreamReader r = new StreamReader(DataFile))
            {
                string json = r.ReadToEnd();
                if (string.IsNullOrWhiteSpace(json))
                    communications = new List<Communication>();
                else
                    communications = JsonConvert.DeserializeObject<List<Communication>>(json);
            }

            return communications;
        }
    }
}