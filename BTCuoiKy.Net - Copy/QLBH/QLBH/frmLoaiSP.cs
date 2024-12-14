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
    public partial class frmLoaiSP : Form
    {
        public frmLoaiSP()
        {
            InitializeComponent();
            getData();
        }

        KetNoi kn = new KetNoi();

        public void getData()
        {
            string query = "select * from TheLoai";
            DataSet ds = kn.LayDuLieu(query);
            dgvTheLoai.DataSource = ds.Tables[0];
        }

        public void clearText()
        {
            // Đặt lại các ô nhập liệu về giá trị mặc định
            txtMaTheLoai.Enabled = true;       // Cho phép chỉnh sửa mã sách
            btnThem.Enabled = true;         // Cho phép thêm mới
            btnSua.Enabled = false;         // Không cho phép sửa
            btnXoa.Enabled = false;         // Không cho phép xóa

            // Làm sạch tất cả các ô nhập liệu liên quan đến sách
            txtMaTheLoai.Text = "";
            txtTenTheLoai.Text = "";
            txtMoTa.Text = "";
          
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            clearText();
            getData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string query = string.Format(
                "INSERT INTO TheLoai (MaTheLoai, TenTheLoai, MoTa ) " +
                "VALUES ('{0}', N'{1}', N'{2}')",
                txtMaTheLoai.Text,
                txtTenTheLoai.Text,
                txtMoTa.Text
                
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
                "UPDATE TheLoai SET TenTheLoai = N'{1}', MoTa = N'{2}'" +
                "WHERE MaTheLoai = '{0}'",
                txtMaTheLoai.Text,
                txtTenTheLoai.Text,
                txtMoTa.Text
                
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

        private void dgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                // Kiểm tra nếu giá trị cột không phải null
                //txtTheLoai.Enabled = false;  // Không cho phép chỉnh sửa mã sách
                btnThem.Enabled = false;     // Không cho phép thêm mới
                btnSua.Enabled = true;       // Cho phép sửa
                btnXoa.Enabled = true;       // Cho phép xóa

                // Gán giá trị từ DataGridView vào các TextBox, kiểm tra null trước khi gán
                txtMaTheLoai.Text = dgvTheLoai.Rows[r].Cells["MaTheLoai"].Value != DBNull.Value ? dgvTheLoai.Rows[r].Cells["MaTheLoai"].Value.ToString() : "";
                txtTenTheLoai.Text = dgvTheLoai.Rows[r].Cells["TenTheLoai"].Value != DBNull.Value ? dgvTheLoai.Rows[r].Cells["TenTheLoai"].Value.ToString() : "";
                txtMoTa.Text = dgvTheLoai.Rows[r].Cells["MoTa"].Value != DBNull.Value ? dgvTheLoai.Rows[r].Cells["MoTa"].Value.ToString() : "";
                
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTheLoai.Text))
            {
                MessageBox.Show("Vui lòng chọn sách để xóa!");
                return;
            }

            // Câu lệnh SQL xóa dữ liệu
            string query = string.Format(
                "DELETE FROM TheLoai WHERE MaTheLoai = '{0}'",
                txtMaTheLoai.Text
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

        private void dgvTheLoai_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dgvTheLoai.HitTest(e.X, e.Y);

            // Nếu người dùng nhấp vào một hàng
            if (hit.RowIndex >= 0)
            {
                // Lấy chỉ số của hàng mà người dùng nhấp vào
                int rowIndex = hit.RowIndex;

                // Gán giá trị từ các ô trong DataGridView vào các TextBox
                txtMaTheLoai.Text = dgvTheLoai.Rows[rowIndex].Cells["MaTheLoai"].Value.ToString();
                txtTenTheLoai.Text = dgvTheLoai.Rows[rowIndex].Cells["TenTheLoai"].Value.ToString();
                txtMoTa.Text = dgvTheLoai.Rows[rowIndex].Cells["MoTa"].Value.ToString();
                

                // Nếu cần, có thể thiết lập các button để phù hợp với thao tác chỉnh sửa hoặc xóa
                //stxtTheLoai.Enabled = false;  // Không cho phép chỉnh sửa mã sách
                btnThem.Enabled = false;    // Không cho phép thêm mới
                btnSua.Enabled = true;      // Cho phép sửa
                btnXoa.Enabled = true;      // Cho phép xóa
            }

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Câu lệnh SQL để tìm kiếm sách theo tên sách
            string query = string.Format(
                "SELECT * FROM TheLoai WHERE TenTheLoai LIKE N'%{0}%'",
                txtTimKiem.Text
            );

            // Lấy dữ liệu từ cơ sở dữ liệu và gán cho DataGridView
            DataSet ds = kn.LayDuLieu(query);
            dgvTheLoai.DataSource = ds.Tables[0];
        }

        private void txtMoTa_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
