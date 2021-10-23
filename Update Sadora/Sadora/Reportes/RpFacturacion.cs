using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Sadora.Reportes
{
    public partial class RpFacturacion : DevExpress.XtraReports.UI.XtraReport
    {
        public RpFacturacion(DataTable HeadTable, DataTable DetailTable = null)
        {
            InitializeComponent();


            if (HeadTable.Rows.Count == 1)
            {
                #region Variables Empresa
                LbNombreEmpresa.Text = HeadTable.Columns.Contains("NombreEmpresa") ? HeadTable.Rows[0]["NombreEmpresa"].ToString() : "MULTI SERVICIOS HERMANOS DE LA ROSA PAYANO SRL";
                LbRncEmpresa.Text = HeadTable.Columns.Contains("RNCEmpresa") ? HeadTable.Rows[0]["RNCEmpresa"].ToString() : "101846399";
                LbSucursal.Text = HeadTable.Columns.Contains("Sucursal") ? HeadTable.Rows[0]["Sucursal"].ToString() : "sucursal Villa Mella"; 
                #endregion

                #region Variables Factura
                LbFechaCreacion.Text = HeadTable.Columns.Contains("FechaCreacion") ? HeadTable.Rows[0]["FechaCreacion"].ToString() : "10/23/2021";
                LbNCF.Text = HeadTable.Columns.Contains("NCF") ? HeadTable.Rows[0]["NCF"].ToString() : "B0200000006";
                LbTipoFactura.Text = LbNCF.Text.Contains("B02") ? "Factura de Consumo" : "Factura de Credito Fiscal";
                LbFechaVencimiento.Text = HeadTable.Columns.Contains("VenceComprobante") ? HeadTable.Rows[0]["VenceComprobante"].ToString() : "12/31/2021";
                
                LbSubTotal.Text = HeadTable.Columns.Contains("SubTotal") ? HeadTable.Rows[0]["SubTotal"].ToString() : "0";
                LbDescuento.Text = HeadTable.Columns.Contains("Descuento") ? HeadTable.Rows[0]["Descuento"].ToString() : "0";
                LbITBIS.Text = HeadTable.Columns.Contains("ITBIS") ? HeadTable.Rows[0]["ITBIS"].ToString() : "0";
                LbTotal.Text = HeadTable.Columns.Contains("Total") ? HeadTable.Rows[0]["Total"].ToString() : "0";
                #endregion

                #region Variables Clientes
                LbRncCliente.Text = HeadTable.Columns.Contains("RNC") ? HeadTable.Rows[0]["RNC"].ToString() : "";
                LbRazonSocial.Text = HeadTable.Columns.Contains("Nombre") ? HeadTable.Rows[0]["Nombre"].ToString() : "";
                #endregion
            }
            else
            {
                new Administracion.FrmCompletarCamposHost("Ha ocurrido un error comuniquese con soporte").ShowDialog();
                return;
            }


            if (DetailTable.Rows.Count >= 1)
            {
                //var result = DetailTable.Columns.Contains("Cantidad") ? DetailTable.Rows[0]["Cantidad"].ToString() : "0";
                for (int i = 0; i < DetailTable.Rows.Count; i++)
                {
                    xrTable2.Rows.Add(new XRTableRow());

                    this.xrTable2.Rows.LastRow.Cells.AddRange(new XRTableCell[]
                    {
                        new XRTableCell() {Text = DetailTable.Columns.Contains("Cantidad") ? (DetailTable.Columns.Contains("Precio") ? DetailTable.Rows[i]["Cantidad"].ToString() + " x "+ DetailTable.Rows[i]["Precio"].ToString() : DetailTable.Rows[i]["Cantidad"].ToString()): "0" },
                        new XRTableCell() {Text = DetailTable.Columns.Contains("Nombre") ? DetailTable.Rows[i]["Nombre"].ToString() : "0" },
                        new XRTableCell() {Text = DetailTable.Columns.Contains("ITBIS") ? ((Convert.ToDouble(DetailTable.Rows[i]["ITBIS"].ToString()) / Convert.ToDouble(DetailTable.Rows[i]["Cantidad"].ToString())).ToString()): "0" },
                        new XRTableCell() {Text = DetailTable.Columns.Contains("Precio") ? DetailTable.Rows[i]["Precio"].ToString() : "0" }
                        //DetailTable.Rows[i]["ITBIS"].ToString() : "0" },
                    });
                }
            }
        }

    }
}
