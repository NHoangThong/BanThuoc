using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace DoAn_CNNet
{
    public partial class fmRPKhachHang : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        DataSet ds_NV = new DataSet();
        public fmRPKhachHang()
        {
            InitializeComponent();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            cnn = new SqlConnection(connectionString);
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            DataTable dtb = new DataTable("KHACHHANG");
            SqlDataAdapter da_NHANVIEN = new SqlDataAdapter("select * from KHACHHANG", cnn);
            da_NHANVIEN.Fill(dtb);
            rptKhachHang baocao = new rptKhachHang();
            baocao.SetDataSource(dtb);
            fmRPKhachHang f = new fmRPKhachHang();
            f.crystalReportViewer1.ReportSource = baocao;
            //f.ShowDialog();

            crystalReportViewer1.ReportSource = baocao;
            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.Refresh();
        }
    }
}
