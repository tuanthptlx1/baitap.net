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
            getData1();
        }

        KetNoi kn = new KetNoi();

        public void getData()
        {
            string query = "select * from HoaDon";
            DataSet ds = kn.LayDuLieu(query);
            dgvHoaDon.DataSource = ds.Tables[0];
        }

        public void getData1()
        {
            string query = "select * from ChiTietHoaDon";
            DataSet ds = kn.LayDuLieu(query);
            dgvChiTietHoaDon.DataSource = ds.Tables[0];
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
            txtMaChiTietHoaDon.Clear();
            txtMaHoaDon.Clear();
            txtMaSach.Clear();
            txtSoLuong.Clear();
            txtDonGia.Clear();

        }


        private void frmHoaDon_Load(object sender, EventArgs e)
        {

        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            getData();
            getData1();
            clearText();   
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = "";

            // Kiểm tra xem thao tác đang ở tab nào
            if (tabControl.SelectedTab.Name == "HoaDon") // Thêm vào bảng HoaDon
            {
                query = string.Format(
                    "INSERT INTO HoaDon (MaHoaDon, NgayLap, MaKhachHang, TongTien) " +
                    "VALUES ('{0}', '{1}', '{2}', {3})",
                    txtMaHoaDon.Text, txtNgayLap.Text, txtMaKhachHang.Text, txtTongTien.Text);
            }
            else if (tabControl.SelectedTab.Name == "ChiTietHoaDon") // Thêm vào bảng ChiTietHoaDon
            {
                query = string.Format(
                    "INSERT INTO ChiTietHoaDon (MaChiTietHoaDon, MaHoaDon, MaSach, SoLuong, DonGia) " +
                    "VALUES (N'{0}', N'{1}', N'{2}', N'{3}', N'{4}')",
                    txtMaChiTietHoaDon.Text, txtMaHoaDon1.Text, txtMaSach.Text, txtSoLuong.Text, txtDonGia.Text);
            }


            if (kn.ThucThi(query))
            {
                MessageBox.Show("Thêm thành công!");
                btnLamMoi.PerformClick(); // Làm mới dữ liệu
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = "";

            // Kiểm tra xem tab hiện tại là tab nào
            if (tabControl.SelectedTab.Name == "HoaDon") // Tab HoaDon
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(txtMaHoaDon.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã hóa đơn!");
                    return;
                }

                query = string.Format(
                    "UPDATE HoaDon SET NgayLap = '{0}', MaKhachHang = '{1}', TongTien = {2} " +
                    "WHERE MaHoaDon = '{3}'",
                    txtNgayLap.Text, txtMaKhachHang.Text, txtTongTien.Text, txtMaHoaDon.Text);
            }
            else if (tabControl.SelectedTab.Name == "ChiTietHoaDon") // Tab ChiTietHoaDon
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(txtMaChiTietHoaDon.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã chi tiết hóa đơn!");
                    return;
                }

                query = string.Format(
                    "UPDATE ChiTietHoaDon SET MaHoaDon = '{0}', MaSach = '{1}', SoLuong = {2}, DonGia = {3} " +
                    "WHERE MaChiTietHoaDon = '{4}'",
                    txtMaHoaDon.Text, txtMaSach.Text, txtSoLuong.Text, txtDonGia.Text, txtMaChiTietHoaDon.Text);
            }

            // Thực thi câu lệnh SQL
            if (kn.ThucThi(query))
            {
                MessageBox.Show("Sửa thành công!");
                btnLamMoi.PerformClick(); // Làm mới dữ liệu
            }
            else
            {
                MessageBox.Show("Sửa thất bại!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string query = "";

            // Kiểm tra xem tab hiện tại là tab nào
            if (tabControl.SelectedTab.Name == "HoaDon") // Tab HoaDon
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(txtMaHoaDon.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã hóa đơn để xóa!");
                    return;
                }

                query = string.Format(
                    "DELETE FROM HoaDon WHERE MaHoaDon = '{0}'", txtMaHoaDon.Text);
            }
            else if (tabControl.SelectedTab.Name == "ChiTietHoaDon") // Tab ChiTietHoaDon
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(txtMaChiTietHoaDon.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã chi tiết hóa đơn để xóa!");
                    return;
                }

                query = string.Format(
                    "DELETE FROM ChiTietHoaDon WHERE MaChiTietHoaDon = '{0}'", txtMaChiTietHoaDon.Text);
            }

            // Thực thi câu lệnh SQL
            if (kn.ThucThi(query))
            {
                MessageBox.Show("Xóa thành công!");
                btnLamMoi.PerformClick(); // Làm mới dữ liệu
            }
            else
            {
                MessageBox.Show("Xóa thất bại!");
            }
        }


        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string query = "";

            // Kiểm tra xem thao tác đang ở tab nào
            if (tabControl.SelectedTab.Name == "tabHoaDon") // Tab HoaDon
            {
                // Câu lệnh SQL để tìm kiếm hóa đơn theo mã hóa đơn hoặc khách hàng
                query = string.Format(
                    "SELECT * FROM HoaDon WHERE MaHoaDon LIKE N'%{0}%' OR MaKhachHang LIKE N'%{0}%'",
                    txtTimKiem.Text
                );

                // Lấy dữ liệu từ cơ sở dữ liệu và gán cho DataGridView
                DataSet ds = kn.LayDuLieu(query);
                dgvHoaDon.DataSource = ds.Tables[0];
            }
            else if (tabControl.SelectedTab.Name == "tabChiTietHoaDon") // Tab ChiTietHoaDon
            {
                // Câu lệnh SQL để tìm kiếm chi tiết hóa đơn theo mã chi tiết hóa đơn hoặc mã sách
                query = string.Format(
                    "SELECT * FROM ChiTietHoaDon WHERE MaChiTietHoaDon LIKE N'%{0}%' OR MaSach LIKE N'%{0}%'",
                    txtTimKiem.Text
                );

                // Lấy dữ liệu từ cơ sở dữ liệu và gán cho DataGridView
                DataSet ds = kn.LayDuLieu(query);
                dgvChiTietHoaDon.DataSource = ds.Tables[0];
            }
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
                txtMaHoaDon.Text = dgvHoaDon.Rows[r].Cells["MaHoaDon"].Value?.ToString() ?? "";
                txtNgayLap.Text = dgvHoaDon.Rows[r].Cells["NgayLap"].Value?.ToString() ?? "";
                txtMaKhachHang.Text = dgvHoaDon.Rows[r].Cells["MaKhachHang"].Value?.ToString() ?? "";
                txtTongTien.Text = dgvHoaDon.Rows[r].Cells["TongTien"].Value?.ToString() ?? "";

                // Gọi hàm lọc chi tiết phiếu nhập
                string maHoaDon = txtMaHoaDon.Text;
                getChiTietPhieuNhap(maHoaDon);

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

        public void getChiTietPhieuNhap(string maHoaDon)
        {
            string query = string.Format("SELECT * FROM ChiTietHoaDon WHERE MaHoaDon = '{0}'", maHoaDon);
            DataSet ds = kn.LayDuLieu(query);
            dgvChiTietHoaDon.DataSource = ds.Tables[0];
        }
        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaHoaDon_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvChiTietHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                // Kiểm tra nếu giá trị cột không phải null
                btnThem.Enabled = false;     // Không cho phép thêm mới
                btnSua.Enabled = true;       // Cho phép sửa
                btnXoa.Enabled = true;       // Cho phép xóa

                // Gán giá trị từ DataGridView vào các TextBox, kiểm tra null trước khi gán
                txtMaChiTietHoaDon.Text = dgvChiTietHoaDon.Rows[r].Cells["MaChiTietHoaDon"].Value?.ToString() ?? "";
                txtMaHoaDon1.Text = dgvChiTietHoaDon.Rows[r].Cells["MaHoaDon"].Value?.ToString() ?? "";
                txtMaSach.Text = dgvChiTietHoaDon.Rows[r].Cells["MaSach"].Value?.ToString() ?? "";
                txtSoLuong.Text = dgvChiTietHoaDon.Rows[r].Cells["SoLuong"].Value?.ToString() ?? "";
                txtDonGia.Text = dgvChiTietHoaDon.Rows[r].Cells["DonGia"].Value?.ToString() ?? "";
            }
        }

        private void dgvChiTietHoaDon_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvChiTietHoaDon.HitTest(e.X, e.Y);

            // Nếu người dùng nhấp vào một hàng
            if (hit.RowIndex >= 0)
            {
                // Lấy chỉ số của hàng mà người dùng nhấp vào
                int rowIndex = hit.RowIndex;

                // Gán giá trị từ các ô trong DataGridView vào các TextBox
                txtMaChiTietHoaDon.Text = dgvChiTietHoaDon.Rows[rowIndex].Cells["MaChiTietHoaDon"].Value.ToString();
                txtMaHoaDon1.Text = dgvChiTietHoaDon.Rows[rowIndex].Cells["MaHoaDon"].Value.ToString();
                txtMaSach.Text = dgvChiTietHoaDon.Rows[rowIndex].Cells["MaSach"].Value.ToString();
                txtSoLuong.Text = dgvChiTietHoaDon.Rows[rowIndex].Cells["SoLuong"].Value.ToString();
                txtDonGia.Text = dgvChiTietHoaDon.Rows[rowIndex].Cells["DonGia"].Value.ToString();

                // Cập nhật các button để phù hợp với thao tác
                btnThem.Enabled = false;    // Không cho phép thêm mới
                btnSua.Enabled = true;      // Cho phép sửa
                btnXoa.Enabled = true;      // Cho phép xóa
            }
        }
        }
    }

