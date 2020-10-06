using ArchiveManager.Objects;
using Microsoft.Win32;
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

namespace ArchiveManager.Windows
{
    public partial class MainWindow : Window
    {
        private string objectImage = "";

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
            int score = 0;
            string genre = AddObjectGenre.GetLineText(0);
            string creator = AddObjectCreator.GetLineText(0);
            float timeForComplete = 0;
            int releaseYear = 0;
            try
            {
                score = int.Parse(AddObjectScore.GetLineText(0));
                if (score > 100)
                {
                    score = 100;
                }
                else
                {
                    if (score < 0)
                    {
                        score = 0;
                    }
                }
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
                            StaticContent.animeCollection.AddObject(newObject, objectImage);
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
                            StaticContent.bookCollection.AddObject(newObject, objectImage);
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
                            StaticContent.filmCollection.AddObject(newObject, objectImage);
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
                            newObject.platform = AddObjectPlatform.Text;
                            newObject.type = ECollectionType.GAME;
                            StaticContent.gameCollection.AddObject(newObject, objectImage);
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
            ClearField();
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

        private void AddObjectType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)AddObjectType;
            var selectedItem = (TextBlock)comboBox.SelectedItem;
            if (selectedItem != null)
            {
                if (selectedItem.Text != "Anime")
                {
                    AddObjectCreator.Visibility = Visibility.Visible;
                    AddObjectCreatorLabel.Visibility = Visibility.Visible;
                    if (selectedItem.Text == "Game")
                    {
                        AddObjectPlatform.Visibility = Visibility.Visible;
                        AddObjectPlatformLabel.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AddObjectPlatform.Visibility = Visibility.Hidden;
                        AddObjectPlatformLabel.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    AddObjectCreator.Visibility = Visibility.Hidden;
                    AddObjectCreatorLabel.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ClearField()
        {
            AddObjectName.Text = "";
            AddObjectScore.Text = "";
            AddObjectGenre.Text = "";
            AddObjectCreator.Text = "";
            AddObjectTimeForComplete.Text = "";
            AddObjectReleaseYear.Text = "";
            AddObjectPlatform.Text = "";
            AddObjectIsCompleted.IsChecked = false;
            objectImage = "";
            AddObjectImageLabel.Visibility = Visibility.Hidden;
        }

        private void AddObjectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                objectImage = op.FileName;
                AddObjectImageLabel.Visibility = Visibility.Visible;
            }
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_ChoseRussianLanguage(object sender, RoutedEventArgs e)
        {
            AnimesTabItem.Header = "Аниме";
            BooksTabItem.Header = "Книги";
            FilmsTabItem.Header = "Фильмы";
            GamesTabItem.Header = "Игры";
            AddObjectTabItem.Header = "Добавить новый предмет";

            AnimeGridImage.Header = "Изображение";
            AnimeGridName.Header = "Название";
            AnimeGridScore.Header = "Оценка";
            AnimeGridGenre.Header = "Жанр";
            AnimeGridReleaseYear.Header = "Год выпуска";
            AnimeGridCompleted.Header = "Завершено";

            BookGridImage.Header = "Изображение";
            BookGridName.Header = "Название";
            BookGridScore.Header = "Оценка";
            BookGridGenre.Header = "Жанр";
            BookGridCreator.Header = "Автор";
            BookGridReleaseYear.Header = "Год выпуска";
            BookGridCompleted.Header = "Завершено";

            FilmGridImage.Header = "Изображение";
            FilmGridName.Header = "Название";
            FilmGridScore.Header = "Оценка";
            FilmGridGenre.Header = "Жанр";
            FilmGridCreator.Header = "Режиссёр";
            FilmGridReleaseYear.Header = "Год выпуска";
            FilmGridCompleted.Header = "Завершено";

            GameGridImage.Header = "Изображение";
            GameGridName.Header = "Название";
            GameGridScore.Header = "Оценка";
            GameGridPlatform.Header = "Платформа";
            GameGridGenre.Header = "Жанр";
            GameGridCreator.Header = "Студия разработчик";
            GameGridReleaseYear.Header = "Год выпуска";
            GameGridCompleted.Header = "Завершено";

            MenuItemFile.Header = "Файл";
            MenuItemChoseLanguage.Header = "Выбор языка";
            MenuItemChoseLanguage_English.Header = "Английский";
            MenuItemChoseLanguage_Russian.Header = "Русский";
            MenuItemExit.Header = "Выход";

            AddObjectSelectTypeLabel.Content = "Выберите тип";
            AddObjectNameLabel.Content = "Название";
            AddObjectScoreLabel.Content = "Оценка";
            AddObjectGenreLabel.Content = "Жанр";
            AddObjectTimeForCompleteLabel.Content = "Время на завершение";
            AddObjectReleaseYearLabel.Content = "Дата выхода";
            AddObjectPlatformLabel.Content = "Платформа";
            AddObjectCreatorLabel.Content = "Автор";
            AddObjectImageLabel.Content = "Изображение было выбрано";
            AddObjectButton.Content = "Добавить";
            AddObjectLoadImageButton.Content = "Выбрать изображение";
        }

        private void MenuItem_ChoseEnglistLanguage(object sender, RoutedEventArgs e)
        {
            AnimesTabItem.Header = "Animes";
            BooksTabItem.Header = "Books";
            FilmsTabItem.Header = "Films";
            GamesTabItem.Header = "Games";
            AddObjectTabItem.Header = "Add New Object";
        }

        void AnimeListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = AnimeListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var showObjectWindow = new ShowObjectWindow(selectedItem);
                showObjectWindow.Show();
            }
        }

        void BookListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = BookListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var showObjectWindow = new ShowObjectWindow(selectedItem);
                showObjectWindow.Show();
            }
        }

        void FilmListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = FilmListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var showObjectWindow = new ShowObjectWindow(selectedItem);
                showObjectWindow.Show();
            }
        }

        void GameListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = GameListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var showObjectWindow = new ShowObjectWindow(selectedItem);
                showObjectWindow.Show();
            }
        }
    }
}
