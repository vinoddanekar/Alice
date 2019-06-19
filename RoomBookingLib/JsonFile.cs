using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RoomBookingLib
{
    static class JsonFile<T>
    {
        public static T Read(string fileName)
        {
            string content;
            T result;
            using (StreamReader r = new StreamReader(fileName))
            {
                content = r.ReadToEnd();
                if (string.IsNullOrWhiteSpace(content))
                    result = (T)Activator.CreateInstance(typeof(T));
                else
                    result = JsonConvert.DeserializeObject<T>(content);
            }

            return result;
        }

        public static void Write(string fileName, T data)
        {
            string content = JsonConvert.SerializeObject(data);
            File.WriteAllText(fileName, content);
        }
    }
}
