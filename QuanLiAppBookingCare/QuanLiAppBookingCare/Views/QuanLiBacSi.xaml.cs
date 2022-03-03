using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuanLiAppBookingCare.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuanLiAppBookingCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuanLiBacSi : ContentPage
    {
        public QuanLiBacSi()
        {
            InitializeComponent();
            KhoiTao();
        }

        protected async void KhoiTao()
        {
            HttpClient httpClient = new HttpClient();
            var subjectList = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetBacSi");
            var subjectListConverted = JsonConvert.DeserializeObject<List<BacSi>>(subjectList);
            lstDsBacSi.ItemsSource = subjectListConverted;

            var chuyenKhoa = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetChuyenKhoa");
            var chuyenKhoaConvert = JsonConvert.DeserializeObject<List<ChuyenKhoa>>(chuyenKhoa);
            List<ChuyenKhoa> chuyenKhoa1 = new List<ChuyenKhoa>();
            chuyenKhoa1.Add(new ChuyenKhoa() { hinh = "0", ten_chuyen_khoa = "Chọn tất cả", id = -1 });
            foreach (ChuyenKhoa khoa in chuyenKhoaConvert)
            {
                chuyenKhoa1.Add(khoa);
            }
            prkFind.ItemsSource = chuyenKhoa1;
        }

        private async void cmdThemBacSiMoi_Clicked(object sender, EventArgs e)
        {
             await Navigation.PushAsync(new ThemBS());
        }

        private async void lstDsBacSi_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            BacSi bacSi = (BacSi) e.SelectedItem;
            if (bacSi != null)
            {
                await Navigation.PushAsync(new ChiTietBacSi(bacSi));
            }
        }

        private async void prkFind_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int indexSelected = picker.SelectedIndex;
            if (indexSelected >= 0)
            {
                HttpClient httpClient = new HttpClient();
                var chuyenkhoa = picker.Items[indexSelected].ToString();
                if (chuyenkhoa == "Chọn tất cả")
                {
                    var subjectList = await httpClient.GetStringAsync("http://192.168.1.64/webapibookingcare/api/DataController/GetBacSi");
                    var subjectListConverted = JsonConvert.DeserializeObject<List<BacSi>>(subjectList);
                    lstDsBacSi.ItemsSource = subjectListConverted;
                }
                else
                {
                    var subjectList = await httpClient.GetStringAsync("http://192.168.1.64/webapibookingcare/api/DataController/GetBacSiTheoKhoa?chuyen_khoa=" + chuyenkhoa);
                    var subjectListConverted = JsonConvert.DeserializeObject<List<BacSi>>(subjectList);
                    lstDsBacSi.ItemsSource = subjectListConverted;
                }
            }
        }
    }
}