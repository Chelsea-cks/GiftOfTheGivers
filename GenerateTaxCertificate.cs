using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DisasterAlleviation.Functions
{
    public class TaxCertificateRequest
    {
        public string DonorName { get; set; } = string.Empty;
        public string DonorEmail { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string TaxReferenceNumber { get; set; } = string.Empty;
    }

    public class GenerateTaxCertificate
    {
        private readonly ILogger<GenerateTaxCertificate> _logger;

        public GenerateTaxCertificate(ILogger<GenerateTaxCertificate> logger)
        {
            _logger = logger;
        }

        [Function("GenerateTaxCertificate")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Processing tax certificate generation request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonSerializer.Deserialize<TaxCertificateRequest>(requestBody, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var response = req.CreateResponse();

            if (data == null || string.IsNullOrEmpty(data.DonorName) || data.Amount <= 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Invalid donation payload. Please supply donor details and a valid amount.");
                return response;
            }

            var certificateResult = new
            {
                CertificateId = $"TAX-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                data.DonorName,
                data.DonorEmail,
                data.Amount,
                IssuedDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss UTC"),
                Organization = "Disaster Alleviation Foundation",
                TaxDeductibleStatus = "Approved Section 18A Certificate"
            };

            response.StatusCode = HttpStatusCode.OK;
            response.Headers.Add("Content-Type", "application/json");
            await response.WriteAsJsonAsync(certificateResult);

            return response;
        }
    }
}