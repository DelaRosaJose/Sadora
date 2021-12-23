using Sadora.Clases;
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

namespace Sadora.Inventario
{
    /// <summary>
    /// Lógica de interacción para UscMovimientoInventario.xaml
    /// </summary>
    public partial class UscMovimientoInventario : UserControl
    {
        public UscMovimientoInventario()
        {
            InitializeComponent();
            Name = "UscMovimientoInventario";
        }

        bool Imprime;
        bool Agrega;
        bool Modifica;

        bool Inicializador = false;
        DataTable reader;
        DataTable tabla;
        DataTable TableGrid;
        //SqlDataReader reader;
        string Estado;
        string Lista;
        int MovimientoID;
        int LastMovimientoID;
        string last;

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

                this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Inicializador = false;
            }

        }

        private void BtnPrimerRegistro_Click(object sender, RoutedEventArgs e)
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles limpiados.
            {
                /*txtMontoGravado,txtMontoGravado*/
            };
            ClassControl.ClearControl(listaControl);
            SetEnabledButton("Modo Consulta");
            setDatos(0, "1");
            BtnPrimerRegistro.IsEnabled = false;
            BtnAnteriorRegistro.IsEnabled = false;
        }

        private void BtnAnteriorRegistro_Click(object sender, RoutedEventArgs e)
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles limpiados.
            {
                /*txtMontoGravado,txtMontoGravado*/
            };
            ClassControl.ClearControl(listaControl);
            SetEnabledButton("Modo Consulta");
            try
            {
                MovimientoID = Convert.ToInt32(txtMovimientoID.Text) - 1;
            }
            catch (Exception exception)
            {
                ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString();
            }


            if (MovimientoID <= 1)
            {
                BtnPrimerRegistro.IsEnabled = false;
                BtnAnteriorRegistro.IsEnabled = false;
                setDatos(0, "1");
            }
            else
            {
                setDatos(0, MovimientoID.ToString());
            }
        }

        private void BtnProximoRegistro_Click(object sender, RoutedEventArgs e)
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles limpiados.
            {
                /*txtMontoGravado,txtMontoGravado*/
            };
            ClassControl.ClearControl(listaControl);
            SetEnabledButton("Modo Consulta");
            try
            {
                MovimientoID = Convert.ToInt32(txtMovimientoID.Text) + 1;
            }
            catch (Exception exception)
            {
                ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString();
            }

            if (MovimientoID >= LastMovimientoID)
            {
                BtnUltimoRegistro.IsEnabled = false;
                BtnProximoRegistro.IsEnabled = false;
                setDatos(0, LastMovimientoID.ToString());
            }
            else
            {
                setDatos(0, MovimientoID.ToString());
            }
        }

        private void BtnUltimoRegistro_Click(object sender, RoutedEventArgs e)
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles limpiados.
            {
                /*txtMontoGravado,txtMontoGravado*/
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
                last = txtMovimientoID.Text;
                SetEnabledButton("Modo Busqueda");
            }
            else if (Estado == "Modo Busqueda")
            {
                List<Control> listaControles = new List<Control>() //Estos son los controles que desahilitaremos al dar click en el boton buscar, los controles que no esten en esta lista se quedaran habilitados para poder buscar un registro por ellos.
                {
                    //txtProveedorID,tbxProveedorID,txtDireccion,txtCorreoElectronico,txtTelefono,txtCelular//,cActivar
                };
                Clases.ClassControl.ActivadorControlesReadonly(null, true, false, false, listaControles);

                setDatos(0, null);

                List<String> ListName = new List<String>() //Estos son los campos que saldran en la ventana de busqueda, solo si se le pasa esta lista de no ser asi, se mostrarian todos
                {
                    "Proveedor ID","ITBIS","Nomenclatura","Desde","Direccion","Activo"
                };

                SetEnabledButton("Modo Consulta");

                if (tabla.Rows.Count > 1)
                {
                    Administracion.FrmMostrarDatosHost frm = new Administracion.FrmMostrarDatosHost(null, tabla, ListName);
                    frm.ShowDialog();

                    if (frm.GridMuestra.SelectedItem != null)
                    {
                        DataRowView item = (frm.GridMuestra as DevExpress.Xpf.Grid.GridControl).SelectedItem as DataRowView;
                        txtMovimientoID.Text = item.Row.ItemArray[0].ToString();
                        setDatos(0, txtMovimientoID.Text);
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
                    //List<Control> listaControl = new List<Control>() //Estos son los controles limpiados.
                    //{
                    //   txtMontoGravado,txtMontoGravado
                    //};
                    //ClassControl.ClearControl(listaControl);
                    //setDatos(0, last);
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
                //SqlDataReader tabla = ClassControl.getDatosCedula(txtMontoGravado.Text);
                //if (tabla != null)
                //{
                //    tabla.Close();
                //    tabla.Dispose();
                if (Estado == "Modo Editar")
                {
                    setDatos(2, null);
                }
                else
                {
                    setDatos(1, null);
                }
                SetEnabledButton("Modo Consulta");
                setDatos(0, txtMovimientoID.Text);
                //this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                //}

            }

        }

        private void txtMovimientoID_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                    ((Control)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
            }
        }

        private void cbxTipoMovimiento_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                    ((Control)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
            }
        }

        private void dtpFechaMovimiento_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                    cbxEstado.Focus();                    //((Control)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
            }
        }

        private void cbxEstado_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((Control)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        void setDatos(int Flag, string Transaccion) //Este es el metodo principal del sistema encargado de conectar, enviar y recibir la informacion de sql
        {
            if (Transaccion == null) //si el parametro llega nulo intentamos llenarlo para que no presente ningun error el sistema
            {
                if (txtMovimientoID.Text == "")
                {
                    MovimientoID = 0;
                }
                else
                {
                    try
                    {
                        MovimientoID = Convert.ToInt32(txtMovimientoID.Text);
                    }
                    catch (Exception exception)
                    {
                        ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString(); //Enviamos la excepcion que nos brinda el sistema en caso de que no pueda convertir el id del Proveedor
                    }
                }
            }
            else //Si pasamos un Proveedor, lo convertimos actualizamos la variable Proveedor principal
            {
                MovimientoID = Convert.ToInt32(Transaccion);
            }

            List<SqlParameter> listSqlParameter = new List<SqlParameter>() //Creamos una lista de parametros con cada parametro de sql, donde indicamos el Nomenclatura en sql y le indicamos el valor o el campo de donde sacara el valor que enviaremos.
            {
                new SqlParameter("Flag",Flag),
                new SqlParameter("@MovimientoID",txtMovimientoID.Text),
                new SqlParameter("@TipoMovimiento",cbxTipoMovimiento.Text),
                new SqlParameter("@FechaMovimiento",dtpFechaMovimiento.Text),
                new SqlParameter("@Estado",cbxEstado.Text),
                new SqlParameter("@UsuarioID",ClassVariables.UsuarioID)
            };

            tabla = Clases.ClassData.runDataTable("sp_invMovimientoInventario", listSqlParameter, "StoredProcedure"); //recibimos el resultado que nos retorne la transaccion digase, consulta, agregar,editar,eliminar en una tabla.

            if (ClassVariables.GetSetError != null) //Si el intento anterior presenta algun error aqui aparece el mismo
            {
                Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost(ClassVariables.GetSetError);
                frm.ShowDialog();
                ClassVariables.GetSetError = null;
            }

            if (tabla.Rows.Count == 1) //evaluamos si la tabla actualizada previamente tiene datos, de ser asi actualizamos los controles en los que mostramos esa info.
            {
                txtMovimientoID.Text = tabla.Rows[0]["MovimientoID"].ToString();
                cbxTipoMovimiento.Text = tabla.Rows[0]["TipoMovimiento"].ToString();
                dtpFechaMovimiento.Text = tabla.Rows[0]["FechaMovimiento"].ToString();
                cbxEstado.Text = tabla.Rows[0]["Estado"].ToString();

                if (Flag == -1) //si pulsamos el boton del ultimo registro se ejecuta el flag -1 es decir que tenemos una busqueda especial
                {
                    try
                    {
                        LastMovimientoID = Convert.ToInt32(txtMovimientoID.Text); //intentamos convertir el id del Proveedor
                    }
                    catch (Exception exception)
                    {
                        ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString(); //si presenta un error al intentar convertirlo lo enviamos
                    }
                }
                //ClassControl.setValidador("select * from TsupProveedores where ProveedorID =", txtProveedorID, tbxProveedorID); //ejecutamos el metodo validador con el campo seleccionado para que lo busque y muestre una vez se guarde el registro
            }
            //else
            //{
            //    if (SnackbarThree.MessageQueue is { } messageQueue)
            //    {
            //        var message = "No se encontraron datos";
            //        Task.Factory.StartNew(() => messageQueue.Enqueue(message));
            //    }
            //}
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

                /******
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

                        DataTable Setter = Clases.ClassData.runDataTable("sp_venDesglosePago", listSqlParamet, "StoredProcedure"); //recibimos el resultado que nos retorne la transaccion digase, consulta, agregar,editar,eliminar en una tabla.

                        listSqlParamet.Clear();

                        if (ClassVariables.GetSetError != null) //Si el intento anterior presenta algun error aqui aparece el mismo
                        {
                            new Administracion.FrmCompletarCamposHost(ClassVariables.GetSetError).ShowDialog();
                            ClassVariables.GetSetError = null;
                        }
                    }
                }
                */
            }

        }

        void setDatosGrid(int Flag) //Este es el metodo principal del sistema encargado de conectar, enviar y recibir la informacion de sql
        {

            string MovimientoID = "";
            string ArticuloID = "";
            string Tarjeta = "";
            int Cantidad = 0;
            int CantidadPrevia = 0;
            int CantidadPostMovimiento = 0;

            if (Estado == "Modo Agregar" || Estado == "Modo Editar")
            {
                MovimientoID = GridMain.GetFocusedRowCellValue("MovimientoID").ToString();
                ArticuloID = GridMain.GetFocusedRowCellValue("ArticuloID").ToString();
                Tarjeta = GridMain.GetFocusedRowCellValue("Tarjeta").ToString();
                Cantidad = Convert.ToInt32(GridMain.GetFocusedRowCellValue("Cantidad").ToString());
                CantidadPrevia = Convert.ToInt32(GridMain.GetFocusedRowCellValue("CantidadPrevia").ToString());
                CantidadPostMovimiento = Convert.ToInt32(GridMain.GetFocusedRowCellValue("CantidadPostMovimiento").ToString());
            }

            List<SqlParameter> listSqlParameter = new List<SqlParameter>() //Creamos una lista de parametros con cada parametro de sql, donde indicamos el NCF en sql y le indicamos el valor o el campo de donde sacara el valor que enviaremos.
            {
                new SqlParameter("Flag",Flag),
                new SqlParameter("@MovimientoID", MovimientoID),
                new SqlParameter("@ArticuloID", ArticuloID),
                new SqlParameter("@TarjetaID", Tarjeta),//string.IsNullOrEmpty(Tarjeta) || Tarjeta == "" ? "1" : Tarjeta),
                new SqlParameter("@Cantidad", Cantidad),
                new SqlParameter("@CantidadPrevia", CantidadPrevia),
                new SqlParameter("@CantidadPostMovimiento", CantidadPostMovimiento)
            };

            TableGrid = Clases.ClassData.runDataTable("sp_invMovimientoInventarioDetalle", listSqlParameter, "StoredProcedure"); //recibimos el resultado que nos retorne la transaccion digase, consulta, agregar,editar,eliminar en una tabla.

            if (ClassVariables.GetSetError != null) //Si el intento anterior presenta algun error aqui aparece el mismo
            {
                new Administracion.FrmCompletarCamposHost(ClassVariables.GetSetError).ShowDialog();
                ClassVariables.GetSetError = null;
            }

            if (TableGrid.Rows.Count > 0) //evaluamos si la tabla actualizada previamente tiene datos, de ser asi actualizamos los controles en los que mostramos esa info.
            {
                AgregarModoGrid(TableGrid);
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
                ClassControl.SetGridReadOnly(GridMain);

            listSqlParameter.Clear(); listaColumnas.Clear(); //Limpiamos la lista de parametros.
        }


        void SetControls(bool Habilitador, string Modo, bool Editando) //Este metodo se encarga de controlar cada unos de los controles del cuerpo de la ventana como los textbox
        {
            List<Control> listaControl = new List<Control>() //Estos son los controles que seran controlados, readonly, enable.
            {
                txtMovimientoID,/*txtMontoGravado,txtMontoExcento,txtProveedorID,*/cbxTipoMovimiento,dtpFechaMovimiento,cbxEstado
            };

            List<Control> listaControles = new List<Control>() //Estos son los controles que desahilitaremos al dar click en el boton buscar, los controles que no esten en esta lista se quedaran habilitados para poder buscar un registro por ellos.
            {
                //txtProveedorID,tbxProveedorID//,txtDireccion,txtCorreoElectronico,txtTelefono,txtCelular//,cActivar
            };

            List<Control> listaControlesValidar = new List<Control>() //Estos son los controles que validaremos al dar click en el boton guardar.
            {
                txtMovimientoID//,txtMontoGravado,txtProveedorID,tbxProveedorID//,txtDireccion,txtCorreoElectronico,txtTelefono,txtCelular
            };

            if (Modo == null) //si no trae ningun modo entra el validador
            {
                if (Estado == "Modo Busqueda")
                    Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando, false, listaControles);
                else if (Estado == "Modo Agregar")
                    Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando, true, null);
                else
                    Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando, false, null);
            }
            else if (Modo == "Validador") //si el parametro modo es igual a validador ingresa.
                Lista = Clases.ClassControl.ValidadorControles(listaControlesValidar); //Este metodo se encarga de validar que cada unos de los controles que se les indica en la lista no se dejen vacios.

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
                BtnGuardar.IsEnabled = false;
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

                BtnCancelar.IsEnabled = true;
                BtnGuardar.IsEnabled = true;
                if (Estado == "Modo Agregar") //Si el estado es modo Agregar enviamos a ejecutar otro metodo parametizado de forma especial
                {
                    SetControls(false, null, false);
                    IconEstado.Kind = MaterialDesignThemes.Wpf.PackIconKind.AddThick;
                    txtMovimientoID.Text = (LastMovimientoID + 1).ToString();
                }
                else //Si el estado es modo Editar enviamos a ejecutar el mismo metodo parametizado de forma especial
                {
                    SetControls(true, null, true);
                    IconEstado.Kind = MaterialDesignThemes.Wpf.PackIconKind.Edit;
                }
                txtMovimientoID.IsReadOnly = true;
                txtArticuloID.IsReadOnly = false;
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
        }

        private void AgregarModoGrid(DataTable table = null)
        {
            if (table == null)
            {
                reader = Clases.ClassData.runDataTable("select ArticuloID, TarjetaID as Tarjeta, CantidadPrevia, Cantidad, CantidadPostMovimiento from TinvMovimientoInventarioDetalle  where MovimientoID = '" + txtMovimientoID.Text + "' ", null, "CommandText");
                GridMain.ItemsSource = reader;
            }
            else
                GridMain.ItemsSource = table;

            foreach (DevExpress.Xpf.Grid.GridColumn Col in GridMain.Columns)
            {
                switch (Col.HeaderCaption)
                {
                    case "Articulo ID":
                        Col.Visible = false;
                        Col.ReadOnly = true;
                        break;

                    case "Tarjeta":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(1, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.ReadOnly = true;
                        break;


                    case "Cantidad":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(1, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.EditSettings.DisplayFormat = "N";
                        break;

                    case "Cantidad Previa":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(1, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.ReadOnly = true;
                        Col.EditSettings.DisplayFormat = "N";
                        break;

                    case "Cantidad Post Movimiento":
                        Col.Width = new DevExpress.Xpf.Grid.GridColumnWidth(1, DevExpress.Xpf.Grid.GridColumnUnitType.Star);
                        Col.ReadOnly = true;
                        Col.EditSettings.DisplayFormat = "N";
                        break;
                }
            }
        }


        private void TablaGrid_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtArticuloID_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            if (Estado == "Modo Agregar")
            {
                if (e.Key == Key.Enter)
                {
                    reader = Clases.ClassData.runDataTable("Select a.ArticuloID ,a.Tarjeta, a.Nombre, 1 as Cantidad, a.Precio, a.Precio as SubTotal, ((a.Precio * b.Porcentaje) / 100) as ITBIS " +
                            ", ((a.Precio * b.Porcentaje) / 100)+a.Precio as Total, b.Porcentaje from TinvArticulos a inner join TinvClaseArticulos b on a.ClaseArticuloID = b.ClaseID " +
                            "where a.Tarjeta = '" + txtArticuloID.Text + "' or a.Nombre like '%" + txtArticuloID.Text + "%' order by a.ArticuloID", null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.


                    List<String> ColumCaption = new List<String>() //Estos son los campos que saldran en la ventana de busqueda, solo si se le pasa esta lista de no ser asi, se mostrarian todos
                    {
                        "Tarjeta", "Nombre", "Precio", "ITBIS", "Total", "Porcentaje"
                    };

                    if (reader.Rows.Count == 1)
                    {

                        DataTable ReadTable = Clases.ClassData.runDataTable("select Cantidad, HaveServices from TinvArticulos where ArticuloID = " + reader.Rows[0]["ArticuloID"].ToString(), null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.

                        string LostTarjeta = reader.Rows[0]["Tarjeta"].ToString();

                        if (ReadTable.Rows.Count == 1 && ReadTable.Columns.Contains("Cantidad"))
                        {
                            if ((double)ReadTable.Rows[0]["Cantidad"] <= 0 && SnackbarThree.MessageQueue is { } messageQueue)
                            {
                                Task.Factory.StartNew(() => messageQueue.Enqueue("No hay cantidad disponible para este articulo"));
                                return;
                            }
                            else if ((double)ReadTable.Rows[0]["Cantidad"] >= 1 && (double)ReadTable.Rows[0]["Cantidad"] <= 3 && SnackbarThree.MessageQueue is { } messageQueue1)
                                Task.Factory.StartNew(() => messageQueue1.Enqueue("Quedan pocas unidades de este articulo: " + (double)ReadTable.Rows[0]["Cantidad"]));

                        }
                        else
                            return;

                        GenITBIS = Convert.ToDouble(reader.Rows[0]["ITBIS"].ToString());

                        if (ValidaArticulosGrid(LostTarjeta, (double)ReadTable.Rows[0]["Cantidad"]) == false)
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
                        string LostTarjeta = null;
                        string Tarjeta = null;
                        string ArticuloID = null;
                        string Nombre = null;
                        double Precio = 0;
                        double ITBIS = 0;
                        double Porcentaje = 0;
                        double Total = 0;

                        if (frm.GridMuestra.SelectedItem != null)
                        {
                            DataRowView item = (frm.GridMuestra as DevExpress.Xpf.Grid.GridControl).SelectedItem as DataRowView;
                            ArticuloID = item.Row.ItemArray[0].ToString();
                            Tarjeta = item.Row.ItemArray[1].ToString();
                            Nombre = item.Row.ItemArray[2].ToString();
                            Precio = Convert.ToDouble(item.Row.ItemArray[4].ToString());
                            ITBIS = Convert.ToDouble(item.Row.ItemArray[6].ToString());
                            GenITBIS = Convert.ToDouble(item.Row.ItemArray[6].ToString());
                            Total = Convert.ToDouble(item.Row.ItemArray[7].ToString());
                            Porcentaje = Convert.ToDouble(item.Row.ItemArray[8].ToString());
                            frm.Close();

                            DataTable ReadTable = Clases.ClassData.runDataTable("select Cantidad, HaveServices  from TinvArticulos where ArticuloID = " + ArticuloID, null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.

                            if (ReadTable.Rows.Count == 1 && ReadTable.Columns.Contains("Cantidad"))
                            {
                                if ((double)ReadTable.Rows[0]["Cantidad"] <= 0 && SnackbarThree.MessageQueue is { } messageQueue)
                                {
                                    Task.Factory.StartNew(() => messageQueue.Enqueue("No hay cantidad disponible para este articulo"));
                                    return;
                                }
                                else if ((double)ReadTable.Rows[0]["Cantidad"] >= 1 && (double)ReadTable.Rows[0]["Cantidad"] <= 3 && SnackbarThree.MessageQueue is { } messageQueue1)
                                    Task.Factory.StartNew(() => messageQueue1.Enqueue("Quedan pocas unidades de este articulo: " + (double)ReadTable.Rows[0]["Cantidad"]));

                                if ((bool)ReadTable.Rows[0]["HaveServices"])
                                {
                                    DataTable ValServices = Clases.ClassData.runDataTable("select NombreServicio, Precio from TinvServicioArticulos where Alta = 1 and ArticuloID = " + ArticuloID, null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.

                                    Administracion.FrmMostrarDatosHost frmServices = new Administracion.FrmMostrarDatosHost(null, ValServices, null);
                                    frmServices.ShowDialog();

                                    if (frmServices.GridMuestra.SelectedItem != null)
                                    {
                                        DataRowView itemService = (frmServices.GridMuestra as DevExpress.Xpf.Grid.GridControl).SelectedItem as DataRowView;
                                        LostTarjeta = Tarjeta;
                                        Tarjeta += " (" + itemService.Row.ItemArray[0].ToString() + ")";
                                        Nombre += " (" + itemService.Row.ItemArray[0].ToString() + ")";
                                        Precio = (double)itemService.Row.ItemArray[1];
                                        //reader.Rows[0]["SubTotal"] = itemService.Row.ItemArray[1].ToString();
                                        ITBIS = ((ITBIS * Porcentaje) / (double)100);
                                        Total = (((Precio * Porcentaje) / 100) + Precio);

                                        frmServices.Close();
                                    }
                                }
                            }
                            else
                                return;

                            if (ValidaArticulosGrid(LostTarjeta, (double)ReadTable.Rows[0]["Cantidad"]) == false)
                            {
                                TablaGrid.AddNewRow();

                                int newRowHandle = DevExpress.Xpf.Grid.DataControlBase.NewItemRowHandle;

                                GridMain.SetCellValue(newRowHandle, "ArticuloID", ArticuloID);
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
            */
        }

        private void txtArticuloID_KeyDown(object sender, KeyEventArgs e)
        {
            ClassControl.ValidadorNumeros(e);
        }
    }
}
