﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Windows.Input;

namespace Sadora.Clases
{
    class ClassControl
    {

        public static void setValidador(string Consulta, TextBox Enviador, TextBox Recibidor) //Este metodo se encarga de validar cualquier campo de la ventana que este llamando otro mantenimiento, Ejemplo(Campo clase de clientes contiene(ClaseID, Detalle de la clase que es el nombre)), 
        {                                                                       // el metodo recibe el textbox que le envia la info, la consulta que debe buscar con esa info y el textbox donde debe depositar el resultado.


            SqlDataReader reader = Clases.ClassData.runSqlDataReader(Consulta + Enviador.Text, null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.

            if (reader.HasRows) //Validamos si el datareader trajo data.
            {
                if (reader.Read()) //Si puede leer la informacion
                {
                    Recibidor.Text = reader["Nombre"].ToString(); //Asignamos el resultado de la columna "Nombre" del datareader en el textbox que le indicamos en el parametro previamente identificado.
                    reader.NextResult();
                }
                reader.Close(); //Cerramos el datareader
                reader.Dispose(); //Cortamos la conexion del datareader
            }
            else //Si no trajo data
            {
                Recibidor.Clear(); //Dejamos en blanco el campo que recibe
                reader.Close(); //limpiamos el reader
                reader.Dispose();
            }

        }


        public static void SetFormularios()
        {

            List<Clases.ClassVariables> listOfUsers = new List<Clases.ClassVariables>()
            {
                new Clases.ClassVariables() { Formulario = new Clientes.UscClientes().Name, Modulo = "Clientes", Titulo = "Maestra de Clientes" },
                new Clases.ClassVariables() { Formulario = new Clientes.UscClaseClientes().Name, Modulo = "Clientes", Titulo = "Clases de Clientes" },
                new Clases.ClassVariables() { Formulario = new Administracion.UscUsuarios().Name, Modulo = "Administracion", Titulo = "Configuracion de usuarios" },
                new Clases.ClassVariables() { Formulario = new Recursos_Humanos.UscEmpleados().Name, Modulo = "Recursos Humanos", Titulo = "Maestra de empleados" },
                new Clases.ClassVariables() { Formulario = new Administracion.UscGruposUsuarios().Name, Modulo = "Administracion", Titulo = "Configuracion de grupos de usuarios" },
                //new Clases.ClassVariables() { Formulario = "UscCuentasxCobrar", Modulo = "Clientes", Titulo = "Cuentas por Cobrar" },
                //new Clases.ClassVariables() { Formulario = "UscCuentasxCobrar", Modulo = "Clientes", Titulo = "soraya" },
            };

            Clases.ClassData.runDataTableSql("sp_sysFormularios", listOfUsers, "DTItemns");
        }

        public static void ClearControl(List<Control> listaControles)
        {
            if (listaControles != null)
            {
                foreach (Control control in listaControles)
                {
                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }
                    else if (control is PasswordBox)
                    {
                        (control as PasswordBox).Clear();  //ClearValue();
                    }
                    else if (control is DatePicker)
                    {
                        (control as DatePicker).Text = "";  //ClearValue();
                    }
                }
            }
            listaControles.Clear();
        }

        public static void ActivadorControlesReadonly(List<Control> listaControl, bool funcion, bool editando, bool Agregando = false, List<Control> listaControles = null)
        {
            if (listaControl != null)
            {
                foreach (Control control in listaControl)
                {
                    if (editando == true)
                    {
                        control.IsEnabled = funcion;

                        if (control is TextBox)
                        {
                            (control as TextBox).IsReadOnly = !funcion;
                        }
                        else if (control is PasswordBox)
                        {
                            (control as PasswordBox).IsEnabled = funcion;
                        }
                        else if (control is ComboBox)
                        {
                            (control as ComboBox).IsEnabled = funcion;
                        }
                        else if (control is CheckBox)
                        {
                            (control as CheckBox).IsEnabled = funcion;
                        }
                        else if (control is DatePicker)
                        {
                            (control as DatePicker).IsEnabled = funcion;
                        }
                    }
                    else if (Agregando == true)
                    {
                        control.IsEnabled = !funcion;

                        if (control is TextBox)
                        {
                            (control as TextBox).IsReadOnly = funcion;
                        }
                        else if (control is PasswordBox)
                        {
                            (control as PasswordBox).IsEnabled = !funcion;
                        }
                        else if (control is ComboBox)
                        {
                            (control as ComboBox).IsEnabled = !funcion;
                        }
                        else if (control is CheckBox)
                        {
                            (control as CheckBox).IsEnabled = !funcion;
                        }
                        else if (control is DatePicker)
                        {
                            (control as DatePicker).IsEnabled = !funcion;
                        }
                    }
                    else
                    {
                        control.IsEnabled = funcion;

                        if (control is TextBox)
                        {
                            (control as TextBox).IsReadOnly = funcion;
                        }
                        else if (control is PasswordBox)
                        {
                            (control as PasswordBox).IsEnabled = !funcion;
                        }
                        else if (control is ComboBox)
                        {
                            (control as ComboBox).IsEnabled = !funcion;
                        }
                        else if (control is CheckBox)
                        {
                            (control as CheckBox).IsEnabled = !funcion;
                        }
                        else if (control is DatePicker)
                        {
                            (control as DatePicker).IsEnabled = !funcion;
                        }
                    }
                    if (funcion == false)
                    {
                        if (control is TextBox)
                        {
                            (control as TextBox).Clear();
                        }
                        else if (control is PasswordBox)
                        {
                            (control as PasswordBox).Clear();  //ClearValue();
                        }
                        else if (control is DatePicker)
                        {
                            (control as DatePicker).Text = "";  //ClearValue();
                        }
                    }
                    if (listaControles != null)
                    {
                        control.IsEnabled = !funcion;
                    }
                }
                listaControl.Clear();
            }
            if (listaControles != null)
            {
                foreach (Control control in listaControles)
                {
                    control.IsEnabled = funcion;

                    if (control is TextBox)
                    {
                        (control as TextBox).Clear();
                    }
                    else if (control is PasswordBox)
                    {
                        (control as PasswordBox).Clear();  //ClearValue();
                    }
                    else if (control is DatePicker)
                    {
                        (control as DatePicker).Text = "";  //ClearValue();
                    }
                }
                listaControles.Clear();
            }
            
        }

