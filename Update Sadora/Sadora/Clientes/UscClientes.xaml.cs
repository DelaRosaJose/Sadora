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

namespace Sadora.Clientes
{
    /// <summary>
    /// Lógica de interacción para UscClientes.xaml
    /// </summary>
    public partial class UscClientes : UserControl
    {
        public UscClientes()
        {
            InitializeComponent();
            Name = "UscClientes";
        }

        public bool Inicializador = false;
        DataTable tabla;
        SqlDataReader reader;
        string Estado;
        string Lista;
        int ClienteID;
        int LastClienteID;

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            //if (this.IsLoaded)
            //{
            //    this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            //}
            Inicializador = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Inicializador == true)
            {
                this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Inicializador = false;
            }

        }

        private void BtnPrimerRegistro_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledButton("Modo Consulta");
            setDatos(0, "1");
            BtnPrimerRegistro.IsEnabled = false;
            BtnAnteriorRegistro.IsEnabled = false;
        }

        private void BtnAnteriorRegistro_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledButton("Modo Consulta");
            try
            {
                ClienteID = Convert.ToInt32(txtClienteID.Text) - 1;
            }
            catch (Exception exception)
            {
                ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString();
            }


            if (ClienteID <= 1)
            {
                BtnPrimerRegistro.IsEnabled = false;
                BtnAnteriorRegistro.IsEnabled = false;
                setDatos(0, "1");
            }
            else
            {
                setDatos(0, ClienteID.ToString());
            }
        }

        private void BtnProximoRegistro_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledButton("Modo Consulta");
            try
            {
                ClienteID = Convert.ToInt32(txtClienteID.Text) + 1;
            }
            catch (Exception exception)
            {
                ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString();
            }

            if (ClienteID >= LastClienteID)
            {
                BtnUltimoRegistro.IsEnabled = false;
                BtnProximoRegistro.IsEnabled = false;
                setDatos(0, LastClienteID.ToString());
            }
            else
            {
                setDatos(0, ClienteID.ToString());
            }
        }

        private void BtnUltimoRegistro_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledButton("Modo Consulta");
            setDatos(-1, "1");
            BtnUltimoRegistro.IsEnabled = false;
            BtnProximoRegistro.IsEnabled = false;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {

            if (Estado == "Modo Consulta")
            {
                //SetControls(false, null, false);
                SetEnabledButton("Modo Busqueda");
            }
            else if (Estado == "Modo Busqueda")
            {
                setDatos(0, null);

                if (tabla.Rows.Count > 1)
                {
                    Administracion.FrmMostrarDatosHost frm = new Administracion.FrmMostrarDatosHost(null, tabla);
                    frm.ShowDialog();

                    if (frm.GridMuestra.SelectedItem != null)
                    {
                        DataRowView item = (frm.GridMuestra as DataGrid).SelectedItem as DataRowView;
                        txtClienteID.Text = item.Row.ItemArray[0].ToString();
                        setDatos(0, txtClienteID.Text);
                    }
                }

                //setDatos(0, txtClienteID.Text);
                SetEnabledButton("Modo Consulta");
            }
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            SetEnabledButton("Modo Agregar");
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
            SqlDataReader tabla = ClassControl.getDatosCedula(txtRNC.Text);
            if (tabla != null)
            {
                tabla.Close();
                tabla.Dispose();
                if (Estado == "Modo Editar")
                {
                    setDatos(2, null);
                }
                else
                {
                    setDatos(1, null);
                }
                this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void btnClaseID_Click(object sender, RoutedEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                Administracion.FrmMostrarDatosHost frm = new Administracion.FrmMostrarDatosHost("Select * from TcliClaseClientes", null);
                frm.ShowDialog();

                if (frm.GridMuestra.SelectedItem != null)
                {
                    DataRowView item = (frm.GridMuestra as DataGrid).SelectedItem as DataRowView;
                    txtClaseID.Text = item.Row.ItemArray[0].ToString();

                    setValidador("select * from TcliClaseClientes where ClaseID =", txtClaseID, tbxClaseID);
                }

            }
        }

        private void txtRNC_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    reader = ClassControl.getDatosCedula(txtRNC.Text);
                    if (reader != null)
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                txtNombre.Text = reader["Nombres"].ToString() + " " + reader["Apellidos"].ToString();
                                txtDireccion.Text = reader["Direccion"].ToString();
                                txtTelefono.Text = reader["Telefono"].ToString();
                                reader.NextResult();

                            }
                            reader.Close();
                            reader.Dispose();
                        }
                        else
                        {
                            reader.Close();
                            reader.Dispose();
                        }
                    }
                    ((TextBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtClienteID_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((TextBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((TextBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtRepresentante_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((TextBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtClaseID_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    if (txtClaseID.Text != "")
                    {
                        setValidador("select Nombre from TcliClaseClientes where ClaseID =", txtClaseID, tbxClaseID);
                    }
                    else
                    {
                        txtClaseID.Text = 0.ToString();
                        setValidador("select Nombre from TcliClaseClientes where ClaseID =", txtClaseID, tbxClaseID);
                    }
                    ((TextBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtClaseID_KeyDown(object sender, KeyEventArgs e)
        {
            ClassControl.ValidadorNumeros(e);
        }

        private void txtDireccion_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((TextBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtCorreoElectronico_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((TextBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtTelefono_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((TextBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void txtCelular_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((TextBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        private void cActivar_KeyUp(object sender, KeyEventArgs e)
        {
            if (Estado != "Modo Consulta")
            {
                if (e.Key == Key.Enter)
                {
                    ((CheckBox)sender).MoveFocus(new TraversalRequest(new FocusNavigationDirection()));
                }
            }
        }

        void setDatos(int Flag, string Cliente)
        {
            if (Cliente == null)
            {
                if (txtClienteID.Text == "")
                {
                    ClienteID = 0;
                }
                else
                {
                    try
                    {
                        ClienteID = Convert.ToInt32(txtClienteID.Text);
                    }
                    catch (Exception exception)
                    {
                        ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString();
                    }
                }
            }
            else
            {
                ClienteID = Convert.ToInt32(Cliente);
            }

            List<SqlParameter> listSqlParameter = new List<SqlParameter>()
            {
                new SqlParameter("Flag",Flag),
                new SqlParameter("@ClienteID",ClienteID),
                new SqlParameter("@RNC",txtRNC.Text),
                new SqlParameter("@Nombre",txtNombre.Text),
                new SqlParameter("@Representante",txtRepresentante.Text),
                new SqlParameter("@ClaseID",txtClaseID.Text),
                new SqlParameter("@Direccion",txtDireccion.Text),
                new SqlParameter("@CorreoElectronico",txtCorreoElectronico.Text),
                new SqlParameter("@Telefono",txtTelefono.Text),
                new SqlParameter("@Celular",txtCelular.Text),
                new SqlParameter("@Activo",Convert.ToInt32(cActivar.IsChecked)),
                new SqlParameter("@UsuarioID",ClassVariables.UsuarioID)
            };

            tabla = Clases.ClassData.runDataTable("sp_cliclientes", listSqlParameter, "StoredProcedure");

            if (ClassVariables.GetSetError != null)
            {
                Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost(ClassVariables.GetSetError);
                frm.ShowDialog();
                ClassVariables.GetSetError = null;
            }

            if (tabla.Rows.Count == 1)
            {
                txtClienteID.Text = tabla.Rows[0]["ClienteID"].ToString();
                txtRNC.Text = tabla.Rows[0]["RNC"].ToString();
                txtNombre.Text = tabla.Rows[0]["Nombre"].ToString();
                txtRepresentante.Text = tabla.Rows[0]["Representante"].ToString();
                txtClaseID.Text = tabla.Rows[0]["ClaseID"].ToString();
                txtDireccion.Text = tabla.Rows[0]["Direccion"].ToString();
                txtCorreoElectronico.Text = tabla.Rows[0]["CorreoElectronico"].ToString();
                txtTelefono.Text = tabla.Rows[0]["Telefono"].ToString();
                txtCelular.Text = tabla.Rows[0]["Celular"].ToString();
                cActivar.IsChecked = Convert.ToBoolean(Convert.ToInt32(tabla.Rows[0]["Activo"].ToString()));
                //reader.NextResult();
                if (Flag == -1)
                {
                    try
                    {
                        LastClienteID = Convert.ToInt32(txtClienteID.Text);
                    }
                    catch (Exception exception)
                    {
                        ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString();
                    }
                }
                setValidador("select * from TcliClaseClientes where ClaseID =", txtClaseID, tbxClaseID);
            }
            //else if (tabla.Rows.Count >= 2)
            //{
            //    //MessageBox.Show("trajo datos" + tabla.Rows.Count);
            //}
            listSqlParameter.Clear();
        }

        void SetControls(bool Habilitador, string Modo, bool Editando)
        {
            List<Control> listaControl = new List<Control>()
            {
                txtClienteID,txtRNC,txtNombre,txtRepresentante,txtClaseID,tbxClaseID,txtDireccion,txtCorreoElectronico,txtTelefono,txtCelular
            };

            List<Control> listaControles = new List<Control>()
            {
                txtRepresentante,txtClaseID,tbxClaseID,txtDireccion,txtCorreoElectronico,txtTelefono,txtCelular,cActivar
            };

            if (Modo == null)
            {
                if (Estado == "Modo Busqueda")
                {
                    Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando, false, listaControles);
                }
                else if (Estado == "Modo Agregar")
                {
                    Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando, true, null);
                }
                else
                {
                    Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando, false, null);
                }
            }
            else if (Modo == "Validador")
            {
                Lista = Clases.ClassControl.ValidadorControles(listaControl);
            }
            listaControl.Clear();
            listaControles.Clear();
        }

        void SetEnabledButton(String status)
        {
            Estado = status;

            if (Estado != "Modo Agregar" && Estado != "Modo Editar")
            {
                if (Estado == "Modo Consulta" || Estado == "Modo Busqueda")
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
                    if (Estado == "Modo Consulta")
                    {
                        SetControls(true, null, false);
                    }
                    else// if (Estado == "Modo Busqueda")
                    {
                        BtnProximoRegistro.IsEnabled = false;
                        BtnAnteriorRegistro.IsEnabled = false;
                        BtnImprimir.IsEnabled = false;
                        BtnEditar.IsEnabled = false;
                        SetControls(false, null, false);
                    }
                }
            }
            else
            {
                if (Estado == "Modo Agregar" || Estado == "Modo Editar")
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
                    if (Estado == "Modo Agregar")
                    {
                        SetControls(false, null, false);
                        txtClienteID.IsReadOnly = true;
                        txtClienteID.Text = (LastClienteID + 1).ToString();
                        txtRNC.Focus();
                    }
                    else
                    {
                        SetControls(true, null, true);
                    }
                }
            }

        }

        void setValidador(string Consulta, TextBox Enviador, TextBox Recibidor)
        {

            reader = Clases.ClassData.runSqlDataReader(Consulta + Enviador.Text, null, "CommandText");

            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    //txtClienteID.Text = reader["ClaseID"].ToString();
                    Recibidor.Text = reader["Nombre"].ToString();
                    reader.NextResult();
                }
                reader.Close();
                reader.Dispose();
            }
            else
            {
                Recibidor.Clear();
                reader.Close();
                reader.Dispose();
            }

        }

    }
}
