using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OnTapDesktop
{
    public partial class frmQuanLyMonAn : Form
    {
        private string connStr;

        private QuanLyMonAn QL_MonAn;
        private QuanLyNhomMonAn QL_NhomMonAn;

        public frmQuanLyMonAn()
        {
            InitializeComponent();
            init();
            layDuLieuTuDB();
            hienThiNhomMonAn(QL_NhomMonAn.dsNhomMonAn);
            hienThiMonAn(QL_MonAn.dsMonAn);
        }

        private void init()
        {
            connStr = "Server=DESKTOP-UT3VJ3N\\SQLEXPRESS; Database=OnTap_QLMonAn; Integrated Security=true;";
            QL_MonAn = new QuanLyMonAn();
            QL_NhomMonAn = new QuanLyNhomMonAn();
        }

        private void layDuLieuTuDB()
        {
            layDuLieu_NhomMonAn();
            layDuLieu_MonAn();
        }

        private void layDuLieu_NhomMonAn()
        {
            QL_NhomMonAn.clear(); // xoá dữ liệu cũ

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "SELECT * FROM NhomMonAn";

            conn.Open();
            SqlDataReader records = cmd.ExecuteReader();
            while (records.Read())
            {
                NhomMonAn i = new NhomMonAn()
                {
                    maNhom = Convert.ToInt32(records["MaNhom"]),
                    tenNhom = records["TenNhom"].ToString()
                };

                QL_NhomMonAn.themNhomMonAn(i);
            }
            conn.Close();
        }

        private void hienThiNhomMonAn(List<NhomMonAn> dsNhomMonAn)
        {
            cbNhomMonAn.DataSource = dsNhomMonAn;
            cbNhomMonAn.DisplayMember = "tenNhom";
            cbNhomMonAn.ValueMember = "maNhom";
            cbNhomMonAn.SelectedItem = null;
        }

        private void layDuLieu_MonAn()
        {
            QL_MonAn.clear(); // xoá dữ liệu cũ

            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "SELECT * FROM MonAn";

            conn.Open();
            SqlDataReader records = cmd.ExecuteReader();
            while (records.Read())
            {
                MonAn i = new MonAn()
                {
                    maMonAn = Convert.ToInt32(records["MaMonAn"]),
                    tenMonAn = records["TenMonAn"].ToString(),
                    nhom = Convert.ToInt32(records["Nhom"])
                };

                QL_MonAn.themMonAn(i);
            }
            conn.Close();
        }

        private string layTenNhomMonAn(int maNhom)
        {
            return QL_NhomMonAn.timNhomMonAnTheoMa(maNhom).tenNhom;
        }

        private void hienThiMonAn(List<MonAn> dsMonAn)
        {
            lvMonAn.Items.Clear();

            foreach (MonAn i in dsMonAn)
            {
                string[] info = {
                    i.maMonAn.ToString(),
                    i.tenMonAn,
                    layTenNhomMonAn(i.nhom)
                };

                ListViewItem item = new ListViewItem(info);
                lvMonAn.Items.Add(item);
            }
        }

        private void reloadMonAn()
        {
            layDuLieu_MonAn();
            hienThiMonAn(QL_MonAn.dsMonAn);
        }

        private void themMonAn(MonAn i)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "INSERT INTO MonAn(TenMonAn, Nhom) VALUES " +
                "(" +
                    "N\'" + i.tenMonAn + "\'" + "," +
                    i.nhom +
                ")";

            conn.Open();
            int res = cmd.ExecuteNonQuery(); // rows affect
            if (res > 0)
            {
                MessageBox.Show("Thêm món ăn thành công");
                reloadMonAn();
                btnMacDinh.PerformClick();
            }
            else
            {
                MessageBox.Show("Thêm món ăn thất bại");
            }
            conn.Close();
        }

        private void suaMonAn(MonAn i)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "UPDATE MonAn SET " +
                "TenMonAn=" + "N\'" + i.tenMonAn + "\'" + "," +
                "Nhom=" + i.nhom + " " +
                "WHERE MaMonAn=" + i.maMonAn;

            conn.Open();
            int res = cmd.ExecuteNonQuery(); // rows affect
            if (res > 0)
            {
                MessageBox.Show("Sửa món ăn thành công");
                reloadMonAn();
            }
            else
            {
                MessageBox.Show("Sửa món ăn thất bại");
            }
            conn.Close();
        }

        private void xoaMonAn(int maMonAn)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText =
                "DELETE FROM MonAn " +
                "WHERE MaMonAn=" + maMonAn;

            conn.Open();
            int res = cmd.ExecuteNonQuery(); // rows affect
            if (res > 0)
            {
                MessageBox.Show("Xoá món ăn thành công");
                reloadMonAn();
                btnMacDinh.PerformClick();
            }
            else
            {
                MessageBox.Show("Xoá món ăn thất bại");
            }
            conn.Close();
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            txtMaMonAn.Text = null;
            txtTenMonAn.Text = null;
            cbNhomMonAn.SelectedItem = null;
            txtTimKiem.Text = null;
        }

        private void lvMonAn_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected) // ngăn sự kiện xử lý 2 lần
            {
                ListView owner = sender as ListView;
                int maMonAn = int.Parse(owner.SelectedItems[0].Text);

                MonAn found = QL_MonAn.timMonAnTheoMa(maMonAn);
                if (found != null)
                {
                    txtMaMonAn.Text = found.maMonAn.ToString();
                    txtTenMonAn.Text = found.tenMonAn;
                    cbNhomMonAn.SelectedValue = found.nhom;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvMonAn.SelectedItems.Count > 0)
            {
                int maMonAn = int.Parse(lvMonAn.SelectedItems[0].Text);
                xoaMonAn(maMonAn);
            }
            else
            {
                MessageBox.Show("Chưa chọn món ăn để xoá");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // kiểm tra dữ liệu trước khi thêm hoặc cập nhật
            if (
                string.IsNullOrEmpty(txtTenMonAn.Text) ||    
                string.IsNullOrWhiteSpace(txtTenMonAn.Text) ||    
                cbNhomMonAn.SelectedItem == null
            )
            {
                MessageBox.Show("Chưa nhập đầy đủ thông tin");
                return;
            }

            // tạo đối tượng lưu trữ thông tin món ăn
            MonAn i = new MonAn()
            {
                tenMonAn = txtTenMonAn.Text,
                nhom = Convert.ToInt32(cbNhomMonAn.SelectedValue)
            };

            // nếu ô Mã món ăn không có giá trị thì thêm mới món ăn
            if (string.IsNullOrEmpty(txtMaMonAn.Text))
            {
                themMonAn(i);
            }
            // ngược lại cập nhật thông tin món ăn
            else
            {
                i.maMonAn = int.Parse(txtMaMonAn.Text);
                suaMonAn(i);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string ten = txtTimKiem.Text;
            if (string.IsNullOrEmpty(ten) || string.IsNullOrWhiteSpace(ten))
            {
                hienThiMonAn(QL_MonAn.dsMonAn);
            }
            else
            {
                List<MonAn> founds = QL_MonAn.timKiemMonAnTheoTen(ten);
                hienThiMonAn(founds);
            }
        }
    }
}
