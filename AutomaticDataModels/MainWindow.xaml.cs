using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace AutomaticDataModels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GridDefinition bigParent = new GridDefinition();
            GridDefinition secondParent = new GridDefinition();
            GridDefinition thirdParent = new GridDefinition();
            ButtonDefinition littleChild = new ButtonDefinition();

            bigParent.BackgroundColor = "Blue";
            bigParent.HeightRequest = 100;
            bigParent.WidthRequest = 100;

            secondParent.BackgroundColor = "Black";
            secondParent.HeightRequest = 90;
            secondParent.WidthRequest = 90;

            thirdParent.BackgroundColor = "Red";
            thirdParent.HeightRequest = 80;
            thirdParent.WidthRequest = 80;

            littleChild.BackgroundColor = "Yellow";
            littleChild.HeightRequest = 23;
            littleChild.WidthRequest = 75;
            littleChild.Text = "Haha";
            littleChild.BorderRadius = 8;


            bigParent.Children.Add(secondParent);
            secondParent.Children.Add(thirdParent);
            thirdParent.Children.Add(littleChild);

            Extractor extractor = Extractor.CreateInstance();

            extractor.ExtractAndBind(bigParent);

            littleChild.BackgroundColor = "White";

            mainGrid.Children.Add((UIElement)bigParent.Control);

            DataModelFactory factory =  DataModelFactory.CreateInstance();
            ButtonDefinition buttonDefinition = factory.ConstructModel<ButtonDefinition>();
            mainGrid.Children.Add((UIElement)buttonDefinition.Control);



            

        }
    }
}
