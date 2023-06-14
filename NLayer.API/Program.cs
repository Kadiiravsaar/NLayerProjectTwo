using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Repository;
using System.Reflection;
using NLayer.Core.Services;
using NLayer.Service.Services;
using NLayer.Service.Mapping;
using FluentValidation.AspNetCore;
using NLayer.Service.Validations;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

// AddFluentValidation Eklemek istiyorsan api katmanýna da fluentvaldation eklemen lazým


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;


    //    // ben global olarak filter eklemek istediðim için yazdým çünkü
    //    // 80 controller olsa baþýna gidip atrribute yazamam
    //

});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundFilter<>));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repository Start
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));

// Repository End


// Service Start
builder.Services.AddScoped(typeof(IService<>),typeof(Service<>));
builder.Services.AddScoped(typeof(IProductService),typeof(ProductService));
builder.Services.AddScoped(typeof(ICategoryService), typeof(CategoryService));

// Service End


builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
