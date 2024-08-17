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

namespace DoAn_CNNet.Resources
{
    public partial class fmThongKeNhapThuoc : Form
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
       
        public fmThongKeNhapThuoc()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            string query = @"
        SELECT PN.MAPN, PN.NGAYNHAP, NV.TENNV, T.MATHUOC, T.TENTHUOC, CT.SOLUONG, CT.DONGIA
        FROM PHIEUNHAP PN
        INNER JOIN NHANVIEN NV ON PN.MANV = NV.MANV
        INNER JOIN CHITIETPHIEUNHAP CT ON PN.MAPN = CT.MAPN
        INNER JOIN THUOC T ON CT.MATHUOC = T.MATHUOC
    ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.Text;

                    DataTable dtb = new DataTable("BaoCaoPhieuNhap");
                    adapter.Fill(dtb);

                    if (dtb.Rows.Count > 0)
                    {
                        rptThongKeNhapThuoc report = new rptThongKeNhapThuoc();
                        report.SetDataSource(dtb);

                        crystalReportViewer1.ReportSource = report;
                        crystalReportViewer1.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu cho ngày đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
