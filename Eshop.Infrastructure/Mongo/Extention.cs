using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Mongo
{
    public static class Extention
    {
        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {            
            var db = new MongoConfig();
            configuration.Bind("mongo", db);

            services.AddSingleton<IMongoClient>(client =>
            {
                return new MongoClient(db.ConnectionString);
            });

            services.AddSingleton<IMongoDatabase>(mDb => { 
                var client = mDb.GetService<IMongoClient>();
                return client.GetDatabase(db.Database);
            });
        }
    }
}
