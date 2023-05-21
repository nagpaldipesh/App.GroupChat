using App.GroupChat.Api.Extensions;
using App.GroupChat.Data;
using App.GroupChat.Services.Automapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddServicesIntoServiceCollection();

builder.Services.AddAutoMapper(typeof(MapDomainToUiDtoProfile), typeof(MapUiDtoToDomainProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentGeneration();

builder.Services.AddDbContext<GroupChatContext>(options => {
    options.UseInMemoryDatabase("GroupChatDb");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<GroupChatContext>())
    context.Database.EnsureCreated();

app.UseCustomExceptionHandling();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
