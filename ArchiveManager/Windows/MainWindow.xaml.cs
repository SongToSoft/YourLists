using ArchiveManager.Objects;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ArchiveManager.Windows
{
    public partial class MainWindow : MyWindowInterface
    {
        private string objectImage = "";

        public MainWindow()
        {
            InitializeComponent();

            this.FontFamily = new FontFamily("Arial");
            this.FontSize = 14;

            LoadAnimeList();
            LoadBookList();
            LoadFilmList();
            LoadGameList();
        }

        private void SetDisplayedScore()
        {
            var animeObjects = StaticContent.animeCollection.GetArchiveObjects();
            var bookObjects = StaticContent.bookCollection.GetArchiveObjects();
            var filmObjects = StaticContent.filmCollection.GetArchiveObjects();
            var gameObjects = StaticContent.gameCollection.GetArchiveObjects();

            AnimeListView.Items.Clear();
            BookListView.Items.Clear();
            FilmListView.Items.Clear();
            GameListView.Items.Clear();

            for (int i = 0; i < animeObjects.Count; ++i)
            {
                animeObjects[i].SetDispledScore();
                AnimeListView.Items.Add(animeObjects[i]);
            }
            for (int i = 0; i < bookObjects.Count; ++i)
            {
                bookObjects[i].SetDispledScore();
                BookListView.Items.Add(bookObjects[i]);
            }
            for (int i = 0; i < filmObjects.Count; ++i)
            {
                filmObjects[i].SetDispledScore();
                FilmListView.Items.Add(filmObjects[i]);
            }
            for (int i = 0; i < gameObjects.Count; ++i)
            {
                gameObjects[i].SetDispledScore();
                GameListView.Items.Add(gameObjects[i]);
            }
        }

        public void LoadAnimeList()
        {
            StaticContent.animeCollection = new ArchiveCollection(ECollectionType.ANIME);
            StaticContent.animeListView = AnimeListView;

            var animeObjects = StaticContent.animeCollection.GetArchiveObjects();
            for (int i = 0; i < animeObjects.Count; ++i)
            {
                animeObjects[i].SetDispledScore();
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
                bookObjects[i].SetDispledScore();
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
                filmObjects[i].SetDispledScore();
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
                gameObjects[i].SetDispledScore();
                GameListView.Items.Add(gameObjects[i]);
            }
        }

        private void AddObjectButton_Click(object sender, RoutedEventArgs e)
        {
            string name = AddObjectName.GetLineText(0);
            if (name.Contains("/") ||
                name.Contains(":") ||
                name.Contains("*") ||
                name.Contains("?") ||
                name.Contains("<") ||
                name.Contains(">") ||
                name.Contains("|"))
            {
                MessageBox.Show(StaticContent.GetErrorNameText());
                return;
            }
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
                Console.WriteLine("Incorrect Duration on Add");
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
                            MessageBox.Show(name + StaticContent.GetFileAddMessage());
                            AnimeListView.Items.Add(newObject);
                        }
                        else
                        {
                            MessageBox.Show(name + StaticContent.GetFileExistAddMessage());
                        }
                        break;
                    case "Book":
                        if (!StaticContent.bookCollection.IsContain(name))
                        {
                            newObject.type = ECollectionType.BOOK;
                            StaticContent.bookCollection.AddObject(newObject, objectImage);
                            MessageBox.Show(name + StaticContent.GetFileAddMessage());
                            BookListView.Items.Add(newObject);
                        }
                        else
                        {
                            MessageBox.Show(name + StaticContent.GetFileExistAddMessage());
                        }
                        break;
                    case "Film":
                        if (!StaticContent.filmCollection.IsContain(name))
                        {
                            newObject.type = ECollectionType.FILM;
                            StaticContent.filmCollection.AddObject(newObject, objectImage);
                            MessageBox.Show(name + StaticContent.GetFileAddMessage());
                            FilmListView.Items.Add(newObject);
                        }
                        else
                        {
                            MessageBox.Show(name + StaticContent.GetFileExistAddMessage());
                        }
                        break;
                    case "Game":
                        if (!StaticContent.gameCollection.IsContain(name))
                        {
                            newObject.platform = AddObjectPlatform.Text;
                            newObject.type = ECollectionType.GAME;
                            StaticContent.gameCollection.AddObject(newObject, objectImage);
                            MessageBox.Show(name + StaticContent.GetFileAddMessage());
                            GameListView.Items.Add(newObject);
                        }
                        else
                        {
                            MessageBox.Show(name + StaticContent.GetFileExistAddMessage());
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
                var changeObjectWindow = new ChangeObjectWindow(selectedItem);
                changeObjectWindow.Show();
                StaticContent.openWindows.Add(changeObjectWindow);
            }
        }

        private void ChangeBookObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = BookListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var changeObjectWindow = new ChangeObjectWindow(selectedItem);
                changeObjectWindow.Show();
                StaticContent.openWindows.Add(changeObjectWindow);
            }
        }

        private void ChangeFilmObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = FilmListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var changeObjectWindow = new ChangeObjectWindow(selectedItem);
                changeObjectWindow.Show();
                StaticContent.openWindows.Add(changeObjectWindow);
            }
        }

        private void ChangeGameObjectButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = GameListView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var changeObjectWindow = new ChangeObjectWindow(selectedItem);
                changeObjectWindow.Show();
                StaticContent.openWindows.Add(changeObjectWindow);
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
            if (StaticContent.setting.language != ELanguage.RUSSIAN)
            {
                StaticContent.setting.language = ELanguage.RUSSIAN;
                SetLanguage();
                for (int i = 0; i < StaticContent.openWindows.Count; ++i)
                {
                    StaticContent.openWindows[i].SetLanguage();
                }
            }
        }

        private void MenuItem_ChoseEnglistLanguage(object sender, RoutedEventArgs e)
        {
            if (StaticContent.setting.language != ELanguage.ENGLISH)
            {
                StaticContent.setting.language = ELanguage.ENGLISH;
                SetLanguage();
                for (int i = 0; i < StaticContent.openWindows.Count; ++i)
                {
                    StaticContent.openWindows[i].SetLanguage();
                }
            }
        }

        void AnimeListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenShowWindow(AnimeListView);
        }

        void BookListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenShowWindow(BookListView);
        }

        void FilmListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenShowWindow(FilmListView);
        }

        void GameListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenShowWindow(GameListView);
        }

        private void OpenShowWindow(ListView listView)
        {
            var selectedItem = listView.SelectedItem as ArchiveObject;
            if (selectedItem != null)
            {
                var showObjectWindow = new ShowObjectWindow(selectedItem);
                showObjectWindow.Show();
                StaticContent.openWindows.Add(showObjectWindow);
            }
        }

        public void SetLanguage()
        {
            if (StaticContent.setting.language == ELanguage.ENGLISH)
            {
                SetEngLanguage();
                AddObjectSelectTypeLabel.SetValue(Canvas.LeftProperty, 40.0);
                AddObjectNameLabel.SetValue(Canvas.LeftProperty, 40.0);
                AddObjectScoreLabel.SetValue(Canvas.LeftProperty, 40.0);
                AddObjectGenreLabel.SetValue(Canvas.LeftProperty, 40.0);
                AddObjectTimeForCompleteLabel.SetValue(Canvas.LeftProperty, 40.0);
                AddObjectReleaseYearLabel.SetValue(Canvas.LeftProperty, 40.0);


                AddObjectLoadImageButton.Width = 100.0;

                AddObjectImageLabel.Width = 115;
                AddObjectImageLabel.SetValue(Canvas.LeftProperty, 680.0);
            }
            else
            {
                SetRuLanguage();
                AddObjectSelectTypeLabel.SetValue(Canvas.LeftProperty, 10.0);
                AddObjectNameLabel.SetValue(Canvas.LeftProperty, 10.0);
                AddObjectScoreLabel.SetValue(Canvas.LeftProperty, 10.0);
                AddObjectGenreLabel.SetValue(Canvas.LeftProperty, 10.0);
                AddObjectTimeForCompleteLabel.SetValue(Canvas.LeftProperty, 10.0);
                AddObjectReleaseYearLabel.SetValue(Canvas.LeftProperty, 10.0);

                AddObjectLoadImageButton.Width = 160.0;

                AddObjectImageLabel.Width = 175.0;
                AddObjectImageLabel.SetValue(Canvas.LeftProperty, 650.0);
            }
            SetDisplayedScore();
        }

        public void SetEngLanguage()
        {
            AnimesTabItem.Header = "Animes/Cartoons";
            BooksTabItem.Header = "Books";
            FilmsTabItem.Header = "Films";
            GamesTabItem.Header = "Games";
            AddObjectTabItem.Header = "Add New Object";

            AnimeGridImage.Header = "Image";
            AnimeGridName.Header = "Name";
            AnimeGridScore.Header = "Score";
            AnimeGridGenre.Header = "Genre";
            AnimeGridReleaseYear.Header = "Release Year";
            AnimeGridCompleted.Header = "Completed";

            BookGridImage.Header = "Image";
            BookGridName.Header = "Name";
            BookGridScore.Header = "Score";
            BookGridGenre.Header = "Genre";
            BookGridCreator.Header = "Author";
            BookGridReleaseYear.Header = "Release Year";
            BookGridCompleted.Header = "Completed";

            FilmGridImage.Header = "Image";
            FilmGridName.Header = "Name";
            FilmGridScore.Header = "Score";
            FilmGridGenre.Header = "Genre";
            FilmGridCreator.Header = "Director";
            FilmGridReleaseYear.Header = "Release Year";
            FilmGridCompleted.Header = "Completed";

            GameGridImage.Header = "Image";
            GameGridName.Header = "Name";
            GameGridScore.Header = "Score";
            GameGridPlatform.Header = "Platform";
            GameGridGenre.Header = "Genre";
            GameGridCreator.Header = "Developer Studio";
            GameGridReleaseYear.Header = "Release Year";
            GameGridCompleted.Header = "Completed";

            MenuItemFile.Header = "File";
            MenuItemChoseLanguage.Header = "Chose Language";
            MenuItemChoseLanguage_English.Header = "English";
            MenuItemChoseLanguage_Russian.Header = "Russian";
            MenuItemExit.Header = "Exit";

            AddObjectSelectTypeLabel.Content = "Chose Type";
            AddObjectNameLabel.Content = "Name";
            AddObjectScoreLabel.Content = "Score";
            AddObjectGenreLabel.Content = "Genre";
            AddObjectTimeForCompleteLabel.Content = "Duration";
            AddObjectReleaseYearLabel.Content = "Release Year";
            AddObjectPlatformLabel.Content = "Platform";
            AddObjectCreatorLabel.Content = "Creator";
            AddObjectImageLabel.Content = "Image was selected";
            AddObjectButton.Content = "Add";
            AddObjectLoadImageButton.Content = "Load Imagе";
            AddObjectIsCompleted.Content = "Is Completed";
        }

        public void SetRuLanguage()
        {
            AnimesTabItem.Header = "Аниме/Мультики";
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
            AddObjectTimeForCompleteLabel.Content = "Продолжительность";
            AddObjectReleaseYearLabel.Content = "Дата выхода";
            AddObjectPlatformLabel.Content = "Платформа";
            AddObjectCreatorLabel.Content = "Автор";
            AddObjectImageLabel.Content = "Изображение было выбрано";
            AddObjectButton.Content = "Добавить";
            AddObjectLoadImageButton.Content = "Выбрать изображение";
            AddObjectIsCompleted.Content = "Закончено";
        }
    }
}
