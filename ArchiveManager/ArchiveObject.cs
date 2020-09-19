using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManager
{
    class ArchiveObject
    {
        public string name { get; set; }
        public float score { get; set; }
        public float timeForComplete { get; set; }
        public int releaseYear { get; set; }
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
