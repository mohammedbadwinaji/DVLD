using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    internal class clsImageDataAccess
    {
        public static string Save(string imagePath)
        {
            if( string.IsNullOrEmpty(imagePath)
                || (! File.Exists(imagePath)) )
            {
                return null;
            }
           
                Guid guid = Guid.NewGuid();
                string copyImagePath = clsSettings.PeopleImagesStoragePath + guid.ToString() + ".png";
                File.Copy(imagePath, copyImagePath);

                return copyImagePath;


        }
        public static bool Delete(string imagePath) {
            if (string.IsNullOrEmpty(imagePath)
                || (!File.Exists(imagePath)))
            {
                return false;

            }
            File.Delete(imagePath);
            return true;
        }
    }
}
