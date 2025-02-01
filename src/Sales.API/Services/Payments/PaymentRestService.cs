using Sales.API.Application.DTOs;
using Sales.API.Application.Responses;
using Sales.API.Application.UseCases.Payments.Create;

namespace Sales.API.Services.Payments;

public sealed class PaymentRestService(HttpClient client)
                  : Service, IPaymentRestService
{
    private readonly HttpClient _client = client;

    public async Task<Response<CreatePaymentResponse>> CreatePaymentAsync(CreatePaymentCommand command)
    {
        var response = await _client.PostAsync("/api/v1/stripe", GetContent(command)).ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<CreatePaymentResponse>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }

    public async Task<Response<IEnumerable<StripeTransactionDTO>>> GetStripeTransactionAsync(string orderCode)
    {
        var response = await _client.GetAsync($"/api/v1/stripe/{orderCode}").ConfigureAwait(false);

        var result = await DeserializeObjectResponse<Response<IEnumerable<StripeTransactionDTO>>>(response);

        return result is not null
            ? new(result.Data, (int)response.StatusCode, result.Message, result.Errors)
            : new(null, 400, "Something failed during the request.");
    }
}
