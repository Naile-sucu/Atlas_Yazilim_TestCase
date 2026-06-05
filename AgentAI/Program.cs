using AgentAI.Configuration;
using AgentAI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var agentConfSection = builder.Configuration.GetSection("AgentConfiguration");
var agentConf = agentConfSection.Get<AgentConfiguration>() ?? throw new Exception("Application configuration not found.");

builder.Services.Configure<AgentConfiguration>(agentConfSection);

builder.Services.AddScoped<IChatService,ChatService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddHttpClient<ChatService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
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
