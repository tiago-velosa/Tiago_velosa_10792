using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace ApiAlunosProxy.Services
{
    public class AlunosService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUrl = "http://localhost:3000/api/alunos";

        public AlunosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAll()
        {
            return await _httpClient.GetStringAsync(baseUrl);
        }

        public async Task<string> GetById(int id)
        {
            return await _httpClient.GetStringAsync($"{baseUrl}/{id}");
        }

        public async Task<HttpResponseMessage> Create(object aluno)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(aluno),
                Encoding.UTF8,
                "application/json");

            return await _httpClient.PostAsync(baseUrl, content);
        }

        public async Task<HttpResponseMessage> Update(int id, object aluno)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(aluno),
                Encoding.UTF8,
                "application/json");

            return await _httpClient.PutAsync($"{baseUrl}/{id}", content);
        }

        public async Task<HttpResponseMessage> Delete(int id, bool isAdmin)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{baseUrl}/{id}");

            if (isAdmin)
                request.Headers.Add("Role", "Admin");

            return await _httpClient.SendAsync(request);
        }
    }
}
