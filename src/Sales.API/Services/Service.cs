﻿using System.Net;
using System.Text;
using System.Text.Json;

namespace Sales.API.Services;

public abstract class Service
{
    protected StringContent GetContent(object data)
        => new(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

    protected async Task<T?> DeserializeObjectResponse<T>(HttpResponseMessage response)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };

        return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), options);
    }

    protected bool OpertationIsValid(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.BadRequest)
            return false;

        var result = response.EnsureSuccessStatusCode();

        return result.IsSuccessStatusCode;
    }
}
