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
    public partial class ThemBS : ContentPage
    {
        string chuyenkhoa;
        public ThemBS()
        {
            InitializeComponent();
            initHienThi();
        }
        public async void initHienThi()
        {
            string[] gioitinh = new string[] { "Nam", "Nữ" };
            prkGioiTinh.ItemsSource = gioitinh;
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetChuyenKhoa");
            var subjectListConverted = JsonConvert.DeserializeObject<List<ChuyenKhoa>>(subjectList);
            prkChuyenKhoa.ItemsSource = subjectListConverted;
        }

        private async void cmdThemBS_Clicked(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/InsertBacSi?ho_ten=" + txtTen.Text+"&gioi_tinh="+prkGioiTinh.SelectedItem.ToString()+"&ngay_sinh="+prkNgaySinh.Date.ToString("dd/MM/yyyy")+"&chuyen_khoa="+chuyenkhoa+"&email="+txtEmail.Text+"&id_zoom="+txtID.Text+"&pass_zoom="+txtPassword.Text);
            await DisplayAlert("Thông báo", "Thêm mới bác sĩ thành công", "OK");
            App.Current.MainPage = new MainPage();
        }

        private void prkChuyenKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int indexSelected = picker.SelectedIndex;
            if (indexSelected >= 0)
            {
                chuyenkhoa = picker.Items[indexSelected].ToString();
            }
        }
    }
}