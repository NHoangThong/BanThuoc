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
    public partial class thongKeThuocBanRa : Form
    {
        SqlConnection conn;
        public thongKeThuocBanRa()
        {
            InitializeComponent();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);
        }
        private void ThongKeThuocBanRaTheoNgay(DateTime ngayXuat)
        {
            try
            {
                
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("ThongKeThuocBanRa", conn))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@NgayXuat", ngayXuat);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Gán DataTable cho báo cáo
                        
                    ThongKeThuocBanRaTheoNgay baocao = new ThongKeThuocBanRaTheoNgay();
                    baocao.SetDataSource(dataTable);
                    thongKeThuocBanRa f = new thongKeThuocBanRa();
                    f.crystalReportViewer1.ReportSource = baocao;
                    f.ShowDialog();
                    }
                    conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi");
            }
        }

        

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            DateTime selectedDate = dtpNgayXuat.Value;
            ThongKeThuocBanRaTheoNgay(selectedDate);
        }
    }
}
