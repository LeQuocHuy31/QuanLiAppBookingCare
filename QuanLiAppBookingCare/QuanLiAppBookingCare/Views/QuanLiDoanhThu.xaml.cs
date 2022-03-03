using Microcharts;
using Newtonsoft.Json;
using QuanLiAppBookingCare.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuanLiAppBookingCare.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuanLiDoanhThu : ContentPage
    {
        public QuanLiDoanhThu()
        {
            InitializeComponent();
        }
        private async void cmdReport_Clicked(object sender, EventArgs e)
        {
            var random = new Random();
            var entries = new List<ChartEntry>();
            var entries1 = new List<ChartEntry>();
            var entries3 = new List<ChartEntry>();
            var entries2 = new List<ChartEntry>();
            var entries4 = new List<ChartEntry>();
            HttpClient httpClient = new HttpClient();
            var kq1 = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetDoanhThu?ngaybatdau=" + prkStart.Date.ToString("dd/MM/yyyy") + "&ngayketthuc=" + prkEnd.Date.ToString("dd/MM/yyyy"));
            var doanhthu1 = JsonConvert.DeserializeObject<List<DoanhThu>>(kq1);
            var kq2 = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetDoanhThu1?ngaybatdau=" + prkStart.Date.ToString("dd/MM/yyyy") + "&ngayketthuc=" + prkEnd.Date.ToString("dd/MM/yyyy"));
            var doanhthu2 = JsonConvert.DeserializeObject<List<DoanhThu1>>(kq2);
            var kq4 = await httpClient.GetStringAsync("http://www.webapibookingcare.somee.com/api/DataController/GetDashboard?ngaybatdau=" + prkStart.Date.ToString("dd/MM/yyyy") + "&ngayketthuc=" + prkEnd.Date.ToString("dd/MM/yyyy"));
            var dashboard = JsonConvert.DeserializeObject<List<DashBoard>>(kq4);
            if (doanhthu1.Count == 0)
            {
                await DisplayAlert("Thông báo", "Không có doanh thu trong thời gian trên. Vui lòng chọn mốc thời gian khác.", "Ok");
            }
            else
            {
                result.IsVisible = true;
                foreach (DoanhThu doanhThu in doanhthu1)
                {
                    var color = String.Format(new CultureInfo("vi-VN"),"#{0:X6}", random.Next(0x1000000));
                    entries.Add(new ChartEntry((float)doanhThu.doanh_thu)
                    {
                        Label = doanhThu.chuyen_khoa,
                        Color = SKColor.Parse(color),
                        ValueLabel = string.Format("{0:#,##0.00}", doanhThu.doanh_thu),
                    });
                    entries1.Add(new ChartEntry((float)doanhThu.so_luong)
                    {
                        Label = doanhThu.chuyen_khoa,
                        Color = SKColor.Parse(color),
                        ValueLabel = doanhThu.so_luong.ToString(),
                    });
                }
                chartViewBar.Chart = new BarChart { Entries = entries, ValueLabelOrientation = Orientation.Horizontal, LabelTextSize = 35, LabelOrientation = Orientation.Horizontal, LabelColor = SKColor.Parse("#000000")};
                chartViewBar1.Chart = new BarChart { Entries = entries1, ValueLabelOrientation = Orientation.Horizontal, LabelTextSize = 35, LabelOrientation = Orientation.Horizontal, LabelColor = SKColor.Parse("#000000") };
                foreach (DoanhThu1 doanhThu in doanhthu2)
                {
                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    entries2.Add(new ChartEntry((float)doanhThu.doanh_thu)
                    {
                        Label = doanhThu.loai_hinh,
                        Color = SKColor.Parse(color),
                        ValueLabel = string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00}", doanhThu.doanh_thu),
                    });
                    entries3.Add(new ChartEntry((float)doanhThu.so_luong)
                    {
                        Label = doanhThu.loai_hinh,
                        Color = SKColor.Parse(color),
                        ValueLabel = doanhThu.so_luong.ToString()
                    });
                }
                foreach (DashBoard dashboard1 in dashboard)
                {
                    var color = String.Format(new CultureInfo("vi-VN"), "#{0:X6}", random.Next(0x1000000));
                    entries4.Add(new ChartEntry((float)dashboard1.doanh_thu)
                    {
                        Label = dashboard1.ngay,
                        Color = SKColor.Parse(color),
                        ValueLabel = string.Format("{0:#,##0.00}", dashboard1.doanh_thu),
                    }); ;
                }
                chartViewPie.Chart = new PieChart { Entries = entries3, HoleRadius = 0.3f ,LabelTextSize=40, LabelColor = SKColor.Parse("#000000") };
                chartViewBar2.Chart = new BarChart { Entries = entries2, ValueLabelOrientation = Orientation.Horizontal, LabelTextSize = 35, LabelOrientation = Orientation.Horizontal , LabelColor = SKColor.Parse("#000000") };
                chartViewLine.Chart = new LineChart { Entries = entries4, LineMode = LineMode.Straight , LabelOrientation = Orientation.Horizontal, ValueLabelOrientation = Orientation.Horizontal, LabelTextSize = 35 };
            }
        }
        
    }
}