using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para UscClaseClientes.xaml
    /// </summary>
    public partial class UscClaseClientes : UserControl
    {
        public UscClaseClientes()
        {
            InitializeComponent();
            Name = "UscClaseClientes";
        }

        SqlDataReader reader;
        string Estado;
        string Lista;
        int ClaseID;
        int LastClaseID;


        int UsuarioID;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //SetEnabledButton("Modo Consulta");
            //BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(BtnUltimoRegistro.Click));

            this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            //BtnUltimoRegistro.Click += new EventHandler(BtnUltimoRegistro);

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
            ClaseID = Convert.ToInt32(txtClaseID.Text) - 1;
            if (ClaseID <= 1)
            {
                BtnPrimerRegistro.IsEnabled = false;
                BtnAnteriorRegistro.IsEnabled = false;
                setDatos(0, "1");
            }
            else
            {
                setDatos(0, ClaseID.ToString());
            }
        }

        private void BtnProximoRegistro_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledButton("Modo Consulta");

            ClaseID = Convert.ToInt32(txtClaseID.Text) + 1;
            if (ClaseID >= LastClaseID)
            {
                BtnUltimoRegistro.IsEnabled = false;
                BtnProximoRegistro.IsEnabled = false;
                setDatos(0, LastClaseID.ToString());
            }
            else
            {
                setDatos(0, ClaseID.ToString());
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
            SetControls(false, null, false);
            SetEnabledButton("Modo Busqueda");
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {

            SetEnabledButton("Modo Agregar");
            //SetEnabledButton("Modo Agregar");

            //MessageBox.Show(BtnAgregar.Content.ToString());

            //List<Control> listaControl = new List<Control>()
            //{
            //    txtCelular,
            //    BtnAgregar,
            //    BtnAnteriorRegistro,
            //    BtnBuscar,
            //    BtnProximoRegistro,
            //    BtnUltimoRegistro,
            //    BtnAnteriorRegistro,
            //    BtnImprimir
            //};



            //List<Control> lista = new List<Control>()
            //{
            //    this
            //};

            //clas.ActivadorControles(listaControl, false);
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
                lAviso.Text = Lista;
                MiniDialogo.IsOpen = true;
                //MessageBox.Show(Lista); 
            }
            else
            {
                if (Estado == "Modo Editar")
                {
                    setDatos(2, null);
                }
                else
                {
                    setDatos(1, null);
                }

                //SetEnabledButton("Modo Consulta");
                this.BtnUltimoRegistro.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            }
        }

        private void txtClaseID_KeyUp(object sender, KeyEventArgs e)
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



        void setDatos(int Flag, string Cliente)
        {

            if (Cliente == null)
            {
                ClaseID = Convert.ToInt32(txtClaseID.Text);
            }
            else
            {
                ClaseID = Convert.ToInt32(Cliente);
            }

            List<SqlParameter> listSqlParameter = new List<SqlParameter>()
            {
                new SqlParameter("Flag",Flag),
                new SqlParameter("@ClaseID",ClaseID),
                new SqlParameter("@Nombre",txtNombre.Text),
                //new SqlParameter("@UsuarioID",UsuarioID)
            };
            reader = Clases.ClassData.runSqlDataReader("sp_cliClaseClientes", listSqlParameter, "StoredProcedure");



            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    txtClaseID.Text = reader["ClaseID"].ToString();
                    txtNombre.Text = reader["Nombre"].ToString();
                    if (Flag == -1)
                    {
                        LastClaseID = Convert.ToInt32(txtClaseID.Text);
                    }
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

        void SetControls(bool Habilitador, string Modo, bool Editando)
        {
            List<Control> listaControl = new List<Control>()
            {
                txtClaseID,txtNombre
            };

            if (Modo == null)
            {
                Clases.ClassControl.ActivadorControlesReadonly(listaControl, Habilitador, Editando);
            }
            else if (Modo == "Validador")
            {
                Lista = Clases.ClassControl.ValidadorControles(listaControl);
            }
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

                    SetControls(true, null, false);

                    if (Estado == "Modo Busqueda")
                    {
                        BtnEditar.IsEnabled = false;
                        BtnImprimir.IsEnabled = false;
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
                    }
                    else
                    {
                        SetControls(true, null, true);
                    }
                }
            }
            //if (Estado == "Modo Consulta")
            //{
            //    BtnPrimerRegistro.IsEnabled = true;
            //    BtnAnteriorRegistro.IsEnabled = true;
            //    BtnProximoRegistro.IsEnabled = true;
            //    BtnUltimoRegistro.IsEnabled = true;

            //    BtnBuscar.IsEnabled = true;
            //    BtnImprimir.IsEnabled = true;

            //    BtnAgregar.IsEnabled = true;
            //    BtnEditar.IsEnabled = true;

            //    BtnCancelar.IsEnabled = false;
            //    BtnGuardar.IsEnabled = false;
            //}
            //else if (Estado == "Modo Consulta Ultimo Registro")
            //{
            //    BtnPrimerRegistro.IsEnabled = true;
            //    BtnAnteriorRegistro.IsEnabled = true;
            //    BtnProximoRegistro.IsEnabled = true;
            //    BtnUltimoRegistro.IsEnabled = false;

            //    BtnBuscar.IsEnabled = true;
            //    BtnImprimir.IsEnabled = true;

            //    BtnAgregar.IsEnabled = true;
            //    BtnEditar.IsEnabled = true;

            //    BtnCancelar.IsEnabled = false;
            //    BtnGuardar.IsEnabled = false;


            //}
            //else if (Estado == "Modo Consulta Primer Registro")
            //{
            //    BtnPrimerRegistro.IsEnabled = false;
            //    BtnAnteriorRegistro.IsEnabled = true;
            //    BtnProximoRegistro.IsEnabled = true;
            //    BtnUltimoRegistro.IsEnabled = true;

            //    BtnBuscar.IsEnabled = true;
            //    BtnImprimir.IsEnabled = true;

            //    BtnAgregar.IsEnabled = true;
            //    BtnEditar.IsEnabled = true;

            //    BtnCancelar.IsEnabled = false;
            //    BtnGuardar.IsEnabled = false;
            //}
            //else if (Estado == "Modo Agregar")
            //{
            //    BtnPrimerRegistro.IsEnabled = false;
            //    BtnAnteriorRegistro.IsEnabled = false;
            //    BtnProximoRegistro.IsEnabled = false;
            //    BtnUltimoRegistro.IsEnabled = false;

            //    BtnBuscar.IsEnabled = false;
            //    BtnImprimir.IsEnabled = false;

            //    BtnAgregar.IsEnabled = false;
            //    BtnEditar.IsEnabled = false;

            //    BtnCancelar.IsEnabled = true;
            //    BtnGuardar.IsEnabled = true;
            //}

        }


    }
}
