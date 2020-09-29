using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ArchiveManager.Objects
{
    [DataContract]
    public class ArchiveObject
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public float score { get; set; }
        [DataMember]
        public float timeForComplete { get; set; }
        [DataMember]
        public int releaseYear { get; set; }
        [DataMember]
        public bool isCompleted { get; set; }
        [DataMember]
        public string genre { get; set; }
        [DataMember]
        public string creator { get; set; }
        [DataMember]
        public ECollectionType type { get; set; }
        [DataMember]
        public string platform { get; set; }
        [DataMember]
        public string image { get; set; }

        public ArchiveObject(string _name = "name", float _score = 0, float _timeForComplete = 0, int _releaseYear = 0, bool _isCompleted = false, string _genre = "", string _creator = "", ECollectionType _type = ECollectionType.ANIME, string _platform = "")
        {
            name = _name;
            score = _score;
            timeForComplete = _timeForComplete;
            releaseYear = _releaseYear;
            isCompleted = _isCompleted;
            genre = _genre;
            creator = _creator;
            type = _type;
            platform = _platform;
            image = Path.GetFullPath((Directory.GetCurrentDirectory() + @"\DataBase\Images\question_icon.png").ToString());
        }

        public void SetImagePath(string imagePath = "")
        {
            if (imagePath != "")
            {
                image = imagePath;
            }
        }
    }
}
