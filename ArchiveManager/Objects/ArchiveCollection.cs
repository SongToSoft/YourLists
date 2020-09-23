using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveManager.Objects
{
    enum ECollectionType
    {
        ANIME,
        BOOK,
        FILM,
        GAME
    }

    [DataContract]
    class ArchiveCollection
    {
        [DataMember]
        private List<ArchiveObject> archiveObjects;
        private string fileName;

        public ArchiveCollection(ECollectionType collectionType)
        {
            archiveObjects = new List<ArchiveObject>();
            switch (collectionType)
            {
                case ECollectionType.ANIME:
                    fileName = "DataBase/anime.json";
                    break;
                case ECollectionType.BOOK:
                    fileName = "DataBase/book.json";
                    break;
                case ECollectionType.FILM:
                    fileName = "DataBase/film.json";
                    break;
                case ECollectionType.GAME:
                    fileName = "DataBase/game.json";
                    break;
                default:
                    fileName = "DataBase/anime.json";
                    break;
            }
            var collection = SerializationJsonSystem.GetValue<ArchiveCollection>(fileName);
            archiveObjects = collection.GetArchiveObjects();
        }

        public List<ArchiveObject> GetArchiveObjects()
        {
            return archiveObjects;
        }

        public void AddObject(ArchiveObject newObject)
        {
            archiveObjects.Add(newObject);
            SerializationJsonSystem.SaveValue<ArchiveCollection>(fileName, this);
        }

        public void RemoveObject(ArchiveObject removableObject)
        {
            archiveObjects.Remove(removableObject);
            SerializationJsonSystem.SaveValue<ArchiveCollection>(fileName, this, System.IO.FileMode.CreateNew);
        }

        public void SaveArchiveObjects()
        {
            SerializationJsonSystem.SaveValue<ArchiveCollection>(fileName, this);
        }

        public bool IsContain(string name)
        {
            for (int i = 0; i < archiveObjects.Count; ++i)
            {
                if (archiveObjects[i].name == name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
