using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Drawing;
using System;

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
            if (File.Exists(dataBasePath))
            {
                var collection = SerializationJsonSystem.GetValue<ArchiveCollection>(dataBasePath);
                archiveObjects = collection.GetArchiveObjects();
            }
            else
            {
                SerializationJsonSystem.SaveValue<ArchiveCollection>(dataBasePath, this, System.IO.FileMode.CreateNew);
            }
            CheckImages();
        }

        public List<ArchiveObject> GetArchiveObjects()
        {
            return archiveObjects;
        }

        public void AddObject(ArchiveObject newObject, string objectImage = "")
        {
            string destFileNameImage = "";
            if (File.Exists(objectImage) && objectImage != null && objectImage != "")
            {
                destFileNameImage = Directory.GetCurrentDirectory() + imagesPath + newObject.name + ".jpg";
                if (objectImage != destFileNameImage)
                {
                    newObject.SetImagePath(Path.GetFullPath((Directory.GetCurrentDirectory() + @"\DataBase\Images\question_icon.png").ToString()));
                    if (File.Exists(destFileNameImage))
                    {
                        var randomize = new Random();
                        destFileNameImage = Directory.GetCurrentDirectory() + imagesPath + newObject.name + "_temporary_name_" + randomize.Next(0, int.MaxValue) + ".jpg";
                    }
                    File.Copy(objectImage, destFileNameImage, true);
                }
                newObject.SetImagePath(destFileNameImage);
            }
            archiveObjects.Add(newObject);
            SerializationJsonSystem.SaveValue<ArchiveCollection>(dataBasePath, this);
        }

        private void CheckImages()
        {
            bool needSave = false;
            for (int i = 0; i < archiveObjects.Count; ++i)
            {
                if (archiveObjects[i].image.Contains("temporary_name"))
                {
                    string destFileNameImage = Directory.GetCurrentDirectory() + imagesPath + archiveObjects[i].name + ".jpg";
                    if (File.Exists(destFileNameImage))
                    {
                        File.Delete(destFileNameImage);
                    }
                    File.Copy(archiveObjects[i].image, destFileNameImage);
                    File.Delete(archiveObjects[i].image);
                    archiveObjects[i].SetImagePath(destFileNameImage);
                    needSave = true;
                }
                else
                {
                    if (archiveObjects[i].image == "" || !File.Exists(archiveObjects[i].image))
                    {
                        archiveObjects[i].SetImagePath(Path.GetFullPath((Directory.GetCurrentDirectory() + @"\DataBase\Images\question_icon.png").ToString()));
                    }
                }
            }
            var imageFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + imagesPath);
            for (int i = 0; i < imageFiles.Length; ++i)
            {
                bool needDelete = true;
                for (int j = 0; j < archiveObjects.Count; ++j)
                {
                    if (imageFiles[i] == archiveObjects[j].image)
                    {
                        needDelete = false;
                        break;
                    }
                }
                if (needDelete)
                {
                    File.Delete(imageFiles[i]);
                }
            }
            if (needSave)
            {
                SerializationJsonSystem.SaveValue<ArchiveCollection>(dataBasePath, this, System.IO.FileMode.CreateNew);
            }
        }

        public void RemoveObject(ArchiveObject removableObject)
        {
            archiveObjects.Remove(removableObject);
            //if (removableObject.image != Path.GetFullPath((Directory.GetCurrentDirectory() + @"\DataBase\Images\question_icon.png").ToString()))
            //{
            //    File.Delete(removableObject.image);
            //}
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
