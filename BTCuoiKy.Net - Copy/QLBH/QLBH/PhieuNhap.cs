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
    public partial class PhieuNhap : Form
    {
        public PhieuNhap()
        {
            InitializeComponent();
            getData();
            getData1();
        }
        KetNoi kn = new KetNoi();
        public void getData()
        {
            string query = "SELECT * FROM PhieuNhap";
            DataSet ds = kn.LayDuLieu(query);
            dgvPhieuNhap.DataSource = ds.Tables[0];
        }
        public void getData1()
        {
            string query = " SELECT * FROM ChiTietPhieuNhap";
            DataSet ds = kn.LayDuLieu(query);
            dgvChiTietPhieuNhap.DataSource = ds.Tables[0];
        }
        
        public void clearText()
        {
            txtMaPhieuNhap.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

            txtNgayLap.Text = "";
            txtMaNhaXuatBan.Text = "";
            txtTongTien.Text = "";
        }
        
        private void txtMaPhieuNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            getData();
            clearText();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string queryPhieuNhap = string.Format(
                     "INSERT INTO PhieuNhap (MaPhieuNhap, NgayLap, MaNhaXuatBan, TongTien) " +
                     "VALUES (N'{0}', N'{1}', N'{2}', N'{3}')",
                     txtMaPhieuNhap.Text,
                     txtNgayLap.Text,
                     txtMaNhaXuatBan.Text,
                     txtTongTien.Text
                 );

            // Chèn vào bảng ChiTietPhieuNhap: Tạo 1 dòng rỗng tương ứng với mã phiếu nhập
            string queryChiTietPhieuNhap = string.Format(
                             "INSERT INTO ChiTietPhieuNhap (MaChiTietPhieuNhap, MaPhieuNhap, MaSach , SoLuong, DonGia) " +
                             "VALUES (NULL,N'{0}', NULL, NULL, NULL)", txtMaPhieuNhap.Text);

            // Thực thi cả hai câu lệnh SQL
            if (kn.ThucThi(queryPhieuNhap) && kn.ThucThi(queryChiTietPhieuNhap))
            {
                MessageBox.Show("Thêm mới thành công! Chi tiết hóa đơn đã được tạo.");
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
                "UPDATE PhieuNhap SET NgayLap = N'{1}', MaNhaXuatBan = N'{2}',TongTien = {3} " +
                "WHERE MaPhieuNhap = '{0}'",
                txtMaPhieuNhap.Text,
                txtNgayLap.Text,
                txtMaNhaXuatBan.Text,
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
            if (string.IsNullOrEmpty(txtMaPhieuNhap.Text))
            {
                MessageBox.Show("Vui lòng chọn sách để xóa!");
                return;
            }

            // Câu lệnh SQL xóa dữ liệu
            string query = string.Format(
                "DELETE FROM PhieuNhap WHERE MaPhieuNhap = '{0}'",
                txtMaPhieuNhap.Text
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
                "SELECT * FROM PhieuNhap WHERE MaPhieuNhap LIKE N'%{0}%'",
                txtTimKiem.Text
            );

            // Lấy dữ liệu từ cơ sở dữ liệu và gán cho DataGridView
            DataSet ds = kn.LayDuLieu(query);
            dgvPhieuNhap.DataSource = ds.Tables[0];
        }


        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
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
            
                    // Gán giá trị vào các TextBox
                    txtMaPhieuNhap.Text = dgvPhieuNhap.Rows[r].Cells["MaPhieuNhap"].Value?.ToString() ?? "";
                    txtNgayLap.Text = dgvPhieuNhap.Rows[r].Cells["NgayLap"].Value?.ToString() ?? "";
                    txtMaNhaXuatBan.Text = dgvPhieuNhap.Rows[r].Cells["MaNhaXuatBan"].Value?.ToString() ?? "";
                    txtTongTien.Text = dgvPhieuNhap.Rows[r].Cells["TongTien"].Value?.ToString() ?? "";

                    // Gọi hàm lọc chi tiết phiếu nhập
                    string maPhieuNhap = txtMaPhieuNhap.Text;
                    getChiTietPhieuNhap(maPhieuNhap);
                
            }
        }

        private void dgvPhieuNhap_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvPhieuNhap.HitTest(e.X, e.Y);

            // Nếu người dùng nhấp vào một hàng
            if (hit.RowIndex >= 0)
            {
                // Lấy chỉ số của hàng mà người dùng nhấp vào
                int rowIndex = hit.RowIndex;

                // Gán giá trị từ các ô trong DataGridView vào các TextBox
                txtMaPhieuNhap.Text = dgvPhieuNhap.Rows[rowIndex].Cells["MaPhieuNhap"].Value.ToString();
                txtNgayLap.Text = dgvPhieuNhap.Rows[rowIndex].Cells["NgayLap"].Value.ToString();
                txtMaNhaXuatBan.Text = dgvPhieuNhap.Rows[rowIndex].Cells["MaNhaXuatBan"].Value.ToString();
                txtTongTien.Text = dgvPhieuNhap.Rows[rowIndex].Cells["TongTien"].Value.ToString();


                // Nếu cần, có thể thiết lập các button để phù hợp với thao tác chỉnh sửa hoặc xóa
                //stxtHoaDon.Enabled = false;  // Không cho phép chỉnh sửa mã sách
                btnThem.Enabled = false;    // Không cho phép thêm mới
                btnSua.Enabled = true;      // Cho phép sửa
                btnXoa.Enabled = true;      // Cho phép xóa
            }
        }

        public void getChiTietPhieuNhap(string maPhieuNhap)
        {
            string query = string.Format("SELECT * FROM ChiTietPhieuNhap WHERE MaPhieuNhap = '{0}'", maPhieuNhap);
            DataSet ds = kn.LayDuLieu(query);
            dgvChiTietPhieuNhap.DataSource = ds.Tables[0];
        }

        private void dgvChiTietPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
