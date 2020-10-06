using ArchiveManager.Objects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class ChangeObjectWindow : Window
    {
        private ArchiveObject selectedArchiveObject;
        private string objectImage = "";

        public ChangeObjectWindow()
        {
            InitializeComponent();
        }

        public void SetValuesInFields(ArchiveObject archiveObject)
        {
            selectedArchiveObject = archiveObject;

            if (archiveObject.name != "")
                ChangeObjectName.Text = archiveObject.name;
            if (archiveObject.score.ToString() != "")
                ChangeObjectScore.Text = archiveObject.score.ToString();
            if (archiveObject.timeForComplete.ToString() != "")
                ChangeObjectTimeForComplete.Text = archiveObject.timeForComplete.ToString();
            if (archiveObject.releaseYear.ToString() != "")
                ChangeObjectReleaseYear.Text = archiveObject.releaseYear.ToString();
            if (archiveObject.genre != "")
                ChangeObjectGenre.Text = archiveObject.genre;
            if (archiveObject.type != ECollectionType.ANIME)
            {
                ChangeObjectCreatorLabel.Visibility = Visibility.Visible;
                ChangeObjectCreator.Visibility = Visibility.Visible;
                if (archiveObject.creator != "")
                    ChangeObjectCreator.Text = archiveObject.creator;
            }
            else
            {
                ChangeObjectCreatorLabel.Visibility = Visibility.Hidden;
                ChangeObjectCreator.Visibility = Visibility.Hidden;
            }
            if (archiveObject.type == ECollectionType.GAME)
            {
                ChangeObjectPlatform.Visibility = Visibility.Visible;
                ChangeObjectPlatformLabel.Visibility = Visibility.Visible;
                if (archiveObject.platform != "")
                    ChangeObjectPlatform.Text = archiveObject.platform;
            }
            else
            {
                ChangeObjectPlatform.Visibility = Visibility.Hidden;
                ChangeObjectPlatformLabel.Visibility = Visibility.Hidden;
            }
            if (archiveObject.type == ECollectionType.BOOK)
            {
                ChangeObjectTimeForComplete.Visibility = Visibility.Hidden;
                ChangeObjectTimeForCompleteLabel.Visibility = Visibility.Hidden;
            }
            ChangeObjectIsCompleted.IsChecked = archiveObject.isCompleted;
            if (File.Exists(archiveObject.image))
            {
                objectImage = archiveObject.image;
                if (!archiveObject.image.Contains(@"\DataBase\Images\question_icon.png"))
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
                    StaticContent.animeListView.Items.Add(changeObject);
                    StaticContent.animeListView.Items.Remove(StaticContent.animeListView.SelectedItem);
                    //StaticContent.animeListView.Items.Refresh();

                    StaticContent.animeCollection.AddObject(changeObject, objectImage);
                    StaticContent.animeCollection.RemoveObject(selectedArchiveObject);
                    MessageBox.Show(name + " was changed");
                    Close();
                    break;
                case ECollectionType.BOOK:
                    StaticContent.bookListView.Items.Add(changeObject);
                    StaticContent.bookListView.Items.Remove(StaticContent.bookListView.SelectedItem);
                    //StaticContent.bookListView.Items.Refresh();

                    StaticContent.bookCollection.AddObject(changeObject, objectImage);
                    StaticContent.bookCollection.RemoveObject(selectedArchiveObject);

                    MessageBox.Show(name + " was changed");
                    Close();
                    break;
                case ECollectionType.FILM:
                    StaticContent.filmListView.Items.Add(changeObject);
                    StaticContent.filmListView.Items.Remove(StaticContent.filmListView.SelectedItem);
                    //StaticContent.filmListView.Items.Refresh();

                    StaticContent.filmCollection.AddObject(changeObject, objectImage);
                    StaticContent.filmCollection.RemoveObject(selectedArchiveObject);

                    MessageBox.Show(name + " was changed");
                    Close();
                    break;
                case ECollectionType.GAME:
                    changeObject.platform = ChangeObjectPlatform.Text;

                    StaticContent.gameListView.Items.Add(changeObject);
                    StaticContent.gameListView.Items.Remove(StaticContent.gameListView.SelectedItem);
                    //StaticContent.filmListView.Items.Refresh();

                    StaticContent.gameCollection.AddObject(changeObject, objectImage);
                    StaticContent.gameCollection.RemoveObject(selectedArchiveObject);

                    MessageBox.Show(name + " was changed");
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
    }
}
