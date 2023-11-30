using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace MadeinHeavenBookStore.Models.ShipandCoupon
{



    public class CouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Coupon>> DeleteCoupon(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7074/");
            await httpClient.DeleteAsync($"api/managecoupon/{id}");
            return null;

        }
        public async Task<List<Coupon>> GetCoupons()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7074/");
            return await httpClient.GetFromJsonAsync<List<Coupon>>("api/ManageCoupon");
            
        }

        public async Task<Coupon> AddCoupon()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7074/");
            var response = await httpClient.PostAsync("api/ManageCoupon", new StringContent("", Encoding.UTF8, "application/json"));
            return null;
        }

        public async Task<List<Coupon>> SaveCoupon(List<Coupon> coupons)
        {

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7074/");
            await httpClient.PutAsJsonAsync("api/ManageCoupon", coupons);
            return null;
        }
    }
}
