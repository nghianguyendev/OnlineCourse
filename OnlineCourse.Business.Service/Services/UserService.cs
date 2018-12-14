using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineCourse.Business.Model;
using OnlineCourse.Business.Model.Helpers;
using OnlineCourse.Business.Service.Interfaces;
using OnlineCourse.Business.Service.Mappers;
using OnlineCourse.Data.Repo;
using OnlineCourse.Data.Repo.DataContext;
using OnlineCourse.Data.Repo.RepositoryFactory;

namespace OnlineCourse.Business.Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryFactory<OCDataContext> repositoryFactory;
        private readonly IRepository repository;
        private readonly AppSettings appSettings;

        public UserService(IRepositoryFactory<OCDataContext> repositoryFactory, OCDataContext dataContext, IOptions<AppSettings> appSettings)
        {
            this.repositoryFactory = repositoryFactory;
            this.repository = this.repositoryFactory.CreateRepository(dataContext);
            this.appSettings = appSettings.Value;
        }

        public UserDto Authenticate(string username, string password)
        {
            var users = new List<UserDto>
            {
                new UserDto()
                {
                    Id = 1,
                    Username = "test", Password = "test"
                }
            };

            var user = users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public UserDto GetById(int userId)
        {
            var users = new List<UserDto>
            {
                new UserDto()
                {
                    Username = "test", Password = "test"
                }
            };
            return users.FirstOrDefault();
        }
    }
}
