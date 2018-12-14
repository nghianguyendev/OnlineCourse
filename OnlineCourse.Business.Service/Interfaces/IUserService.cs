using OnlineCourse.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Business.Service.Interfaces
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);

        UserDto GetById(int userId);
    }
}
