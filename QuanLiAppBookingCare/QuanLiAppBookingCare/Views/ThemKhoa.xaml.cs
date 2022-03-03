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
    public partial class ThemKhoa : ContentPage
    {
        public ThemKhoa()
        {
            InitializeComponent();
        }
        private async void cmdThemKhoa_Clicked(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/InsertKhoa?ten_chuyen_khoa=" + txtKhoa.Text+"&hinh="+txtImg.Text);
            await DisplayAlert("Thông báo", "Thêm mới khoa thành công", "OK");
            App.Current.MainPage = new MainPage();
        }
    }
}