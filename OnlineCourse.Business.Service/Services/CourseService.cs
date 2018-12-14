using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using OnlineCourse.Business.Model;
using OnlineCourse.Business.Service.Interfaces;
using OnlineCourse.Business.Service.Mappers;
using OnlineCourse.Data.Repo;
using OnlineCourse.Data.Repo.DataContext;
using OnlineCourse.Data.Repo.RepositoryFactory;

namespace OnlineCourse.Business.Service
{
    public class CourseService : ICourseService
    {
        private readonly IRepositoryFactory<OCDataContext> repositoryFactory;
        private readonly IRepository repository;

        public CourseService(IRepositoryFactory<OCDataContext> repositoryFactory, OCDataContext dataContext)
        {
            this.repositoryFactory = repositoryFactory;
            this.repository = this.repositoryFactory.CreateRepository(dataContext);
        }

        public void Create(CourseDto course)
        {
            var entity = CourseMapper.ToEntity(course);
            repository.Create(entity);
        }
    }
}
