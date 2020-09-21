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
        }

        public void LoadBookList()
        {
            bookCollection = new ArchiveCollection(ECollectionType.BOOK);
            var bookObjects = bookCollection.GetArchiveObjects();
            for (int i = 0; i < bookObjects.Count; ++i)
            {
                BookListView.Items.Add(bookObjects[i]);
            }
        }

        public void LoadFilmList()
        {
            filmCollection = new ArchiveCollection(ECollectionType.FILM);
            var filmObjects = filmCollection.GetArchiveObjects();
            for (int i = 0; i < filmObjects.Count; ++i)
            {
                FilmListView.Items.Add(filmObjects[i]);
            }
        }

        public void LoadGameList()
        {
            gameCollection = new ArchiveCollection(ECollectionType.GAME);
            var gameObjects = gameCollection.GetArchiveObjects();
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                GameListView.Items.Add(gameObjects[i]);
            }
        }

        private void AddObjectButton_Click(object sender, RoutedEventArgs e)
        {
            string name = AddObjectName.GetLineText(0);
            float score = 0;
            float timeForComplete = 0;
            int releaseYear = 0;
            try
            {
                score = float.Parse(AddObjectScore.GetLineText(0));
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect Score on Add");
            }
            try
            {
                timeForComplete = float.Parse(AddObjectTimeForComplete.GetLineText(0));
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect Time For Complete on Add");
            }
            try
            {
                releaseYear = int.Parse(AddObjectReleaseYear.GetLineText(0));
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect Release Year on Add");
            }
            bool isCompleted = (AddObjectIsCompleted.IsChecked != null) ? AddObjectIsCompleted.IsChecked.Value : false;
            ArchiveObject newObject = new ArchiveObject(name, score, timeForComplete, releaseYear, isCompleted);

            ComboBox comboBox = (ComboBox)AddObjectType;
            var selectedItem = (TextBlock)comboBox.SelectedItem;
            if (selectedItem != null)
            {
                switch (selectedItem.Text)
                {
                    case "Anime":
                        animeCollection.AddObject(newObject);
                        MessageBox.Show(selectedItem.Text + ": " + name + " was added");
                        AnimeListView.Items.Add(newObject);
                        break;
                    case "Book":
                        bookCollection.AddObject(newObject);
                        MessageBox.Show(selectedItem.Text + ": " + name + " was added");
                        BookListView.Items.Add(newObject);
                        break;
                    case "Film":
                        filmCollection.AddObject(newObject);
                        MessageBox.Show(selectedItem.Text + ": " + name + " was added");
                        FilmListView.Items.Add(newObject);
                        break;
                    case "Game":
                        gameCollection.AddObject(newObject);
                        MessageBox.Show(selectedItem.Text + ": " + name + " was added");
                        GameListView.Items.Add(newObject);
                        break;
                }
            }
        }
    }
}
