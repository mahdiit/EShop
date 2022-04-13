using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Infrastructure.Mongo
{
    public interface IDbInit
    {
        Task InitAsync();
    }

    public class DbInit : IDbInit
    {
        IMongoDatabase database;
        bool isInit;
        public DbInit(IMongoDatabase mongoDatabase)
        {
            database = mongoDatabase;
        }
        public async Task InitAsync()
        {
            if (isInit)
                return;

            IConventionPack pack = new ConventionPack()
            {
                new IgnoreExtraElementsConvention(true),
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String)
            };

            ConventionRegistry.Register("EShop", pack, c => true);
            isInit = true;

            await Task.CompletedTask;
        }
    }
}
