using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArchiveManager
{
    public partial class MainWindow : Window
    {
        private ArchiveCollection animeCollection, bookCollection, filmCollection, gameCollection;

        public MainWindow()
        {
            InitializeComponent();

            LoadAnimeList();
            LoadBookList();
            LoadFilmList();
            LoadGameList();
        }

        public void LoadAnimeList()
        {
            animeCollection = new ArchiveCollection(ECollectionType.ANIME);
            var animeObjects = animeCollection.GetArchiveObjects();
            for (int i = 0; i < animeObjects.Count; ++i)
            {
                AnimeListView.Items.Add(animeObjects[i]);
            }
            animeCollection.SaveArchiveObjects();
        }

        public void LoadBookList()
        {
            bookCollection = new ArchiveCollection(ECollectionType.BOOK);
            var bookObjects = bookCollection.GetArchiveObjects();
            for (int i = 0; i < bookObjects.Count; ++i)
            {
                BookListView.Items.Add(bookObjects[i]);
            }
            bookCollection.SaveArchiveObjects();
        }

        public void LoadFilmList()
        {
            filmCollection = new ArchiveCollection(ECollectionType.FILM);
            var filmObjects = filmCollection.GetArchiveObjects();
            for (int i = 0; i < filmObjects.Count; ++i)
            {
                FilmListView.Items.Add(filmObjects[i]);
            }
            filmCollection.SaveArchiveObjects();
        }

        public void LoadGameList()
        {
            gameCollection = new ArchiveCollection(ECollectionType.GAME);
            var gameObjects = gameCollection.GetArchiveObjects();
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                GameListView.Items.Add(gameObjects[i]);
            }
            gameCollection.SaveArchiveObjects();
        }
    }
}
