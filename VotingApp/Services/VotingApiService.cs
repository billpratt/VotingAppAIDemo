using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Settings;

namespace VotingApp.Services
{
    public class VotingApiService
    {
        private readonly HttpClient _httpClient;
        private readonly TelemetryClient _telemetryClient;

        public VotingApiService(IOptions<VotingApiSettings> settingsWrapper, TelemetryClient telemetryClient)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(settingsWrapper.Value.Url) };
            _telemetryClient = telemetryClient;
        }

        public async Task<(bool success, string msg)> PostVote(string voterId, string vote)
        {
            TrackVoteEvent(voterId, vote);

            var resource = "api/votes";

            var voteRequest = new VotingApiPostRequest
            {
                VoterId = voterId,
                Vote = vote
            };

            var json = JsonConvert.SerializeObject(voteRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(resource, content);

            var success = response.IsSuccessStatusCode;
            var msg = await response.Content.ReadAsStringAsync();

            return (success, msg);
        }

        private void TrackVoteEvent(string voterId, string vote)
        {
            _telemetryClient.TrackEvent("Vote", new Dictionary<string, string>
            {
                { "voterId", voterId },
                { "vote", vote }
            });
        }
    }
}
