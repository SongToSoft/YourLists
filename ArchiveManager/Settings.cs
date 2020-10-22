using System;
using System.Collections.Generic;
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
    }
}
