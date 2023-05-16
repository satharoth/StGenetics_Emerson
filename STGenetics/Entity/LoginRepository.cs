using STGenetics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Data;

namespace STGenetics.Entity
{
    public class LoginRepository : ILogin
    {

        private readonly STGeneticsContext _context;
        public LoginRepository(STGeneticsContext context)
        {
            _context = context;
        }

        public async Task<Login> LoginDB(Login login)
        {
			login.Token = GenerateToken(login);
			var query = $"UPDATE Login SET Token = '{login.Token}' WHERE UserName LIKE '{login.UserName}' AND Password LIKE '{login.Password}';";
			var query2 = $"SELECT * FROM Login WHERE UserName LIKE '{login.UserName}' AND Password LIKE '{login.Password}';";
			using (var connection = _context.CreateConnection())
            {
				var res = await connection.ExecuteAsync(query);
				return await connection.QueryFirstAsync<Login>(query2);
			}
        }


		public async Task<Login> VerifyToken(string token)
		{
			var query = $"SELECT * FROM Login WHERE Token LIKE '{token}'";
            try
            {
				using (var connection = _context.CreateConnection())
				{
					var res = await connection.QueryFirstAsync<Login>(query);
					return res;
				}
			}
            catch (Exception ex)
            {
				return null;
            }
			
		}


		public string GenerateToken(Login login)
		{
			var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
			var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

			var myIssuer = login.UserName;
			var myAudience = login.Password;

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
			new Claim(ClaimTypes.NameIdentifier, login.UserName.ToString()),
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				Issuer = myIssuer,
				Audience = myAudience,
				SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}


	}
}
