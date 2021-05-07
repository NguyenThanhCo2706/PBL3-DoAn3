using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI.BLL;
using GUI.DTO;

namespace GUI
{
    public partial class NhanVien : Form
    {
        List<CHI_TIET_HOA_DON_BAN> list = new List<CHI_TIET_HOA_DON_BAN>();
        public string maNV { get; set; }
        bool checkKhachHang = false;
        public NhanVien()
        {
            InitializeComponent();
            setCbbMaSach();
            setData1();
            setCbbLoaiSach();
        }

        public void setData1()
        {
            dataGridView1.DataSource = BLL_QuanLy.Instance.Bll_GetAllSach();
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
        }
        public void setData2()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = list;
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
            if(list.Count != 0) dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[1];
        }
        public void setCbbMaSach()
        {
            foreach(string i in BLL_QuanLy.Instance.Bll_GetAllMaSach())
            {
                cbbMaSach.Items.Add(i);
            }
            cbbMaSach.SelectedIndex = 0;
        }
        public void setCbbLoaiSach()
        {
            cbbTheLoai.Items.Add("All");
            foreach (string i in BLL_QuanLy.Instance.Bll_GetAllMaLS())
            {
                cbbTheLoai.Items.Add(i);
            }
            cbbMaSach.SelectedIndex = 0;
        }
        private void NhanVien_Load(object sender, EventArgs e)
        {
            txt1.Text = BLL_QuanLy.Instance.Bll_GetNameNVByMaNV(maNV);
            txt1.Enabled = false;
            TongCong.Text = "0";
        }

        private void Them_Click(object sender, EventArgs e)
        {
            if(MaDonBan.Text == "")
            {
                MessageBox.Show("Vui long nhap ma don ban");
                return;
            }
            if (SDT.Text == "")
            {
                MessageBox.Show("Vui long nhap số điện thoại");
                return;
            }
            if (HoTen.Text == "")
            {
                MessageBox.Show("Vui long nhap họ tên");
                return;
            }
            foreach (string i in BLL_QuanLy.Instance.Bll_GetAllMaDonBan())
            {
                if(MaDonBan.Text == i)
                {
                    MessageBox.Show("Mã đơn này đã tồn tại");
                    return;
                }
            }
            string a = SDT.Text;
            for (int i = 0; i < SDT.TextLength; i++)
            {
                if ((int)a[i] < 48 || (int)a[i] > 57)
                {
                    MessageBox.Show("Số điện thoại ko hợp lệ");
                    return;
                }
            }
            if(numericUpDown1.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng sách");
                return;
            }
            int soSachinList = 0;
            foreach(CHI_TIET_HOA_DON_BAN i in list)
            {
                if(i.MaSach == cbbMaSach.SelectedItem.ToString())
                {
                    soSachinList += i.SoLuong;
                }
            }
            if(numericUpDown1.Value + soSachinList > BLL_QuanLy.Instance.Bll_GetSLByMaSach(cbbMaSach.SelectedItem.ToString()))
            {
                MessageBox.Show("Số lượng sách trong kho không đủ");
                return;
            }
            HoTen.Enabled = false;
            SDT.Enabled = false;
            MaDonBan.Enabled = false;
            int donGia = BLL_QuanLy.Instance.Bll_GetGiaTienByMaSach(cbbMaSach.SelectedItem.ToString());
            int soLuong = Convert.ToInt32(numericUpDown1.Value.ToString());
            CHI_TIET_HOA_DON_BAN chitiet = new CHI_TIET_HOA_DON_BAN()
            {
                MaDonBan = MaDonBan.Text,
                MaSach = cbbMaSach.SelectedItem.ToString(),
                SoLuong = soLuong,
                DonGia = donGia,
                ThanhTien = donGia * soLuong,
            };
            int tong = Convert.ToInt32(TongCong.Text);
            TongCong.Text = (tong + chitiet.ThanhTien).ToString();
            foreach(CHI_TIET_HOA_DON_BAN p in list)
            {
                if(chitiet.MaSach == p.MaSach)
                {
                    p.SoLuong += chitiet.SoLuong;
                    p.ThanhTien = p.SoLuong * p.DonGia;
                    setData2();
                    return;
                }
            }
            list.Add(chitiet);
            setData2();
            return;
        }

        private void ChinhSua_Click(object sender, EventArgs e)
        {
            int stt = Convert.ToInt32(dataGridView2.CurrentRow.Cells["STT"].Value.ToString());
            foreach(CHI_TIET_HOA_DON_BAN a in list)
            {
                
            }
        }

        private void Xoa_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection r = dataGridView2.SelectedRows;
            foreach(DataGridViewRow i in r)
            {
                list.RemoveAt(i.Index); 
            }
            setData2();
        }

        private void ThanhToan_Click(object sender, EventArgs e)
        {
            if(checkKhachHang == false)
            {
                KHACH_HANG kh = new KHACH_HANG();
                kh.SDT = SDT.Text;
                kh.HoTen = HoTen.Text;
                BLL_QuanLy.Instance.Bll_AddKhachHang(kh);
            }
            HOA_DON_BAN hdb = new HOA_DON_BAN()
            {
                MaDonBan = MaDonBan.Text,
                SDT_KH = SDT.Text,
                MaNhanVien = maNV,
                NgayBan = DateTime.Now,
            };
            BLL_QuanLy.Instance.Bll_AddHoaDonBan(hdb);
            foreach(CHI_TIET_HOA_DON_BAN a in list)
            {
                BLL_QuanLy.Instance.Bll_AddChiTietHoaDonBan(a);
                string maSach = a.MaSach;
                int soLuongban = a.SoLuong;
                int soLuongNow = BLL_QuanLy.Instance.Bll_GetSLByMaSach(maSach);
                BLL_QuanLy.Instance.Bll_EditSLByMaSach(maSach, soLuongNow - soLuongban);
            }
            list.Clear();
            dataGridView2.DataSource = null;
            HoTen.Enabled = true;
            SDT.Enabled = true;
            MaDonBan.Enabled = true;
            checkKhachHang = false;
            MaDonBan.Text = "";
            SDT.Text = "";
            HoTen.Text = "";
            cbbMaSach.SelectedIndex = 0;
            numericUpDown1.Value = 0;
            setData1();
        }
        private void Thoat_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void SDT_TextChanged(object sender, EventArgs e)
        {
            foreach(string i in BLL_QuanLy.Instance.Bll_GetAllSDT())
            {
                if(SDT.Text == i)
                {
                    checkKhachHang = true;
                    HoTen.Text = BLL_QuanLy.Instance.Bll_GetKHBySDT(SDT.Text);
                    HoTen.Enabled = false;
                    return;
                }
                else
                {
                    checkKhachHang = false;
                    HoTen.Text = "";
                    HoTen.Enabled = true;
                }
            }
        }

        private void cbbTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbTheLoai.SelectedItem.ToString() == "All")
            {
                setData1();
            }
            else
            {
                dataGridView1.DataSource = BLL_QuanLy.Instance.Bll_GetSachByLS(cbbTheLoai.SelectedItem.ToString());
            }
        }
    }
}
