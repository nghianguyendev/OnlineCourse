using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCourse.Data.Repo.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Data.Repo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OCDataContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection")));
        }
    }
}
