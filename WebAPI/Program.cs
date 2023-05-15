using Microsoft.EntityFrameworkCore;
using WebAPI.Application.Models;
using WebAPI.Application.Models.MapperProfiles;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MasterDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("SampleDatabase"))
);
builder.Services.AddHttpLogging(logging => { });
builder.Services.AddCors();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add auto mapper
builder.Services.AddAutoMapper(typeof(MapperProfile));
// Add Services
builder.AddServices();

var app = builder.Build();
// Apply migration
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<MasterDbContext>();
//    dbContext.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
