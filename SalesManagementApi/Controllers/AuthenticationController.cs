using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SalesManagementLibrary.Repo.Interfaces;

namespace SalesManagementApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;

    public AuthenticationController(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    public record AuthenticationData(string? UserName, string? Password);

    public record UserData(int Id, string UserName);

    [HttpPost("token")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticationData authData)
    {
        var user = await ValidateCredentials(authData);

        if (user == null)
        {
            return Unauthorized();
        }

        string token = GenerateToken(user);
        return Ok(token);
    }

    private string GenerateToken(UserData user)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_config.GetValue<string>("Authentication:SecretKey"))
        );

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        List<Claim> claims = new List<Claim>();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
        //claims.Add(new(JwtRegisteredClaimNames.GivenName, user.FirstName));
        //claims.Add(new(JwtRegisteredClaimNames.FamilyName, user.LastName));

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(60),
            signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<UserData?> ValidateCredentials(AuthenticationData authData)
    {
        var user = await _userRepository.GetUserByUsername(authData.UserName!);

        if (user != null && BCrypt.Net.BCrypt.Verify(authData.Password, user.PasswordHash))
        {
            return new UserData(user.Id, user.Username);
        }

        return null;
    }

    // Depricated

    //private bool CompareValues(string? actual, string expected)
    //{
    //    if (actual is not null)
    //    {
    //        if (actual.Equals(expected))
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}
}
