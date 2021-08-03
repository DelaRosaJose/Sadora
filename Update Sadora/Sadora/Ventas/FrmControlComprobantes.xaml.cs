using Sadora.Clases;
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

namespace Sadora.Ventas
{
    /// <summary>
    /// Lógica de interacción para FrmControlComprobantes.xaml
    /// </summary>
    public partial class FrmControlComprobantes : Window
    {
        public bool Resultado;
        private int ClienteID;

        public FrmControlComprobantes()
        { }

        public FrmControlComprobantes(int clienteID)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            ClienteID = clienteID;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FindRazonSocial();
        }

        private void ButtonCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnValorFiscal_Click(object sender, RoutedEventArgs e)
        {
            FindRazonSocial();
            TabItem.Header = "Valor Fiscal";
        }

        private void BtnValorGubernamental_Click(object sender, RoutedEventArgs e)
        {
            FindRazonSocial();
            TabItem.Header = "Valor Gubernamental";
        }

        private void BtnConsumidorFinal_Click(object sender, RoutedEventArgs e)
        {
            FindRazonSocial();
            TabItem.Header = "Consumidor Final";
        }

        private void BtnRegimenEspecial_Click(object sender, RoutedEventArgs e)
        {
            FindRazonSocial();
            TabItem.Header = "Regimen Especial";
        }

        void FindNCF()
        {
            DataTable reader = Clases.ClassData.runDataTable("select RNC, RazonSocial from getCliente('" + txtRNC.Text + "'," + ClienteID + ")", null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.
            if ((ClienteID != 0 && txtRNC.Text == "") || (ClienteID != 0 && txtRNC.Text == null))
            {
                if (reader.Rows.Count == 1)
                {
                    txtRazonSocial.Text = reader.Rows[0]["RazonSocial"].ToString();
                    txtRNC.Text = reader.Rows[0]["RNC"].ToString();
                }
            }
            else
            {
                if (reader.Rows.Count == 1)
                {
                    txtRazonSocial.Text = reader.Rows[0]["RazonSocial"].ToString();
                    txtRNC.Text = reader.Rows[0]["RNC"].ToString();
                }
                else if (reader.Rows.Count == 0)
                {
                    if (SnackbarThree.MessageQueue is { } messageQueue)
                    {
                        Task.Factory.StartNew(() => messageQueue.Enqueue("No se encontraron datos"));
                    }
                }
                else
                {
                    if (SnackbarThree.MessageQueue is { } messageQueue)
                    {
                        Task.Factory.StartNew(() => messageQueue.Enqueue("Mas de una razon social con este RNC, comuniquese con su proveedor de servicios."));
                    }
                }
            }
        }

        void FindRazonSocial()
        {
            DataTable reader = Clases.ClassData.runDataTable("select RNC, RazonSocial from getCliente('" + txtRNC.Text + "'," + ClienteID + ")", null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.
            if ((ClienteID != 0 && txtRNC.Text == "") || (ClienteID != 0 && txtRNC.Text == null))
            {
                if (reader.Rows.Count == 1)
                {
                    txtRazonSocial.Text = reader.Rows[0]["RazonSocial"].ToString();
                    txtRNC.Text = reader.Rows[0]["RNC"].ToString();
                }
            }
            else {
                if (reader.Rows.Count == 1)
                {
                    txtRazonSocial.Text = reader.Rows[0]["RazonSocial"].ToString();
                    txtRNC.Text = reader.Rows[0]["RNC"].ToString();
                }
                else if (reader.Rows.Count == 0)
                {
                    if (SnackbarThree.MessageQueue is { } messageQueue)
                    {
                        Task.Factory.StartNew(() => messageQueue.Enqueue("No se encontraron datos"));
                    }
                }
                else
                {
                    if (SnackbarThree.MessageQueue is { } messageQueue)
                    {
                        Task.Factory.StartNew(() => messageQueue.Enqueue("Mas de una razon social con este RNC, comuniquese con su proveedor de servicios."));
                    }
                }
            }
        }

        private void txtRNC_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                FindRazonSocial();
        }

    }
}
