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
    public partial class fmRPNhanVien : Form
    {
        SqlConnection cnn; 
        SqlDataAdapter da;
        DataSet ds_NV = new DataSet();
        public fmRPNhanVien()
        {
            InitializeComponent();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            cnn = new SqlConnection(connectionString);
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            DataTable dtb = new DataTable("NHANVIEN");
            SqlDataAdapter da_NHANVIEN = new SqlDataAdapter("select * from NHANVIEN",cnn);
            da_NHANVIEN.Fill(dtb);
            rptListNhanVien baocao = new rptListNhanVien();
            baocao.SetDataSource(dtb);
            fmRPNhanVien f = new fmRPNhanVien();
            f.crystalReportViewer1.ReportSource = baocao;
            //f.ShowDialog();

            crystalReportViewer1.ReportSource = baocao;
            crystalReportViewer1.DisplayStatusBar = false;
            crystalReportViewer1.DisplayToolbar = true;
            crystalReportViewer1.Refresh();
        }

    }
}
