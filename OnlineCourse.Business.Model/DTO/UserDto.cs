using System;
using OnlineCourse.Business.Model.DTO.Base;

namespace OnlineCourse.Business.Model
{
    public class UserDto : BaseDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }
    }
}
