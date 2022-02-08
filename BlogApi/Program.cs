using BlogApi.Models;
using BlogApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlogApiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dev"));
});
// Add services to the container.
builder.Services.AddScoped<BlogApiContext>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<TermService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(BlogApi.Helpers.MapperProfile));
var app = builder.Build();

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
