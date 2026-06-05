using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Forms
{
    public class clsTextFile
    {
        private static string Separator = "/##/";

        private static string _RemoveLastSeparator(string data)
        {
            if (string.IsNullOrEmpty(data)) return "";

            if (data.EndsWith(Separator))
            {
                return data.Substring(0, data.Length - Separator.Length);
            }

            return data;
        }

        public static List<string> ReadFirstLine(string filePath, out string errorMessage)
        {
            errorMessage = "";
            if (!File.Exists(filePath))
            {
                errorMessage = "File Doesn't exists";
                return null;
            }

            try
            {
                string firstLine = File.ReadLines(filePath).FirstOrDefault();

                if (firstLine == null)
                {
                    errorMessage = "File is empty";
                    return null;
                }

                string[] dataArray = firstLine.Split(new string[] { Separator }, StringSplitOptions.None);
                List<string> data = new List<string>(dataArray);
                return data;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }
        }

        public static bool OverWrite(string filePath, List<string> list, out string errorMessage)
        {
            errorMessage = "";
            if (!File.Exists(filePath))
            {
                errorMessage = "File Doesn't exists";
                return false;
            }

            if (list == null || list.Count == 0)
            {
                errorMessage = "No Data Provided";
                return false;
            }

            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (string item in list)
                {
                    sb.Append(item);
                    sb.Append(Separator);
                }

                string line = sb.ToString();
                line = _RemoveLastSeparator(line);

                File.WriteAllText(filePath, line);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
