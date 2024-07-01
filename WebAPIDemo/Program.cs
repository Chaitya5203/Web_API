using WebAPIDemo;
using WebAPIDemoRepositorys.Data;
using WebAPIDemoRepositorys.Implementation;
using WebAPIDemoRepositorys.Interface;
using WebAPIDemoService.Implementation;
using WebAPIDemoService.Interface;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>();
// Add services to the container.
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("log/villalogs.txt",rollingInterval:RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddHttpClient<IDemoServices, DemoServices>();
builder.Services.AddScoped<IDemoServices, DemoServices>();
builder.Services.AddScoped<IDemoRepository, DemoRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();