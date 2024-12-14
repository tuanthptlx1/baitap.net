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
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
            getData();
        }

        KetNoi kn = new KetNoi();

        public void getData()
        {
            string query = "select * from HoaDon";
            DataSet ds = kn.LayDuLieu(query);
            dgvHoaDon.DataSource = ds.Tables[0];
        }

        public void clearText()
        {
            // Đặt lại các ô nhập liệu về giá trị mặc định
            txtMaHoaDon.Enabled = true;       // Cho phép chỉnh sửa mã sách
            btnThem.Enabled = true;         // Cho phép thêm mới
            btnSua.Enabled = false;         // Không cho phép sửa
            btnXoa.Enabled = false;         // Không cho phép xóa

            // Làm sạch tất cả các ô nhập liệu liên quan đến sách
            txtMaHoaDon.Text = "";
            txtNgayLap.Text = "";
            txtMaKhachHang.Text = "";
            txtTongTien.Text = "";

        }


        private void frmHoaDon_Load(object sender, EventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            getData();
            clearText();   
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = string.Format(
                "INSERT INTO HoaDon (MaHoaDon, NgayLap, MaKhachHang,TongTien ) " +
                "VALUES ('{0}', '{1}', '{2}','{3}')",
                txtMaHoaDon.Text,
                txtNgayLap.Text,
                txtMaKhachHang.Text,
                txtTongTien.Text

            );

            if (kn.ThucThi(query) == true)
            {
                MessageBox.Show("Thêm mới thành công!");
                btnLamMoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Thêm mới thất bại!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = string.Format(
                "UPDATE HoaDon SET NgayLap = N'{1}', MaKhachHang = N'{2}',TongTien = {3} "+
                "WHERE MaHoaDon = '{0}'",
                txtMaHoaDon.Text,
                txtNgayLap.Text,
                txtMaKhachHang.Text,
                Convert.ToInt32(txtTongTien.Text)

            );

            if (kn.ThucThi(query) == true)
            {
                MessageBox.Show("Sửa thành công!");
                btnLamMoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Sửa thất bại!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHoaDon.Text))
            {
                MessageBox.Show("Vui lòng chọn sách để xóa!");
                return;
            }

            // Câu lệnh SQL xóa dữ liệu
            string query = string.Format(
                "DELETE FROM HoaDon WHERE MaHoaDon = '{0}'",
                txtMaHoaDon.Text
            );

            // Thực thi câu lệnh
            if (kn.ThucThi(query) == true)
            {
                MessageBox.Show("Xóa thành công!");
                btnLamMoi.PerformClick();  // Làm mới dữ liệu
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Câu lệnh SQL để tìm kiếm sách theo tên sách
            string query = string.Format(
                "SELECT * FROM HoaDon WHERE MaHoaDon LIKE N'%{0}%'",
                txtTimKiem.Text
            );

            // Lấy dữ liệu từ cơ sở dữ liệu và gán cho DataGridView
            DataSet ds = kn.LayDuLieu(query);
            dgvHoaDon.DataSource = ds.Tables[0];
        }

        private void dgvHoaDon_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                // Kiểm tra nếu giá trị cột không phải null
                //txtHoaDon.Enabled = false;  // Không cho phép chỉnh sửa mã sách
                btnThem.Enabled = false;     // Không cho phép thêm mới
                btnSua.Enabled = true;       // Cho phép sửa
                btnXoa.Enabled = true;       // Cho phép xóa

                // Gán giá trị từ DataGridView vào các TextBox, kiểm tra null trước khi gán
                txtMaHoaDon.Text = dgvHoaDon.Rows[r].Cells["MaHoaDon"].Value != DBNull.Value ? dgvHoaDon.Rows[r].Cells["MaHoaDon"].Value.ToString() : "";
                txtNgayLap.Text = dgvHoaDon.Rows[r].Cells["NgayLap"].Value != DBNull.Value ? dgvHoaDon.Rows[r].Cells["NgayLap"].Value.ToString() : "";
                txtMaKhachHang.Text = dgvHoaDon.Rows[r].Cells["MaKhachHang"].Value != DBNull.Value ? dgvHoaDon.Rows[r].Cells["MaKhachHang"].Value.ToString() : "";
                txtTongTien.Text = dgvHoaDon.Rows[r].Cells["TongTien"].Value != DBNull.Value ? dgvHoaDon.Rows[r].Cells["TongTien"].Value.ToString() : "";

            }
        }

        private void dgvHoaDon_MouseClick_1(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvHoaDon.HitTest(e.X, e.Y);

            // Nếu người dùng nhấp vào một hàng
            if (hit.RowIndex >= 0)
            {
                // Lấy chỉ số của hàng mà người dùng nhấp vào
                int rowIndex = hit.RowIndex;

                // Gán giá trị từ các ô trong DataGridView vào các TextBox
                txtMaHoaDon.Text = dgvHoaDon.Rows[rowIndex].Cells["MaHoaDon"].Value.ToString();
                txtNgayLap.Text = dgvHoaDon.Rows[rowIndex].Cells["NgayLap"].Value.ToString();
                txtMaKhachHang.Text = dgvHoaDon.Rows[rowIndex].Cells["MaKhachHang"].Value.ToString();
                txtTongTien.Text = dgvHoaDon.Rows[rowIndex].Cells["TongTien"].Value.ToString();


                // Nếu cần, có thể thiết lập các button để phù hợp với thao tác chỉnh sửa hoặc xóa
                //stxtHoaDon.Enabled = false;  // Không cho phép chỉnh sửa mã sách
                btnThem.Enabled = false;    // Không cho phép thêm mới
                btnSua.Enabled = true;      // Cho phép sửa
                btnXoa.Enabled = true;      // Cho phép xóa
            }
        }
    }
}

