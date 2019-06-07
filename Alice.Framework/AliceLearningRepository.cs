using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Alice.Common;

namespace Alice.Framework
{
    public class AliceCommandRepository
    {
        private string _fileName;
        private string DataFile
        {
            get
            {
                string filePath = HttpContext.Current.Server.MapPath("~/app_data/");
                filePath = Path.Combine(filePath, _fileName);
                return filePath;
            }
        }

        public AliceCommandRepository()
        {
        }

        public AliceCommandRepository(string fileName)
        {
            _fileName = fileName;
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