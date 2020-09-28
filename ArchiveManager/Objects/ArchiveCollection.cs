using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ArchiveManager.Objects
{
    public enum ECollectionType
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
        private string dataBasePath;

        public ArchiveCollection(ECollectionType collectionType)
        {
            archiveObjects = new List<ArchiveObject>();
            switch (collectionType)
            {
                case ECollectionType.ANIME:
                    dataBasePath = "DataBase/animes.json";
                    break;
                case ECollectionType.BOOK:
                    dataBasePath = "DataBase/books.json";
                    break;
                case ECollectionType.FILM:
                    dataBasePath = "DataBase/films.json";
                    break;
                case ECollectionType.GAME:
                    dataBasePath = "DataBase/games.json";
                    break;
                default:
                    dataBasePath = "DataBase/animes.json";
                    break;
            }
            var collection = SerializationJsonSystem.GetValue<ArchiveCollection>(dataBasePath);
            archiveObjects = collection.GetArchiveObjects();
        }

        public List<ArchiveObject> GetArchiveObjects()
        {
            return archiveObjects;
        }

        public void AddObject(ArchiveObject newObject)
        {
            archiveObjects.Add(newObject);
            SerializationJsonSystem.SaveValue<ArchiveCollection>(dataBasePath, this);
        }

        public void RemoveObject(ArchiveObject removableObject)
        {
            archiveObjects.Remove(removableObject);
            SerializationJsonSystem.SaveValue<ArchiveCollection>(dataBasePath, this, System.IO.FileMode.CreateNew);
        }

        public void SaveArchiveObjects()
        {
            SerializationJsonSystem.SaveValue<ArchiveCollection>(dataBasePath, this);
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
