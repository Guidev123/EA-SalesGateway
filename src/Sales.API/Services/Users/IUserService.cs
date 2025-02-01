namespace Sales.API.Services.Users;

public interface IUserService
{
    Guid? GetUserId();
    string GetToken();
}
