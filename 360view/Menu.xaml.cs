using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace _360view
{
    /// <summary>
    /// Lógica interna para Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private Int32 posicao = 0;

        public Menu()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
            ExibiImagem();

        }

        public void ExibiImagem()
        {

            String caminho = String.Concat("c:/images/StyleS51Pen/capa.jpg");
            BitmapImage bitmapImage = new BitmapImage(new Uri(caminho));
            String caminho1 = String.Concat("c:/images/OdysseyZ/capa.jpg");
            BitmapImage bitmapImage1 = new BitmapImage(new Uri(caminho1));
            String caminho2 = String.Concat("c:/images/HMDOdyssey/capa.jpg");
            BitmapImage bitmapImage2 = new BitmapImage(new Uri(caminho2));
            String caminho3 = String.Concat("c:/images/Expert/capa.jpg");
            BitmapImage bitmapImage3 = new BitmapImage(new Uri(caminho3));
            String caminho4 = String.Concat("c:/images/Essencials/capa.jpg");
            BitmapImage bitmapImage4 = new BitmapImage(new Uri(caminho4));
            String caminho5 = String.Concat("c:/images/AIO/capa.jpg");
            BitmapImage bitmapImage5 = new BitmapImage(new Uri(caminho5));
            menu1.Source = bitmapImage;
            menu2.Source = bitmapImage1;
            menu3.Source = bitmapImage2;
            menu4.Source = bitmapImage3;
            menu5.Source = bitmapImage4;
            menu6.Source = bitmapImage5;

        }

        private void menu1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ObservableCollection<Opcoes> visoes = new ObservableCollection<Opcoes>();
            visoes.Add(new Opcoes("Apresentação", "apresentacao"));
            visoes.Add(new Opcoes("Notebook", "notebook"));
            visoes.Add(new Opcoes("Tablet", "tablet"));
            visoes.Add(new Opcoes("Tenda", "tenda"));
            MainWindow main = new MainWindow("StyleS51Pen", visoes);
            main.Show();
        }

        private void menu2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ObservableCollection<Opcoes> visoes = new ObservableCollection<Opcoes>();
            visoes.Add(new Opcoes("Frente", "frente"));
            visoes.Add(new Opcoes("Atrás", "atras"));
            MainWindow main = new MainWindow("OdysseyZ", visoes);
            main.Show();
        }

        private void menu3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ObservableCollection<Opcoes> visoes = new ObservableCollection<Opcoes>();
            visoes.Add(new Opcoes("Visor", "visor"));
            visoes.Add(new Opcoes("Controles", "controles"));
            MainWindow main = new MainWindow("HMDOdyssey", visoes);
            main.Show();
        }

        private void menu4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ObservableCollection<Opcoes> visoes = new ObservableCollection<Opcoes>();
            visoes.Add(new Opcoes("", "normal"));
            MainWindow main = new MainWindow("Expert", visoes);
            main.Show();
        }

        private void menu5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ObservableCollection<Opcoes> visoes = new ObservableCollection<Opcoes>();
            visoes.Add(new Opcoes("", "normal"));
            MainWindow main = new MainWindow("Essencials", visoes);
            main.Show();
        }

        private void menu6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ObservableCollection<Opcoes> visoes = new ObservableCollection<Opcoes>();
            visoes.Add(new Opcoes("Monitor", "monitor"));
            visoes.Add(new Opcoes("Mouse", "mouse"));
            visoes.Add(new Opcoes("Teclado", "teclado"));
            MainWindow main = new MainWindow("AIO", visoes);
            main.Show();
        }




    }
}
