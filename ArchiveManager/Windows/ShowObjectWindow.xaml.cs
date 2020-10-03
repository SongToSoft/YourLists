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
    public partial class ShowObjectWindow : Window
    {
        private ArchiveObject archiveObject;

        public ShowObjectWindow(ArchiveObject _archiveObject)
        {
            InitializeComponent();
            archiveObject = _archiveObject;
            SetValuesInFields();
        }

        private void SetValuesInFields()
        {
            ShowObjectImage.Source = new ImageSourceConverter().ConvertFromString(archiveObject.image) as ImageSource;
            ShowObjectNameText.Content = archiveObject.name;
            ShowObjectScoreText.Content = archiveObject.score.ToString();
            ShowObjectGenreText.Content = archiveObject.genre;

            if (archiveObject.type != ECollectionType.ANIME)
            {
                ShowObjectCreatorLabel.Visibility = Visibility.Visible;
                ShowObjectCreatorText.Visibility = Visibility.Visible;
                ShowObjectCreatorText.Content = archiveObject.creator;
            }
            else
            {
                ShowObjectCreatorLabel.Visibility = Visibility.Hidden;
                ShowObjectCreatorText.Visibility = Visibility.Hidden;
            }

            if (archiveObject.type == ECollectionType.GAME)
            {
                ShowObjectPlatformText.Visibility = Visibility.Visible;
                ShowObjectPlatformText.Content = archiveObject.platform;
            }
            else
            {
                ShowObjectPlatformLabel.Visibility = Visibility.Hidden;
                ShowObjectPlatformText.Visibility = Visibility.Hidden;
            }
            ShowObjectTimeForCompleteText.Content = archiveObject.timeForComplete.ToString();
            ShowObjectReleaseYearText.Content = archiveObject.releaseYear.ToString();
            ShowObjectIsCompleteText.Content = archiveObject.isCompleted.ToString();
        }
    }
}
