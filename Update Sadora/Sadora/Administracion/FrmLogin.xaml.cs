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

namespace Sadora.Administracion
{
    /// <summary>
    /// Lógica de interacción para FrmLogin.xaml
    /// </summary>
    public partial class FrmLogin : Window
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BotonSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BotonAcceder_Click(object sender, RoutedEventArgs e)
        {
            //hecho.IsOpen = true;
            if (hecho.IsOpen == false)
            {
                Clases.ClassVariables.UsuarioID = 1;
                FrmMain main = new FrmMain();
                main.Show();
                this.Close();
            }
            //main.Show();
            //this.Close();
        }

        private void ButtonAceptarDialogo_Click(object sender, RoutedEventArgs e)
        {
            hecho.IsOpen = false;
        }
    }
}
