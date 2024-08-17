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
    public partial class fmDangNhap : Form
    {
        
        SqlConnection conn;
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        DataTable dtcom = new DataTable();
        SqlCommand cmd;
        string sql, connstr;
        int i;

        public fmDangNhap()
        {
            InitializeComponent();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            conn = new SqlConnection(connectionString);
        }
        private void fmDangNhap_Load(object sender, EventArgs e)
        {
        }
        private void bttLog_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "" || txtPass.Text == "") 
            {
                MessageBox.Show("Please Enter valid Username and Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("select VAITRO from NHANVIEN where EMAIL='" + txtUser.Text + "' and MATKHAU='" + txtPass.Text + "'", conn);
                conn.Open();
                string stat = Convert.ToString(cmd.ExecuteScalar());
                if (stat.Trim() == "admin")
                {
                    fmAdmin ad = new fmAdmin();
                    ad.ShowDialog();
                    this.Close();
                }
                else if (stat.Trim() == "salesman")
                {
                    fmSalesman sl = new fmSalesman();
                    sl.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUser.Clear();
                    txtPass.Clear();
                }
                conn.Close();
            }
        }

        private void fmDangNhap_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bttLog.PerformClick();
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
