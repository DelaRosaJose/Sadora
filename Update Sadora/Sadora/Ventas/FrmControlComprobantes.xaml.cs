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
        //public bool Resultado;
        private int ClienteID;
        //private int ClaseComprobanteId;
        private bool NotData = false;
        string FormaPago;
        double MontoPagar = 0;
        ClassVariables ClasesVariab;

        public FrmControlComprobantes()
        { }

        public FrmControlComprobantes(int clienteID, string formaPago, double montoPagar, ClassVariables ClasesVariables)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            FormaPago = formaPago;

            ClienteID = clienteID != 1 ? clienteID : 0;

            MontoPagar = montoPagar;
            ClasesVariab = ClasesVariables;

            if (ClienteID != 0)
                txtRNC.IsReadOnly = true;
            //ClaseComprobanteId = clienteID == 1 ? 1 : default;

            //ClienteID = clienteID;
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
            FindRazonSocial(1);
            PutTextbox(1);
        }

        private void BtnValorGubernamental_Click(object sender, RoutedEventArgs e)
        {
            FindRazonSocial(4);
            PutTextbox(4);
        }

        private void BtnConsumidorFinal_Click(object sender, RoutedEventArgs e)
        {
            FindRazonSocial(2);
            PutTextbox(2);
        }

        private void BtnRegimenEspecial_Click(object sender, RoutedEventArgs e)
        {
            FindRazonSocial(3);
            PutTextbox(3);
        }

        //void FindNCF()
        //{
        //    DataTable reader = Clases.ClassData.runDataTable("select RNC, RazonSocial from getCliente('" + txtRNC.Text + "'," + ClienteID + ")", null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.
        //    if ((ClienteID != 0 && txtRNC.Text == "") || (ClienteID != 0 && txtRNC.Text == null))
        //    {
        //        if (reader.Rows.Count == 1)
        //        {
        //            txtRazonSocial.Text = reader.Rows[0]["RazonSocial"].ToString();
        //            txtRNC.Text = reader.Rows[0]["RNC"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        if (reader.Rows.Count == 1)
        //        {
        //            txtRazonSocial.Text = reader.Rows[0]["RazonSocial"].ToString();
        //            txtRNC.Text = reader.Rows[0]["RNC"].ToString();
        //        }
        //        else if (reader.Rows.Count == 0)
        //        {
        //            if (SnackbarThree.MessageQueue is { } messageQueue)
        //            {
        //                Task.Factory.StartNew(() => messageQueue.Enqueue("No se encontraron datos"));
        //            }
        //        }
        //        else
        //        {
        //            if (SnackbarThree.MessageQueue is { } messageQueue)
        //            {
        //                Task.Factory.StartNew(() => messageQueue.Enqueue("Mas de una razon social con este RNC, comuniquese con su proveedor de servicios."));
        //            }
        //        }
        //    }
        //}

        void PutTextbox(int value, bool pass = false)
        {
            if ((ClienteID == 0 && !NotData) || pass)
            {
                if (value == 1)
                    TabItem.Header = "Valor Fiscal";
                else if (value == 2)
                    TabItem.Header = "Consumidor Final";
                else if (value == 3)
                    TabItem.Header = "Regimen Especial";
                else if (value == 4)
                    TabItem.Header = "Valor Gubernamental";
            }
        }

        void FindRazonSocial(int TipoComprobante = 0)
        {
            DataTable reader = Clases.ClassData.runDataTable("select RNC, RazonSocial from getCliente('" + txtRNC.Text + "'," + ClienteID + ")", null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.
            if ((ClienteID != 0 && txtRNC.Text == "") || (ClienteID != 0 && txtRNC.Text == null) || (ClienteID == 0 && (txtRNC.Text.Length == 9 || txtRNC.Text.Length == 11)))
            {
                if (reader.Rows.Count == 1)
                {
                    txtRazonSocial.Text = reader.Rows[0]["RazonSocial"].ToString();
                    txtRNC.Text = reader.Rows[0]["RNC"].ToString();
                    NotData = false;
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
                    if (ClienteID != 0 || (txtRNC.Text.Length == 11 && txtRNC.Text.Length == 9))
                    {
                        if (SnackbarThree.MessageQueue is { } messageQueue)
                        {
                            Task.Factory.StartNew(() => messageQueue.Enqueue("No se encontraron datos"));
                            NotData = true;
                        }
                    }
                    else
                    {
                        if (SnackbarThree.MessageQueue is { } messageQueue)
                        {
                            Task.Factory.StartNew(() => messageQueue.Enqueue("RNC o cedula incorrecta"));
                            NotData = true;
                        }
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

            string ClassComprobante = null;

            if (ClienteID != 0)
            {
                DataTable Result = Clases.ClassData.runDataTable("select * from TcliClientes where ClienteID = " + ClienteID, null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.
                                                                                                                                                    //{
                if (Result.Rows.Count == 1)
                    ClassComprobante = Result.Rows[0]["ClaseComprobanteID"].ToString();

                Result.Clear();
                Result.Dispose();
                ClassControl.setValidador("Select NCF as Nombre from getNextNCF(" + ClassComprobante + ",NULL) --", null, txtNCF, true);
                if (txtNCF.Text.ToUpper().Contains("B01"))
                    PutTextbox(1, true);
                else if (txtNCF.Text.ToUpper().Contains("B02"))
                    PutTextbox(2, true);
                else if (txtNCF.Text.ToUpper().Contains("B14"))
                    PutTextbox(3, true);
                else if (txtNCF.Text.ToUpper().Contains("B15"))
                    PutTextbox(4, true);

                txtFechaVencimiento.Text = "12/31/2021";
                getFinalView();
            }
            else if (TipoComprobante != 0 && !string.IsNullOrWhiteSpace(txtRNC.Text) && (txtRNC.Text.Length == 11 || txtRNC.Text.Length == 9))
            {
                ClassControl.setValidador("Select NCF as Nombre from getNextNCF(" + TipoComprobante + ",NULL) --", null, txtNCF, true);
                if (txtNCF.Text.ToUpper().Contains("B01"))
                    PutTextbox(1);
                else if (txtNCF.Text.ToUpper().Contains("B02"))
                    PutTextbox(2);
                else if (txtNCF.Text.ToUpper().Contains("B14"))
                    PutTextbox(3);
                else if (txtNCF.Text.ToUpper().Contains("B15"))
                    PutTextbox(4);

                txtFechaVencimiento.Text = "12/31/2021";
                getFinalView();
            }
        }

        void getFinalView() 
        {
            if (string.IsNullOrWhiteSpace(txtNCF.Text) || string.IsNullOrWhiteSpace(txtRazonSocial.Text) || string.IsNullOrWhiteSpace(txtRNC.Text) || string.IsNullOrWhiteSpace(txtFechaVencimiento.Text) || TabItem.Header == "Seleccionar Tipo de comprobante")
            {
                if (SnackbarThree.MessageQueue is { } messageQueue)
                {
                    Task.Factory.StartNew(() => messageQueue.Enqueue("No se puede pasar a ventana desglose de metodo de pago"));
                }
            }
            else
            {
                ClasesVariab.ClienteDinamic = txtRazonSocial.Text + " (CM)";
                this.Close();
                new FrmControlFormaPago(FormaPago, MontoPagar).ShowDialog();
                
            }
        }
        
        private void txtRNC_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                FindRazonSocial();
        }

    }
}
