using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Windows.Input;

namespace Sadora.Clases
{
    class ClassControl
    {

        public static void SetFormularios()
        {

            List<Clases.ClassVariables> listOfUsers = new List<Clases.ClassVariables>()
            {
                new Clases.ClassVariables() { Formulario = new Clientes.UscClientes().Name, Modulo = "Clientes", Titulo = "Maestra de Clientes" },
                new Clases.ClassVariables() { Formulario = new Clientes.UscClaseClientes().Name, Modulo = "Clientes", Titulo = "Clases de Clientes" },
                new Clases.ClassVariables() { Formulario = "UscCuentasxCobrar", Modulo = "Clientes", Titulo = "Cuentas por Cobrar" },
                new Clases.ClassVariables() { Formulario = "UscCuentasxCobrar", Modulo = "Clientes", Titulo = "soraya" },
            };

            Clases.ClassData.runDataTableSql("sp_sysFormularios", listOfUsers, "DTItemns");
        }

        public static void ActivadorControlesReadonly(List<Control> listaControl, bool funcion, bool editando,bool Agregando = false, List<Control> listaControles = null)
        {
            foreach (TextBox control in listaControl)
            {
                if (editando == true)
                {

                    control.IsReadOnly = !funcion;
                    control.IsEnabled = funcion;
                }
                else if (Agregando == true)
                {
                    control.IsReadOnly = funcion;
                    control.IsEnabled = !funcion;
                }
                else
                {
                    control.IsReadOnly = funcion;
                    control.IsEnabled = funcion;
                }
                if (funcion == false)
                {
                    control.Clear();
                }
                if (listaControles != null)
                {
                    control.IsEnabled = !funcion;
                }
            }
            if (listaControles != null)
            {
                foreach (Control control in listaControles)
                {
                    //control.IsReadOnly = !funcion;
                    control.IsEnabled = funcion;
                }
            }
        }

        public static string ValidadorControles(List<Control> listaControl)
        {
            //List<string> Controles = new List<string>();
            string Linea = "Debe Completar los Campos: ";
            int contador = 0;

            foreach (TextBox control in listaControl)
            {
                if (control.Text == "")
                {
                    if (contador == 0)
                    {
                        control.Focus(); 
                    }
                    contador++;
                    //Array.Resize(ref Controles, Controles.Length + 1);
                    //data[Controles.Length - 1] = Value;
                    if (Linea == "Debe Completar los Campos: ")
                    {
                        Linea = Linea + control.Name;
                    }
                    else
                    {
                        Linea = Linea + ", " + control.Name;
                    }
                    Linea = Linea.Replace("txt", "").Replace("tbx","");
                    //Controles.Add(control.Name);

                    //control.IsReadOnly = funcion;

                }
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

                //MessageBox.Show(identityCardValid.ToString());
                //cedula = identityCardValid;
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
                if (ClassControl.ValidateIdentityCard(Cedula) == true)
                {
                    tabla = Clases.ClassData.runSqlDataReader("exec spCedula '" + Cedula + "'", null, "CommandText");
                }
                else
                {
                    Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost("RNC invalido");
                    frm.ShowDialog();
                }
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




    }
}
