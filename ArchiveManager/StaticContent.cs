using ArchiveManager.Objects;
using ArchiveManager.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ArchiveManager
{

    interface MyWindowInterface
    {
        void SetLanguage();
        void SetEngLanguage();
        void SetRuLanguage();
    }

    static class StaticContent
    {
        static public ArchiveCollection animeCollection, bookCollection, filmCollection, gameCollection;
        static public ListView animeListView, bookListView, filmListView, gameListView;
        static public List<MyWindowInterface> openWindows;
        static public Settings setting;

        static public void Init()
        {
            openWindows = new List<MyWindowInterface>();
            setting = new Settings();
            setting.GetSettings();
        }

        static public string GetErrorNameText()
        {
            if (setting.GetLanguage() == ELanguage.ENGLISH)
            {
                return ("Incorect name, dont use /:*?<>|");
            }
            else
            {
                return ("Неверное имя, не используйте /:*?<>|");
            }
        }

        static public string GetDuplicateNameText()
        {
            if (setting.GetLanguage() == ELanguage.ENGLISH)
            {
                return ("This name is already in use");
            }
            else
            {
                return ("Данное имя уже используется");
            }
        }

        static public string GetDisplayedScoreText()
        {
            if (setting.GetLanguage() == ELanguage.ENGLISH)
            {
                return ("Score not defined");
            }
            else
            {
                return ("Оценка не определена");
            }
        }

        static public string GetFileAddMessage()
        {
            if (setting.GetLanguage() == ELanguage.ENGLISH)
            {
                return (" was added");
            }
            else
            {
                return (" был добавлен");
            }
        }

        static public string GetFileExistAddMessage()
        {
            if (setting.GetLanguage() == ELanguage.ENGLISH)
            {
                return (" already exist in collection");
            }
            else
            {
                return (" уже содержится в коллекции");
            }
        }

        static public string GetChangeMessage()
        {
            if (setting.GetLanguage() == ELanguage.ENGLISH)
            {
                return (" was changed");
            }
            else
            {
                return (" был изменён");
            }
        }

        static public string GetAddObjectWrongTypeMessage()
        {
            if (setting.GetLanguage() == ELanguage.ENGLISH)
            {
                return ("Object type not selected");
            }
            else
            {
                return ("Тип объекта не выбран");
            }
        }
    }
}
