var builder = WebApplication.CreateBuilder(args);

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins",
                      policyBuilder =>
                      {
                          policyBuilder.WithOrigins("http://localhost:4200")
                                       .AllowAnyHeader()
                                       .AllowAnyMethod();
                      });
});

// Agrega servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura la canalización de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyAllowSpecificOrigins"); // Asegúrate de llamar a UseCors antes de UseRouting y UseEndpoints.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
