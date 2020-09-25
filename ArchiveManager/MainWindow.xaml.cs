using ArchiveManager.Objects;
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
            StaticContent.animeCollection = new ArchiveCollection(ECollectionType.ANIME);
            StaticContent.animeListView = AnimeListView;

            var animeObjects = StaticContent.animeCollection.GetArchiveObjects();
            for (int i = 0; i < animeObjects.Count; ++i)
            {
                AnimeListView.Items.Add(animeObjects[i]);
            }
        }

        public void LoadBookList()
        {
            StaticContent.bookCollection = new ArchiveCollection(ECollectionType.BOOK);
            StaticContent.bookListView = BookListView;

            var bookObjects = StaticContent.bookCollection.GetArchiveObjects();
            for (int i = 0; i < bookObjects.Count; ++i)
            {
                BookListView.Items.Add(bookObjects[i]);
            }
        }

        public void LoadFilmList()
        {
            StaticContent.filmCollection = new ArchiveCollection(ECollectionType.FILM);
            StaticContent.filmListView = FilmListView;

            var filmObjects = StaticContent.filmCollection.GetArchiveObjects();
            for (int i = 0; i < filmObjects.Count; ++i)
            {
                FilmListView.Items.Add(filmObjects[i]);
            }
        }

        public void LoadGameList()
        {
            StaticContent.gameCollection = new ArchiveCollection(ECollectionType.GAME);
            StaticContent.gameListView = GameListView;

            var gameObjects = StaticContent.gameCollection.GetArchiveObjects();
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                GameListView.Items.Add(gameObjects[i]);
            }
        }

        private void AddObjectButton_Click(object sender, RoutedEventArgs e)
        {
            string name = AddObjectName.GetLineText(0);
            float score = 0;
            string genre = AddObjectGenre.GetLineText(0);
            string creator = AddObjectCreator.GetLineText(0);
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
            ArchiveObject newObject = new ArchiveObject(name, score, timeForComplete, releaseYear, isCompleted, genre, creator);

            ComboBox comboBox = (ComboBox)AddObjectType;
            var selectedItem = (TextBlock)comboBox.SelectedItem;
            if (selectedItem != null)
            {
                switch (selectedItem.Text)
                {
                    case "Anime":
                        if (!StaticContent.animeCollection.IsContain(name))
                        {
                            newObject.type = ECollectionType.ANIME;
                            StaticContent.animeCollection.AddObject(newObject);
                            MessageBox.Show(selectedItem.Text + ": " + name + " was added");
                            AnimeListView.Items.Add(newObject);
                        }
                        else
                        {
                            MessageBox.Show(selectedItem.Text + ": " + name + " already  exist in collection");
                        }
                        break;
                    case "Book":
                        if (!StaticContent.bookCollection.IsContain(name))
                        {
                            newObject.type = ECollectionType.BOOK;
                            StaticContent.bookCollection.AddObject(newObject);
                            MessageBox.Show(selectedItem.Text + ": " + name + " was added");
                            BookListView.Items.Add(newObject);
                        }
                        else
                        {
                            MessageBox.Show(selectedItem.Text + ": " + name + " already  exist in collection");
                        }
                        break;
                    case "Film":
                        if (!StaticContent.filmCollection.IsContain(name))
                        {
                            newObject.type = ECollectionType.FILM;
                            StaticContent.filmCollection.AddObject(newObject);
                            MessageBox.Show(selectedItem.Text + ": " + name + " was added");
                            FilmListView.Items.Add(newObject);
                        }
                        else
                        {
                            MessageBox.Show(selectedItem.Text + ": " + name + " already  exist in collection");
                        }
                        break;
                    case "Game":
                        if (!StaticContent.gameCollection.IsContain(name))
                        {
                            newObject.type = ECollectionType.GAME;
                            StaticContent.gameCollection.AddObject(newObject);
                            MessageBox.Show(selectedItem.Text + ": " + name + " was added");
                            GameListView.Items.Add(newObject);
                        }
                        else
                        {
                            MessageBox.Show(selectedItem.Text + ": " + name + " already  exist in collection");
                        }
                        break;
                }
            }
        }

        private void DeleteAnimeObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = AnimeListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                StaticContent.animeCollection.RemoveObject(selectedItem);
                AnimeListView.Items.Remove(AnimeListView.SelectedItem);
            }
        }

        private void DeleteBookObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = BookListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                StaticContent.bookCollection.RemoveObject(selectedItem);
                BookListView.Items.Remove(BookListView.SelectedItem);
            }
        }

        private void DeleteFilmObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = FilmListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                StaticContent.filmCollection.RemoveObject(selectedItem);
                FilmListView.Items.Remove(FilmListView.SelectedItem);
            }
        }

        private void DeleteGameObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GameListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                StaticContent.gameCollection.RemoveObject(selectedItem);
                GameListView.Items.Remove(GameListView.SelectedItem);
            }
        }

        private void ChangeAnimeObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = AnimeListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var changeObjectWindow = new ChangeObjectWindow();
                changeObjectWindow.SetValuesInFields(selectedItem);
                changeObjectWindow.Show();
            }
        }

        private void ChangeBookObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = BookListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var changeObjectWindow = new ChangeObjectWindow();
                changeObjectWindow.SetValuesInFields(selectedItem);
                changeObjectWindow.Show();
            }
        }

        private void ChangeFilmObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = FilmListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var changeObjectWindow = new ChangeObjectWindow();
                changeObjectWindow.SetValuesInFields(selectedItem);
                changeObjectWindow.Show();
            }
        }

        private void ChangeGameObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GameListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var changeObjectWindow = new ChangeObjectWindow();
                changeObjectWindow.SetValuesInFields(selectedItem);
                changeObjectWindow.Show();
            }
        }
    }
}
