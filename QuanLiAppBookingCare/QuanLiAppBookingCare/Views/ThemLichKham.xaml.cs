using Newtonsoft.Json;
using QuanLiAppBookingCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuanLiAppBookingCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThemLichKham : ContentPage
    {
        string chuyenkhoa, bacsi;
        public ThemLichKham()
        {
            InitializeComponent();
            initHienThi();
        }
        public async void  initHienThi()
        {
            string[] buoi = new string[] { "Buổi sáng", "Buổi chiều", "Buổi tối" };
            string[] loaihinh = new string[] { "Trực tiếp", "Video Call" };
            prkThoiGian.ItemsSource = buoi;
            prkType.ItemsSource = loaihinh;
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetChuyenKhoa");
            var subjectListConverted = JsonConvert.DeserializeObject<List<ChuyenKhoa>>(subjectList);
            prkChuyenKhoa.ItemsSource = subjectListConverted;
        }
    
        private async void cmdThemLich_Clicked(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            string insertString = "http://www.webapibookingcare.somee.com/api/DataController/InsertLichKham?chuyen_khoa=" + chuyenkhoa+"&bac_si="+bacsi+"&ngay="+prkThu.Date.ToString("dd/MM/yyyy") +"&thoi_gian="+prkThoiGian.SelectedItem.ToString()+"&phong_kham="+txtPhong.Text+"&loai_hinh="+prkType.SelectedItem.ToString()+"&gia="+Double.Parse(txtGia.Text);
            var subjectList = await httpClient.GetStringAsync(insertString);
            await DisplayAlert("Thông báo", "Thêm lịch khám mới thành công", "Ok");
            App.Current.MainPage = new MainPage();
        }

        private void prkChuyenKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int indexSelected = picker.SelectedIndex;
            if (indexSelected >= 0)
            {
                string chuyenKhoa = picker.Items[indexSelected].ToString();
                chuyenkhoa = chuyenKhoa;
                InitHienThiBacSi(chuyenKhoa);
            }
        }

        private async void InitHienThiBacSi(string chuyenKhoa)
        {
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetBacSiTheoKhoa?chuyen_khoa=" + chuyenKhoa);
            var subjectListConverted = JsonConvert.DeserializeObject<List<BacSi>>(subjectList);
            prkBacSi.ItemsSource = subjectListConverted;
        }

        private void prkBacSi_SelectedIndexChanged(object sender, EventArgs e)
        {

            var picker = (Picker)sender;
            int indexSelected = picker.SelectedIndex;
            if (indexSelected >= 0)
            {
                 bacsi = picker.Items[indexSelected].ToString();
            }
        }
    }
}