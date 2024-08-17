using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAn_CNNet
{
    public partial class frmRPHoaDonNhap : Form
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
       
        public frmRPHoaDonNhap()
        {
            InitializeComponent();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpNgaySX.Value.Date;

            string query = @"
                            SELECT PN.MAPN, PN.NGAYNHAP, NV.TENNV, T.MATHUOC, T.TENTHUOC, CT.SOLUONG, CT.DONGIA
                            FROM PHIEUNHAP PN
                            INNER JOIN NHANVIEN NV ON PN.MANV = NV.MANV
                            INNER JOIN CHITIETPHIEUNHAP CT ON PN.MAPN = CT.MAPN
                            INNER JOIN THUOC T ON CT.MATHUOC = T.MATHUOC
                            WHERE CONVERT(DATE, PN.NGAYNHAP) = @NgayNhap
                        ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.Text;
                    adapter.SelectCommand.Parameters.AddWithValue("@NgayNhap", selectedDate);

                    DataTable dtb = new DataTable("BaoCaoPhieuNhap");
                    adapter.Fill(dtb);

                    if (dtb.Rows.Count > 0)
                    {
                        rptHoaDonNhap1 baocao = new rptHoaDonNhap1();
                        baocao.SetDataSource(dtb);

                        crystalReportViewer1.ReportSource = baocao;
                        crystalReportViewer1.DisplayStatusBar = false;
                        crystalReportViewer1.DisplayToolbar = true;
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