using System;
using System.Collections.Generic;
using System.IO;

namespace DVLD.Forms
{
    public static class clsRememberMeManager
    {

        public static string RememberMeDataPath = clsSettings.RememberMeDataPath;
        public static bool SaveCredentials(string username, string password, out string errorMessage)
        {
            errorMessage = "";

            string directory = Path.GetDirectoryName(RememberMeDataPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(RememberMeDataPath))
            {
                File.Create(RememberMeDataPath).Close();
            }

            List<string> credentialsList = new List<string> { username, password };

            return clsTextFile.OverWrite(RememberMeDataPath, credentialsList, out errorMessage);
        }

        public static bool GetStoredCredentials(out string username, out string password, out string errorMessage)
        {
            username = "";
            password = "";
            errorMessage = "";

            if (!File.Exists(RememberMeDataPath))
            {
                return false;
            }

            List<string> data = clsTextFile.ReadFirstLine(RememberMeDataPath, out errorMessage);

            if (data != null && data.Count >= 2)
            {
                username = data[0];
                password = data[1];
                return true;
            }

            return false;
        }


        public static bool ClearCredentials(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                if (File.Exists(RememberMeDataPath))
                {
                    File.Delete(RememberMeDataPath);
                }
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
