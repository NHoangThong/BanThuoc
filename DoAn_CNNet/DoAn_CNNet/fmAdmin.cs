using DoAn_CNNet.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_CNNet
{
    public partial class fmAdmin : Form
    {
        public fmAdmin()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem_DangNhap_Click(object sender, EventArgs e)
        {
            fmDangNhap dn = new fmDangNhap();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_QuanLyTaiKhoan_Click(object sender, EventArgs e)
        {
            //fmAccount dn = new fmAccount();
            //dn.ShowDialog();
            //dn.Close();
        }

        private void toolStripDropDownButton_QLNV_Click(object sender, EventArgs e)
        {
            fmNhanVien dn = new fmNhanVien();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_ThongTinThuoc_Click(object sender, EventArgs e)
        {
            fmThuoc dn = new fmThuoc();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_NhapThuoc_Click(object sender, EventArgs e)
        {
            fmHoaDonNhap dn = new fmHoaDonNhap();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_BanThuoc_Click(object sender, EventArgs e)
        {
            fmHoaDonXuat dn = new fmHoaDonXuat();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripDropDownButton_QLNCC_Click(object sender, EventArgs e)
        {
            fmNhaCungCap dn = new fmNhaCungCap();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripDropDownButton_KHACH_Click(object sender, EventArgs e)
        {
            fmKhachHang dn = new fmKhachHang();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripDropDownButton_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmAdmin_Load(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.BackgroundImage = Properties.Resources.admin;
        }

        private void toolStripMenuItem__Logout_Click(object sender, EventArgs e)
        {
            fmMain dn = new fmMain();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_BCNhapThuoc_Click(object sender, EventArgs e)
        {
            frmRPHoaDonNhap dn = new frmRPHoaDonNhap();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_BCBanThuoc_Click(object sender, EventArgs e)
        {
            fmReportHoaDonXuat dn = new fmReportHoaDonXuat();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_TKNhapThuoc_Click(object sender, EventArgs e)
        {
            fmThongKeNhapThuoc dn = new fmThongKeNhapThuoc();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_TKBanThuoc_Click(object sender, EventArgs e)
        {
            thongKeThuocBanRa dn = new thongKeThuocBanRa();
            dn.ShowDialog();
            dn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fmNhanVien dn = new fmNhanVien();
            dn.ShowDialog();
            dn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fmKhachHang dn = new fmKhachHang();
            dn.ShowDialog();
            dn.Close();
        }

        private void btnQLNV_Click(object sender, EventArgs e)
        {
            fmNhaCungCap dn = new fmNhaCungCap();
            dn.ShowDialog();
            dn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton_QLKH_Click(object sender, EventArgs e)
        {
            fmKhachHang dn = new fmKhachHang();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripButton_QLNCC_Click(object sender, EventArgs e)
        {
            fmNhaCungCap dn = new fmNhaCungCap();
            dn.ShowDialog();
            dn.Close();
        }

        
    }
}
