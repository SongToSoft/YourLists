using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManager
{
    [DataContract]
    class ArchiveObject
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

        public ArchiveObject(string _name, float _score, float _timeForComplete, int _releaseYear, bool _isCompleted)
        {
            name = _name;
            score = _score;
            timeForComplete = _timeForComplete;
            releaseYear = _releaseYear;
            isCompleted = _isCompleted;
        }
    }
}
