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
    public partial class fmSalesman : Form
    {
        public fmSalesman()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem_DangNhap_Click(object sender, EventArgs e)
        {
            fmDangNhap dn = new fmDangNhap();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripDropDownButton_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmSalesman_Load(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.BackgroundImage = Properties.Resources.sales;
        }

        private void toolStripMenuItem_Logout_Click(object sender, EventArgs e)
        {
            fmMain dn = new fmMain();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_ThongTinThuoc_Click_1(object sender, EventArgs e)
        {
            fmThuoc dn = new fmThuoc();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_NhapThuoc_Click_1(object sender, EventArgs e)
        {
            fmHoaDonNhap dn = new fmHoaDonNhap();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_BanThuoc_Click_1(object sender, EventArgs e)
        {
            fmHoaDonXuat dn = new fmHoaDonXuat();
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

        private void toolStripMenuItem_TKThuocNhap_Click(object sender, EventArgs e)
        {
            fmThongKeNhapThuoc dn = new fmThongKeNhapThuoc();
            dn.ShowDialog();
            dn.Close();
        }

        private void toolStripMenuItem_TKThuocBan_Click(object sender, EventArgs e)
        {
            thongKeThuocBanRa dn = new thongKeThuocBanRa();
            dn.ShowDialog();
            dn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fmThuoc dn = new fmThuoc();
            dn.ShowDialog();
            dn.Close();
        }

      

    }
}
