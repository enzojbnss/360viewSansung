using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace _360view
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {


        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;
        Point? pontoInicial;
        private String modulo;
        private ObservableCollection<Opcoes> visoes;
        private Opcoes visao;
        private double posicao = 1;
        private bool comZoom = false;
        private bool giroAtivo = false;

        public MainWindow(String modulo, ObservableCollection<Opcoes> visoes)
        {
            //this.WindowState = WindowState.Maximized;


            this.modulo = modulo;
            this.visoes = visoes;
            InitializeComponent();
            grid.Width = SystemParameters.PrimaryScreenWidth - 100;
            grid.Height = SystemParameters.PrimaryScreenHeight - 100;
            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            scrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;

            scrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            scrollViewer.MouseMove += OnMouseMove;

            slider.ValueChanged += OnSliderValueChanged;
            this.MaxWidth = SystemParameters.PrimaryScreenWidth + 100;
            this.MaxHeight = SystemParameters.PrimaryScreenHeight;
            cmbPosicao.ItemsSource = visoes;
            if(visoes.Count == 1)
            {
                labelVisao.Visibility = Visibility.Hidden;
                menuVisao.Visibility = Visibility.Hidden;
            }
            else
            {
                labelVisao.Visibility = Visibility.Visible;
                menuVisao.Visibility = Visibility.Visible;
            }
            visao = this.visoes[0];
            carregaImage();
            carregaBotoes();
            AjustaImagem();
        }


        private void carregaBotoes()
        {
            String caminho1 = String.Concat("c:/images/zoommais.png");
            String caminho2 = String.Concat("c:/images/zoommenus.png");
            try
            {
                BitmapImage bitmapImage1= new BitmapImage(new Uri(caminho1));
                BitmapImage bitmapImage2 = new BitmapImage(new Uri(caminho2));
                zoomPlus.Source = bitmapImage1;
                zoomMinus.Source = bitmapImage2;
            }
            catch
            {

            }
        }



        private void carregaImage()
        {
            String caminho = String.Concat("c:/images/", this.modulo, "/", visao.valor, "/image (", posicao, ").jpg");
            try
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(caminho));
                image360.Source = bitmapImage;
                
            }
            catch
            {

            }
            
            
        }


        private void AjustaImagem()
        {
            image360.Width = SystemParameters.PrimaryScreenWidth - 100;
            image360.Height = SystemParameters.PrimaryScreenHeight - 100;
            scrollViewer.Height = image360.Height;
        }

        private void tela_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //tela.MaxWidth = SystemParameters.PrimaryScreenWidth;
            //tela.MaxHeight = SystemParameters.PrimaryScreenHeight;
            AjustaImagem();
        }

        private void cmbPosicao_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = (ComboBox)sender;
            visao = (Opcoes)combo.SelectedItem;
            carregaImage();
        }



        public void MoveParaEsquerda(double fator)
        {
            fator = 1;
            posicao = posicao + fator;
            if (posicao > 48)
            {
                posicao = posicao - 48;
            }
            carregaImage();
        }

        public void MoveParaDireita(double fator)
        {
            fator = 1;
            posicao = posicao - fator;
            if (posicao < 1)
            {
                posicao = posicao + 48;
            }
            carregaImage();

        }


        void OnMouseMove(object sender, MouseEventArgs e)
        {

            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(scrollViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;
                if (comZoom)
                {
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                    scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
                }
                else
                {
                    double fator;
                    if (pontoInicial != null && comZoom == false)
                    {
                        if (lastDragPoint.Value.X != pontoInicial.Value.X || lastDragPoint.Value.X != pontoInicial.Value.X)
                        {
                            if (lastDragPoint.Value.X > pontoInicial.Value.X)
                            {
                                fator = lastDragPoint.Value.X - pontoInicial.Value.X;
                                MoveParaDireita(fator);
                            }
                            else
                            {
                                fator = pontoInicial.Value.X - lastDragPoint.Value.X;
                                MoveParaEsquerda(fator);
                            }
                        }
                    }
                }
            }
        }

        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(scrollViewer);
            if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y < scrollViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                if (comZoom)
                {
                    scrollViewer.Cursor = Cursors.SizeAll;
                }
                lastDragPoint = mousePos;
                pontoInicial = mousePos;
                Mouse.Capture(scrollViewer);

            }

        }

        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lastMousePositionOnTarget = Mouse.GetPosition(grid);

            if (e.Delta > 0)
            {
                slider.Value += 0.5;
            }
            if (e.Delta < 0)
            {
                slider.Value -= 0.5;
            }

            e.Handled = true;
            
        }

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.Cursor = Cursors.Arrow;
            scrollViewer.ReleaseMouseCapture();
            lastDragPoint = null;
            pontoInicial = null;
        }

        void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            comZoom = (e.NewValue > 1);
            scaleTransform.ScaleX = e.NewValue;
            scaleTransform.ScaleY = e.NewValue;
            var centerOfViewport = new Point(scrollViewer.ViewportWidth * 20, scrollViewer.ViewportHeight * 20);
            lastCenterPositionOnTarget = scrollViewer.TranslatePoint(centerOfViewport, tela);
        }

        void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(scrollViewer.ViewportWidth / 2, scrollViewer.ViewportHeight / 2);
                        Point centerOfTargetNow = scrollViewer.TranslatePoint(centerOfViewport, grid);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(grid);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth / grid.Width;
                    double multiplicatorY = e.ExtentHeight / grid.Height;

                    double newOffsetX = scrollViewer.HorizontalOffset - dXInTargetPixels * multiplicatorX;
                    double newOffsetY = scrollViewer.VerticalOffset - dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    scrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    scrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }

        private void zoomPlus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            slider.Value += 0.1;
        }

        private void zoomMinus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            slider.Value -= 0.1;
        }

        private void tela_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
        }
    }
}
