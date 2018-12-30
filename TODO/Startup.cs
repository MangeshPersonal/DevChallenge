using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq;
using TODO.LOGGER;
using TODO.LOGIC;
using TODO.MODELS.APIResponse;
using TODO.MODELS.ResponseModel;
using TODO.REPOSITORY;

namespace TODO.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);

            //Adding Database context...
            services.AddDbContext<ToDoContext>(opt => opt.UseSqlServer(Configuration["Database:Connectionstring"]));


            // Adding Global Handler for the Data Validation 
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new ValidationError
                        {
                            Name = e.Key,
                            Description = e.Value.Errors.First().ErrorMessage
                        }).ToList();

                    return new BadRequestObjectResult(new ToDoResponse(statusCode: (int)System.Net.HttpStatusCode.BadRequest, result: errors, errorMessage: "Bad Request"));
                };
            });

            //Adding Repository
            services.AddScoped<TODO.MODELS.REPOSITORY.IToDoRespository<MODELS.DataModels.ToDoDataModel>, TODO.REPOSITORY.ToDoRepository>();
            //Adding Buisness Logic 
            services.AddScoped<TODO.MODELS.Contracts.IBusinessLogic, ToDoDataLogic>();

            //Adding Swagger services
            services.AddSwaggerGen(opt => opt.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info()
            {
                Version="2.0",Title="ToDo API",Description="ToDo Description"
                
            }));

            // Adding Logger services
            services.AddSingleton<ILogger, ExceptionLogger>();


            services.AddCors(options =>
            {
                options.AddPolicy("ToDoAPIPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            ////}
            ///
            app.UseCors("ToDoAPIPolicy");
            app.UseCutomExceptionMiddleWare();
            app.UseMvc();
            
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "TODO API");
            });

            // Global Exception Handler.
           
        }
    }
}
