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
        static public List<MyWindowInterface> openWindows = new List<MyWindowInterface>();
        static public Settings setting = new Settings();

        static public string GetErrorNameText()
        {
            if (setting.language == ELanguage.ENGLISH)
            {
                return ("Incorect name, dont use /:*?<>|");
            }
            else
            {
                return ("Неверное имя, не используйте /:*?<>|");
            }
        }

        static public string GetDisplayedScoreText()
        {
            if (setting.language == ELanguage.ENGLISH)
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
            if (setting.language == ELanguage.ENGLISH)
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
            if (setting.language == ELanguage.ENGLISH)
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
            if (setting.language == ELanguage.ENGLISH)
            {
                return (" was changed");
            }
            else
            {
                return (" был изменён");
            }
        }
    }
}
