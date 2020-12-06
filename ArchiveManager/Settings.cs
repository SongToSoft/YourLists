using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

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
        private ELanguage language = ELanguage.ENGLISH;
        private string path = @"/settings.json";
        private string fullpath;

        public Settings()
        {
            fullpath = Path.GetFullPath(Directory.GetCurrentDirectory() + path);
            List<string> folders = new List<string> { "/DataBase", "/DataBase/Images", "/DataBase/Images", "/DataBase/Images/Animes", "/DataBase/Images/Books", "/DataBase/Images/Films", "/DataBase/Images/Games" }; 
            for (int i = 0; i < folders.Count; ++i)
            {
                if (!Directory.Exists(Path.GetFullPath(Directory.GetCurrentDirectory() + folders[i])))
                {
                    Directory.CreateDirectory(Path.GetFullPath(Directory.GetCurrentDirectory() + folders[i]));
                    Console.WriteLine(folders[i] + " directory dont exist");
                }
            }
        }

        public void GetSettings()
        {
            Console.WriteLine(fullpath);
            if (File.Exists(fullpath))
            {
                var settings = SerializationJsonSystem.GetValue<Settings>(fullpath);
                language = settings.language;
                Console.WriteLine("Settings file exist");
            }
            else
            {
                Console.WriteLine("Settings file not exist");
                SaveSettings();
            }
        }

        public void SaveSettings()
        {
            SerializationJsonSystem.SaveValue<Settings>(fullpath, this, FileMode.CreateNew);
        }

        public ELanguage GetLanguage()
        {
            return language;
        }

        public void SetLanguage(ELanguage _language)
        {
            language = _language;
            SaveSettings();
        }

    }
}
