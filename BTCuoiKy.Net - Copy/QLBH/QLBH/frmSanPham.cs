using System;
using System.Data;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
            
            getData();
        }

        KetNoi kn = new KetNoi();

        public void getData()
        {
            string query = "select * from Sach";
            DataSet ds = kn.LayDuLieu(query);
            dgvSanPham.DataSource = ds.Tables[0];
        }

        public void clearText()
        {
            // Đặt lại các ô nhập liệu về giá trị mặc định
            txtMaSach.Enabled = true;       // Cho phép chỉnh sửa mã sách
            btnThem.Enabled = true;         // Cho phép thêm mới
            btnSua.Enabled = false;         // Không cho phép sửa
            btnXoa.Enabled = false;         // Không cho phép xóa

            // Làm sạch tất cả các ô nhập liệu liên quan đến sách
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtTenTacGia.Text = "";
            txtNhaXuatBan.Text = "";
            txtNgayXuatBan.Text = "";
            txtTheLoai.Text = "";
            txtGia.Text = "";
            txtSoLuong.Text = "";
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
            getData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Đảm bảo cột trong bảng 'SanPham' khớp với tên cột trong câu lệnh SQL
            // Kiểm tra thông tin
            if (string.IsNullOrEmpty(txtMaSach.Text) ||
                string.IsNullOrEmpty(txtTenSach.Text) ||
                string.IsNullOrEmpty(txtTenTacGia.Text) ||
                string.IsNullOrEmpty(txtNhaXuatBan.Text) ||
                string.IsNullOrEmpty(txtNgayXuatBan.Text) ||
                string.IsNullOrEmpty(txtTheLoai.Text) ||
                string.IsNullOrEmpty(txtGia.Text) ||
                string.IsNullOrEmpty(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            // Kiểm tra mã sách có tồn tại không
            string checkQuery = string.Format("SELECT COUNT(*) FROM Sach WHERE MaSach = '{0}'", txtMaSach.Text);
            DataSet ds = kn.LayDuLieu(checkQuery);
            if (ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
            {
                MessageBox.Show("Mã sách đã tồn tại, vui lòng nhập mã khác!");
                return;
            }

            // Chuẩn bị câu lệnh SQL thêm mới
            string query = string.Format(
<<<<<<< HEAD
                "INSERT INTO Sach (MaSach, TenSach, TenTacGia, NhaXuatBan, NgayXuatBan,MaTheLoai ,Gia, SoLuong) " +
                "VALUES ('{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}', N'{6}',N'{7}')",
=======
                "INSERT INTO Sach (MaSach, TenSach, TenTacGia, NhaXuatBan, NgayXuatBan, MaTheLoai, Gia, SoLuong) " +
                "VALUES ('{0}', N'{1}', N'{2}', N'{3}', '{4}', '{5}', {6}, {7})",
>>>>>>> e7a74af6ed5da090ba69ef5bea7af1291b9a8b08
                txtMaSach.Text,
                txtTenSach.Text,
                txtTenTacGia.Text,
                txtNhaXuatBan.Text,
                txtNgayXuatBan.Text,
                txtTheLoai.Text,
                Convert.ToDecimal(txtGia.Text),    // Đảm bảo giá trị giá là số
                Convert.ToInt32(txtSoLuong.Text)  // Đảm bảo giá trị số lượng là số
            );

            // Thực thi câu lệnh
            try
            {
                if (kn.ThucThi(query))
                {
                    MessageBox.Show("Thêm mới thành công!");
                    btnLamMoi.PerformClick();
                }
                else
                {
                    MessageBox.Show("Thêm mới thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = string.Format(
                "UPDATE Sach SET TenSach = N'{1}', TenTacGia = N'{2}', NhaXuatBan = N'{3}', NgayXuatBan = N'{4}', MaTheLoai = '{5}', Gia = {6}, SoLuong = {7} " +
                "WHERE MaSach = '{0}'",
                txtMaSach.Text,
                txtTenSach.Text,
                txtTenTacGia.Text,
                txtNhaXuatBan.Text,
                txtNgayXuatBan.Text,
                txtTheLoai.Text,
                Convert.ToDecimal(txtGia.Text), // Convert để tránh lỗi khi giá là số
                Convert.ToInt32(txtSoLuong.Text) // Convert để tránh lỗi khi số lượng là kiểu số
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

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                // Kiểm tra nếu giá trị cột không phải null
                //txtMaSach.Enabled = false;  // Không cho phép chỉnh sửa mã sách
                btnThem.Enabled = false;     // Không cho phép thêm mới
                btnSua.Enabled = true;       // Cho phép sửa
                btnXoa.Enabled = true;       // Cho phép xóa

                // Gán giá trị từ DataGridView vào các TextBox, kiểm tra null trước khi gán
                txtMaSach.Text = dgvSanPham.Rows[r].Cells["MaSach"].Value != DBNull.Value ? dgvSanPham.Rows[r].Cells["MaSach"].Value.ToString() : "";
                txtTenSach.Text = dgvSanPham.Rows[r].Cells["TenSach"].Value != DBNull.Value ? dgvSanPham.Rows[r].Cells["TenSach"].Value.ToString() : "";
                txtTenTacGia.Text = dgvSanPham.Rows[r].Cells["TenTacGia"].Value != DBNull.Value ? dgvSanPham.Rows[r].Cells["TenTacGia"].Value.ToString() : "";
                txtNhaXuatBan.Text = dgvSanPham.Rows[r].Cells["NhaXuatBan"].Value != DBNull.Value ? dgvSanPham.Rows[r].Cells["NhaXuatBan"].Value.ToString() : "";
                txtNgayXuatBan.Text = dgvSanPham.Rows[r].Cells["NgayXuatBan"].Value != DBNull.Value ? dgvSanPham.Rows[r].Cells["NgayXuatBan"].Value.ToString() : "";
                txtTheLoai.Text = dgvSanPham.Rows[r].Cells["MaTheLoai"].Value != DBNull.Value ? dgvSanPham.Rows[r].Cells["MaTheLoai"].Value.ToString() : "";
                txtGia.Text = dgvSanPham.Rows[r].Cells["Gia"].Value != DBNull.Value ? dgvSanPham.Rows[r].Cells["Gia"].Value.ToString() : "";
                txtSoLuong.Text = dgvSanPham.Rows[r].Cells["SoLuong"].Value != DBNull.Value ? dgvSanPham.Rows[r].Cells["SoLuong"].Value.ToString() : "";
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu mã sách không rỗng
            if (string.IsNullOrEmpty(txtMaSach.Text))
            {
                MessageBox.Show("Vui lòng chọn sách để xóa!");
                return;
            }

            // Câu lệnh SQL xóa dữ liệu
            string query = string.Format(
                "DELETE FROM Sach WHERE MaSach = '{0}'",
                txtMaSach.Text
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

        private void dgvSanPham_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvSanPham.HitTest(e.X, e.Y);

            // Nếu người dùng nhấp vào một hàng
            if (hit.RowIndex >= 0)
            {
                // Lấy chỉ số của hàng mà người dùng nhấp vào
                int rowIndex = hit.RowIndex;

                // Gán giá trị từ các ô trong DataGridView vào các TextBox
                txtMaSach.Text = dgvSanPham.Rows[rowIndex].Cells["MaSach"].Value.ToString();
                txtTenSach.Text = dgvSanPham.Rows[rowIndex].Cells["TenSach"].Value.ToString();
                txtTenTacGia.Text = dgvSanPham.Rows[rowIndex].Cells["TenTacGia"].Value.ToString();
                txtNhaXuatBan.Text = dgvSanPham.Rows[rowIndex].Cells["NhaXuatBan"].Value.ToString();
                txtNgayXuatBan.Text = dgvSanPham.Rows[rowIndex].Cells["NgayXuatBan"].Value.ToString();
                txtTheLoai.Text = dgvSanPham.Rows[rowIndex].Cells["MaTheLoai"].Value.ToString();
                txtGia.Text = dgvSanPham.Rows[rowIndex].Cells["Gia"].Value.ToString();
                txtSoLuong.Text = dgvSanPham.Rows[rowIndex].Cells["SoLuong"].Value.ToString();

                // Nếu cần, có thể thiết lập các button để phù hợp với thao tác chỉnh sửa hoặc xóa
                //stxtMaSach.Enabled = false;  // Không cho phép chỉnh sửa mã sách
                btnThem.Enabled = false;    // Không cho phép thêm mới
                btnSua.Enabled = true;      // Cho phép sửa
                btnXoa.Enabled = true;      // Cho phép xóa
            }

        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {

            // Câu lệnh SQL để tìm kiếm sách theo tên sách
            string query = string.Format(
                "SELECT * FROM Sach WHERE TenSach LIKE N'%{0}%'",
                txtTimKiem.Text
            );

            // Lấy dữ liệu từ cơ sở dữ liệu và gán cho DataGridView
            DataSet ds = kn.LayDuLieu(query);
            dgvSanPham.DataSource = ds.Tables[0];
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {

        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
