using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para FrmMostrarDatosHost.xaml
    /// </summary>
    public partial class FrmMostrarDatosHost : Window
    {
        private DataTable dt;

        public FrmMostrarDatosHost(string Lista, DataTable tabla)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            if (Lista == null)
            {
                dt = tabla;
            }
            else
            {
                dt = Clases.ClassData.runDataTable(Lista, null, "CommandText");
            }
            GridMuestra.ItemsSource = dt.DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //MiniDialogo.IsOpen = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //MiniDialogo.IsOpen = false;
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (GridMuestra.SelectedItem == null)
            {
                FrmCompletarCamposHost frm = new FrmCompletarCamposHost("Debe Seleccionar un registro.");
                frm.ShowDialog();
            }
            else
            {

                this.Hide();
                //Clientes.UscClientes clientes = new Clientes.UscClientes();
                //clientes.txtClaseID.Text = GridMuestra.
            }
        }
    }
}
