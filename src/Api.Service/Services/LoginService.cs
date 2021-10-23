using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;

        private SigningConfigurations _signingConfigurations;

        private TokenConfigurations _tokenConfigurations;

        private IConfiguration _configuration;

        public LoginService(
            IUserRepository repository,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IConfiguration configuration
        )
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _configuration = configuration;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
            var baseUser = new UserEntity();
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);
                if (baseUser == null)
                    return new {
                        authenticated = false,
                        MessageProcessingHandler = "Falha ao autenticar"
                    };

                var identity =
                    new ClaimsIdentity(new GenericIdentity(user.Email),
                        new []
                        {
                            new Claim(JwtRegisteredClaimNames.Jti,
                                Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName,
                                user.Email)
                        });
                var createDate = DateTime.Now;
                var expirationDate =
                    createDate +
                    TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var token =
                    CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate,
                expirationDate,
                token,
                baseUser);
            }

            return new {
                authenticated = false,
                MessageProcessingHandler = "Falha ao autenticar"
            };
        }

        private object
        SuccessObject(
            DateTime createDate,
            DateTime expirationDate,
            string token,
            UserEntity user
        )
        {
            return new {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                userName = user.Name,
                userEmail = user.Email,
                MessageProcessingHandler = "Usuário logado com sucesso"
            };
        }

        private string
        CreateToken(
            ClaimsIdentity identity,
            DateTime createDate,
            DateTime expirationDate,
            JwtSecurityTokenHandler handler
        )
        {
            var securityToken =
                handler
                    .CreateToken(new SecurityTokenDescriptor {
                        Issuer = _tokenConfigurations.Issuer,
                        Audience = _tokenConfigurations.Audience,
                        SigningCredentials =
                            _signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = createDate,
                        Expires = expirationDate
                    });
            var token = handler.WriteToken(securityToken);
            return token;
        }
    }
}
