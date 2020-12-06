using ArchiveManager.Objects;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace ArchiveManager.Windows
{
    public partial class ChangeObjectWindow : MyWindowInterface
    {
        private ArchiveObject selectedArchiveObject;
        private string objectImage = "";

        public ChangeObjectWindow(ArchiveObject archiveObject)
        {
            InitializeComponent();

            this.FontFamily = StaticContent.GetFont();
            this.FontSize = StaticContent.GetFontSize();

            selectedArchiveObject = archiveObject;
            SetLanguage();
            SetValuesInFields();
        }

        private void SetValuesInFields()
        {
            if (selectedArchiveObject.name != "")
                ChangeObjectName.Text = selectedArchiveObject.name;
            if (selectedArchiveObject.score.ToString() != "")
                ChangeObjectScore.Text = selectedArchiveObject.score.ToString();
            if (selectedArchiveObject.timeForComplete.ToString() != "")
                ChangeObjectTimeForComplete.Text = selectedArchiveObject.timeForComplete.ToString();
            if (selectedArchiveObject.releaseYear.ToString() != "")
                ChangeObjectReleaseYear.Text = selectedArchiveObject.releaseYear.ToString();
            if (selectedArchiveObject.genre != "")
                ChangeObjectGenre.Text = selectedArchiveObject.genre;

            if (selectedArchiveObject.type != ECollectionType.ANIME)
            {
                ChangeObjectCreatorLabel.Visibility = Visibility.Visible;
                ChangeObjectCreator.Visibility = Visibility.Visible;
                if (selectedArchiveObject.creator != "")
                    ChangeObjectCreator.Text = selectedArchiveObject.creator;
            }
            else
            {
                ChangeObjectCreatorLabel.Visibility = Visibility.Hidden;
                ChangeObjectCreator.Visibility = Visibility.Hidden;

                ChangeObjectTimeForCompleteLabel.SetValue(Canvas.TopProperty, 170.0);
                ChangeObjectTimeForComplete.SetValue(Canvas.TopProperty, 170.0);
                ChangeObjectReleaseYearLabel.SetValue(Canvas.TopProperty, 210.0);
                ChangeObjectReleaseYear.SetValue(Canvas.TopProperty, 210.0);
                ChangeObjectIsCompleted.SetValue(Canvas.TopProperty, 250.0);
            }

            if (selectedArchiveObject.type == ECollectionType.FILM)
            {
                ChangeObjectIsCompleted.SetValue(Canvas.TopProperty, 290.0);
            }

            if (selectedArchiveObject.type == ECollectionType.GAME)
            {
                ChangeObjectPlatform.Visibility = Visibility.Visible;
                ChangeObjectPlatformLabel.Visibility = Visibility.Visible;
                if (selectedArchiveObject.platform != "")
                    ChangeObjectPlatform.Text = selectedArchiveObject.platform;
            }
            else
            {
                ChangeObjectPlatform.Visibility = Visibility.Hidden;
                ChangeObjectPlatformLabel.Visibility = Visibility.Hidden;
            }

            if (selectedArchiveObject.type == ECollectionType.BOOK)
            {
                ChangeObjectTimeForComplete.Visibility = Visibility.Hidden;
                ChangeObjectTimeForCompleteLabel.Visibility = Visibility.Hidden;

                ChangeObjectReleaseYearLabel.SetValue(Canvas.TopProperty, 210.0);
                ChangeObjectReleaseYear.SetValue(Canvas.TopProperty, 210.0);
                ChangeObjectIsCompleted.SetValue(Canvas.TopProperty, 250.0);
            }

            ChangeObjectIsCompleted.IsChecked = selectedArchiveObject.isCompleted;
            if (File.Exists(selectedArchiveObject.image))
            {
                objectImage = selectedArchiveObject.image;
                if (!selectedArchiveObject.image.Contains(@"\DataBase\Images\question_icon.png"))
                {
                    ChangeObjectImageLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    ChangeObjectImageLabel.Visibility = Visibility.Hidden;
                }
            }

        }

        public void ChangeObjectButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ChangeObjectName.GetLineText(0);
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
            string genre = ChangeObjectGenre.GetLineText(0);
            string creator = ChangeObjectCreator.GetLineText(0);
            float timeForComplete = 0;
            int releaseYear = 0;
            try
            {
                score = int.Parse(ChangeObjectScore.GetLineText(0));
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
                timeForComplete = float.Parse(ChangeObjectTimeForComplete.GetLineText(0));
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect Time For Complete on Add");
            }
            try
            {
                releaseYear = int.Parse(ChangeObjectReleaseYear.GetLineText(0));
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect Release Year on Add");
            }
            bool isCompleted = (ChangeObjectIsCompleted.IsChecked != null) ? ChangeObjectIsCompleted.IsChecked.Value : false;

            ECollectionType type = selectedArchiveObject.type;
            ArchiveObject changeObject = new ArchiveObject(name, score, timeForComplete, releaseYear, isCompleted, genre, creator, type);

            switch (type)
            {
                case ECollectionType.ANIME:
                    if (name != selectedArchiveObject.name)
                    {
                        if (CheckDuplicateName(name, StaticContent.animeCollection))
                            break;
                    }
                    StaticContent.animeListView.Items.Add(changeObject);
                    StaticContent.animeListView.Items.Remove(StaticContent.animeListView.SelectedItem);

                    StaticContent.animeCollection.AddObject(changeObject, objectImage);
                    StaticContent.animeCollection.RemoveObject(selectedArchiveObject);
                    MessageBox.Show(name + StaticContent.GetChangeMessage());
                    Close();
                    break;
                case ECollectionType.BOOK:
                    if (name != selectedArchiveObject.name)
                    {
                        if (CheckDuplicateName(name, StaticContent.bookCollection))
                            break;
                    }
                    StaticContent.bookListView.Items.Add(changeObject);
                    StaticContent.bookListView.Items.Remove(StaticContent.bookListView.SelectedItem);

                    StaticContent.bookCollection.AddObject(changeObject, objectImage);
                    StaticContent.bookCollection.RemoveObject(selectedArchiveObject);

                    MessageBox.Show(name + StaticContent.GetChangeMessage());
                    Close();
                    break;
                case ECollectionType.FILM:
                    if (name != selectedArchiveObject.name)
                    {
                        if (CheckDuplicateName(name, StaticContent.filmCollection))
                            break;
                    }
                    StaticContent.filmListView.Items.Add(changeObject);
                    StaticContent.filmListView.Items.Remove(StaticContent.filmListView.SelectedItem);

                    StaticContent.filmCollection.AddObject(changeObject, objectImage);
                    StaticContent.filmCollection.RemoveObject(selectedArchiveObject);

                    MessageBox.Show(name + StaticContent.GetChangeMessage());
                    Close();
                    break;
                case ECollectionType.GAME:
                    if (name != selectedArchiveObject.name)
                    {
                        if (CheckDuplicateName(name, StaticContent.gameCollection))
                            break;
                    }
                    changeObject.platform = ChangeObjectPlatform.Text;

                    StaticContent.gameListView.Items.Add(changeObject);
                    StaticContent.gameListView.Items.Remove(StaticContent.gameListView.SelectedItem);

                    StaticContent.gameCollection.AddObject(changeObject, objectImage);
                    StaticContent.gameCollection.RemoveObject(selectedArchiveObject);

                    MessageBox.Show(name + StaticContent.GetChangeMessage());
                    Close();
                    break;
            }
        }

        private void ChangeObjectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                objectImage = op.FileName;
                ChangeObjectImageLabel.Visibility = Visibility.Visible;
            }
        }

        public void SetLanguage()
        {
            if (StaticContent.setting.GetLanguage() == ELanguage.ENGLISH)
            {
                SetEngLanguage();
                ChangeObjectImageButton.Width = 105;
            }
            else
            {
                SetRuLanguage();
                ChangeObjectImageButton.Width = 160;
            }
        }

        public void SetEngLanguage()
        {
            ChangeObjectButton.Content = "Change";
            ChangeObjectImageButton.Content = "Load Image";
            ChangeObjectIsCompleted.Content = "Is Completed";
            ChangeObjectNameLabel.Content = "Name";
            ChangeObjectScoreLabel.Content = "Score";
            ChangeObjectGenreLabel.Content = "Genre";

            switch (selectedArchiveObject.type)
            {
                case ECollectionType.BOOK:
                    ChangeObjectCreatorLabel.Content = "Author";
                    break;
                case ECollectionType.FILM:
                    ChangeObjectCreatorLabel.Content = "Director";
                    break;
                case ECollectionType.GAME:
                    ChangeObjectCreatorLabel.Content = "Developer Studio";
                    break;
                default:
                    ChangeObjectCreatorLabel.Content = "Author";
                    break;
            }
            ChangeObjectTimeForCompleteLabel.Content = "Duration";
            ChangeObjectReleaseYearLabel.Content = "Release Year";
            ChangeObjectPlatformLabel.Content = "Platform";

            ChangeObjectImageLabel.Content = "Image was selected";

        }

        public void SetRuLanguage()
        {
            ChangeObjectButton.Content = "Изменить";
            ChangeObjectImageButton.Content = "Загрузить изображение";
            ChangeObjectIsCompleted.Content = "Закончено";
            ChangeObjectNameLabel.Content = "Название";
            ChangeObjectScoreLabel.Content = "Оценка";
            ChangeObjectGenreLabel.Content = "Жанр";

            switch (selectedArchiveObject.type)
            {
                case ECollectionType.BOOK:
                    ChangeObjectCreatorLabel.Content = "Автор";
                    break;
                case ECollectionType.FILM:
                    ChangeObjectCreatorLabel.Content = "Режиссер";
                    break;
                case ECollectionType.GAME:
                    ChangeObjectCreatorLabel.Content = "Студия разработчик";
                    break;
                default:
                    ChangeObjectCreatorLabel.Content = "Автор";
                    break;
            }
            ChangeObjectTimeForCompleteLabel.Content = "Продолжительность";
            ChangeObjectReleaseYearLabel.Content = "Год выпуска";
            ChangeObjectPlatformLabel.Content = "Платформа";

            ChangeObjectImageLabel.Content = "Изображение выбрано";
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            StaticContent.openWindows.Remove(this);
        }

        private bool CheckDuplicateName(string name, ArchiveCollection colection)
        {
            for (int i = 0; i < colection.GetArchiveObjects().Count; ++i)
            {
                if (name == colection.GetArchiveObjects()[i].name)
                {
                    MessageBox.Show(StaticContent.GetDuplicateNameText());
                    return true;
                }
            }
            return false;
        }
    }
}
