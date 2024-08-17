﻿using System;
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
    public partial class fmNhaCungCap : Form
    {
      
       
        string strconnect = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        SqlConnection conn = null;
        SqlDataAdapter da_NCC = null;
        DataTable dt_NCC = null;
        DataSet ds_NCC = new DataSet();

        public fmNhaCungCap()
        {
            InitializeComponent();
        }

        bool Them;

        void LoadData()
        {
            try
            {
                conn = new SqlConnection(strconnect);
                da_NCC = new SqlDataAdapter("select * from NHACUNGCAP", conn);
                dt_NCC = new DataTable();
                dt_NCC.Clear();
                da_NCC.Fill(dt_NCC);
                dataGridView1.DataSource = dt_NCC;
                this.textBoxMaNhaCungCap.ResetText();
                this.textBoxTenNhaCungCap.ResetText();
                this.textBoxSoDienThoai.ResetText();
                this.textBoxEmail.ResetText();
                //không cho thao tác trên nút Lưu
                this.btnLuu.Enabled = false;
                //Cho thao tác trên các nút Thêm/Xóa/Sửa/Đóng
                this.btnThem.Enabled = true;
                this.btnXoa.Enabled = true;
                this.btnSua.Enabled = true;
                this.btnDong.Enabled = true;
            }
            catch (SqlException)
            {
                MessageBox.Show("Lỗi");
            }
        }
        private void fmNhaCungCap_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(strconnect);
                da_NCC = new SqlDataAdapter("select * from NHACUNGCAP", conn);
                dt_NCC = new DataTable();
                dt_NCC.Clear();
                da_NCC.Fill(dt_NCC);
                dataGridView1.DataSource = dt_NCC;
            }
            catch (SqlException)
            {
                MessageBox.Show("Lỗi");
            }
            textBoxMaNhaCungCap.Enabled = textBoxTenNhaCungCap.Enabled = textBoxSoDienThoai.Enabled = textBoxEmail.Enabled = false;
            btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = false;

        }

        private void fmNhaCungCap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            textBoxMaNhaCungCap.Enabled = textBoxTenNhaCungCap.Enabled = textBoxSoDienThoai.Enabled = textBoxEmail.Enabled = true;

            Them = true;
            //Xóa trống các đối tượng
            this.textBoxMaNhaCungCap.ResetText();
            this.textBoxTenNhaCungCap.ResetText();
            this.textBoxSoDienThoai.ResetText();
            this.textBoxEmail.ResetText();
            //button Lưu có hiệu lực
            this.btnLuu.Enabled = true; ;
            //không cho thaoo tác trên các nút Thêm/Xóa/Sửa/Đóng
            this.btnThem.Enabled = false; ;
            this.btnXoa.Enabled = false; ;
            this.btnSua.Enabled = false;
            this.btnDong.Enabled = false;
            //đưa con trỏ đến textfield textBoxMaNhaCungCap
            this.textBoxMaNhaCungCap.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                int r = dataGridView1.CurrentCell.RowIndex;
                string strMANCC = dataGridView1.Rows[r].Cells[0].Value.ToString();
                cmd.CommandText = System.String.Concat("delete from NHACUNGCAP where MANHACUNGCAP = '" + strMANCC + "'");
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                LoadData();
                MessageBox.Show("Đã xóa xong!");
            }
            catch (SqlException)
            {
                MessageBox.Show("Không xóa được. Lỗi rồi!!!");
            }
            conn.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            textBoxTenNhaCungCap.Enabled = true;
            textBoxSoDienThoai.Enabled = true;
            textBoxEmail.Enabled = true;
            textBoxMaNhaCungCap.Enabled = false;

            Them = false;
            int r = dataGridView1.CurrentCell.RowIndex;
            this.textBoxMaNhaCungCap.Text = dataGridView1.Rows[r].Cells[0].Value.ToString();
            this.textBoxTenNhaCungCap.Text = dataGridView1.Rows[r].Cells[1].Value.ToString();
            this.textBoxSoDienThoai.Text = dataGridView1.Rows[r].Cells[2].Value.ToString();
            this.textBoxEmail.Text = dataGridView1.Rows[r].Cells[3].Value.ToString();
            this.btnLuu.Enabled = true; ;
            this.btnThem.Enabled = false; ;
            this.btnXoa.Enabled = false; ;
            this.btnSua.Enabled = false;
            this.btnXoa.Enabled = false;
            this.btnDong.Enabled = false;
            this.textBoxMaNhaCungCap.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            conn.Open();
            //kiểm tra thông tin vừa nhập hoặc sửa cho phù hợp
            if (textBoxMaNhaCungCap.Text == string.Empty)
            {
                MessageBox.Show("Chưa nhập Mã nhà cung cấp");
                textBoxMaNhaCungCap.Focus();
                return;
            }
            if (textBoxTenNhaCungCap.Text == string.Empty)
            {
                MessageBox.Show("Chưa nhập tên nhà cung cấp");
                textBoxTenNhaCungCap.Focus();
                return;
            }
            if (textBoxSoDienThoai.Text == string.Empty)
            {
                MessageBox.Show("Chưa nhập SĐT");
                textBoxSoDienThoai.Focus();
                return;
            }
            if (textBoxEmail.Text == string.Empty)
            {
                MessageBox.Show("Chưa nhập Email");
                textBoxEmail.Focus();
                return;
            }
            if (Them)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = System.String.Concat("insert into NHACUNGCAP values(" + "'" + this.textBoxMaNhaCungCap.Text.ToString() + "','" + this.textBoxTenNhaCungCap.Text.ToString() + "','" + this.textBoxSoDienThoai.Text.ToString() + "','" + this.textBoxEmail.Text.ToString() + "')");
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    LoadData();
                    MessageBox.Show("Đã thêm thành công");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Thất bại!!!");
                }
            }
            if (!Them)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    int r = dataGridView1.CurrentCell.RowIndex;
                    string strMANCC = dataGridView1.Rows[r].Cells[0].Value.ToString();
                    cmd.CommandText = System.String.Concat("Update NHACUNGCAP set TENNHACUNGCAP='" + this.textBoxTenNhaCungCap.Text.ToString() + "',SDT='" + this.textBoxSoDienThoai.Text.ToString() + "',EMAIL='" + this.textBoxEmail.Text.ToString() + "'where MANHACUNGCAP='" + strMANCC + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    LoadData();
                    MessageBox.Show("Đã sửa thành công");
                }
                catch (SqlException)
                {
                    MessageBox.Show("Thất bại!!!");
                }
            }
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text;

            // Filter the DataTable based on the search term
            DataView dataView = dt_NCC.DefaultView;
            dataView.RowFilter = $"TENNHACUNGCAP LIKE '%{searchTerm}%'";

            // Update the DataGridView with the filtered data
            dataGridView1.DataSource = dataView;

            // Clear the TextBoxes
            ClearTextBoxes();

        }

        private void ClearTextBoxes()
        {
            textBoxTenNhaCungCap.Text = string.Empty;
            textBoxSoDienThoai.Text = string.Empty;
            textBoxEmail.Text = string.Empty;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = true;
        }
    }
}
