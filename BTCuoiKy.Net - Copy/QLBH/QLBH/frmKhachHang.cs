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
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
            getData();
        }
        KetNoi kn = new KetNoi();
        public void getData()
        {
            string query = "select * from KhachHang";
            DataSet ds = kn.LayDuLieu(query);
            dgvKhachHang.DataSource = ds.Tables[0];
        }

        public void clearText()
        {
            // Đặt lại các ô nhập liệu về giá trị mặc định
            txtMaKhachHang.Enabled = true;       // Cho phép chỉnh sửa mã sách
            btnThem.Enabled = true;         // Cho phép thêm mới
            btnSua.Enabled = false;         // Không cho phép sửa
            btnXoa.Enabled = false;         // Không cho phép xóa

            // Làm sạch tất cả các ô nhập liệu liên quan đến sách
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
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
               "INSERT INTO KhachHang (MaKhachHang, TenKhachHang, DiaChi, DienThoai, Email) " +
               "VALUES ('{0}', N'{1}', N'{2}', N'{3}', N'{4}')",
               txtMaKhachHang.Text,
           txtTenKhachHang.Text,

       txtDiaChi.Text,
       txtDienThoai.Text,
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
"UPDATE KhachHang SET TenKhachHang=N'{1}',  DiaChi=N'{2}', DienThoai='{3}', Email='{4}' " +
"WHERE MaKhachHang='{0}'",
txtMaKhachHang.Text,
txtTenKhachHang.Text,

txtDiaChi.Text,
txtDienThoai.Text,
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
            if (string.IsNullOrEmpty(txtMaKhachHang.Text))
            {
                MessageBox.Show("Vui lòng chọn sách để xóa!");
                return;
            }

            // Câu lệnh SQL xóa dữ liệu
            string query = string.Format(
                "DELETE FROM KhachHang WHERE MaKhachHang = '{0}'",
                txtMaKhachHang.Text
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
                "SELECT * FROM KhachHang WHERE TenKhachHang LIKE N'%{0}%'",
                txtTimKiem.Text
            );

            // Lấy dữ liệu từ cơ sở dữ liệu và gán cho DataGridView
            DataSet ds = kn.LayDuLieu(query);
            dgvKhachHang.DataSource = ds.Tables[0];
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                txtMaKhachHang.Enabled = false;  // Không cho phép chỉnh sửa mã nhân viên
                btnThem.Enabled = false;       // Không cho phép thêm mới
                btnSua.Enabled = true;         // Cho phép sửa
                btnXoa.Enabled = true;         // Cho phép xóa

                txtMaKhachHang.Text = dgvKhachHang.Rows[r].Cells["MaKhachHang"].Value?.ToString() ?? "";
                txtTenKhachHang.Text = dgvKhachHang.Rows[r].Cells["TenKhachHang"].Value?.ToString() ?? "";

                txtDiaChi.Text = dgvKhachHang.Rows[r].Cells["DiaChi"].Value?.ToString() ?? "";
                txtDienThoai.Text = dgvKhachHang.Rows[r].Cells["DienThoai"].Value?.ToString() ?? "";
                txtEmail.Text = dgvKhachHang.Rows[r].Cells["Email"].Value?.ToString() ?? "";
            }
        }

        private void dgvKhachHang_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvKhachHang.HitTest(e.X, e.Y);
            if (hit.RowIndex >= 0)
            {
                // Lấy chỉ số của hàng mà người dùng nhấp vào
                int rowIndex = hit.RowIndex;

                // Gán giá trị từ các ô trong DataGridView vào các TextBox
                txtMaKhachHang.Text = dgvKhachHang.Rows[rowIndex].Cells["MaKhachHang"].Value.ToString();
                txtTenKhachHang.Text = dgvKhachHang.Rows[rowIndex].Cells["TenKhachHang"].Value.ToString();
                txtDiaChi.Text = dgvKhachHang.Rows[rowIndex].Cells["DiaChi"].Value.ToString();
                txtDienThoai.Text = dgvKhachHang.Rows[rowIndex].Cells["DienThoai"].Value.ToString();
                txtEmail.Text = dgvKhachHang.Rows[rowIndex].Cells["Email"].Value.ToString();

                // Nếu cần, có thể thiết lập các button để phù hợp với thao tác chỉnh sửa hoặc xóa
                //stxtMaKhachHang.Enabled = false;  // Không cho phép chỉnh sửa mã sách
                btnThem.Enabled = false;    // Không cho phép thêm mới
                btnSua.Enabled = true;      // Cho phép sửa
                btnXoa.Enabled = true;      // Cho phép xóa
            }
        }
    }
}
