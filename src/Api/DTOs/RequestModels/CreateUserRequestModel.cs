namespace Api.DTOs.RequestModels;

public class CreateUserRequestModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Cpf { get; set; }
}
