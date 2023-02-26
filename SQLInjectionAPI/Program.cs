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


app.MapGet("/performance/{name}/{country}", (string name, string country) =>
{
    string query = "INSERT INTO Performance(Name, Country) " +
    " VALUES('\"+name+\"','\"+country+\"')";

using (SqlConnection connection = new SqlConnection(connectionString))
    {
        SqlCommand command = new SqlCommand(query, connection);

        connection.Open();
        command.ExecuteNonQuery();

    }

})
.WithOpenApi();

app.Run();

