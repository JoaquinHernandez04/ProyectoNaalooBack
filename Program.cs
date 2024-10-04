using ClientesAPI.Repositories;  
var builder = WebApplication.CreateBuilder(args);

// Configura ASP.NET Core para usar Newtonsoft.Json
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

var supabaseUrl = builder.Configuration["Supabase:Url"];
var supabaseApiKey = builder.Configuration["Supabase:ApiKey"];

if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseApiKey))
{
    throw new InvalidOperationException("Supabase URL or API Key no configurada.");
}

var supabaseClient = new Supabase.Client(supabaseUrl, supabaseApiKey);
builder.Services.AddSingleton(supabaseClient);

builder.Services.AddScoped<ClienteRepository>();

var app = builder.Build();

app.UseMiddleware<MiddlewareTime>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();