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
    public partial class ChangeObjectWindow : Window
    {
        private ArchiveObject selectedArchiveObject;
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
            if (archiveObject.creator != "")
                ChangeObjectCreator.Text = archiveObject.creator;
        }

        public void ChangeObjectButton_Click(object sender, RoutedEventArgs e)
        {
            string name = ChangeObjectName.GetLineText(0);
            float score = 0;
            string genre = ChangeObjectGenre.GetLineText(0);
            string creator = ChangeObjectCreator.GetLineText(0);
            float timeForComplete = 0;
            int releaseYear = 0;
            try
            {
                score = float.Parse(ChangeObjectScore.GetLineText(0));
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
                    StaticContent.animeCollection.RemoveObject(selectedArchiveObject);
                    StaticContent.animeCollection.AddObject(changeObject);

                    StaticContent.animeListView.Items.Remove(StaticContent.animeListView.SelectedItem);
                    StaticContent.animeListView.Items.Add(changeObject);
                    MessageBox.Show(name + " was changed");
                    Close();
                    break;
                case ECollectionType.BOOK:
                    StaticContent.bookCollection.RemoveObject(selectedArchiveObject);
                    StaticContent.bookCollection.AddObject(changeObject);

                    StaticContent.bookListView.Items.Remove(StaticContent.bookListView.SelectedItem);
                    StaticContent.bookListView.Items.Add(changeObject);
                    MessageBox.Show(name + " was changed");
                    Close();
                    break;
                case ECollectionType.FILM:
                    StaticContent.filmCollection.RemoveObject(selectedArchiveObject);
                    StaticContent.filmCollection.AddObject(changeObject);

                    StaticContent.filmListView.Items.Remove(StaticContent.filmListView.SelectedItem);
                    StaticContent.filmListView.Items.Add(changeObject);
                    MessageBox.Show(name + " was changed");
                    Close();
                    break;
                case ECollectionType.GAME:
                    StaticContent.gameCollection.RemoveObject(selectedArchiveObject);
                    StaticContent.gameCollection.AddObject(changeObject);

                    StaticContent.gameListView.Items.Remove(StaticContent.gameListView.SelectedItem);
                    StaticContent.gameListView.Items.Add(changeObject);
                    MessageBox.Show(name + " was changed");
                    Close();
                    break;
            }
        }
    }
}
