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
    public partial class fmMain : Form
    {
        public fmMain()
        {
            InitializeComponent();
        }

        private void toolStripDropDownButton_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.BackgroundImage = Properties.Resources.main;
        }

        private void toolStripMenuItem_DangNhap_Click(object sender, EventArgs e)
        {
            fmDangNhap dn = new fmDangNhap();
            dn.ShowDialog();
            this.Close();
        }
    }
}
