using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApi.Data.Entities;

namespace VotingApi.Data.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private static string DatabaseName = "votes";
        private static string CollectionName = "votes";

        private IMongoClient _mongoClient;
        private TelemetryClient _telemetryClient;

        public VoteRepository(IMongoClient mongoClient, TelemetryClient telemetryClient)
        {
            _mongoClient = mongoClient;
            _telemetryClient = telemetryClient;
        }

        public async Task Create(string voterId, string vote)
        {
            // Start tracking our dependency
            var startTime = DateTimeOffset.UtcNow;
            var success = false;
            var voteRecord = new VoteRecord
            {
                VoterId = voterId,
                Vote = vote
            };
                        
            try
            {
                await Collection.InsertOneAsync(voteRecord);
                success = true;
            }
            catch (Exception ex)
            {
                _telemetryClient.TrackException(ex);
                success = false;
            }
            
            var duration = DateTimeOffset.UtcNow - startTime;
            var dependencyTelemetry = new DependencyTelemetry
            {
                Type = "MongoDB",
                Name = "MongoDB",
                Data = $"InsertOne: {JsonConvert.SerializeObject(voteRecord)}",
                Timestamp = startTime,
                Duration = duration,
                Success = success
            };

            _telemetryClient.TrackDependency(dependencyTelemetry);
        }

        private IMongoCollection<VoteRecord> Collection
        {
            get
            {
                var db = _mongoClient.GetDatabase(DatabaseName);
                var collection = db.GetCollection<VoteRecord>(CollectionName);

                return collection;
            } 
        }
    }
}
