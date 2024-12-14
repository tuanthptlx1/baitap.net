using System;
using System.Data;
using System.Windows.Forms;

namespace QLBH
{
    public partial class frmTacGia : Form
    {
        public frmTacGia()
        {
            InitializeComponent();
            getData();
        }
        KetNoi kn = new KetNoi();
        public void getData()
        {
            string query = "select * from TacGia";
            DataSet ds = kn.LayDuLieu(query);
            dgvTacGia.DataSource = ds.Tables[0];
        }

        public void clearText()
        {
            // Đặt lại các ô nhập liệu về giá trị mặc định
            txtMaSach.Enabled = true;       // Cho phép chỉnh sửa mã sách
            btnThem.Enabled = true;         // Cho phép thêm mới
            btnSua.Enabled = false;         // Không cho phép sửa
            btnXoa.Enabled = false;         // Không cho phép xóa

            // Làm sạch tất cả các ô nhập liệu liên quan đến sách
            txtMaTacGia.Text = "";
            txtTenTacGia.Text = "";
            txtMaSach.Text = "";
            txtGioiThieu.Text = "";
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
                "INSERT INTO TacGia (MaTacGia, TenTacGia, MaSach, GioiThieu, DiaChi, DienThoai, Email) " +
                "VALUES ('{0}', N'{1}', N'{2}', N'{3}', N'{4}', '{5}', '{6}')",
                txtMaTacGia.Text,
                txtTenTacGia.Text,
                txtMaSach.Text,
                txtGioiThieu.Text,
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
                "UPDATE TacGia SET TenTacGia = N'{1}', MaSach = N'{2}', GioiThieu = N'{3}', DiaChi = N'{4}', DienThoai = {5}, Email = {6} " +
                "WHERE MaTacGia = '{0}'",
                txtMaTacGia.Text,
                txtTenTacGia.Text,
                txtMaSach.Text,
                txtGioiThieu.Text,
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

        private void dgvTacGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                // Kiểm tra nếu giá trị cột không phải null
                //txtMaTacGia.Enabled = false;  // Không cho phép chỉnh sửa mã tác giả
                btnThem.Enabled = false;      // Không cho phép thêm mới
                btnSua.Enabled = true;        // Cho phép sửa
                btnXoa.Enabled = true;        // Cho phép xóa

                // Gán giá trị từ DataGridView vào các TextBox, kiểm tra null trước khi gán
                txtMaTacGia.Text = dgvTacGia.Rows[r].Cells["MaTacGia"].Value != DBNull.Value ? dgvTacGia.Rows[r].Cells["MaTacGia"].Value.ToString() : "";
                txtTenTacGia.Text = dgvTacGia.Rows[r].Cells["TenTacGia"].Value != DBNull.Value ? dgvTacGia.Rows[r].Cells["TenTacGia"].Value.ToString() : "";
                txtMaSach.Text = dgvTacGia.Rows[r].Cells["MaSach"].Value != DBNull.Value ? dgvTacGia.Rows[r].Cells["MaSach"].Value.ToString() : "";
                txtGioiThieu.Text = dgvTacGia.Rows[r].Cells["GioiThieu"].Value != DBNull.Value ? dgvTacGia.Rows[r].Cells["GioiThieu"].Value.ToString() : "";
                txtDiaChi.Text = dgvTacGia.Rows[r].Cells["DiaChi"].Value != DBNull.Value ? dgvTacGia.Rows[r].Cells["DiaChi"].Value.ToString() : "";
                txtSDT.Text = dgvTacGia.Rows[r].Cells["DienThoai"].Value != DBNull.Value ? dgvTacGia.Rows[r].Cells["DienThoai"].Value.ToString() : "";
                txtEmail.Text = dgvTacGia.Rows[r].Cells["Email"].Value != DBNull.Value ? dgvTacGia.Rows[r].Cells["Email"].Value.ToString() : "";
            }
        }

        private void dgvTacGia_MouseClick(object sender, MouseEventArgs e)
        {
            int rowIndex = dgvTacGia.HitTest(e.X, e.Y).RowIndex;

            if (rowIndex >= 0)
            {
                // Kiểm tra nếu giá trị cột không phải null
                //txtMaTacGia.Enabled = false;  // Không cho phép chỉnh sửa mã tác giả
                btnThem.Enabled = false;      // Không cho phép thêm mới
                btnSua.Enabled = true;        // Cho phép sửa
                btnXoa.Enabled = true;        // Cho phép xóa

                // Gán giá trị từ DataGridView vào các TextBox, kiểm tra null trước khi gán
                txtMaTacGia.Text = dgvTacGia.Rows[rowIndex].Cells["MaTacGia"].Value != DBNull.Value ? dgvTacGia.Rows[rowIndex].Cells["MaTacGia"].Value.ToString() : "";
                txtTenTacGia.Text = dgvTacGia.Rows[rowIndex].Cells["TenTacGia"].Value != DBNull.Value ? dgvTacGia.Rows[rowIndex].Cells["TenTacGia"].Value.ToString() : "";
                txtMaSach.Text = dgvTacGia.Rows[rowIndex].Cells["MaSach"].Value != DBNull.Value ? dgvTacGia.Rows[rowIndex].Cells["MaSach"].Value.ToString() : "";
                txtGioiThieu.Text = dgvTacGia.Rows[rowIndex].Cells["GioiThieu"].Value != DBNull.Value ? dgvTacGia.Rows[rowIndex].Cells["GioiThieu"].Value.ToString() : "";
                txtDiaChi.Text = dgvTacGia.Rows[rowIndex].Cells["DiaChi"].Value != DBNull.Value ? dgvTacGia.Rows[rowIndex].Cells["DiaChi"].Value.ToString() : "";
                txtSDT.Text = dgvTacGia.Rows[rowIndex].Cells["DienThoai"].Value != DBNull.Value ? dgvTacGia.Rows[rowIndex].Cells["DienThoai"].Value.ToString() : "";
                txtEmail.Text = dgvTacGia.Rows[rowIndex].Cells["Email"].Value != DBNull.Value ? dgvTacGia.Rows[rowIndex].Cells["Email"].Value.ToString() : "";
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTacGia.Text))
            {
                MessageBox.Show("Vui lòng chọn sách để xóa!");
                return;
            }

            // Câu lệnh SQL xóa dữ liệu
            string query = string.Format(
                "DELETE FROM TacGia WHERE MaTacGia = '{0}'",
                txtMaTacGia.Text
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
            string query = string.Format(
                "SELECT * FROM TacGia WHERE TenTacGia LIKE N'%{0}%'",
                txtTimKiem.Text
            );

            // Lấy dữ liệu từ cơ sở dữ liệu và gán cho DataGridView
            DataSet ds = kn.LayDuLieu(query);
            dgvTacGia.DataSource = ds.Tables[0];
        }

        private void frmTacGia_Load(object sender, EventArgs e)
        {

        }

        private void txtMaTacGia_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
