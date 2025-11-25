
using Agent_Testing.Repository.Interface;
using Agent_Testing.Repository;
using Agent_Testing.Services.Interface;
using Agent_Testing.Services;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos;
using Agent_Testing.DBcontext;

namespace Agent_Testing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IPersonDetails, PersonDetails>();
            builder.Services.AddTransient<IPersonDataRepo, PersonDataRepo>();
            builder.Services.AddTransient<IPersonCosmosService, PersonCosmosService>();
            builder.Services.AddTransient<IPersonCosmosRepo, PersonCosmosRepo>();

            builder.Services.AddControllers();
            //Cosmos DB setup
            ConfigureCosmosDb(builder.Services, builder.Configuration);
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();

        }
        static void ConfigureCosmosDb(IServiceCollection services, IConfiguration config)
        {
            string connectionString = config.GetConnectionString("CosmosDb");
            string databaseName = "iseries-to-cosmos";
            var cosmosClient = new CosmosClientBuilder(connectionString)
                    .WithConnectionModeGateway()
                    .WithSerializerOptions(new CosmosSerializationOptions()
                    {
                        IgnoreNullValues = true,
                        PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                    })
                    .Build();

            services.AddSingleton(cosmosClient);
            var dbContext = new CosmosDbContext(databaseName, cosmosClient);
            services.AddSingleton(dbContext);
        }
    }
}
