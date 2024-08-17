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
    public partial class fmKhachHang : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        DataSet ds_khachhang = new DataSet();

        public fmKhachHang()
        {
            InitializeComponent();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            cnn = new SqlConnection(connectionString);
        }

        void LoadDuLieuKhachHang()
        {
            string strSelect = "Select * from KHACHHANG";
            da = new SqlDataAdapter(strSelect, cnn);
            da.Fill(ds_khachhang, "KHACHHANG");

            DataColumn[] key = new DataColumn[1];
            key[0] = ds_khachhang.Tables["KHACHHANG"].Columns[0];
            ds_khachhang.Tables["KHACHHANG"].PrimaryKey = key;

            grdKH.DataSource = ds_khachhang.Tables["KHACHHANG"];
            grdKH.ReadOnly = true;
        }

        void Databingding(DataTable pDT)
        {
            txtMaKH.DataBindings.Clear();
            txtTenKH.DataBindings.Clear();
            txtSdt.DataBindings.Clear();
            txtCCCD.DataBindings.Clear();

            txtMaKH.DataBindings.Add("Text", pDT, "MAKH");
            txtTenKH.DataBindings.Add("Text", pDT, "TENKHACHHANG");
            txtSdt.DataBindings.Add("Text", pDT, "SDT");
            txtCCCD.DataBindings.Add("Text", pDT, "CCCD");
        }

        private void fmKhachHang_Load(object sender, EventArgs e)
        {
            LoadDuLieuKhachHang();
            DisableAllFields();
            btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = false;
            grdKH.ReadOnly = true;
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            EnableAllFields();
            btnLuu.Enabled = true;
            grdKH.ReadOnly = false;

            if (txtMaKH.Enabled == true)
            {
                // Kiểm tra các trường dữ liệu có được điền đầy đủ hay không
                if (string.IsNullOrEmpty(txtMaKH.Text) || string.IsNullOrEmpty(txtTenKH.Text) || string.IsNullOrEmpty(txtSdt.Text) || string.IsNullOrEmpty(txtCCCD.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtMaKH.Enabled == true)
                {
                    string newMAKH = txtMaKH.Text;
                    string newSdt = txtSdt.Text;
                    string newCCCD = txtCCCD.Text;
                    DataRow existingRow = ds_khachhang.Tables["KHACHHANG"].Rows.Find(newMAKH);
                    DataRow existingRow1 = ds_khachhang.Tables["KHACHHANG"].Rows.Find(newSdt);
                    DataRow existingRow2 = ds_khachhang.Tables["KHACHHANG"].Rows.Find(newCCCD);
                    if (existingRow != null)
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập lại một mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DataRow insert_New = ds_khachhang.Tables["KHACHHANG"].NewRow();
                    insert_New["MAKH"] = newMAKH;
                    insert_New["TENKHACHHANG"] = txtTenKH.Text;
                    insert_New["SDT"] = txtSdt.Text;
                    insert_New["CCCD"] = txtCCCD.Text;
                    ds_khachhang.Tables["KHACHHANG"].Rows.Add(insert_New);
                }
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            if (grdKH.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn nhân viên để sửa");
                return;
            }

            EnableAllFields();
            btnLuu.Enabled = true;
            grdKH.ReadOnly = false;
            txtMaKH.Enabled = false; // Disable editing of the employee ID
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            EnableAllFields();
            if (txtMaKH.Text == string.Empty)
            {
                MessageBox.Show("Chưa nhập Mã Khách Hàng");
                txtMaKH.Focus();
                return;
            }

            DataRow update_Row = ds_khachhang.Tables["KHACHHANG"].Rows.Find(txtMaKH.Text);

            if (update_Row != null)
            {
                update_Row["TENKHACHHANG"] = txtTenKH.Text;
                update_Row["SDT"] = txtSdt.Text;
                update_Row["CCCD"] = txtCCCD.Text;
            }

            try
            {
                Databingding(ds_khachhang.Tables["KHACHHANG"]);
                SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                da.Update(ds_khachhang, "KHACHHANG");
                MessageBox.Show("Cập nhật thành công");
                btnLuu.Enabled = false;
                grdKH.ReadOnly = true;
                grdKH.AllowUserToAddRows = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cập nhật dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật dữ liệu: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            if (MessageBox.Show("Bạn muốn xóa?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                DataTable dt_PhieuXuat = new DataTable();
                SqlDataAdapter da_PX = new SqlDataAdapter("Select * from PHIEUXUAT where MAKH = '" + txtMaKH.Text + "'", cnn);

                da_PX.Fill(dt_PhieuXuat);
                if (dt_PhieuXuat.Rows.Count > 0)
                {
                    MessageBox.Show("Dữ liệu đang sử dụng không thể xóa");
                    return;
                }

                int rowIndex = grdKH.CurrentRow.Index;
                grdKH.Rows.RemoveAt(rowIndex);

                try
                {
                    SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                    da.Update(ds_khachhang, "KHACHHANG");
                    MessageBox.Show("Thành công");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi cập nhật dữ liệu: " + ex.Message);
                }
            }
        }

        private void grdKH_SelectionChanged(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = true;
            // Kiểm tra xem có hàng được chọn hay không
            if (grdKH.SelectedRows.Count > 0)
            {
                // Lấy hàng hiện tại đang được chọn
                DataGridViewRow selectedRow = grdKH.SelectedRows[0];

                // Lấy đối tượng dữ liệu (DataRowView) liên kết với hàng đó
                DataRowView dataRowView = selectedRow.DataBoundItem as DataRowView;

                // Kiểm tra xem có đối tượng dữ liệu hay không
                if (dataRowView != null)
                {
                    // Truy cập các cột dữ liệu thông qua đối tượng dữ liệu
                    DataRow dataRow = dataRowView.Row;
                    txtMaKH.Text = dataRow["MaKH"].ToString();
                    txtTenKH.Text = dataRow["TENKHACHHANG"].ToString();
                    txtSdt.Text = dataRow["Sdt"].ToString();
                    txtCCCD.Text = dataRow["CCCD"].ToString();
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            string searchTerm = txtSearch.Text.Trim();

            DataRow foundRow = ds_khachhang.Tables["KHACHHANG"].Rows.Find(searchTerm);

            if (foundRow == null)
            {
                DataRow[] foundRows = ds_khachhang.Tables["KHACHHANG"].Select("TENKHACHHANGACHHANG LIKE '%" + searchTerm + "%'");

                if (foundRows.Length == 0)
                {
                    MessageBox.Show("No matching records found.");
                    return;
                }

                foundRow = foundRows[0];
            }

            txtMaKH.Text = foundRow["MAKH"].ToString();
            txtTenKH.Text = foundRow["TENKHACHHANG"].ToString();
            txtSdt.Text = foundRow["SDT"].ToString();
            txtCCCD.Text = foundRow["CCCD"].ToString();

            grdKH.DataSource = ds_khachhang.Tables["KHACHHANG"];
            (grdKH.DataSource as DataTable).DefaultView.RowFilter = "MAKH = '" + txtMaKH.Text + "' OR TENKHACHHANGACHHANG LIKE '%" + txtTenKH.Text + "%'";

            DisableAllFields();
        }
        private void ClearFields()
        {
            txtMaKH.Clear();
            txtTenKH.Clear();
            txtSdt.Clear();
            txtCCCD.Clear();
        }

        private void EnableAllFields()
        {
            txtMaKH.Enabled = true;
            txtTenKH.Enabled = true;
            txtSdt.Enabled = true;
            txtCCCD.Enabled = true;
            txtSearch.Enabled = true;
        }

        private void DisableAllFields()
        {
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            txtSdt.Enabled = false;
            txtCCCD.Enabled = false;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            fmRPNhanVien sl = new fmRPNhanVien();
            sl.ShowDialog();
            this.Close();
        }
    }
}
