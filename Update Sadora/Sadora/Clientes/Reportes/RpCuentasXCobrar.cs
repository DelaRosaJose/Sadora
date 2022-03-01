﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Sadora.Administracion;
using System.Threading.Tasks;
using System.Globalization;

namespace Sadora.Clientes.Reportes
{
    public partial class RpCuentasXCobrar : DevExpress.XtraReports.UI.XtraReport
    {
        public RpCuentasXCobrar(DataTable HeadTable)
        {
            InitializeComponent();

            DataTable reader = Clases.ClassData.runDataTable("select * from TcliClientes where " +
                "ClienteID = " + (HeadTable.Columns.Contains("ClienteID") ? HeadTable.Rows[0]["ClienteID"].ToString() : "" ), null, "CommandText"); //En esta linea de codigo estamos ejecutando un metodo que recibe una consulta, la busca en sql y te retorna el resultado en un datareader.

            if (HeadTable.Rows.Count == 1)
            {
                Task.Run(() =>
                {
                    #region Variables Sistema
                    Bitmap bmp = new Bitmap(new System.IO.MemoryStream(Clases.ClassVariables.LogoEmpresa));
                    Image img = bmp;

                    PicLogo.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource(img);
                    #endregion
                });
                Task.Run(() =>
                {
                    #region Variables Empresa
                    LbNombreEmpresa.Text = Clases.ClassVariables.ClasesVariables.NombreEmpresa;//HeadTable.Columns.Contains("NombreEmpresa") ? HeadTable.Rows[0]["NombreEmpresa"].ToString() : "CAVERNA DESIGN SRL";
                    LbRncEmpresa.Text = Clases.ClassVariables.ClasesVariables.RNCEmpresa;//HeadTable.Columns.Contains("RNCEmpresa") ? HeadTable.Rows[0]["RNCEmpresa"].ToString() : "132255186";
                    LbTelefonoEmpresa.Text = Clases.ClassVariables.ClasesVariables.TelefonoEmpresa;//HeadTable.Columns.Contains("RNCEmpresa") ? HeadTable.Rows[0]["RNCEmpresa"].ToString() : "132255186";
                    LbDireccionEmpresa.Text = Clases.ClassVariables.ClasesVariables.DireccionEmpresa;//HeadTable.Columns.Contains("RNCEmpresa") ? HeadTable.Rows[0]["RNCEmpresa"].ToString() : "132255186";
                    LbSucursal.Text = "Sucursal: Villa Mella";//Clases.ClassVariables.ClasesVariables.sucu;//able.Columns.Contains("Sucursal") ? HeadTable.Rows[0]["Sucursal"].ToString() : "Sucursal Villa Mella";
                    
                    #endregion
                });
                Task.Run(() =>
                {
                    #region Variables Factura
                    LbFechaCreacion.Text = HeadTable.Columns.Contains("Fecha") ? HeadTable.Rows[0]["Fecha"].ToString() : "10/23/2021";
                    //LbNCF.Text = HeadTable.Columns.Contains("NCF") ? HeadTable.Rows[0]["NCF"].ToString() : "B0200000006";
                    LbTipoFactura.Text = HeadTable.Columns.Contains("TipoTransaccion") ? HeadTable.Rows[0]["TipoTransaccion"].ToString() : "Cuentas por Cobrar";
                    //LbFechaVencimiento.Text = HeadTable.Columns.Contains("VenceComprobante") ? HeadTable.Rows[0]["VenceComprobante"].ToString() : "12/31/2021";
                });
                Task.Run(() =>
                {
                    LbSubTotal.Text = HeadTable.Columns.Contains("SubTotal") ? HeadTable.Rows[0]["SubTotal"].ToString() : "0";
                    LbDescuento.Text = HeadTable.Columns.Contains("Descuento") ? HeadTable.Rows[0]["Descuento"].ToString() : "0";
                    LbITBIS.Text = HeadTable.Columns.Contains("ITBIS") ? HeadTable.Rows[0]["ITBIS"].ToString() : "0";
                    LbTotal.Text = HeadTable.Columns.Contains("Total") ? HeadTable.Rows[0]["Total"].ToString() : "0";
                    #endregion
                });
                //Task.Run(() =>
                //{
                //    #region VisibleControl
                //    if (!HeadTable.Rows[0]["NCF"].ToString().Contains("B0"))
                //        xrLabel4.Visible = LbRncCliente.Visible = LbFechaVencimiento.Visible = xrLabel13.Visible = false;
                //    #endregion
                //});

                Task.Run(() =>
                {
                    #region Variables Clientes
                    LbRncCliente.Text = reader.Columns.Contains("RNC") ? reader.Rows[0]["RNC"].ToString() : "";
                    LbRazonSocial.Text = reader.Columns.Contains("Nombre") ? reader.Rows[0]["Nombre"].ToString() : "";
                    #endregion
                });
            }
            else
            {
                new Administracion.FrmCompletarCamposHost("Ha ocurrido un error comuniquese con soporte").ShowDialog();
                return;
            }


            //if (DetailTable.Rows.Count >= 1)
            //{
            //    Task.Run(() =>
            //    {
            //        //var result = DetailTable.Columns.Contains("Cantidad") ? DetailTable.Rows[0]["Cantidad"].ToString() : "0";
            //        for (int i = 0; i < DetailTable.Rows.Count; i++)
            //        {
            //            xrTable2.Rows.Add(new XRTableRow());

            //            this.xrTable2.Rows.LastRow.Cells.AddRange(new XRTableCell[]
            //            {
            //            new XRTableCell() {Text = DetailTable.Columns.Contains("Cantidad") ? (DetailTable.Columns.Contains("Precio") ? DetailTable.Rows[i]["Cantidad"].ToString() + " x "+ DetailTable.Rows[i]["Precio"].ToString() : DetailTable.Rows[i]["Cantidad"].ToString()): "0" },
            //            new XRTableCell() {Text = DetailTable.Columns.Contains("Nombre") ? DetailTable.Rows[i]["Nombre"].ToString() : "0" },
            //            new XRTableCell() {Text = DetailTable.Columns.Contains("ITBIS") ? DetailTable.Rows[i]["ITBIS"].ToString() : "0" },//((Convert.ToDouble(DetailTable.Rows[i]["ITBIS"].ToString()) / Convert.ToDouble(DetailTable.Rows[i]["Cantidad"].ToString())).ToString()): "0" },
            //            new XRTableCell() {Text = DetailTable.Columns.Contains("Precio") ? DetailTable.Rows[i]["Total"].ToString() : "0" }
            //            //DetailTable.Rows[i]["ITBIS"].ToString() : "0" },
            //            });
            //        }
            //    });
            //}
        }

    }
}
