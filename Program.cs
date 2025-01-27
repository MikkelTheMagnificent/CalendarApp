var builder = WebApplication.CreateBuilder(args);

// Nødvendige tjenester
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

// Konfigurer middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "API is running!");


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();


app.Run();

