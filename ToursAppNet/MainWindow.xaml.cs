using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

namespace ToursAppNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrameID.Navigate(new ToursPage());
            Manager.MainFrame = MainFrameID;

            //ImportTours();
        }

        private void ImportTours()
        {
            string[] images = Directory.GetFiles(@"D:\Колледж\Управление и автоматизация баз данных\курс3\Ресурсы\Туры фото");

            foreach (string image in images)
            {
                string filename = System.IO.Path.GetFileNameWithoutExtension(image).ToLower();
                
                try
                {
                    ToursEntities context = ToursEntities.GetContext();
                    Tour currentTour = context.Tours.SingleOrDefault(p => p.Name.ToLower().Contains(filename));
                    currentTour.ImagePreview = File.ReadAllBytes(image);

                    context.Entry(currentTour).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                  
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

 
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            BtnBack.Visibility = MainFrameID.CanGoBack ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