        public static string ValidadorControles(List<Control> listaControl)
        {
            string Linea = "Debe Completar los Campos: ";
            int contador = 0;

            foreach (Control control in listaControl)
            {

                if (control is TextBox)
                {
                    if ((control as TextBox).Text == "")
                    {
                        if (contador == 0)
                        {
                            control.Focus();
                        }

                        contador++;

                        if (Linea == "Debe Completar los Campos: ")
                        {
                            Linea = Linea + control.Name;
                        }
                        else
                        {
                            Linea = Linea + ", " + control.Name;
                        }
                        Linea = Linea.Replace("txt", "").Replace("tbx", "");
                    }
                        //(control as TextBox).IsReadOnly = !funcion;
                }
                else if (control is DatePicker)
                {
                    if ((control as DatePicker).Text == "")
                    {
                        if (contador == 0)
                        {
                            control.Focus();
                        }

                        contador++;

                        if (Linea == "Debe Completar los Campos: ")
                        {
                            Linea = Linea + control.Name;
                        }
                        else
                        {
                            Linea = Linea + ", " + control.Name;
                        }
                        Linea = Linea.Replace("txt", "").Replace("tbx", "").Replace("dtp", "");
                    }
                        //(control as DatePicker).Text = funcion;
                }

                //if (control.Text == "")
                //{
                    //if (contador == 0)
                    //{
                    //    control.Focus();
                    //}

                    //contador++;

                    //if (Linea == "Debe Completar los Campos: ")
                    //{
                    //    Linea = Linea + control.Name;
                    //}
                    //else
                    //{
                    //    Linea = Linea + ", " + control.Name;
                    //}
                    //Linea = Linea.Replace("txt", "").Replace("tbx", "");
                //}
            }
            return Linea;
        }

        public static bool ValidateIdentityCard(string identityCard)
        {
            string replaceScore = identityCard.Replace("-", "");

            if (Regex.IsMatch(replaceScore, "^[0-9]{11}$"))
            {

                string identityCardSub = replaceScore.Substring(0, replaceScore.Length - 1);
                string checkerNumber = replaceScore.Substring(replaceScore.Length - 1, 1);
                int sum = 0;
                bool identityCardValid = false;

                for (int i = 0; i < identityCardSub.Length; i++)
                {
                    int module = 0;

                    if ((i % 2) == 0)
                        module = 1;
                    else
                        module = 2;

                    int result = int.Parse(identityCardSub.Substring(i, 1)) * module;
                    if (result > 9)
                    {
                        string resultString = result.ToString();
                        string one = resultString.Substring(0, 1);
                        string two = resultString.Substring(1, 1);
                        result = int.Parse(one) + int.Parse(two);

                    }

                    sum += result;
                }

                int numberValidate = (10 - (sum % 10)) % 10;

                if ((numberValidate == int.Parse(checkerNumber)) && (identityCardSub.Substring(0, 3) != "000"))
                {
                    identityCardValid = true;
                }
                else
                {
                    identityCardValid = false;
                }

                return identityCardValid;

            }
            else
            {
                return false;
            }


        }

        public static SqlDataReader getDatosCedula(string Cedula)
        {
            SqlDataReader tabla = null;
            if (Cedula.Length > 13)
            {
                Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost("No puede ingresar mas de 13 digitos en el RNC");
                frm.ShowDialog();
            }
            else
            {
                //if (ClassControl.ValidateIdentityCard(Cedula) == true)
                //{
                    tabla = Clases.ClassData.runSqlDataReader("exec spCedula '" + Cedula + "'", null, "CommandText");
                //}
                //else
                //{
                //    Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost("RNC invalido");
                //    frm.ShowDialog();
                //}
            }
            return tabla;


        }

        public static void ValidadorNumeros(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        public static void GridAllowEdit(DevExpress.Xpf.Grid.GridControl Grid, List<String> ListaColumnas, Boolean AllowEdit, string opcion = "--AllowEdit-- o --Visible--") 
        {
            DevExpress.Utils.DefaultBoolean DevExbool;

            if (AllowEdit)
            {
                DevExbool = DevExpress.Utils.DefaultBoolean.True;
            }
            else
            {
                DevExbool = DevExpress.Utils.DefaultBoolean.False;
            }

            foreach (String Valor in ListaColumnas)
            {
                Grid.Columns[Valor].AllowEditing = DevExbool;

                //Grid.Columns[Valor].Visible    = false;
            }
        }

        public static void GridCheckEdit(DevExpress.Xpf.Grid.GridControl Grid, string Columna, Boolean Opcion)
        {
            Grid.View.MoveFirstRow();
            for (int i = 0; i < Grid.VisibleRowCount; i++)
            {
                Grid.SetFocusedRowCellValue(Columna, Opcion);
                Grid.View.MoveNextRow();
            }
        }


    }
}