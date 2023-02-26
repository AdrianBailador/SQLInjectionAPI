using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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



string connectionString = "Server=ADRIAN\\SQLEXPRESS;Database=TestPerformance;Trusted_Connection=True;TrustServerCertificate=True";


app.MapGet("/performance/{name}/{country}", (string name, string country) =>
{
    string query = "INSERT INTO Performance(Name, Country) " +
    " VALUES(@name,@country)";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@country", country);
        connection.Open();
        command.ExecuteNonQuery();

    }

})
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
