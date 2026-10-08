using apiFestivos.presentacion.InyeccionDependencias;

var builder = WebApplication.CreateBuilder(args);

var configuracion = builder.Configuration;

builder.Services.AgregarDependencias(configuracion);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
