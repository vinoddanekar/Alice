using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Alice.Framework
{
    class UnhandledMessageRepository
    {
        private string _fileName;
        private string DataFile
        {
            get
            {
                string filePath = HttpContext.Current.Server.MapPath("~/app_data/AliceRequests");
                filePath = Path.Combine(filePath, _fileName);
                return filePath;
            }
        }

        public UnhandledMessageRepository(string fileName)
        {
            _fileName = fileName;
        }

        public void Add(UnhandledMessage unhandledMessage)
        {
            List<UnhandledMessage> list = List();

            UnhandledMessage existingMessage = list.Find(o => o.UserMessage.Equals(unhandledMessage.UserMessage, System.StringComparison.InvariantCultureIgnoreCase));
            if (existingMessage == null)
                list.Add(unhandledMessage);
            else
                existingMessage.InstanceCount += 1;

            Common.JsonFile<IList<UnhandledMessage>>.Write(DataFile, list);
        }
        
        public List<UnhandledMessage> List()
        {
            if (!File.Exists(DataFile))
                return new List<UnhandledMessage>();

            List<UnhandledMessage> list;
            list = Common.JsonFile<List<UnhandledMessage>>.Read(DataFile);

            return list;
        }
    }
}
