using System.IO;
using System.Runtime.Serialization.Json;

namespace ArchiveManager
{
    class SerializationJsonSystem
    {
        public static void SaveValue<T>(string fileName, T value, FileMode fileMode = FileMode.OpenOrCreate)
        {
            var jsonFormatter = new DataContractJsonSerializer(typeof(T));
            if (fileMode == FileMode.CreateNew && File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (FileStream fs = new FileStream(fileName, fileMode))
            {
                jsonFormatter.WriteObject(fs, value);
                fs.Close();
            }
        }

        public static T GetValue<T>(string fileName)
        {
            T value = default;
            if (File.Exists(fileName))
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    var jsonFormatter = new DataContractJsonSerializer(typeof(T));
                    value = (T)jsonFormatter.ReadObject(fs);
                }
            }
            return value;
        }
    }
}
