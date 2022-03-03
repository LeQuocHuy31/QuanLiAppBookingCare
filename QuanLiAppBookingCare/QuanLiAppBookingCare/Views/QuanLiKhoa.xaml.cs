using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using QuanLiAppBookingCare.Models;

namespace QuanLiAppBookingCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuanLiKhoa : ContentPage
    {
        public QuanLiKhoa()
        {
            InitializeComponent();
            KhoiTao();
        }
     
        async void KhoiTao()
        {
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetChuyenKhoa");
            var subjectListConverted = JsonConvert.DeserializeObject<List<ChuyenKhoa>>(subjectList);
            lstDsKhoa.ItemsSource = subjectListConverted;
        }

        private async void cmdThemKhoaMoi_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ThemKhoa());
        }
    }
}