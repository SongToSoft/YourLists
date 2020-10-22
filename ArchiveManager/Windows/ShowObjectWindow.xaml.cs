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
using System.Windows.Shapes;

namespace ArchiveManager.Windows
{
    public partial class ShowObjectWindow : Window, MyWindowInterface
    {
        private ArchiveObject selectedArchiveObject;
        private int marginTextX = 365;
        public ShowObjectWindow(ArchiveObject _archiveObject)
        {
            InitializeComponent();
            selectedArchiveObject = _archiveObject;
            SetValuesInFields();

            SetLanguage();
        }

        private void SetValuesInFields()
        {
            ShowObjectImage.Source = new ImageSourceConverter().ConvertFromString(selectedArchiveObject.image) as ImageSource;
            ShowObjectNameText.Content = selectedArchiveObject.name;
            ShowObjectScoreText.Content = selectedArchiveObject.displayedScore.ToString();
            ShowObjectGenreText.Content = selectedArchiveObject.genre;

            if (selectedArchiveObject.type == ECollectionType.GAME)
            {
                ShowObjectPlatformText.Visibility = Visibility.Visible;
                ShowObjectPlatformText.Content = selectedArchiveObject.platform;
            }
            else
            {
                ShowObjectPlatformLabel.Visibility = Visibility.Hidden;
                ShowObjectPlatformText.Visibility = Visibility.Hidden;
                ShowObjectIsCompleteLabel.Margin = new Thickness(250, 280, 0, 0);
                ShowObjectIsCompleteText.Margin = new Thickness(marginTextX, 280, 0, 0);
            }

            if (selectedArchiveObject.type != ECollectionType.ANIME)
            {
                ShowObjectCreatorLabel.Visibility = Visibility.Visible;
                ShowObjectCreatorText.Visibility = Visibility.Visible;
                ShowObjectCreatorText.Content = selectedArchiveObject.creator;
            }
            else
            {
                ShowObjectCreatorLabel.Visibility = Visibility.Hidden;
                ShowObjectCreatorText.Visibility = Visibility.Hidden;
                ShowObjectTimeForCompleteLabel.Margin = new Thickness(250, 160, 0, 0);
                ShowObjectTimeForCompleteText.Margin = new Thickness(marginTextX, 160, 0, 0);
                ShowObjectReleaseYearLabel.Margin = new Thickness(250, 200, 0, 0);
                ShowObjectReleaseYearText.Margin = new Thickness(marginTextX, 200, 0, 0);
                ShowObjectIsCompleteLabel.Margin = new Thickness(250, 240, 0, 0);
                ShowObjectIsCompleteText.Margin = new Thickness(marginTextX, 240, 0, 0);
            }

            if (selectedArchiveObject.type == ECollectionType.BOOK)
            {
                ShowObjectTimeForCompleteLabel.Visibility = Visibility.Hidden;
                ShowObjectTimeForCompleteText.Visibility = Visibility.Hidden;
                ShowObjectReleaseYearLabel.Margin = new Thickness(250, 200, 0, 0);
                ShowObjectReleaseYearText.Margin = new Thickness(marginTextX, 200, 0, 0);
                ShowObjectIsCompleteLabel.Margin = new Thickness(250, 240, 0, 0);
                ShowObjectIsCompleteText.Margin = new Thickness(marginTextX, 240, 0, 0);
            }

            ShowObjectTimeForCompleteText.Content = selectedArchiveObject.timeForComplete.ToString();
            ShowObjectReleaseYearText.Content = selectedArchiveObject.releaseYear.ToString();
            ShowObjectIsCompleteText.Content = selectedArchiveObject.isCompleted.ToString();
        }

        public void SetLanguage()
        {
            if (StaticContent.setting.language == ELanguage.ENGLISH)
            {
                SetEngLanguage();
            }
            else
            {
                SetRuLanguage();
            }
        }

        public void SetEngLanguage()
        {
            ShowObjectNameLabel.Content = "Name";
            ShowObjectScoreLabel.Content = "Score";
            ShowObjectGenreLabel.Content = "Genre";
            switch (selectedArchiveObject.type)
            {
                case ECollectionType.BOOK:
                    ShowObjectCreatorLabel.Content = "Author";
                    break;
                case ECollectionType.FILM:
                    ShowObjectCreatorLabel.Content = "Director";
                    break;
                case ECollectionType.GAME:
                    ShowObjectCreatorLabel.Content = "Developer Studio";
                    break;
                default:
                    ShowObjectCreatorLabel.Content = "Author";
                    break;
            }
            ShowObjectTimeForCompleteLabel.Content = "Time For Complete";
            ShowObjectReleaseYearLabel.Content = "Release Year";
            ShowObjectPlatformLabel.Content = "Platforma";
            ShowObjectIsCompleteLabel.Content = "Is Complete";
        }

        public void SetRuLanguage()
        {
            ShowObjectNameLabel.Content = "Название";
            ShowObjectScoreLabel.Content = "Оценка";
            ShowObjectGenreLabel.Content = "Жанр";
            switch (selectedArchiveObject.type)
            {
                case ECollectionType.BOOK:
                    ShowObjectCreatorLabel.Content = "Автор";
                    break;
                case ECollectionType.FILM:
                    ShowObjectCreatorLabel.Content = "Режиссёр";
                    break;
                case ECollectionType.GAME:
                    ShowObjectCreatorLabel.Content = "Студия разработчик";
                    break;
                default:
                    ShowObjectCreatorLabel.Content = "Автор";
                    break;
            }
            ShowObjectTimeForCompleteLabel.Content = "Время завершения";
            ShowObjectReleaseYearLabel.Content = "Год выпуска";
            ShowObjectPlatformLabel.Content = "Платформа";
            ShowObjectIsCompleteLabel.Content = "Закончен";
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            StaticContent.openWindows.Remove(this);
        }
    }
}
