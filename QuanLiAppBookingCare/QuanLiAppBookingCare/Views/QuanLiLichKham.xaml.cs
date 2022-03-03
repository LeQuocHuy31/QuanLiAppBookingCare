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
    public partial class QuanLiLichKham : ContentPage
    {
        public QuanLiLichKham()
        {
            InitializeComponent();
            initHienThi();
        }
        public async void initHienThi()
        {
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetLichKham");
            var subjectListConverted = JsonConvert.DeserializeObject<List<LichKham>>(subjectList);
            lstLichKham.ItemsSource = subjectListConverted;
        }

        private async void cmdNewSchedule_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ThemLichKham());
        }

        private async void lstLichKham_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            LichKham lichKham = (LichKham)e.SelectedItem;
            if (lichKham != null)
            {
                await Navigation.PushAsync(new ChiTietLichKham(lichKham));
            }
        }
    }
}