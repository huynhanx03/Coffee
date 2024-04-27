using Coffee.DTOs;
using Coffee.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Utils
{
    public class RecommendSystemService
    {
        private static RecommendSystemService _ins;

        public static RecommendSystemService Ins
        {
            get
            {
                if (_ins == null)
                    _ins = new RecommendSystemService();
                return _ins;
            }
            private set
            {
                _ins = value;
            }
        }

        private string baseAPI = "http://127.0.0.1:5000";

        public async Task getRecommend(string MaSanPham)
        {
            string json = JsonConvert.SerializeObject(MaSanPham);

            // Gửi yêu cầu POST đến Colab
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(baseAPI + "/recommend", content);
                response.EnsureSuccessStatusCode();

                // Đọc và chuyển đổi kết quả từ Colab thành danh sách List<Product>
                string responseJson = await response.Content.ReadAsStringAsync();
                //List<ProductDTO> recommendedProducts = JsonSerializer.Deserialize<List<ProductDTO>>(responseJson);

                //// In ra danh sách List<Product> nhận được từ Colab
                //foreach (var product in recommendedProducts)
                //{
                //    Console.WriteLine($"ProductId: {product.ProductId}, Name: {product.Name}");
                //}
            }
        }
    }
}
