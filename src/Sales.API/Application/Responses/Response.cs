﻿using System.Text.Json.Serialization;

namespace Sales.API.Application.Responses;

public class Response<TData>(
    TData? data,
    int? code = null,
    string? message = null,
    string[]? errors = null)
{
    [JsonIgnore]
    public readonly int StatusCode = code ?? DEFAULT_STATUS_CODE;
    public const int DEFAULT_STATUS_CODE = 200;
    public TData? Data { get; set; } = data;
    public string? Message { get; } = message;
    public string[]? Errors { get; } = errors;
    public bool IsSuccess =>
        StatusCode is >= DEFAULT_STATUS_CODE and <= 299;
}

public class Response(
    int? code = null,
    string? message = null,
    string[]? errors = null)
{
    [JsonIgnore]
    public readonly int StatusCode = code ?? DEFAULT_STATUS_CODE;
    public const int DEFAULT_STATUS_CODE = 200;
    public string? Message { get; } = message;
    public string[]? Errors { get; } = errors;
    public bool IsSuccess =>
        StatusCode is >= DEFAULT_STATUS_CODE and <= 299;
}