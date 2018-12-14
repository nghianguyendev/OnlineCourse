using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateWay.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCourse.Business.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GateWay.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : Controller
    {
        private ICourseService courseService;

        public CourseController(ICourseService service)
        {
            this.courseService = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = new List<CourseVm>
            {
                new CourseVm()
                {
                    Name = "dot net activity",
                    Url = "http://course.vn/1"
                }
            };
            return Ok(users);
        }

    }
}
