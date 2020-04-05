using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertApi.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoMapper;

namespace AdvertApi.Services
{
    public class DynamoDbAdvertStorageService : IAdvertStorageService
    {
        private readonly IMapper _mapper;

        public DynamoDbAdvertStorageService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<string> Add(AdvertModel model)
        {
            var dbModel = _mapper.Map<AdvertDbModel>(model);
            dbModel.Id = new Guid().ToString();
            dbModel.CreationDateTime = DateTime.UtcNow;
            dbModel.Status = AdvertStatus.Pending;

            using var client = new AmazonDynamoDBClient();
            using var context = new DynamoDBContext(client);
            await context.SaveAsync(dbModel);

            return dbModel.Id;
        }

        public async Task<AdvertDbModel> GetById(string id)
        {
            using var client = new AmazonDynamoDBClient();
            using var context = new DynamoDBContext(client);
            var record = await context.LoadAsync<AdvertDbModel>(id) ??
                         throw new KeyNotFoundException($"A record with ID = {id} was not found");

            return record;
        }

        public async Task Confirm(ConfirmAdvertModel model)
        {
            using var client = new AmazonDynamoDBClient();
            using var context = new DynamoDBContext(client);
            var record = await context.LoadAsync<AdvertDbModel>(model.Id) ?? 
                         throw new KeyNotFoundException($"A record with ID = {model.Id} was not found");
            if (model.Status == AdvertStatus.Active)
            {
                record.Status = AdvertStatus.Active;
                await context.SaveAsync(record);
            }
            else
            {
                await context.DeleteAsync(record);
            }
        }

        public async Task<bool> CheckHealthAsync()
        {
            using var client = new AmazonDynamoDBClient();
            var tableData = await client.DescribeTableAsync("Adverts");
            return tableData.Table.TableStatus.Value.Equals("active", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
