using System.IdentityModel.Tokens.Jwt;

namespace Api.Token;

public class JwtToken
{
    private JwtSecurityToken _token;

    internal JwtToken(JwtSecurityToken token)
    {
        this._token = token;
    }

    public DateTime ValidTo => _token.ValidTo;

    public string value => new JwtSecurityTokenHandler().WriteToken(this._token);
}
