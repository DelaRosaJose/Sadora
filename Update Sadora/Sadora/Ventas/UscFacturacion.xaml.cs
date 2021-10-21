﻿using Sadora.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using Sadora.Administracion;
using Sadora.Properties;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sadora.Ventas
{
    /// <summary>
    /// Lógica de interacción para UscFacturacion.xaml
    /// </summary>
    public partial class UscFacturacion : UserControl
    {
        bool Imprime;
        bool Agrega;
        bool Modifica;
        bool Anula;

        public bool Inicializador = false;
        DataTable reader;
        DataTable tabla;
        string Estado;
        string Lista;
        int FacturaID;
        int LastFacturaID = 100000;
        string last = "";

        double GenITBIS = 0;

        double TSubTotal = 0;
        double TITBIS = 0;
        double TTotal = 0;

        int NewCantidad = 1;
        int LastCantidad = 1;

        ClassVariables ClasesVariables = new ClassVariables();

        #region Variables para Eventos
        string Efect = "";
        string Tarj = "";
        string Trans = "";
        string Ck = "";
        string FormaPago = "";
        #endregion

        public UscFacturacion()
        {
            InitializeComponent();
            this.DataContext = ClasesVariables;
            Name = "UscFacturacion";
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            Inicializador = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Inicializador == true)
            {
                Imprime = ClassVariables.Imprime;
                Agrega = ClassVariables.Agrega;
                Modifica = ClassVariables.Modifica;
                Anula = ClassVariables.Anula;

                this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Inicializador = false;

                ControlEvent();
            }
        }

        private void BtnPrimerRegistro_Click(object sender, RoutedEventArgs e)
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles limpiados.
            {
               txtNCF
            };
            ClassControl.ClearControl(listaControl);
            SetEnabledButton("Modo Consulta");
            setDatos(0, "100001");
            BtnPrimerRegistro.IsEnabled = false;
            BtnAnteriorRegistro.IsEnabled = false;
        }

        private void BtnAnteriorRegistro_Click(object sender, RoutedEventArgs e)
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles limpiados.
            {
                //txtNCF
            };
            ClassControl.ClearControl(listaControl);
            SetEnabledButton("Modo Consulta");
            try
            {
                FacturaID = Convert.ToInt32(txtFacturaID.Text) - 1;
            }
            catch (Exception exception)
            {
                ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString();
            }


            if (FacturaID <= 100001)
            {
                BtnPrimerRegistro.IsEnabled = false;
                BtnAnteriorRegistro.IsEnabled = false;
                setDatos(0, "100001");
            }
            else
                setDatos(0, FacturaID.ToString());
        }

        private void BtnProximoRegistro_Click(object sender, RoutedEventArgs e)
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles limpiados.
            {
               txtNCF
            };
            ClassControl.ClearControl(listaControl);
            SetEnabledButton("Modo Consulta");
            try
            {
                FacturaID = Convert.ToInt32(txtFacturaID.Text) + 1;
            }
            catch (Exception exception)
            {
                ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString();
            }

            if (FacturaID >= LastFacturaID)
            {
                BtnUltimoRegistro.IsEnabled = false;
                BtnProximoRegistro.IsEnabled = false;
                setDatos(0, LastFacturaID.ToString());
            }
            else
                setDatos(0, FacturaID.ToString());
        }

        private void BtnUltimoRegistro_Click(object sender, RoutedEventArgs e)
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles limpiados.
            {
               txtNCF
            };
            ClassControl.ClearControl(listaControl);
            SetEnabledButton("Modo Consulta");
            setDatos(-1, "1");
            BtnUltimoRegistro.IsEnabled = false;
            BtnProximoRegistro.IsEnabled = false;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {

            if (Estado == "Modo Consulta")
            {
                last = txtFacturaID.Text;
                SetEnabledButton("Modo Busqueda");
            }
            else if (Estado == "Modo Busqueda")
            {
                List<Control> listaControles = new List<Control>() //Estos son los controles que desahilitaremos al dar click en el boton buscar, los controles que no esten en esta lista se quedaran habilitados para poder buscar un registro por ellos.
                {
                    txtClienteID,tbxClienteID
                };
                Clases.ClassControl.ActivadorControlesReadonly(null, true, false, false, listaControles);

                setDatos(0, null);

                List<String> ListName = new List<String>() //Estos son los campos que saldran en la ventana de busqueda, solo si se le pasa esta lista de no ser asi, se mostrarian todos
                {
                    "Usuario ID","NCF","Empleado ID","Grupo ID","Activo"
                };

                SetEnabledButton("Modo Consulta");

                if (tabla.Rows.Count > 1)
                {
                    Administracion.FrmMostrarDatosHost frm = new Administracion.FrmMostrarDatosHost(null, tabla, ListName);
                    frm.ShowDialog();

                    if (frm.GridMuestra.SelectedItem != null)
                    {
                        DataRowView item = (frm.GridMuestra as DevExpress.Xpf.Grid.GridControl).SelectedItem as DataRowView;
                        txtFacturaID.Text = item.Row.ItemArray[0].ToString();
                        setDatos(0, txtFacturaID.Text);
                        frm.Close();
                    }
                    else
                        setDatos(0, last);
                }
                else if (tabla.Rows.Count < 1)
                {
                    BtnProximoRegistro.IsEnabled = false;
                    BtnAnteriorRegistro.IsEnabled = false;
                    if (SnackbarThree.MessageQueue is { } messageQueue)
                    {
                        var message = "No se encontraron datos";
                        Task.Factory.StartNew(() => messageQueue.Enqueue(message));
                    }
                }
            }
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            SetEnabledButton("Modo Agregar");
            AgregarModoGrid();
            txtSubTotal.Text = 0.ToString("C");
            txtDescuento.Text = 0.ToString("C");
            txtITBIS.Text = 0.ToString("C");
            txtTotal.Text = 0.ToString("C");
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledButton("Modo Editar");
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledButton("Modo Consulta");
            this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            SetControls(false, "Validador", false);
            if (Lista != "Debe Completar los Campos: ")
            {
                Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost(Lista);
                frm.ShowDialog();
            }
            else
            {
                if (Estado == "Modo Editar")
                {
                    setDatos(2, null);
                }
                else
                {
                    Estado = "Modo Agregar";
                    setDatos(1, null);
                }
                SetEnabledButton("Modo Consulta");
                setDatos(0, txtFacturaID.Text);
            }
        }

        private void txtFacturaID_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((Control)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtNCF_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((Control)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtClienteID_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    if (txtClienteID.Text != "")
                    {
                        ClasesVariables.ClienteDinamic = ClassControl.setPropBinding("Select Nombre from TcliClientes where  Activo = 1 and ClienteID =", txtClienteID);
                        ClasesVariables.NCFDinamic = ClassControl.setPropBinding("Select NCF as Nombre from getNextNCF(NULL," + txtClienteID.Text + ") --", null, true);
                        ClasesVariables.RNCDinamic = ClassControl.setPropBinding("Select RNC as Nombre from TcliClientes where  Activo = 1 and ClienteID =", txtClienteID);
                    }
                    else
                    {
                        txtClienteID.Text = 0.ToString();
                        ClasesVariables.ClienteDinamic = ClassControl.setPropBinding("Select Nombre from TcliClientes where ClienteID =", txtClienteID);
                        ClasesVariables.NCFDinamic = ClassControl.setPropBinding("Select NCF as Nombre from getNextNCF(NULL," + txtClienteID.Text + ") --", null, true);
                        ClasesVariables.RNCDinamic = ClassControl.setPropBinding("Select RNC as Nombre from TcliClientes where ClienteID =", txtClienteID);
                    }
                    txtArticuloID.Focus();
                    //((Control)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtClienteID_KeyDown(object sender, KeyEventArgs e)
        {
            ClassControl.ValidadorNumeros(e);
        }

        private void btnClienteID_Click(object sender, RoutedEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                Administracion.FrmMostrarDatosHost frm = new Administracion.FrmMostrarDatosHost("Select ClienteID, RNC, Nombre, Representante, Direccion,Activo from TcliClientes where Activo = 1", null);
                frm.ShowDialog();

                if (frm.GridMuestra.SelectedItem != null)
                {
                    DataRowView item = (frm.GridMuestra as DevExpress.Xpf.Grid.GridControl).SelectedItem as DataRowView;
                    txtClienteID.Text = item.Row.ItemArray[0].ToString();

                    ClasesVariables.ClienteDinamic = ClassControl.setPropBinding("select * from TcliClientes where ClienteID =", txtClienteID);
                }

            }
        }

        private void BtnPay_Click(object sender, RoutedEventArgs e)
        {
            if (PanelOpcionesPagos.Visibility == Visibility.Visible)
            {
                PanelOpcionesPagos.Visibility = Visibility.Hidden;
            }
            else
            {
                PanelOpcionesPagos.Visibility = Visibility.Visible;
                //ControlEvent();
            }

        }

        private void PanelOpcionesPagos_LostFocus(object sender, RoutedEventArgs e)
        {
            PanelOpcionesPagos.Visibility = Visibility.Hidden;
        }

        //private void BtnEfectivo_Click(object sender, RoutedEventArgs e)
        //{
        //    //MessageBox.Show(BtnEfectivo.Name);
        //    PanelOpcionesPagos.Visibility = Visibility.Hidden;
        //    int client;
        //    try
        //    {
        //        client = Convert.ToInt32(txtClienteID.Text);
        //    }
        //    catch
        //    {
        //        client = 0;
        //    }

        //    new FrmControlComprobantes(client).ShowDialog();

        //    //Administracion.FrmMostrarDatosHost frm = new Administracion.FrmMostrarDatosHost(null, reader, ColumCaption);
        //    //frm.ShowDialog();
        //}

        //private void BtnTarjeta_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show(BtnTarjeta.Name);
        //    PanelOpcionesPagos.Visibility = Visibility.Hidden;
        //}

        private void txtArticuloID_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado == "Modo Agregar")
            {
                if (e.Key == Key.Enter)
                {
                    reader = Clases.ClassData.runDataTable("Select a.ArticuloID ,a.Tarjeta, a.Nombre, 1 as Cantidad, a.Precio, a.Precio as SubTotal, ((a.Precio * b.Porcentaje) / 100) as ITBIS " +
                            ", ((a.Precio * b.Porcentaje) / 100)+a.Precio as Total from TinvArticulos a inner join TinvClaseArticulos b on a.ClaseArticuloID = b.ClaseID " +
                            "where a.Tarjeta = '" + txtArticuloID.Text + "' or a.Nombre like '%" + txtArticuloID.Text + "%' order by a.ArticuloID", null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.


                    List<String> ColumCaption = new List<String>() //Estos son los campos que saldran en la ventana de busqueda, solo si se le pasa esta lista de no ser asi, se mostrarian todos
                    {
                        "Tarjeta", "Nombre", "Precio", "ITBIS", "Total"
                    };

                    if (reader.Rows.Count == 1)
                    {
                        GenITBIS = Convert.ToDouble(reader.Rows[0]["ITBIS"].ToString());

                        if (ValidaArticulosGrid() == false)
                        {
                            TablaGrid.AddNewRow();

                            int newRowHandle = DevExpress.Xpf.Grid.DataControlBase.NewItemRowHandle;

                            GridMain.SetCellValue(newRowHandle, "ArticuloID", reader.Rows[0]["ArticuloID"]);
                            GridMain.SetCellValue(newRowHandle, "Tarjeta", reader.Rows[0]["Tarjeta"]);
                            GridMain.SetCellValue(newRowHandle, "Nombre", reader.Rows[0]["Nombre"]);
                            //GridMain.SetCellValue(newRowHandle, "Modelo", reader.Rows[0]["Modelo"]);
                            GridMain.SetCellValue(newRowHandle, "Cantidad", reader.Rows[0]["Cantidad"]);
                            GridMain.SetCellValue(newRowHandle, "Precio", reader.Rows[0]["Precio"]);
                            GridMain.SetCellValue(newRowHandle, "SubTotal", reader.Rows[0]["SubTotal"]);
                            GridMain.SetCellValue(newRowHandle, "ITBIS", reader.Rows[0]["ITBIS"]);
                            GridMain.SetCellValue(newRowHandle, "Total", reader.Rows[0]["Total"]);

                            ((Control)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                            GridMain.CurrentColumn = TablaGrid.VisibleColumns[2];
                            TablaGrid.FocusedRowHandle = newRowHandle;
                            GridMain.SelectItem(newRowHandle);
                            TablaGrid.FocusedColumn = TablaGrid.VisibleColumns[2];
                        }

                        txtArticuloID.Clear();
                    }
                    else if (reader.Rows.Count > 1)
                    {

                        Administracion.FrmMostrarDatosHost frm = new Administracion.FrmMostrarDatosHost(null, reader, ColumCaption);
                        frm.ShowDialog();
                        string Tarjeta = null;
                        string Nombre = null;
                        int Precio = 0;
                        int ITBIS = 0;
                        int Total = 0;

                        if (frm.GridMuestra.SelectedItem != null)
                        {
                            DataRowView item = (frm.GridMuestra as DevExpress.Xpf.Grid.GridControl).SelectedItem as DataRowView;
                            Tarjeta = item.Row.ItemArray[1].ToString();
                            Nombre = item.Row.ItemArray[2].ToString();
                            Precio = Convert.ToInt32(item.Row.ItemArray[4].ToString());
                            ITBIS = Convert.ToInt32(item.Row.ItemArray[6].ToString());
                            GenITBIS = Convert.ToDouble(item.Row.ItemArray[6].ToString());
                            Total = Convert.ToInt32(item.Row.ItemArray[7].ToString());
                            frm.Close();

                            if (ValidaArticulosGrid(Tarjeta) == false)
                            {
                                TablaGrid.AddNewRow();

                                int newRowHandle = DevExpress.Xpf.Grid.DataControlBase.NewItemRowHandle;

                                GridMain.SetCellValue(newRowHandle, "Tarjeta", Tarjeta);
                                GridMain.SetCellValue(newRowHandle, "Nombre", Nombre);
                                //GridMain.SetCellValue(newRowHandle, "Modelo", reader.Rows[0]["Modelo"]);
                                GridMain.SetCellValue(newRowHandle, "Cantidad", 1);
                                GridMain.SetCellValue(newRowHandle, "Precio", Precio);
                                GridMain.SetCellValue(newRowHandle, "SubTotal", Precio);
                                GridMain.SetCellValue(newRowHandle, "ITBIS", ITBIS);
                                GridMain.SetCellValue(newRowHandle, "Total", Total);

                                ((Control)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                                GridMain.CurrentColumn = TablaGrid.VisibleColumns[2];
                                TablaGrid.FocusedRowHandle = newRowHandle;
                                GridMain.SelectItem(newRowHandle);
                                TablaGrid.FocusedColumn = TablaGrid.VisibleColumns[2];
                            }
                        }
                        else
                        {
                            if (SnackbarThree.MessageQueue is { } messageQueue)
                            {
                                var message = "No se selecciono ningun articulo";
                                Task.Factory.StartNew(() => messageQueue.Enqueue(message));
                            }
                        }
                        txtArticuloID.Clear();
                    }
                    else if (reader.Rows.Count < 1)
                    {
                        if (SnackbarThree.MessageQueue is { } messageQueue)
                        {
                            var message = "No se encontro el articulo";
                            Task.Factory.StartNew(() => messageQueue.Enqueue(message));
                        }
                    }

                    //if (reader.Rows.Count > 0) //evaluamos si la tabla actualizada previamente tiene datos, de ser asi actualizamos los controles en los que mostramos esa info.
                    //{
                    //    

                    //}
                }
            }
        }

        private void txtColCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            ClassControl.ValidadorNumeros(e);
        }

        void setDatos(int Flag, string Factura) //Este es el metodo principal del sistema encargado de conectar, enviar y recibir la informacion de sql
        {
            if (Factura == null) //si el parametro llega nulo intentamos llenarlo para que no presente ningun error el sistema
            {
                if (txtFacturaID.Text == "")
                {
                    FacturaID = 0;
                }
                else
                {
                    try
                    {
                        FacturaID = Convert.ToInt32(txtFacturaID.Text);
                    }
                    catch (Exception exception)
                    {
                        ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString(); //Enviamos la excepcion que nos brinda el sistema en caso de que no pueda convertir el id del Usuario
                    }
                }
            }
            else //Si pasamos un Usuario, lo convertimos actualizamos la variable Usuario principal
            {
                FacturaID = Convert.ToInt32(Factura);
            }

            List<SqlParameter> listSqlParameter = new List<SqlParameter>() //Creamos una lista de parametros con cada parametro de sql, donde indicamos el NCF en sql y le indicamos el valor o el campo de donde sacara el valor que enviaremos.
            {
                new SqlParameter("@flag",Flag),
                new SqlParameter("@FacturaID",FacturaID),
                new SqlParameter("@ClienteID", txtClienteID.Text),
                new SqlParameter("@RNC", txtRNC.Text),
                new SqlParameter("@ClaseNCF", ""),
                new SqlParameter("@NCF", txtNCF.Text),
                new SqlParameter("@Nombre", tbxClienteID.Text),
                new SqlParameter("@Descuento", "0"),
                new SqlParameter("@SubTotal", TSubTotal),
                new SqlParameter("@ITBIS", TITBIS),
                new SqlParameter("@Total", TTotal),
                new SqlParameter("@Estado", cbxEstado.Text),
                new SqlParameter("@UsuarioID", ClassVariables.UsuarioID)
            };

            tabla = Clases.ClassData.runDataTable("sp_venFacturas", listSqlParameter, "StoredProcedure"); //recibimos el resultado que nos retorne la transaccion digase, consulta, agregar,editar,eliminar en una tabla.

            if (ClassVariables.GetSetError != null) //Si el intento anterior presenta algun error aqui aparece el mismo
            {
                Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost(ClassVariables.GetSetError);
                frm.ShowDialog();
                ClassVariables.GetSetError = null;
            }

            if (tabla.Rows.Count == 1) //evaluamos si la tabla actualizada previamente tiene datos, de ser asi actualizamos los controles en los que mostramos esa info.
            {
                txtFacturaID.Text = tabla.Rows[0]["FacturaID"].ToString();
                txtClienteID.Text = tabla.Rows[0]["ClienteID"].ToString();
                ClasesVariables.ClienteDinamic = tabla.Rows[0]["Nombre"].ToString();
                ClasesVariables.RNCDinamic = tabla.Rows[0]["RNC"].ToString();
                txtDescuento.Text = tabla.Rows[0]["Descuento"].ToString();
                ClasesVariables.NCFDinamic = tabla.Rows[0]["NCF"].ToString();
                txtSubTotal.Text = tabla.Rows[0]["SubTotal"].ToString();
                txtITBIS.Text = tabla.Rows[0]["ITBIS"].ToString();
                txtTotal.Text = tabla.Rows[0]["Total"].ToString();
                cbxEstado.SelectedItem = tabla.Rows[0]["Estado"].ToString();

                if (Flag == -1) //si pulsamos el boton del ultimo registro se ejecuta el flag -1 es decir que tenemos una busqueda especial
                {
                    try
                    {
                        LastFacturaID = Convert.ToInt32(txtFacturaID.Text); //intentamos convertir el id del Usuario
                    }
                    catch (Exception exception)
                    {
                        ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString(); //si presenta un error ald intentar convertirlo lo enviamos
                    }
                }

                //ClasesVariables.ClienteDinamic = ClassControl.setPropBinding("select * from TcliClientes where ClienteID = ", txtClienteID); //ejecutamos el metodo validador con el campo seleccionado para que lo busque y muestre una vez se guarde el registro

            }
            listSqlParameter.Clear(); //Limpiamos la lista de parametros.

            if (Estado == "Modo Consulta")
            {
                setDatosGrid(0);
            }
            else if (Estado == "Modo Agregar" || Estado == "Modo Editar")
            {
                GridMain.View.MoveFirstRow();
                for (int i = 0; i < GridMain.VisibleRowCount; i++)
                {
                    setDatosGrid(1);
                    GridMain.View.MoveNextRow();
                }

                DataTable AllMetodos = Clases.ClassData.runDataTable("SELECT MetodoID, Nombre FROM [Sadora].[dbo].[TvenMetodoPagos]", null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.
                List<Clases.ClassVariables> ListMetodos = new List<Clases.ClassVariables>();

                if (AllMetodos.Rows.Count > 0)
                {
                    if (AllMetodos.Columns.Contains("Nombre") && AllMetodos.Columns.Contains("MetodoID"))
                    {
                        for (int i = 0; i < AllMetodos.Rows.Count; i++)
                            ListMetodos.Add(new ClassVariables() { IdFormaPago = AllMetodos.Rows[i]["MetodoID"].ToString(), FormaPago = AllMetodos.Rows[i]["Nombre"].ToString() });

                    }
                }


                var FormasDePago = ClassVariables.ListFormasPagos.ToList();//.Where(x => x.FormaPago == FormaPagoAplicada);//new ClassVariables().FormaPago;

                if (FormasDePago.Any() && ListMetodos.Any())
                {
                    foreach (var Forma in FormasDePago)
                    {
                        var MetodoID = ListMetodos.Where(x => x.FormaPago == Forma.FormaPago).Select(x => x.IdFormaPago).FirstOrDefault();

                        //MessageBox.Show("MetodoID = " + MetodoID + ", Forma Pago = " + Forma.FormaPago + ", Cantidad Pago = " + Forma.CantidadFormaPago.ToString());
                        //var cantidad = Forma.CantidadFormaPago;
                        //var forma = Forma.FormaPago;


                        List<SqlParameter> listSqlParamet = new List<SqlParameter>() //Creamos una lista de parametros con cada parametro de sql, donde indicamos el NCF en sql y le indicamos el valor o el campo de donde sacara el valor que enviaremos.
                        {
                            new SqlParameter("@flag", Flag),
                            new SqlParameter("@MetodoPagoID", MetodoID),
                            new SqlParameter("@TransaccionID", txtFacturaID.Text),                            
                            new SqlParameter("@Monto", Forma.CantidadFormaPago)
                        };

                        tabla = Clases.ClassData.runDataTable("sp_venDesglosePago", listSqlParamet, "StoredProcedure"); //recibimos el resultado que nos retorne la transaccion digase, consulta, agregar,editar,eliminar en una tabla.

                        listSqlParamet.Clear();

                        if (ClassVariables.GetSetError != null) //Si el intento anterior presenta algun error aqui aparece el mismo
                        {
                            new Administracion.FrmCompletarCamposHost(ClassVariables.GetSetError).ShowDialog();
                            ClassVariables.GetSetError = null;
                        }


                    }
                }
                //FormasDePago.ForEach(var c on) ;
                //FormasDePago.ForEach(c => c.CantidadFormaPago = Convert.ToDouble(txtMontoDistribuido.Text));
                //else
                //    ListOfFormasPagos.Add(new ClassVariables() { FormaPago = FormaPagoAplicada, CantidadFormaPago = Convert.ToDouble(txtMontoDistribuido.Text) });


            }
        }

        void setDatosGrid(int Flag) //Este es el metodo principal del sistema encargado de conectar, enviar y recibir la informacion de sql
        {

            string ArticuloID = "";
            string Tarjeta = "";
            int Cantidad = 0;
            double Precio = 0;
            double Descuento = 0.00;
            double SubTotal = 0;
            double ITBIS = 0;
            double Total = 0;

            if (Estado == "Modo Agregar" || Estado == "Modo Editar")
            {
                ArticuloID = GridMain.GetFocusedRowCellValue("ArticuloID").ToString();
                Tarjeta = GridMain.GetFocusedRowCellValue("Tarjeta").ToString();
                Cantidad = Convert.ToInt32(GridMain.GetFocusedRowCellValue("Cantidad").ToString());
                Precio = Convert.ToDouble(GridMain.GetFocusedRowCellValue("Precio").ToString());
                SubTotal = Convert.ToDouble(GridMain.GetFocusedRowCellValue("SubTotal").ToString());
                ITBIS = Convert.ToDouble(GridMain.GetFocusedRowCellValue("ITBIS").ToString());
                Total = Convert.ToDouble(GridMain.GetFocusedRowCellValue("Total").ToString());
            }

            List<SqlParameter> listSqlParameter = new List<SqlParameter>() //Creamos una lista de parametros con cada parametro de sql, donde indicamos el NCF en sql y le indicamos el valor o el campo de donde sacara el valor que enviaremos.
            {
                new SqlParameter("Flag",Flag),
                new SqlParameter("@FacturaID", txtFacturaID.Text),
                new SqlParameter("@ArticuloID", ArticuloID),
                //new SqlParameter("@NCF", Tarjeta),
                new SqlParameter("@Cantidad", Cantidad),
                new SqlParameter("@Precio", Precio),
                new SqlParameter("@Descuento", Descuento),
                //new SqlParameter("", Precio),
                new SqlParameter("@SubTotal", SubTotal),
                new SqlParameter("@ITBIS", ITBIS),
                new SqlParameter("@Total", Total),
                new SqlParameter("@UsuarioID", ClassVariables.UsuarioID)
            };

            DataTable TablaGrid = Clases.ClassData.runDataTable("sp_venFacturasDetalles", listSqlParameter, "StoredProcedure"); //recibimos el resultado que nos retorne la transaccion digase, consulta, agregar,editar,eliminar en una tabla.

            if (ClassVariables.GetSetError != null) //Si el intento anterior presenta algun error aqui aparece el mismo
            {
                Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost(ClassVariables.GetSetError);
                frm.ShowDialog();
                ClassVariables.GetSetError = null;
            }

            if (TablaGrid.Rows.Count > 0) //evaluamos si la tabla actualizada previamente tiene datos, de ser asi actualizamos los controles en los que mostramos esa info.
            {
                AgregarModoGrid(TablaGrid);
                //GridMain.ItemsSource = TablaGrid;
                //GridMain.Columns["FormularioID"].Visible = false;
            }
            //else
            //{
            //    GridMain.ItemsSource = null;
            //}

            List<String> listaColumnas = new List<String>() //Estos son los controles que seran controlados, readonly, enable.
            {
                //"Visualiza","Agrega","Modifica","Imprime","Anula"
            };

            if (Estado == "Modo Agregar" && Estado == "Modo Editar")
            {
                //Clases.ClassControl.SetGridReadOnly(GridMain, listaColumnas, false);
            }
            else
            {
                ClassControl.SetGridReadOnly(GridMain);
            }

            listSqlParameter.Clear(); listaColumnas.Clear(); //Limpiamos la lista de parametros.
        }

        void SetControls(bool Habilitador, string Modo, bool Editando) //Este metodo se encarga de controlar cada unos de los controles del cuerpo de la ventana como los textbox
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles que seran controlados, readonly, enable.
            {
                txtFacturaID,txtClienteID, txtArticuloID//,txtClaseNCF
            };

            List<Control> listaControles = new List<Control>() //Estos son los controles que desahilitaremos al dar click en el boton buscar, los controles que no esten en esta lista se quedaran habilitados para poder buscar un registro por ellos.
            {
                txtClienteID,tbxClienteID, txtArticuloID//,txtClaseNCF,tbxClaseNCF
            };

            List<Control> listaControlesValidar = new List<Control>() //Estos son los controles que desahilitaremos al dar click en el boton buscar, los controles que no esten en esta lista se quedaran habilitados para poder buscar un registro por ellos.
            {
                txtFacturaID, txtClienteID, tbxClienteID, txtRNC, txtNCF, txtTotal, txtSubTotal,txtITBIS
            };

            List<Control> listaControlesSoloLimpiar = new List<Control>() //Estos son los controles que desahilitaremos al dar click en el boton buscar, los controles que no esten en esta lista se quedaran habilitados para poder buscar un registro por ellos.
            {
                txtRNC, txtNCF,tbxClienteID
            };

            if (Modo == null) //si no trae ningun modo entra el validador
            {
                if (Estado == "Modo Busqueda")
                {
                    Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando, false, listaControles, listaControlesSoloLimpiar);
                }
                else if (Estado == "Modo Agregar")
                {
                    Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando, true, null, listaControlesSoloLimpiar);
                }
                else
                {
                    Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando, false, null, null);
                }
            }
            else if (Modo == "Validador") //si el parametro modo es igual a validador ingresa.
            {
                Lista = Clases.ClassControl.ValidadorControles(listaControlesValidar); //Este metodo se encarga de validar que cada unos de los controles que se les indica en la lista no se dejen vacios.
            }
            listaControl.Clear(); //limpiamos ambas listas
            listaControles.Clear();
            listaControlesValidar.Clear();
        }

        void SetEnabledButton(String status) //Este metodo se encarga de crear la interacion de los botones de la ventana segun el estado en el que se encuentra
        {

            Estado = status;
            lIconEstado.ToolTip = Estado;

            if (Estado != "Modo Agregar" && Estado != "Modo Editar") //Si el sistema se encuentra en modo consulta o busqueda entra el validador
            {
                BtnPrimerRegistro.IsEnabled = true;
                BtnAnteriorRegistro.IsEnabled = true;
                BtnProximoRegistro.IsEnabled = true;
                BtnUltimoRegistro.IsEnabled = true;
                BtnBuscar.IsEnabled = true;
                BtnImprimir.IsEnabled = true;
                BtnAgregar.IsEnabled = true;
                BtnEditar.IsEnabled = true;

                BtnCancelar.IsEnabled = false;
                //BtnGuardar.IsEnabled = false;
                if (Estado == "Modo Consulta") //Si el estado es modo consulta enviamos a ejecutar otro metodo parametizado de forma especial
                {
                    SetControls(true, null, false);
                    IconEstado.Kind = MaterialDesignThemes.Wpf.PackIconKind.EyeOutline;
                }
                else //Si el estado es modo busqueda enviamos a ejecutar el mismo metodo parametizado de forma especial y cambiamos el estado de los botones
                {
                    BtnProximoRegistro.IsEnabled = false;
                    BtnAnteriorRegistro.IsEnabled = false;
                    BtnImprimir.IsEnabled = false;
                    BtnEditar.IsEnabled = false;
                    SetControls(false, null, false);
                    IconEstado.Kind = MaterialDesignThemes.Wpf.PackIconKind.Search;
                }
            }
            else  //Si el sistema se encuentra en modo Agregar o Editar entra el validador
            {
                BtnPrimerRegistro.IsEnabled = false;
                BtnAnteriorRegistro.IsEnabled = false;
                BtnProximoRegistro.IsEnabled = false;
                BtnUltimoRegistro.IsEnabled = false;

                BtnBuscar.IsEnabled = false;
                BtnImprimir.IsEnabled = false;

                BtnAgregar.IsEnabled = false;
                BtnEditar.IsEnabled = false;
                BtnAnular.IsEnabled = false;

                BtnCancelar.IsEnabled = true;
                //BtnGuardar.IsEnabled = true;
                if (Estado == "Modo Agregar") //Si el estado es modo Agregar enviamos a ejecutar otro metodo parametizado de forma especial
                {
                    SetControls(false, null, false);
                    IconEstado.Kind = MaterialDesignThemes.Wpf.PackIconKind.AddThick;
                    txtFacturaID.IsReadOnly = true;
                    txtFacturaID.Text = (LastFacturaID + 1).ToString();
                }
                else //Si el estado es modo Editar enviamos a ejecutar el mismo metodo parametizado de forma especial
                {
                    SetControls(true, null, true);
                    IconEstado.Kind = MaterialDesignThemes.Wpf.PackIconKind.Edit;
                }
            }
            if (Imprime == false)
            {
                BtnImprimir.IsEnabled = Imprime;
            }
            if (Agrega == false)
            {
                BtnAgregar.IsEnabled = Agrega;
            }
            if (Modifica == false)
            {
                BtnEditar.IsEnabled = Modifica;
            }
            if (Anula == false)
            {
                BtnAnular.IsEnabled = Anula;
            }
        }

        private bool ValidaArticulosGrid(string ReaderValue = null)
        {
            bool resultado = false;

            for (int i = 0; i < GridMain.VisibleRowCount; i++)
            {
                int rowHandle = GridMain.GetRowHandleByVisibleIndex(i);
                string cellValue = GridMain.GetCellValue(rowHandle, "Tarjeta").ToString();
                string rowTarjeta = reader.Rows[0]["Tarjeta"].ToString();
                if (ReaderValue != null)
                    rowTarjeta = ReaderValue;

                if (cellValue == rowTarjeta)
                {
                    resultado = true;

                    int cantidad = Convert.ToInt32(GridMain.GetCellValue(rowHandle, "Cantidad").ToString());

                    GridMain.SetCellValue(rowHandle, "Cantidad", cantidad + 1);
                    TablaGrid.FocusedRowHandle = rowHandle;
                    setValorgrid();

                    break;
                }
            }

            return resultado;
        }

        private void EvaluaGrid()
        {

            TSubTotal = default;
            TITBIS = default;
            TTotal = default;

            for (int i = 0; i < GridMain.VisibleRowCount; i++)
            {
                int rowHandle = GridMain.GetRowHandleByVisibleIndex(i);

                TSubTotal += Convert.ToDouble(GridMain.GetCellValue(rowHandle, "SubTotal").ToString());
                TITBIS += Convert.ToDouble(GridMain.GetCellValue(rowHandle, "ITBIS").ToString());
                TTotal += Convert.ToDouble(GridMain.GetCellValue(rowHandle, "Total").ToString());
            }
            txtSubTotal.Text = TSubTotal.ToString("C");
            txtITBIS.Text = TITBIS.ToString("C");
            txtTotal.Text = TTotal.ToString("C");

        }

        private void setValorgrid()
        {
            double Cantidad = Convert.ToDouble(GridMain.GetCellValue(TablaGrid.FocusedRowHandle, "Cantidad").ToString());
            double Precio = Convert.ToDouble(GridMain.GetCellValue(TablaGrid.FocusedRowHandle, "Precio").ToString());
            double Subtotal = (Cantidad * Precio);
            double ActualITBIS = Convert.ToDouble(GridMain.GetCellValue(TablaGrid.FocusedRowHandle, "ITBIS").ToString());
            double ITBIS = (GenITBIS * Cantidad);
            //if (Cantidad > 1)
            //    ITBIS = ((ActualITBIS / (Cantidad - 1)) + ActualITBIS);
            //else
            //    ITBIS = ActualITBIS; 



            //( //Convert.ToDouble(GridMain.GetCellValue(TablaGrid.FocusedRowHandle, "ITBIS").ToString()) / (Cantidad - 1))
            //+ (Convert.ToDouble(GridMain.GetCellValue(TablaGrid.FocusedRowHandle, "ITBIS").ToString()))

            //); 
            /// (Cantidad - 1)) 
            //+ (Convert.ToDouble(GridMain.GetCellValue(TablaGrid.FocusedRowHandle, "ITBIS").ToString())));


            //(Convert.ToDouble(GridMain.GetCellValue(TablaGrid.FocusedRowHandle, "ITBIS").ToString()) * Cantidad);
            double Total = (ITBIS + Subtotal);

            GridMain.SetCellValue(TablaGrid.FocusedRowHandle, "SubTotal", Subtotal);
            GridMain.SetCellValue(TablaGrid.FocusedRowHandle, "ITBIS", ITBIS);
            GridMain.SetCellValue(TablaGrid.FocusedRowHandle, "Total", Total);
            EvaluaGrid();
        }

        private void TablaGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TablaGrid.FocusedColumn.HeaderCaption.ToString() == "Cantidad")
                {
                    if (NewCantidad >= LastCantidad)
                    {
                        setValorgrid();
                        txtArticuloID.Focus();
                    }
                    else
                    {
                        NewCantidad = Convert.ToInt32(GridMain.GetCellValue(TablaGrid.FocusedRowHandle, "Cantidad").ToString());
                        FrmControlAccesos frm = new FrmControlAccesos();
                        frm.ShowDialog();
                        if (frm.Resultado == false)
                        {
                            GridMain.SetCellValue(TablaGrid.FocusedRowHandle, "Cantidad", LastCantidad);
                        }
                        frm.Close();
                        //new FrmControlAccesos().ShowDialog();
                        //MessageBox.Show("No puede disminuir el valor de la cantidad");
                    }
                }
            }
        }

        private void AgregarModoGrid(DataTable table = null)
        {
            if (table == null)
            {
                reader = Clases.ClassData.runDataTable("Select a.ArticuloID ,a.Tarjeta, a.Nombre, 1 as Cantidad, a.Precio, a.Precio as SubTotal, ((a.Precio * b.Porcentaje) / 100) as ITBIS " +
                                ", ((a.Precio * b.Porcentaje) / 100)+a.Precio as Total from TinvArticulos a inner join TinvClaseArticulos b on a.ClaseArticuloID = b.ClaseID " +
                                "where a.Tarjeta = '-1' or a.Nombre like '%-1%' order by a.ArticuloID", null, "CommandText");

                GridMain.ItemsSource = reader;
                txtClienteID.Focus();
                BtnPay.IsEnabled = true;
            }
            else
            {
                GridMain.ItemsSource = table;
            }

            foreach (DevExpress.Xpf.Grid.GridColumn Col in GridMain.Columns)
            {
                switch (Col.HeaderCaption)
                {
                    case "Articulo ID":
                        Col.Visible = false;
                        Col.ReadOnly = true;
                        break;

                    case "Tarjeta":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(1.30, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.ReadOnly = true;
                        break;

                    case "Nombre":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(2, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.ReadOnly = true;
                        break;

                    case "Cantidad":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(101, DevExpress.Xpf.Grid.GridColumnUnitType.Pixel);
                        //Col.KeyDown += txtColCantidad_KeyDown;
                        break;

                    case "Precio":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(1, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.ReadOnly = true;
                        Col.EditSettings.DisplayFormat = "N";
                        break;

                    case "Sub Total":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(1, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.ReadOnly = true;
                        Col.EditSettings.DisplayFormat = "N";
                        break;

                    case "ITBIS":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(1, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.ReadOnly = true;
                        Col.EditSettings.DisplayFormat = "N";
                        break;

                    case "Total":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(1, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.ReadOnly = true;
                        Col.EditSettings.DisplayFormat = "N";
                        break;
                }
            }

        }

        private void TablaGrid_FocusedColumnChanged(object sender, DevExpress.Xpf.Grid.FocusedColumnChangedEventArgs e)
        {
            try
            {
                if (TablaGrid.FocusedColumn.HeaderCaption.ToString() == "Precio")
                    TablaGrid.FocusedColumn = TablaGrid.VisibleColumns[2];
            }
            catch
            {
            }
        }

        private void TablaGrid_CellValueChanging(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            NewCantidad = Convert.ToInt32(e.Value.ToString());
        }

        private void TablaGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (TablaGrid.FocusedColumn.HeaderCaption.ToString() == "Cantidad")
            {
                if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                {
                    int rowHandle = GridMain.GetSelectedRowHandles()[0];//GridMain.GetRowHandleByVisibleIndex(GridMain.GetSelectedRowHandles());

                    LastCantidad = Convert.ToInt32(GridMain.GetCellValue(rowHandle, "Cantidad").ToString());
                }
            }
        }

        private void ControlEvent()
        {
            var CajaConfigurada = (int)Settings.Default["Caja"];

            if (CajaConfigurada <= 0)
            {
                if (SnackbarThree.MessageQueue is { } messageQueue)
                    Task.Factory.StartNew(() => messageQueue.Enqueue("No hay caja configurada"));
            }
            else
            {
                string CreateNameButton = "";

                DataTable MetodoCaja = Clases.ClassData.runDataTable("select a.Nombre from TvenMetodoPagos a inner join TvenCajasDetalle b on a.MetodoID = b.MetodoID where b.CajaID = " + CajaConfigurada + " and Alta = 1", null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.
                for (int i = 0; i < MetodoCaja.Rows.Count; i++)
                {
                    CreateNameButton = MetodoCaja.Rows[i]["Nombre"].ToString();

                    switch (CreateNameButton)
                    {
                        case string a when a.ToUpper().Contains("EFECTIVO"):
                            #region Create Border
                            Border myBorderEfect = new Border()
                            {
                                BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
                                BorderThickness = new Thickness(2),
                                HorizontalAlignment = HorizontalAlignment.Right,
                                VerticalAlignment = VerticalAlignment.Stretch,
                                Padding = new Thickness(0),
                                CornerRadius = new CornerRadius(5),
                                Margin = new Thickness(3)
                            };
                            #endregion
                            #region Create Button
                            Button MyButtonEfect = new Button()
                            {
                                Padding = new Thickness(0),
                                Height = 59,
                                MinWidth = 100,
                                //Width = 100,
                                ToolTip = "Pulsar para elegir metodo de pago " + CreateNameButton,
                                Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#33C8C8C8"),
                                Style = Application.Current.FindResource("MaterialDesignFlatButton") as Style
                            };
                            #endregion
                            #region Asing event, Create Stack and Icon with text
                            MyButtonEfect.Click += new RoutedEventHandler(handlerEfect_Click);

                            StackPanel MyStackEfect = new StackPanel();

                            Efect = CreateNameButton;

                            var packIconMaterialEfect = new MaterialDesignThemes.Wpf.PackIcon()
                            {
                                Kind = MaterialDesignThemes.Wpf.PackIconKind.Cash,
                                Width = 36,
                                Height = 36,
                                HorizontalAlignment = HorizontalAlignment.Center
                            };

                            TextBlock MyTextEfect = new TextBlock()
                            {
                                Text = CreateNameButton,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                FontSize = 17
                            };
                            #endregion
                            #region Asing Childs
                            MyStackEfect.Children.Add(packIconMaterialEfect);
                            MyStackEfect.Children.Add(MyTextEfect);
                            MyButtonEfect.Content = MyStackEfect;
                            myBorderEfect.Child = MyButtonEfect;

                            PanelWrap.Children.Add(myBorderEfect);
                            #endregion
                            break;
                        case string a when a.ToUpper().Contains("TARJETA"):
                            #region Create Border
                            Border myBorderTarj = new Border()
                            {
                                BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
                                BorderThickness = new Thickness(2),
                                HorizontalAlignment = HorizontalAlignment.Right,
                                VerticalAlignment = VerticalAlignment.Stretch,
                                Padding = new Thickness(0),
                                CornerRadius = new CornerRadius(5),
                                Margin = new Thickness(3)
                            };
                            #endregion
                            #region Create Button
                            Button MyButtonTarj = new Button()
                            {
                                Padding = new Thickness(0),
                                Height = 59,
                                MinWidth = 100,
                                //Width = 100,
                                ToolTip = "Pulsar para elegir metodo de pago " + CreateNameButton,
                                Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#33C8C8C8"),
                                Style = Application.Current.FindResource("MaterialDesignFlatButton") as Style
                            };
                            #endregion
                            #region Asing event, Create Stack and Icon with text
                            MyButtonTarj.Click += new RoutedEventHandler(handlerTarj_Click);

                            StackPanel MyStackTarj = new StackPanel();

                            Tarj = CreateNameButton;

                            var packIconMaterialTarj = new MaterialDesignThemes.Wpf.PackIcon()
                            {
                                Kind = MaterialDesignThemes.Wpf.PackIconKind.CreditCardOutline,
                                Width = 36,
                                Height = 36,
                                HorizontalAlignment = HorizontalAlignment.Center
                            };

                            TextBlock MyTextTarj = new TextBlock()
                            {
                                Text = CreateNameButton/*"Tarjeta"*/,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                FontSize = 17
                            };
                            #endregion
                            #region Asing Childs
                            MyStackTarj.Children.Add(packIconMaterialTarj);
                            MyStackTarj.Children.Add(MyTextTarj);
                            MyButtonTarj.Content = MyStackTarj;
                            myBorderTarj.Child = MyButtonTarj;

                            PanelWrap.Children.Add(myBorderTarj);
                            #endregion
                            break;
                        case string a when a.ToUpper().Contains("TRANSFERENCIA"):
                            #region Create Border
                            Border myBorderTrans = new Border()
                            {
                                BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
                                BorderThickness = new Thickness(2),
                                HorizontalAlignment = HorizontalAlignment.Right,
                                VerticalAlignment = VerticalAlignment.Stretch,
                                Padding = new Thickness(0),
                                CornerRadius = new CornerRadius(5),
                                Margin = new Thickness(3)
                            };
                            #endregion
                            #region Create Button
                            Button MyButtonTrans = new Button()
                            {
                                Padding = new Thickness(0),
                                Height = 59,
                                MinWidth = 100,
                                //Width = 100,
                                ToolTip = "Pulsar para elegir metodo de pago " + CreateNameButton,
                                Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#33C8C8C8"),
                                Style = Application.Current.FindResource("MaterialDesignFlatButton") as Style
                            };
                            #endregion
                            #region Asing event, Create Stack and Icon with text
                            MyButtonTrans.Click += new RoutedEventHandler(handlerTrans_Click);

                            StackPanel MyStackTrans = new StackPanel();

                            Trans = CreateNameButton;

                            var packIconMaterialTrans = new MaterialDesignThemes.Wpf.PackIcon()
                            {
                                Kind = MaterialDesignThemes.Wpf.PackIconKind.BankTransfer,
                                Width = 36,
                                Height = 36,
                                HorizontalAlignment = HorizontalAlignment.Center
                            };

                            TextBlock MyTextTrans = new TextBlock()
                            {
                                Text = /*"Transferencia"*/ CreateNameButton,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                FontSize = 17
                            };
                            #endregion
                            #region Asing Childs
                            MyStackTrans.Children.Add(packIconMaterialTrans);
                            MyStackTrans.Children.Add(MyTextTrans);
                            MyButtonTrans.Content = MyStackTrans;
                            myBorderTrans.Child = MyButtonTrans;

                            PanelWrap.Children.Add(myBorderTrans);
                            #endregion
                            break;
                        case string a when a.ToUpper().Contains("CHEQUE"):
                            #region Create Border
                            Border myBorderCk = new Border()
                            {
                                BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
                                BorderThickness = new Thickness(2),
                                HorizontalAlignment = HorizontalAlignment.Right,
                                VerticalAlignment = VerticalAlignment.Stretch,
                                Padding = new Thickness(0),
                                CornerRadius = new CornerRadius(5),
                                Margin = new Thickness(3)
                            };
                            #endregion
                            #region Create Button
                            Button MyButtonCk = new Button()
                            {
                                Padding = new Thickness(0),
                                Height = 59,
                                MinWidth = 100,
                                //Width = 100,
                                ToolTip = "Pulsar para elegir metodo de pago " + CreateNameButton,
                                Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#33C8C8C8"),
                                Style = Application.Current.FindResource("MaterialDesignFlatButton") as Style
                            };
                            #endregion
                            #region Asing event, Create Stack and Icon with text
                            MyButtonCk.Click += new RoutedEventHandler(handlerCk_Click);

                            StackPanel MyStackCk = new StackPanel();

                            Ck = CreateNameButton;

                            var packIconMaterialCk = new MaterialDesignThemes.Wpf.PackIcon()
                            {
                                Kind = MaterialDesignThemes.Wpf.PackIconKind.Bank,
                                Width = 36,
                                Height = 36,
                                HorizontalAlignment = HorizontalAlignment.Center
                            };

                            TextBlock MyTextCk = new TextBlock()
                            {
                                Text = CreateNameButton/*"Cheque"*/,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                FontSize = 17
                            };
                            #endregion
                            #region Asing Childs
                            MyStackCk.Children.Add(packIconMaterialCk);
                            MyStackCk.Children.Add(MyTextCk);
                            MyButtonCk.Content = MyStackCk;
                            myBorderCk.Child = MyButtonCk;

                            PanelWrap.Children.Add(myBorderCk);
                            #endregion
                            break;
                    }
                }
            }
        }

        private void handlerEfect_Click(object sender, RoutedEventArgs e)
        {
            FormaPago = Efect;
            OpenControlComprobantes();
        }
        private void handlerTarj_Click(object sender, RoutedEventArgs e)
        {
            FormaPago = Tarj;
            OpenControlComprobantes();
        }
        private void handlerTrans_Click(object sender, RoutedEventArgs e)
        {
            FormaPago = Trans;
            OpenControlComprobantes();
        }
        private void handlerCk_Click(object sender, RoutedEventArgs e)
        {
            FormaPago = Ck;
            OpenControlComprobantes();
        }

        void OpenControlComprobantes()
        {
            PanelOpcionesPagos.Visibility = Visibility.Hidden;
            int client;

            try
            {
                client = Convert.ToInt32(txtClienteID.Text);
            }
            catch
            {
                client = 0;
            }

            if (txtClienteID.Text != "")
            {
                SetControls(false, "Validador", false);
                if (Lista != "Debe Completar los Campos: ")
                    new Administracion.FrmCompletarCamposHost(Lista).ShowDialog();
                else
                {
                    new FrmControlComprobantes(client, FormaPago, TTotal, ClasesVariables).ShowDialog();
                    if (ClassVariables.IsFullFormaPago)
                    {
                        if (Estado == "Modo Editar")
                            setDatos(2, null);
                        else
                        {
                            cbxEstado.Text = "Cerrada";
                            Estado = "Modo Agregar";
                            setDatos(1, null);
                        }
                        SetEnabledButton("Modo Consulta");
                    }

                }
            }
            else
            {
                if (SnackbarThree.MessageQueue is { } messageQueue)
                    Task.Factory.StartNew(() => messageQueue.Enqueue("Alerta, Debe seleccionar un cliente."));
            }
        }
    }
}