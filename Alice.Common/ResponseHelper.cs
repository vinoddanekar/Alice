using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alice.Common
{
    public static class ResponseHelper
    {
        public static string CreateHintCommand(string text)
        {
            string result = CreateHintCommand(text,string.Empty);
            return result;
        }
        public static string CreateHintCommand(string text, string command)
        {
            string result;
            result = @"<a href=""#"" class=""aliceRequestMarker"" onclick=""javascript: autoHintRequest(this);"" ";

            if (command != string.Empty)
                result += string.Format(@" data-request=""{0}""", command);

            result += string.Format(">{0}</ a > ", text);

            return result;
        }
    }
}
