using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sadora.Clases
{
    public class ClassVariables : INotifyPropertyChanged
    {
        public static string GetSetError;

        public static bool ValidarAccion;
        //public static int IntentoEntrada;
        //public static string EmpleadoID;
        public static int UsuarioID;
        public static string UsuarioNombre;
        private static bool existclient = false;

        //public static string UsuarioNombre;
        //public static string TipoCuenta;
        public static bool Imprime;
        public static bool Agrega;
        public static bool Modifica;
        public static bool Anula;

        public string Nombre { get; set; }
        public string Formulario { get; set; }
        public string Modulo { get; set; }
        public string Titulo { get; set; }

        #region FormaPagoProperty
        public string IdFormaPago { get; set; }
        public string FormaPago { get; set; }
        public double CantidadFormaPago { get; set; }
        #endregion
        
        public static bool ExistClient
        {
            get { return existclient; }
            set { existclient = value; }
        }

        public static bool IsFullFormaPago;

        private string cliente;
        private string rnc;
        private string ncf;
        private string Clasencf;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); }

        public  string ClienteDinamic { get{return cliente;} set{cliente = value; OnPropertyChanged();} }
        public string RNCDinamic { get{return rnc;} set{rnc = value; OnPropertyChanged();} }
        public string NCFDinamic { get{return ncf;} set{ncf = value; OnPropertyChanged();} }
        public string ClaseNCFDinamic { get { return Clasencf; } set { Clasencf = value; OnPropertyChanged(); } }

        public static List<Clases.ClassVariables> ListFormasPagos = new List<Clases.ClassVariables>();

        public string UserID { get; set; }
        public int CountIntent { get; set; }

    }
}
