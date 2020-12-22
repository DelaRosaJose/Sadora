using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para FrmSplash.xaml
    /// </summary>
    public partial class FrmSplash : Window
    {
        FrmLogin login = new FrmLogin();
        bool Estado = true;

        public FrmSplash()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            login.Show();
            this.Hide();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();

        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(30);
            }

        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (Estado == true)
            {
                pbStatus.Value = e.ProgressPercentage;
            }


            if (pbStatus.Value == 10)
            {

                Clases.ClassControl.SetFormularios();

                if (Clases.ClassVariables.GetSetError != null)
                {
                    Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost(Clases.ClassVariables.GetSetError);

                    Estado = false;
                    pbStatus.Value = 11;

                    //MessageBox.Show(Clientes.UscClientes..ToString());

                    frm.ShowDialog();

                    Application.Current.Shutdown();
                }

                //try
                //{
                //    Clases.ClassData.sqlConnection.Open();
                //}
                //catch (Exception exception)
                //{

                //    Clases.ClassVariables.GetSetError = "Ha ocurrido un error: " + exception.ToString();

                //    Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost(Clases.ClassVariables.GetSetError);

                //    Estado = false;
                //    pbStatus.Value = 11;

                //    //MessageBox.Show(Clientes.UscClientes..ToString());

                //    frm.ShowDialog();

                //    Application.Current.Shutdown();

                //    //System.Diagnostics.Process.GetCurrentProcess().Kill();
                //}
            }
            else if (pbStatus.Value == 100)
            {
                login.Show();
                this.Close();


                //MessageBox.Show(Clientes.UscClientes.NameProperty.ToString());
            }
        }


    }
}
