namespace Sales.API.Application.DTOs;

public record AddressDTO(string Street, string Number, string AdditionalInfo,
                         string Neighborhood, string ZipCode, string City,
                         string State);