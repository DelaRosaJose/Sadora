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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sadora.Clientes
{
    /// <summary>
    /// Lógica de interacción para FrmMenu.xaml
    /// </summary>
    public partial class FrmMenu : Window
    {

        bool FrmMenuClosed = true;
        bool Pasador = false;
        string Modulo = "";


        public FrmMenu()
        {
            InitializeComponent();

            Modulo = TextBlock1.Text;
            TextService();

            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        void TextService(bool cerrado = true)
        {
            if (cerrado == true)
            {
                TextBlock1.Text = String.Join(Environment.NewLine, TextBlock1.Text.Select(c => new String(c, 1)).ToArray());
                TextBlock1.FontSize = 25;
            }
            else
            {
                TextBlock1.Text = Modulo;
            }
        }

        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    SnackbarOne.IsActive = false;
        //}

        void OpenUsercontrol(UserControl Usc, string Nombre, MaterialDesignThemes.Wpf.PackIconKind icon = MaterialDesignThemes.Wpf.PackIconKind.FileOutline)
        {
            for (int i = 0; i < TabMain.Items.Count; i++)
            {
                TabMain.SelectedIndex = i;
                string name = (TabMain.SelectedItem as TabItem).Header.ToString();
                if (name == Nombre)
                {
                    Pasador = true;
                    break;
                }
                else
                {
                    Pasador = false;
                }
            }

            if (Pasador == true)
            {
                if (SnackbarThree.MessageQueue is { } messageQueue)
                {
                    //use the message queue to send a message.
                    var message = "Dicha ventana se encuentra abierta";
                    //the message queue can be called from any thread
                    Task.Factory.StartNew(() => messageQueue.Enqueue(message));
                }
                //SnackbarOne.Message.Content = "Dicha ventana se encuentra abierta";
                //SnackbarOne.IsActive = true;

                //System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                //dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                //dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
                //dispatcherTimer.Start();

                Pasador = false;
            }
            else
            {

                if (Usc == null)
                {
                    MiniDialogo.IsOpen = true;
                }
                else
                {

                    var packIconMaterial = new MaterialDesignThemes.Wpf.PackIcon()
                    {
                        Kind = icon,
                        Width = 24,
                        Height = 24,
                        Margin = new Thickness(7, 0, 0, 0),
                    };

                    Wpf.Controls.TabItem tabItem = new Wpf.Controls.TabItem()
                    {
                        FontSize = 18.5,
                        FontWeight = FontWeights.SemiBold,
                        FontFamily = new FontFamily("pack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto"),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Header = Nombre,
                        Content = Usc,
                        Icon = packIconMaterial
                    };

                    TabMain.Items.Add(tabItem);
                }

            }

        }

        private void ButtonOpenMenu_Click_1(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;

            if (FrmMenuClosed)
            {
                Storyboard openFrmMenu = (Storyboard)ButtonOpenMenu.FindResource("OpenMenu");
                openFrmMenu.Begin();

                TextService(false);
            }
            else
            {
                Storyboard closeFrmMenu = (Storyboard)ButtonOpenMenu.FindResource("CloseMenu");
                closeFrmMenu.Begin();

                TextService();
            }

            FrmMenuClosed = !FrmMenuClosed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;

            if (FrmMenuClosed)
            {
                Storyboard openFrmMenu = (Storyboard)ButtonOpenMenu.FindResource("OpenMenu");
                openFrmMenu.Begin();

                TextService(false);
            }
            else
            {
                Storyboard closeFrmMenu = (Storyboard)ButtonOpenMenu.FindResource("CloseMenu");
                closeFrmMenu.Begin();

                TextService();
            }

            FrmMenuClosed = !FrmMenuClosed;
        }

        private void ButtonCerrar_Click(object sender, RoutedEventArgs e)
        {
            new Administracion.FrmMain().Show();
            this.Close();
        }

        private void btnMenuMaestraClientes_MouseUp(object sender, MouseButtonEventArgs e)
        {

            OpenUsercontrol(new UscClientes(), "Maestra de Clientes", iconMenuMaestraClientes.Kind);
        }

        private void btnMenuCuentasXCobrar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenUsercontrol(null, null);
        }

        private void btnMenuClaseClientes_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenUsercontrol(new UscClaseClientes(), "Clase de Clientes", iconMenuClaseClientes.Kind);
        }

        private void btnMenuGestionClientes_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenUsercontrol(null, null);
        }

        private void btnMenuGestionListados_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenUsercontrol(null, null);
        }


    }
}
