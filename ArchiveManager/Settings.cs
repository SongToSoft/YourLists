using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManager
{
    public enum ELanguage
    {
        ENGLISH,
        RUSSIAN
    }

    [DataContract]
    class Settings
    {
        [DataMember]
        public ELanguage language = ELanguage.ENGLISH;
        private string path = @"/settings.json";

        public Settings()
        {
            GetSettings();
        }

        public void GetSettings()
        {
            string fullpath = Path.GetFullPath(Directory.GetCurrentDirectory() + path);
            Console.WriteLine(fullpath);
            if (File.Exists(fullpath))
            {
                Console.WriteLine("Settings file exist");
            }
            else
            {
                Console.WriteLine("Settings file not exist");
                File.Create(fullpath);
            }
        }

        public void SaveSettings()
        {

        }

    }
}
