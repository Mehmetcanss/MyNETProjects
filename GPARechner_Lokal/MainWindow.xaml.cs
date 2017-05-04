using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Win32;

using System.IO;
using System.Xml.Serialization;

namespace GPARechner_Lokal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        private List<TextBox> LoadedTextBoxList = new List<TextBox>();

        GPACalculator InformatikGPACalculator;


        SaveFileDialog sfd;
        OpenFileDialog ofd;


        private Lecture _selected;

        public Lecture SelectedLecture
        {
            get { return _selected; }
            set
            {
                _selected = value;
                SetPropertyChanged("SelectedLecture");
            }
        }

        private void SetPropertyChanged(string v)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private ObservableCollection<Lecture> _lectures;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Lecture> Lectures
        {
            get { return _lectures; }
            set { _lectures = value; }
        }

        public MainWindow()
        {

            InitializeComponent();
            this.KeyDown += MainWindow_KeyDown;
            this.PropertyChanged += MainWindow_PropertyChanged;
            SaveLecturesButton.Click += SaveLecturesButton_Click;
            LoadLecturesButton.Click += LoadLecturesButton_Click;

            weightTextBox.MouseDoubleClick += TextBox_MouseDoubleClick;
            nameTextBox.MouseDoubleClick += TextBox_MouseDoubleClick;
            weightTextBox.PreviewTextInput += WeightTextBox_PreviewTextInput;


            saveLectureButton.Click += SaveLectureButton_Click;
            newLectureButton.Click += NewLectureButton_Click;
            deleteLectureButton.Click += DeleteLectureButton_Click;
            Lectures = new ObservableCollection<Lecture>();
            lecturesListBox.ItemsSource = Lectures;
            lecturesListBox.SelectionMode = SelectionMode.Single;
            lecturesListBox.SelectionChanged += LecturesListBox_SelectionChanged;
            lecturesListBox.DisplayMemberPath = "Name";
            this.WindowState = WindowState.Maximized;
            InformatikGPACalculator = new GPACalculator();
            ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.Filter = "Lecture Files | *.lecture";
            sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.Filter = "Lecture Files | *.lecture";


        }

        private void DeleteLectureButton_Click(object sender, RoutedEventArgs e)
        {
            Lectures.Remove(this.SelectedLecture);
        }

        private void NewLectureButton_Click(object sender, RoutedEventArgs e)
        {
            Lecture l = new Lecture();
            l.Name = "New Lecture...";
            l.Weight = -1;
            l.Note = -1;
            this.Lectures.Add(l);
            this.lecturesListBox.SelectedItem = l;
        }

        private void SaveLectureButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedLecture.Name = nameTextBox.Text;
            SelectedLecture.Weight = (int)Convert.ToInt32(weightTextBox.Text);
            SelectedLecture.Note = (double)Convert.ToDouble(noteTextBox.Text);
        }

        private void LoadLecturesButton_Click(object sender, RoutedEventArgs e)
        {
            if (ofd.ShowDialog() == true)
            {
                if (ofd.FileName != "")
                {
                    StreamReader ff = System.IO.File.OpenText(ofd.FileName);
                    XmlSerializer ser = new XmlSerializer(typeof(Lecture[]));
                    Lecture[] deserialized = (Lecture[])ser.Deserialize(ff);
                    this.Lectures.Clear();
                    foreach (Lecture l in deserialized)
                    {
                        this.Lectures.Add(l);
                    }

                }
            }
        }

        private void SaveLecturesButton_Click(object sender, RoutedEventArgs e)
        {
            if (sfd.ShowDialog() == true)
            {
                if (sfd.FileName != "")
                {
                    string text = InformatikGPACalculator.SerializeLectures(this.Lectures.ToArray<Lecture>());
                    System.IO.File.WriteAllText(sfd.FileName, text);

                }
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                lecturesListBox.SelectedIndex = -1;
            }
        }

        private void MainWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedLecture")
            {
                nameTextBox.DataContext = SelectedLecture;
                weightTextBox.DataContext = SelectedLecture;
                noteTextBox.DataContext = SelectedLecture;
            }
        }


        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void WeightTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !isTextAllowed(e.Text);
        }

        private bool isTextAllowed(string text)
        {
            try
            {

                int value = Convert.ToInt32(text);
            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }


        private void LecturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lecturesListBox.SelectedItem == null)
            {
                SelectedLecture = null;
            }
            else
            {

                SelectedLecture = (Lecture)lecturesListBox.SelectedItem;
            }
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            double a = InformatikGPACalculator.calculateGPA(Lectures.AsEnumerable<Lecture>().ToArray());
            gpaLabel.Content = a;

        }





        private void TextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
