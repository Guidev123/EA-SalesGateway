namespace Sales.API.Services.User;

public interface IUserService
{
    Guid? GetUserId();
    string GetToken();
}
