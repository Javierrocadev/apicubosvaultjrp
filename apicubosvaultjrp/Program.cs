using apicubosvaultjrp.Data;
using apicubosvaultjrp.Helpers;
using apicubosvaultjrp.Repositories;
using Microsoft.EntityFrameworkCore;
using NSwag.Generation.Processors.Security;
using NSwag;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);
HelperActionServicesOAuth helper = new HelperActionServicesOAuth(builder.Configuration);
builder.Services.AddSingleton<HelperActionServicesOAuth>(helper);
builder.Services.AddAuthentication(helper.GetAuthenticateSchema()).AddJwtBearer(helper.GetJwtBearerOptions());

// Add services to the container.

builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient
    (builder.Configuration.GetSection("KeyVault"));
});
//DEBEMOS PODER RECUPERAR UN OBJETO INYECTADO EN CLASES  
//QUE NO TIENEN CONSTRUCTOR 
SecretClient secretClient =
builder.Services.BuildServiceProvider().GetService<SecretClient>();
KeyVaultSecret secret =
    await secretClient.GetSecretAsync("SqlAzure");
string connectionString = secret.Value;

//string connectionString = builder.Configuration.GetConnectionString("SqlAzure");
builder.Services.AddTransient<RepositoryCubos>();
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddDbContext<CubosContext>(options => options.UseSqlServer(connectionString));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddOpenApiDocument(document =>
{
    document.Title = "Api prueba";
    document.Description = "Api con seguridad 2024";
    // CONFIGURAMOS LA SEGURIDAD JWT PARA SWAGGER, 
    // PERMITE AÑADIR EL TOKEN JWT A LA CABECERA. 
    document.AddSecurity("JWT", Enumerable.Empty<string>(),
        new NSwag.OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copia y pega el Token en el campo 'Value:' así: Bearer {Token JWT}."
        }
    );
    document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseOpenApi();
//app.UseSwagger();
app.UseSwaggerUI(options =>
{
    //INDICAMOS DONDE ESTA EL ENDPOINT DE OPEN API
    options.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json", name: "Api v1");
    //INDICAMOS QUE INDEX SERA LA PAGINA PRINCIPAL DE 
    //NUESTRO API
    options.RoutePrefix = "";
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
