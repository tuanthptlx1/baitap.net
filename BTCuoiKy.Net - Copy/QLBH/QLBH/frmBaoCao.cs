﻿using System;
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
                "select * from HoaDon where Month(NgayLap) = {0} and Year(NgayLap) = {1}",
                txtThang.Text,
                txtNam.Text
            );
            DataSet ds = kn.LayDuLieu(query);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu ! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgvThongKe.DataSource = ds.Tables[0];
            }
        }
    }
}
