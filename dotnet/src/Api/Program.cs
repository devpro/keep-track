var builder = WebApplication.CreateBuilder(args);

var configuration = new AppConfiguration(builder.Configuration);

// adds services to the container
builder.Services.AddSingleton(configuration.ConfigurationRoot)
    .AddCarInfrastructureMongoDb()
    .AddInventoryInfrastructureMongoDb()
    .AddMovieInfrastructureMongoDb()
    .AddMongoDbContext<AppConfiguration>();

var mappingConfig = new MapperConfiguration(x =>
{
    // Infrastructure MongoDB
    x.AddProfile(new KeepTrack.CarComponent.Infrastructure.MongoDb.MappingProfiles.CarMappingProfile());
    x.AddProfile(new KeepTrack.InventoryComponent.Infrastructure.MongoDb.MappingProfiles.InventoryMappingProfile());
    x.AddProfile(new KeepTrack.MovieComponent.Infrastructure.MongoDb.MappingProfiles.MovieMappingProfile());
    x.CreateMap<ObjectId, string>().ConvertUsing<ObjectIdToStringConverter>();
    x.CreateMap<string, ObjectId>().ConvertUsing<StringToObjectIdConverter>();
    // Api
    x.AddProfile(new KeepTrack.Api.MappingProfiles.GenericMappingProfile());
    // General
    x.AllowNullCollections = true;
});
var mapper = mappingConfig.CreateMapper();
mapper.ConfigurationProvider.AssertConfigurationIsValid();
builder.Services.AddSingleton(mapper);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = configuration.ConfigurationRoot["Authentication:JwtBearer:Authority"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration.ConfigurationRoot["Authentication:JwtBearer:TokenValidation:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration.ConfigurationRoot["Authentication:JwtBearer:TokenValidation:Audience"],
            ValidateLifetime = true
        };
    });

if (configuration.CorsAllowedOrigin != null)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            "CorsPolicyName",
            builder =>
            {
                builder
                    .WithOrigins(configuration.CorsAllowedOrigin.ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
}

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add<KeepTrack.Api.Filters.CustomExceptionFilterAttribute>();
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(configuration.OpenApiInfo.Version,
    new OpenApiInfo { Title = configuration.OpenApiInfo.Title, Version = configuration.OpenApiInfo.Version });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new[] { "readAccess", "writeAccess" }
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddHealthChecks();

var app = builder.Build();

// configures the HTTP request pipeline
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint($"/swagger/{configuration.OpenApiInfo.Version}/swagger.json", configuration.OpenApiInfo.Title);
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicyName");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers()
        .RequireCors("CorsPolicyName");
    endpoints.MapHealthChecks("/health");
});

app.Run();

#pragma warning disable CA1050 // Declare types in namespaces
/// <summary>
/// Fix: make Program class public for tests
/// </summary>
public partial class Program { }
#pragma warning restore CA1050
