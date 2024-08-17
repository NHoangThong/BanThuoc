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
    public partial class fmNhanVien : Form
    {
        SqlConnection cnn;
        SqlDataAdapter da;
        DataSet ds_nhanvien = new DataSet();

        public fmNhanVien()
        {
            InitializeComponent();
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            cnn = new SqlConnection(connectionString);
        }

        void LoadDuLieuNhanVien()
        {
            string strSelect = "SELECT * FROM NHANVIEN";
            da = new SqlDataAdapter(strSelect, cnn);
            da.Fill(ds_nhanvien, "NHANVIEN");

            DataColumn[] key = new DataColumn[1];
            key[0] = ds_nhanvien.Tables["NHANVIEN"].Columns["MANV"];
            ds_nhanvien.Tables["NHANVIEN"].PrimaryKey = key;

            grdNV.DataSource = ds_nhanvien.Tables["NHANVIEN"];
            grdNV.ReadOnly = true;
        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            LoadDuLieuNhanVien();
            DisableAllFields();
            btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = false;
            grdNV.ReadOnly = true;
        }


        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            EnableAllFields();
            btnLuu.Enabled = true;
            grdNV.ReadOnly = false;

            if (txtMaNV.Enabled == true)
            {
                // Kiểm tra các trường dữ liệu có được điền đầy đủ hay không
                if (string.IsNullOrEmpty(txtMaNV.Text) || string.IsNullOrEmpty(txtTenNV.Text) || string.IsNullOrEmpty(txtSdt.Text) || string.IsNullOrEmpty(txtCCCD.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Text) || string.IsNullOrEmpty(txtVaiTro.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string newMaNV = txtMaNV.Text;
                string newSdt = txtSdt.Text;
                string newCCCD = txtCCCD.Text;
                string newEmail = txtEmail.Text;
                DataRow existingRow = ds_nhanvien.Tables["NHANVIEN"].Rows.Find(newMaNV);
                DataRow existingRow1 = ds_nhanvien.Tables["NHANVIEN"].Rows.Find(newSdt);
                DataRow existingRow2 = ds_nhanvien.Tables["NHANVIEN"].Rows.Find(newCCCD);
                DataRow existingRow3 = ds_nhanvien.Tables["NHANVIEN"].Rows.Find(newEmail);
                if (existingRow != null)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại. Vui lòng nhập lại một mã khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataRow insert_New = ds_nhanvien.Tables["NHANVIEN"].NewRow();
                insert_New["MANV"] = newMaNV;
                insert_New["TENNV"] = txtTenNV.Text;
                insert_New["SDT"] = txtSdt.Text;
                insert_New["CCCD"] = txtCCCD.Text;
                insert_New["EMAIL"] = txtEmail.Text;
                insert_New["MATKHAU"] = txtPass.Text;
                insert_New["VAITRO"] = txtVaiTro.Text;
                ds_nhanvien.Tables["NHANVIEN"].Rows.Add(insert_New);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (grdNV.CurrentRow == null)
            {
                MessageBox.Show("Chưa chọn nhân viên để sửa");
                return;
            }

            EnableAllFields();
            btnLuu.Enabled = true;
            grdNV.ReadOnly = false;
            txtMaNV.Enabled = false; // Disable editing of the employee ID
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            EnableAllFields();
            if (txtMaNV.Text == string.Empty)
            {
                MessageBox.Show("Chưa nhập Mã Nhân Viên");
                txtMaNV.Focus();
                return;
            }

            DataRow update_Row = ds_nhanvien.Tables["NHANVIEN"].Rows.Find(txtMaNV.Text);

            if (update_Row != null)
            {
                update_Row["TENNV"] = txtTenNV.Text;
                update_Row["SDT"] = txtSdt.Text;
                update_Row["CCCD"] = txtCCCD.Text;
                update_Row["EMAIL"] = txtEmail.Text;
                update_Row["MATKHAU"] = txtPass.Text;
                update_Row["VAITRO"] = txtVaiTro.Text;
            }

            try
            {
                DataBingding(ds_nhanvien.Tables["NHANVIEN"]);
                SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                da.Update(ds_nhanvien, "NHANVIEN");
                MessageBox.Show("Cập nhật thành công");
                btnLuu.Enabled = false;
                grdNV.ReadOnly = true;
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
                DataTable dt_PhieuNhap = new DataTable();
                SqlDataAdapter da_PN = new SqlDataAdapter("Select * from PHIEUNHAP where MANV = '" + txtMaNV.Text + "'", cnn);

                da_PN.Fill(dt_PhieuNhap);
                if (dt_PhieuNhap.Rows.Count > 0)
                {
                    MessageBox.Show("Dữ liệu đang sử dụng không thể xóa");
                    return;
                }

                int rowIndex = grdNV.CurrentRow.Index;
                grdNV.Rows.RemoveAt(rowIndex);

                try
                {
                    SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                    da.Update(ds_nhanvien, "NHANVIEN");
                    MessageBox.Show("Thành công");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi cập nhật dữ liệu: " + ex.Message);
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

            DataRow foundRow = ds_nhanvien.Tables["NHANVIEN"].Rows.Find(searchTerm);

            if (foundRow == null)
            {
                DataRow[] foundRows = ds_nhanvien.Tables["NHANVIEN"].Select("TENNV LIKE '%" + searchTerm + "%' OR MANV = '" + searchTerm + "'");
                if (foundRows.Length == 0)
                {
                    MessageBox.Show("No matching records found.");
                    return;
                }

                foundRow = foundRows[0];
            }

            txtMaNV.Text = foundRow["MANV"].ToString();
            txtTenNV.Text = foundRow["TENNV"].ToString();
            txtSdt.Text = foundRow["SDT"].ToString();
            txtCCCD.Text = foundRow["CCCD"].ToString();
            txtEmail.Text = foundRow["EMAIL"].ToString();
            txtPass.Text = foundRow["MATKHAU"].ToString();
            txtVaiTro.Text = foundRow["VAITRO"].ToString();

            grdNV.DataSource = ds_nhanvien.Tables["NHANVIEN"];
            (grdNV.DataSource as DataTable).DefaultView.RowFilter = "MANV = '" + txtMaNV.Text + "' OR TENNV LIKE '%" + txtTenNV.Text + "%'";

            DisableAllFields();
        }

        private void ClearFields()
        {
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtSdt.Clear();
            txtCCCD.Clear();
            txtPass.Clear();
            txtEmail.Clear();
            txtVaiTro.Clear();
        }

        private void EnableAllFields()
        {
            txtMaNV.Enabled = true;
            txtTenNV.Enabled = true;
            txtSdt.Enabled = true;
            txtCCCD.Enabled = true;
            txtEmail.Enabled = true;
            txtPass.Enabled = true;
            txtVaiTro.Enabled = true;
            txtSearch.Enabled = true;
        }

        private void DisableAllFields()
        {
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtSdt.Enabled = false;
            txtCCCD.Enabled = false;
            txtEmail.Enabled = false;
            txtPass.Enabled = false;
            txtVaiTro.Enabled = false;
        }

        private void grdNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = true;
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && grdNV.Columns[e.ColumnIndex].ReadOnly == false)
            {
                EnableAllFields();
                btnLuu.Enabled = true;
            }
        }

        private void grdNV_SelectionChanged(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = true;
            // Kiểm tra xem có hàng được chọn hay không
            if (grdNV.SelectedRows.Count > 0)
            {
                // Lấy hàng hiện tại đang được chọn
                DataGridViewRow selectedRow = grdNV.SelectedRows[0];

                // Lấy đối tượng dữ liệu (DataRowView) liên kết với hàng đó
                DataRowView dataRowView = selectedRow.DataBoundItem as DataRowView;

                // Kiểm tra xem có đối tượng dữ liệu hay không
                if (dataRowView != null)
                {
                    // Truy cập các cột dữ liệu thông qua đối tượng dữ liệu
                    DataRow dataRow = dataRowView.Row;
                    txtMaNV.Text = dataRow["MaNV"].ToString();
                    txtTenNV.Text = dataRow["TenNV"].ToString();
                    txtSdt.Text = dataRow["Sdt"].ToString();
                    txtEmail.Text = dataRow["Email"].ToString();
                    txtCCCD.Text = dataRow["CCCD"].ToString();
                    txtPass.Text = dataRow["MatKhau"].ToString();
                }
            }
        }
        void DataBingding(DataTable pDT)
        {
            txtMaNV.DataBindings.Clear();
            txtTenNV.DataBindings.Clear();
            txtSdt.DataBindings.Clear();
            txtCCCD.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            txtPass.DataBindings.Clear();
            txtVaiTro.DataBindings.Clear();

            txtMaNV.DataBindings.Add("Text", pDT, "MANV");
            txtTenNV.DataBindings.Add("Text", pDT, "TENNV");
            txtSdt.DataBindings.Add("Text", pDT, "SDT");
            txtCCCD.DataBindings.Add("Text", pDT, "CCCD");
            txtEmail.DataBindings.Add("Text", pDT, "EMAIL");
            txtPass.DataBindings.Add("Text", pDT, "MATKHAU");
            txtVaiTro.DataBindings.Add("Text", pDT, "VAITRO");
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            fmRPNhanVien sl = new fmRPNhanVien();
            sl.ShowDialog();
            this.Close();
        }
    }
}