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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
            getData();
        }
        KetNoi kn = new KetNoi();
        public void getData()
        {
            string query = "select * from NhanVien";
            DataSet ds = kn.LayDuLieu(query);
            dgvNhanVien.DataSource = ds.Tables[0];
        }

        public void clearText()
        {
            // Đặt lại các ô nhập liệu về giá trị mặc định
            txtMaNhanVien.Enabled = true;       // Cho phép chỉnh sửa mã sách
            btnThem.Enabled = true;         // Cho phép thêm mới
            btnSua.Enabled = false;         // Không cho phép sửa
            btnXoa.Enabled = false;         // Không cho phép xóa

            // Làm sạch tất cả các ô nhập liệu liên quan đến sách
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtChucVu.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
            getData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = string.Format(
                 "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, ChucVu, DiaChi, DienThoai, Email) " +
                 "VALUES ('{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}')",
                 txtMaNhanVien.Text,
             txtTenNhanVien.Text,
             txtChucVu.Text,
         txtDiaChi.Text,
         txtSDT.Text,
         txtEmail.Text
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
  "UPDATE NhanVien SET TenNhanVien=N'{1}', ChucVu=N'{2}', DiaChi=N'{3}', DienThoai='{4}', Email='{5}' " +
  "WHERE MaNhanVien='{0}'",
  txtMaNhanVien.Text,
  txtTenNhanVien.Text,
  txtChucVu.Text,
  txtDiaChi.Text,
  txtSDT.Text,
  txtEmail.Text
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

            // Kiểm tra nếu mã sách không rỗng
            if (string.IsNullOrEmpty(txtMaNhanVien.Text))
            {
                MessageBox.Show("Vui lòng chọn sách để xóa!");
                return;
            }

            // Câu lệnh SQL xóa dữ liệu
            string query = string.Format(
                "DELETE FROM NhanVien WHERE MaNhanVien = '{0}'",
                txtMaNhanVien.Text
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
                "SELECT * FROM NhanVien WHERE TenNhanVien LIKE N'%{0}%'",
                txtTimKiem.Text
            );

            // Lấy dữ liệu từ cơ sở dữ liệu và gán cho DataGridView
            DataSet ds = kn.LayDuLieu(query);
            dgvNhanVien.DataSource = ds.Tables[0];
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaNhanVien.Enabled = false;  // Không cho phép chỉnh sửa mã nhân viên
                btnThem.Enabled = false;       // Không cho phép thêm mới
                btnSua.Enabled = true;         // Cho phép sửa
                btnXoa.Enabled = true;         // Cho phép xóa

                txtMaNhanVien.Text = dgvNhanVien.Rows[r].Cells["MaNhanVien"].Value?.ToString() ?? "";
                txtTenNhanVien.Text = dgvNhanVien.Rows[r].Cells["TenNhanVien"].Value?.ToString() ?? "";
                txtChucVu.Text = dgvNhanVien.Rows[r].Cells["ChucVu"].Value?.ToString() ?? "";
                txtDiaChi.Text = dgvNhanVien.Rows[r].Cells["DiaChi"].Value?.ToString() ?? "";
                txtSDT.Text = dgvNhanVien.Rows[r].Cells["DienThoai"].Value?.ToString() ?? "";
                txtEmail.Text = dgvNhanVien.Rows[r].Cells["Email"].Value?.ToString() ?? "";
            }
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvNhanVien_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvNhanVien.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0)
            {
                // Lấy chỉ số của hàng mà người dùng nhấp vào
                int rowIndex = hit.RowIndex;

                // Gán giá trị từ các ô trong DataGridView vào các TextBox
                txtMaNhanVien.Text = dgvNhanVien.Rows[rowIndex].Cells["MaNhanVien"].Value.ToString();
                txtTenNhanVien.Text = dgvNhanVien.Rows[rowIndex].Cells["TenNhanVien"].Value.ToString();
                txtChucVu.Text = dgvNhanVien.Rows[rowIndex].Cells["ChucVu"].Value.ToString();
                txtDiaChi.Text = dgvNhanVien.Rows[rowIndex].Cells["DiaChi"].Value.ToString();
                txtSDT.Text = dgvNhanVien.Rows[rowIndex].Cells["DienThoai"].Value.ToString();
                txtEmail.Text = dgvNhanVien.Rows[rowIndex].Cells["Email"].Value.ToString();

                // Nếu cần, có thể thiết lập các button để phù hợp với thao tác chỉnh sửa hoặc xóa
                //stxtMaNhanVien.Enabled = false;  // Không cho phép chỉnh sửa mã sách
                btnThem.Enabled = false;    // Không cho phép thêm mới
                btnSua.Enabled = true;      // Cho phép sửa
                btnXoa.Enabled = true;      // Cho phép xóa
            }
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {

        }
    }
}
