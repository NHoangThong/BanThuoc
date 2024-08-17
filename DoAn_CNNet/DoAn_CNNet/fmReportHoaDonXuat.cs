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
    public partial class fmReportHoaDonXuat : Form
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        SqlConnection conn;
        public fmReportHoaDonXuat()
        {
            InitializeComponent();
           
            conn = new SqlConnection(connectionString);

        }
        private string GetNextIDFromDatabase(string tableName, string idColumnName)
        {
            // Hàm này để lấy số tự tăng tiếp theo từ cơ sở dữ liệu
            // Bạn cần thay đổi nó phù hợp với cơ sở dữ liệu của bạn
            string sqlQuery = "SELECT MAX(" + idColumnName + ") FROM " + tableName;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }

                    return null; // Trường hợp đầu tiên
                }
            }

        }
        private string GenerateMaPX()
        {
            // Hàm này để sinh mã phiếu XUAT mới, bạn có thể tùy chỉnh theo nhu cầu

            return GetNextIDFromDatabase("PHIEUXUAT", "MAPX");
          
        }

        private void fmReportHoaDonXuat_Load(object sender, EventArgs e)
        {
            string maPX = GenerateMaPX();
            conn.Open();
            using (SqlCommand command = new SqlCommand("HoaDon", conn))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MAPX", maPX);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Gán DataTable cho báo cáo

                HoaDonXuat baocao = new HoaDonXuat();
                baocao.SetDataSource(dataTable);
                //fmReportHoaDonXuat f = new fmReportHoaDonXuat();
                //f.crystalReportViewer1.ReportSource = baocao;
                //f.ShowDialog();
                crystalReportViewer1.ReportSource = baocao;
                crystalReportViewer1.Refresh();
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
