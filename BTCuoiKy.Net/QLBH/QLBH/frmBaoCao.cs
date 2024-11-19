using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmBaoCao : Form
    {
        public frmBaoCao()
        {
            InitializeComponent();
        }

        KetNoi kn = new KetNoi();

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            string query = string.Format(
                "select * from DatHang where Month(NgayDatHang) = {0} and Year(NgayDatHang) = {1}",
                txtThang.Text,
                txtNam.Text
            );
            DataSet ds = kn.LayDuLieu(query);
            dgvThongKe.DataSource = ds.Tables[0];
        }
    }
}
