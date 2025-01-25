using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Sales.API.Configurations;
using System.Text.Json.Serialization;

namespace Sales.API.Responses;

public class PagedResponse<TData> : Response<TData>
{
    [JsonConstructor]
    public PagedResponse(
    int totalCount,
        TData? data = default,
        int currentPage = ApiConfiguration.DEFAULT_PAGE,
        int pageSize = ApiConfiguration.DEFAULT_PAGE_SIZE,
        int code = DEFAULT_STATUS_CODE,
        string? message = null,
        string[]? errors = null)
        : base(data, code, message, errors)
    {
        Data = data;
        TotalCount = totalCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public PagedResponse(
        TData? data,
        int code = DEFAULT_STATUS_CODE,
        string? message = null,
        string[]? errors = null)
        : base(data, code, message, errors)
    {
    }

    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int PageSize { get; set; } = ApiConfiguration.DEFAULT_PAGE_SIZE;
    public int TotalCount { get; set; }
}
