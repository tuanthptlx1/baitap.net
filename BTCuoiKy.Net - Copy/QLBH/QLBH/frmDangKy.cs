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
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        KetNoi kn = new KetNoi();
        private void DangKyTaiKhoan()
        {
            // Kiểm tra nếu các trường bị bỏ trống
            if (string.IsNullOrEmpty(txtTaiKhoan.Text) || string.IsNullOrEmpty(txtMatKhau.Text) || string.IsNullOrEmpty(txtNhapLaiMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra mật khẩu và nhập lại mật khẩu
            if (txtMatKhau.Text != txtNhapLaiMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu không khớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tài khoản đã tồn tại hay chưa
            string checkQuery = string.Format("SELECT * FROM DANGNHAP WHERE username = '{0}'", txtTaiKhoan.Text);
            DataSet ds = kn.LayDuLieu(checkQuery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("Tài khoản đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm tài khoản mới vào cơ sở dữ liệu
            string insertQuery = string.Format(
                "INSERT INTO DANGNHAP (username, password) VALUES ('{0}', '{1}')",
                txtTaiKhoan.Text,
                txtMatKhau.Text
            );
            bool result = kn.ThucThi(insertQuery);
            if (result)
            {
                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng ký thất bại. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtTaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNhapLaiMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            DangKyTaiKhoan();
        }

        private void lblDangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangNhap frm = new frmDangNhap();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.FormClosed += (s, args) => this.Close();
            this.Hide();
            frm.ShowDialog();
        }
    }
}
