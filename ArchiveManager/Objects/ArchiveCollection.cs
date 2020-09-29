using System.Collections.Generic;
using System.IO;
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
        private string imagesPath;

        public ArchiveCollection(ECollectionType collectionType)
        {
            archiveObjects = new List<ArchiveObject>();
            switch (collectionType)
            {
                case ECollectionType.ANIME:
                    dataBasePath = "DataBase/animes.json";
                    imagesPath = "/DataBase/Images/Animes/";
                    break;
                case ECollectionType.BOOK:
                    dataBasePath = "DataBase/books.json";
                    imagesPath = "/DataBase/Images/Books/";
                    break;
                case ECollectionType.FILM:
                    dataBasePath = "DataBase/films.json";
                    imagesPath = "/DataBase/Images/Films/";
                    break;
                case ECollectionType.GAME:
                    dataBasePath = "DataBase/games.json";
                    imagesPath = "/DataBase/Images/Games/";
                    break;
                default:
                    //dataBasePath = "DataBase/animes.json";
                    //imagesPath = "DataBase/Images/Games/";
                    break;
            }
            var collection = SerializationJsonSystem.GetValue<ArchiveCollection>(dataBasePath);
            archiveObjects = collection.GetArchiveObjects();
        }

        public List<ArchiveObject> GetArchiveObjects()
        {
            return archiveObjects;
        }

        public void AddObject(ArchiveObject newObject, string objectImage = "")
        {
            string destFileNameImage = "";
            if (objectImage != null && objectImage != "")
            {
                destFileNameImage = Directory.GetCurrentDirectory() + imagesPath + newObject.name + ".jpg";
                if (File.Exists(destFileNameImage))
                {
                    File.Delete(destFileNameImage);
                }
                newObject.SetImagePath(destFileNameImage);
                File.Copy(objectImage, destFileNameImage);
            }
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
