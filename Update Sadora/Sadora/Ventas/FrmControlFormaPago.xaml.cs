using Sadora.Clases;
using Sadora.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Lógica de interacción para FrmControlFormaPago.xaml
    /// </summary>
    public partial class FrmControlFormaPago : INotifyPropertyChanged
    {
        //public bool Resultado;
        private int ClienteID;
        private int ClaseComprobanteId;
        private bool NotData = false;
        private string FormaPago;
        private string montorestante;


        public event PropertyChangedEventHandler PropertyChanged;

        public string MontoRestante
        {
            get { return FormaPago; }
            set
            {
                FormaPago = value;
                OnPropertyChanged();
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        #region Variables para Eventos
        bool PassEfect = false;
        string Efect = "";
        string Tarj = "";
        string Trans = "";
        string Ck = "";
        //string FormaPago = "";
        #endregion

        public FrmControlFormaPago()
        { }

        public FrmControlFormaPago(string formaPago, double MontoPagar)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            FormaPago = formaPago;
            MiniDialogo.IsOpen = true;
            txtMontoDistribuido.Focus();
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ControlEvent();
        }

        //private void ControlEvent()
        //{
        //    //var CajaConfigurada = (int)Settings.Default["Caja"];

        //    //if (CajaConfigurada == 0)
        //    //{
        //    //    if (SnackbarThree.MessageQueue is { } messageQueue)
        //    //        Task.Factory.StartNew(() => messageQueue.Enqueue("No hay caja configurada"));
        //    //}
        //    //else
        //    //{
        //    //    string CreateNameButton = "";

        //    //    DataTable MetodoCaja = Clases.ClassData.runDataTable("select a.Nombre from TvenMetodoPagos a inner join TvenCajasDetalle b on a.MetodoID = b.MetodoID where b.CajaID = " + CajaConfigurada + " and Alta = 1", null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.
        //    //    for (int i = 0; i < MetodoCaja.Rows.Count; i++)
        //    //    {
        //    //        CreateNameButton = MetodoCaja.Rows[i]["Nombre"].ToString();

        //    //        switch (CreateNameButton)
        //    //        {
        //    //            case string a when a.ToUpper().Contains("EFECTIVO"):
        //    //                #region Create TabItem
        //    //                TabItem myTabItemEfect = new TabItem()
        //    //                {
        //    //                    Header = CreateNameButton,
        //    //                    FontSize = 25
        //    //                };
        //    //                #endregion
        //    //                #region Create Campo

        //    //                Border myBorderEfect = new Border()
        //    //                {
        //    //                    Margin = new Thickness(5, 10, 15, 10),
        //    //                    BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
        //    //                    BorderThickness = new Thickness(2, 0, 0, 2),
        //    //                    CornerRadius = new CornerRadius(5)
        //    //                };

        //    //                StackPanel MyStackEfect = new StackPanel() { Orientation = Orientation.Vertical };

        //    //                TextBlock MyTextEfect = new TextBlock()
        //    //                {
        //    //                    Margin = new Thickness(10, 5, 0, 0),
        //    //                    Text = "Monto a pagar",//CreateNameButton,
        //    //                    FontWeight = FontWeights.Bold
        //    //                };

        //    //                TextBox MyTextBoxEfect = new TextBox()
        //    //                {
        //    //                    Height = 30,
        //    //                    Padding = new Thickness(0),
        //    //                    BorderThickness = new Thickness(1),
        //    //                    Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#4CEDEDED"),
        //    //                    BorderBrush = null,
        //    //                    Style = Application.Current.FindResource("MaterialDesignTextBoxBase") as Style
        //    //                };

        //    //                MyStackEfect.Children.Add(MyTextEfect);
        //    //                MyStackEfect.Children.Add(MyTextBoxEfect);

        //    //                myBorderEfect.Child = MyStackEfect;
        //    //                myTabItemEfect.Content = myBorderEfect;

        //    //                TabMainControl.Items.Add(myTabItemEfect);
        //    //                #endregion
        //    //                break;
        //    //            case string a when a.ToUpper().Contains("TARJETA"):
        //    //                #region Create TabItem
        //    //                TabItem myTabItemTarj = new TabItem()
        //    //                {
        //    //                    Header = CreateNameButton,
        //    //                    FontSize = 25
        //    //                };
        //    //                #endregion
        //    //                #region Create Campo

        //    //                Border myBorderTarj = new Border()
        //    //                {
        //    //                    Margin = new Thickness(5, 10, 15, 10),
        //    //                    BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
        //    //                    BorderThickness = new Thickness(2, 0, 0, 2),
        //    //                    CornerRadius = new CornerRadius(5)
        //    //                };

        //    //                StackPanel MyStackTarj = new StackPanel() { Orientation = Orientation.Vertical };

        //    //                TextBlock MyTextTarj = new TextBlock()
        //    //                {
        //    //                    Margin = new Thickness(10, 5, 0, 0),
        //    //                    Text = "Monto a pagar",//CreateNameButton,
        //    //                    FontWeight = FontWeights.Bold
        //    //                };

        //    //                TextBox MyTextBoxTarj = new TextBox()
        //    //                {
        //    //                    Height = 30,
        //    //                    Padding = new Thickness(0),
        //    //                    BorderThickness = new Thickness(1),
        //    //                    Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#4CEDEDED"),
        //    //                    BorderBrush = null,
        //    //                    Style = Application.Current.FindResource("MaterialDesignTextBoxBase") as Style
        //    //                };

        //    //                MyStackTarj.Children.Add(MyTextTarj);
        //    //                MyStackTarj.Children.Add(MyTextBoxTarj);

        //    //                myBorderTarj.Child = MyStackTarj;
        //    //                myTabItemTarj.Content = myBorderTarj;

        //    //                TabMainControl.Items.Add(myTabItemTarj);
        //    //                #endregion
        //    //                break;
        //    //            case string a when a.ToUpper().Contains("TRANSFERENCIA"):
        //    //                #region Create TabItem
        //    //                TabItem myTabItemTrans = new TabItem()
        //    //                {
        //    //                    Header = CreateNameButton,
        //    //                    FontSize = 25
        //    //                };
        //    //                #endregion
        //    //                #region Create Campo

        //    //                Border myBorderTrans = new Border()
        //    //                {
        //    //                    Margin = new Thickness(5, 10, 15, 10),
        //    //                    BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
        //    //                    BorderThickness = new Thickness(2, 0, 0, 2),
        //    //                    CornerRadius = new CornerRadius(5)
        //    //                };

        //    //                StackPanel MyStackTrans = new StackPanel() { Orientation = Orientation.Vertical };

        //    //                TextBlock MyTextTrans = new TextBlock()
        //    //                {
        //    //                    Margin = new Thickness(10, 5, 0, 0),
        //    //                    Text = "Monto a pagar",//CreateNameButton,
        //    //                    FontWeight = FontWeights.Bold
        //    //                };

        //    //                TextBox MyTextBoxTrans = new TextBox()
        //    //                {
        //    //                    Height = 30,
        //    //                    Padding = new Thickness(0),
        //    //                    BorderThickness = new Thickness(1),
        //    //                    Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#4CEDEDED"),
        //    //                    BorderBrush = null,
        //    //                    Style = Application.Current.FindResource("MaterialDesignTextBoxBase") as Style
        //    //                };

        //    //                MyStackTrans.Children.Add(MyTextTrans);
        //    //                MyStackTrans.Children.Add(MyTextBoxTrans);

        //    //                myBorderTrans.Child = MyStackTrans;
        //    //                myTabItemTrans.Content = myBorderTrans;

        //    //                TabMainControl.Items.Add(myTabItemTrans);
        //    //                #endregion
        //    //                break;
        //    //            case string a when a.ToUpper().Contains("CHEQUE"):
        //    //                #region Create TabItem
        //    //                TabItem myTabItemCk = new TabItem()
        //    //                {
        //    //                    Header = CreateNameButton,
        //    //                    FontSize = 25
        //    //                };
        //    //                #endregion
        //    //                #region Create Campo

        //    //                Border myBorderCk = new Border()
        //    //                {
        //    //                    Margin = new Thickness(5, 10, 15, 10),
        //    //                    BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
        //    //                    BorderThickness = new Thickness(2, 0, 0, 2),
        //    //                    CornerRadius = new CornerRadius(5)
        //    //                };

        //    //                StackPanel MyStackCk = new StackPanel() { Orientation = Orientation.Vertical };

        //    //                TextBlock MyTextCk = new TextBlock()
        //    //                {
        //    //                    Margin = new Thickness(10, 5, 0, 0),
        //    //                    Text = "Monto a pagar",//CreateNameButton,
        //    //                    FontWeight = FontWeights.Bold
        //    //                };

        //    //                TextBox MyTextBoxCk = new TextBox()
        //    //                {
        //    //                    Height = 30,
        //    //                    Padding = new Thickness(0),
        //    //                    BorderThickness = new Thickness(1),
        //    //                    Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#4CEDEDED"),
        //    //                    BorderBrush = null,
        //    //                    Style = Application.Current.FindResource("MaterialDesignTextBoxBase") as Style
        //    //                };

        //    //                MyStackCk.Children.Add(MyTextCk);
        //    //                MyStackCk.Children.Add(MyTextBoxCk);

        //    //                myBorderCk.Child = MyStackCk;
        //    //                myTabItemCk.Content = myBorderCk;

        //    //                TabMainControl.Items.Add(myTabItemCk);
        //    //                #endregion
        //    //                break;

        //    //                #region Comment

        //    //                //case string a when a.ToUpper().Contains("TARJETA"):
        //    //                //    #region Create Border
        //    //                //    Border myBorderTarj = new Border()
        //    //                //    {
        //    //                //        BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
        //    //                //        BorderThickness = new Thickness(2),
        //    //                //        HorizontalAlignment = HorizontalAlignment.Right,
        //    //                //        VerticalAlignment = VerticalAlignment.Stretch,
        //    //                //        Padding = new Thickness(0),
        //    //                //        CornerRadius = new CornerRadius(5),
        //    //                //        Margin = new Thickness(3)
        //    //                //    };
        //    //                //    #endregion
        //    //                //    #region Create Button
        //    //                //    Button MyButtonTarj = new Button()
        //    //                //    {
        //    //                //        Padding = new Thickness(0),
        //    //                //        Height = 59,
        //    //                //        MinWidth = 100,
        //    //                //        //Width = 100,
        //    //                //        ToolTip = "Pulsar para elegir metodo de pago " + CreateNameButton,
        //    //                //        Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#33C8C8C8"),
        //    //                //        Style = Application.Current.FindResource("MaterialDesignFlatButton") as Style
        //    //                //    };
        //    //                //    #endregion
        //    //                //    #region Asing event, Create Stack and Icon with text
        //    //                //    MyButtonTarj.Click += new RoutedEventHandler(handlerTarj_Click);

        //    //                //    StackPanel MyStackTarj = new StackPanel();

        //    //                //    Tarj = CreateNameButton;

        //    //                //    var packIconMaterialTarj = new MaterialDesignThemes.Wpf.PackIcon()
        //    //                //    {
        //    //                //        Kind = MaterialDesignThemes.Wpf.PackIconKind.CreditCardOutline,
        //    //                //        Width = 36,
        //    //                //        Height = 36,
        //    //                //        HorizontalAlignment = HorizontalAlignment.Center
        //    //                //    };

        //    //                //    TextBlock MyTextTarj = new TextBlock()
        //    //                //    {
        //    //                //        Text = CreateNameButton/*"Tarjeta"*/,
        //    //                //        HorizontalAlignment = HorizontalAlignment.Center,
        //    //                //        FontSize = 17
        //    //                //    };
        //    //                //    #endregion
        //    //                //    #region Asing Childs
        //    //                //    MyStackTarj.Children.Add(packIconMaterialTarj);
        //    //                //    MyStackTarj.Children.Add(MyTextTarj);
        //    //                //    MyButtonTarj.Content = MyStackTarj;
        //    //                //    myBorderTarj.Child = MyButtonTarj;

        //    //                //    PanelWrap.Children.Add(myBorderTarj);
        //    //                //    #endregion
        //    //                //    break;
        //    //                //case string a when a.ToUpper().Contains("TRANSFERENCIA"):
        //    //                //    #region Create Border
        //    //                //    Border myBorderTrans = new Border()
        //    //                //    {
        //    //                //        BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
        //    //                //        BorderThickness = new Thickness(2),
        //    //                //        HorizontalAlignment = HorizontalAlignment.Right,
        //    //                //        VerticalAlignment = VerticalAlignment.Stretch,
        //    //                //        Padding = new Thickness(0),
        //    //                //        CornerRadius = new CornerRadius(5),
        //    //                //        Margin = new Thickness(3)
        //    //                //    };
        //    //                //    #endregion
        //    //                //    #region Create Button
        //    //                //    Button MyButtonTrans = new Button()
        //    //                //    {
        //    //                //        Padding = new Thickness(0),
        //    //                //        Height = 59,
        //    //                //        MinWidth = 100,
        //    //                //        //Width = 100,
        //    //                //        ToolTip = "Pulsar para elegir metodo de pago " + CreateNameButton,
        //    //                //        Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#33C8C8C8"),
        //    //                //        Style = Application.Current.FindResource("MaterialDesignFlatButton") as Style
        //    //                //    };
        //    //                //    #endregion
        //    //                //    #region Asing event, Create Stack and Icon with text
        //    //                //    MyButtonTrans.Click += new RoutedEventHandler(handlerTrans_Click);

        //    //                //    StackPanel MyStackTrans = new StackPanel();

        //    //                //    Trans = CreateNameButton;

        //    //                //    var packIconMaterialTrans = new MaterialDesignThemes.Wpf.PackIcon()
        //    //                //    {
        //    //                //        Kind = MaterialDesignThemes.Wpf.PackIconKind.BankTransfer,
        //    //                //        Width = 36,
        //    //                //        Height = 36,
        //    //                //        HorizontalAlignment = HorizontalAlignment.Center
        //    //                //    };

        //    //                //    TextBlock MyTextTrans = new TextBlock()
        //    //                //    {
        //    //                //        Text = /*"Transferencia"*/ CreateNameButton,
        //    //                //        HorizontalAlignment = HorizontalAlignment.Center,
        //    //                //        FontSize = 17
        //    //                //    };
        //    //                //    #endregion
        //    //                //    #region Asing Childs
        //    //                //    MyStackTrans.Children.Add(packIconMaterialTrans);
        //    //                //    MyStackTrans.Children.Add(MyTextTrans);
        //    //                //    MyButtonTrans.Content = MyStackTrans;
        //    //                //    myBorderTrans.Child = MyButtonTrans;

        //    //                //    PanelWrap.Children.Add(myBorderTrans);
        //    //                //    #endregion
        //    //                //    break;
        //    //                //case string a when a.ToUpper().Contains("CHEQUE"):
        //    //                //    #region Create Border
        //    //                //    Border myBorderCk = new Border()
        //    //                //    {
        //    //                //        BorderBrush = (Brush)Application.Current.FindResource("PrimaryHueDarkBrush"),
        //    //                //        BorderThickness = new Thickness(2),
        //    //                //        HorizontalAlignment = HorizontalAlignment.Right,
        //    //                //        VerticalAlignment = VerticalAlignment.Stretch,
        //    //                //        Padding = new Thickness(0),
        //    //                //        CornerRadius = new CornerRadius(5),
        //    //                //        Margin = new Thickness(3)
        //    //                //    };
        //    //                //    #endregion
        //    //                //    #region Create Button
        //    //                //    Button MyButtonCk = new Button()
        //    //                //    {
        //    //                //        Padding = new Thickness(0),
        //    //                //        Height = 59,
        //    //                //        MinWidth = 100,
        //    //                //        //Width = 100,
        //    //                //        ToolTip = "Pulsar para elegir metodo de pago " + CreateNameButton,
        //    //                //        Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#33C8C8C8"),
        //    //                //        Style = Application.Current.FindResource("MaterialDesignFlatButton") as Style
        //    //                //    };
        //    //                //    #endregion
        //    //                //    #region Asing event, Create Stack and Icon with text
        //    //                //    MyButtonCk.Click += new RoutedEventHandler(handlerCk_Click);

        //    //                //    StackPanel MyStackCk = new StackPanel();

        //    //                //    Ck = CreateNameButton;

        //    //                //    var packIconMaterialCk = new MaterialDesignThemes.Wpf.PackIcon()
        //    //                //    {
        //    //                //        Kind = MaterialDesignThemes.Wpf.PackIconKind.Bank,
        //    //                //        Width = 36,
        //    //                //        Height = 36,
        //    //                //        HorizontalAlignment = HorizontalAlignment.Center
        //    //                //    };

        //    //                //    TextBlock MyTextCk = new TextBlock()
        //    //                //    {
        //    //                //        Text = CreateNameButton/*"Cheque"*/,
        //    //                //        HorizontalAlignment = HorizontalAlignment.Center,
        //    //                //        FontSize = 17
        //    //                //    };
        //    //                //    #endregion
        //    //                //    #region Asing Childs
        //    //                //    MyStackCk.Children.Add(packIconMaterialCk);
        //    //                //    MyStackCk.Children.Add(MyTextCk);
        //    //                //    MyButtonCk.Content = MyStackCk;
        //    //                //    myBorderCk.Child = MyButtonCk;

        //    //                //    PanelWrap.Children.Add(myBorderCk);
        //    //                //    #endregion
        //    //                //    break;

        //    //                #endregion

        //    //        }
        //    //    }

        //    //    //string name = (TabMain.SelectedItem as TabItem).Header.ToString();


        //    //    foreach (TabItem TabItemControl in TabMainControl.Items)
        //    //    {
        //    //        if (TabItemControl.Header.ToString().ToUpper().Contains(FormaPago.ToUpper()))
        //    //        {
        //    //            TabItemControl.IsSelected = true;
        //    //        }
        //    //    }
        //    //    //ta
        //    //}
        //}

        private void txtMontoDistribuido_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                txtMontoRestante.Text = default;
                var result = "";

                //txtMontoRestante.Text = Convert.ToDouble(txtMontoDistribuido.Text = txtMontoDistribuido.Text != null && txtMontoDistribuido.Text != "" ? txtMontoDistribuido.Text : "0") == 0 ? (Convert.ToDouble(lMonto.Text)).ToString() :
                //   (Convert.ToDouble(lMonto.Text) - Convert.ToDouble(txtMontoRestante.Text = (txtMontoRestante.Text != null && txtMontoRestante.Text != "" ? txtMontoRestante.Text : txtMontoDistribuido.Text))).ToString();

                txtMontoRestante.Text = Convert.ToDouble(result = txtMontoDistribuido.Text != null && txtMontoDistribuido.Text != "" ? txtMontoDistribuido.Text : "0") == 0 ? (Convert.ToDouble(lMonto.Text)).ToString() :
                    (Convert.ToDouble(lMonto.Text) - Convert.ToDouble(txtMontoRestante.Text = (txtMontoRestante.Text != null && txtMontoRestante.Text != "" ? txtMontoRestante.Text : txtMontoDistribuido.Text))).ToString();

            }
            catch (Exception exception)
            {
                Administracion.FrmCompletarCamposHost frm = new Administracion.FrmCompletarCamposHost("Ha ocurrido un error: " + exception.ToString());
                frm.ShowDialog();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((Convert.ToDouble(lMonto.Text) < Convert.ToDouble(txtMontoDistribuido.Text)) && SnackbarThree.MessageQueue is { } messageQueue && MontoRestante != Efect)
                    Task.Factory.StartNew(() => messageQueue.Enqueue("No puede distribuir mas del monto a pagar"));
                else if (MontoRestante == Efect && Convert.ToDouble(lMonto.Text) < Convert.ToDouble(txtMontoDistribuido.Text))
                {
                    //PassEfect = true;
                    new Administracion.FrmValidarAccion("Esta seguro que desea distribuir esta cantidad?").ShowDialog();
                    if (ClassVariables.ValidarAccion)
                        new Administracion.FrmCompletarCamposHost("Devuelta: "+txtMontoRestante.Text).ShowDialog();
                }
                else if (Convert.ToDouble(txtMontoRestante.Text) != 0 && Convert.ToDouble(lMonto.Text) != Convert.ToDouble(txtMontoDistribuido.Text))
                {
                    BtAceptarCancelar.Visibility = Visibility.Hidden;
                    PanelOpcionesPagos.Visibility = Visibility.Visible;
                }
            }
            catch { }
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
            MontoRestante = Efect;
            BtAceptarCancelar.Visibility = Visibility.Visible;
            PanelOpcionesPagos.Visibility = Visibility.Hidden;
            //OpenControlComprobantes();
        }
        private void handlerTarj_Click(object sender, RoutedEventArgs e)
        {
            MontoRestante = Tarj;
            BtAceptarCancelar.Visibility = Visibility.Visible;
            PanelOpcionesPagos.Visibility = Visibility.Hidden;
            //OpenControlComprobantes();
        }
        private void handlerTrans_Click(object sender, RoutedEventArgs e)
        {
            MontoRestante = Trans;
            BtAceptarCancelar.Visibility = Visibility.Visible;
            PanelOpcionesPagos.Visibility = Visibility.Hidden;
            //OpenControlComprobantes();
        }
        private void handlerCk_Click(object sender, RoutedEventArgs e)
        {
            MontoRestante = Ck;
            BtAceptarCancelar.Visibility = Visibility.Visible;
            PanelOpcionesPagos.Visibility = Visibility.Hidden;
            //OpenControlComprobantes();
        }

        private void PanelOpcionesPagos_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtMontoDistribuido_KeyDown(object sender, KeyEventArgs e)
        {
            ClassControl.ValidadorNumeros(e);
        }
    }
}
