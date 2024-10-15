using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MajorBot
{

    public class MajorBots
    {

        private readonly MajorQuery PubQuery;
        private readonly string AccessToken;
        private readonly long UserId;
        public readonly bool HasError;
        public readonly string ErrorMessage;

        public MajorBots(MajorQuery Query)
        {
            PubQuery = Query;
            var GetToken = MajprGetToken().Result;
            if (GetToken != null)
            {
                AccessToken = GetToken.AccessToken;
                UserId = GetToken.User?.Id ?? 0;
                HasError = false;
                ErrorMessage = "";
            }
            else
            {
                AccessToken = "";
                HasError = true;
                ErrorMessage = "get token failed";
            }
        }

        private async Task<MajorAccessTokenResponse?> MajprGetToken()
        {
            var MAPI = new MajorApi(0, PubQuery.Auth, PubQuery.Index);
            var request = new MajorAccessTokenRequest() { InitData = PubQuery.Auth };
            string serializedRequest = JsonSerializer.Serialize(request);
            var serializedRequestContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
            var httpResponse = await MAPI.MAPIPost("https://major.bot/api/auth/tg/", serializedRequestContent);
            if (httpResponse != null)
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                    var responseJson = await JsonSerializer.DeserializeAsync<MajorAccessTokenResponse>(responseStream);
                    return responseJson;
                }
            }

            return null;
        }

        public async Task<MajorUserDetailResponse?> MajprUserDetail()
        {
            var MAPI = new MajorApi(1, AccessToken, PubQuery.Index);
            var httpResponse = await MAPI.MAPIGet($"https://major.bot/api/users/{UserId}/");
            if (httpResponse != null)
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                    var responseJson = await JsonSerializer.DeserializeAsync<MajorUserDetailResponse>(responseStream);
                    return responseJson;
                }
            }

            return null;
        }

        public async Task<List<MajorGetTaskResponse>?> MajorGetTasks(bool Daily)
        {
            var MAPI = new MajorApi(1, AccessToken, PubQuery.Index);
            var httpResponse = await MAPI.MAPIGet($"https://major.bot/api/tasks/?is_daily={(Daily ? "true" : "false")}");
            if (httpResponse != null)
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                    var responseJson = await JsonSerializer.DeserializeAsync<List<MajorGetTaskResponse>>(responseStream);
                    return responseJson;
                }
            }

            return null;
        }

        public async Task<MajorDoneTaskResponse?> MajorDoneTask(int TaskID)
        {
            var MAPI = new MajorApi(1, AccessToken, PubQuery.Index);
            var request = new MajorDoneTaskRequest() { TaskId = TaskID };
            string serializedRequest = JsonSerializer.Serialize(request);
            var serializedRequestContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
            var httpResponse = await MAPI.MAPIPost("https://major.bot/api/tasks/", serializedRequestContent);
            if (httpResponse != null)
            {
                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                    var responseJson = await JsonSerializer.DeserializeAsync<MajorDoneTaskResponse>(responseStream);
                    return responseJson;
                }
            }

            return null;
        }

        public async Task<int> MajorDurov()
        {
            var client = new HttpClient() { Timeout = new TimeSpan(0, 0, 30) };
            client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true, NoStore = true, MaxAge = TimeSpan.FromSeconds(0d) };
            var GetDurev = await client.GetAsync("https://raw.githubusercontent.com/glad-tidings/MajorBot/refs/heads/main/puzzle.json");
            if (GetDurev is not null)
            {
                if (GetDurev.IsSuccessStatusCode)
                {
                    var responseStream = await GetDurev.Content.ReadAsStreamAsync();
                    var responseJson = await JsonSerializer.DeserializeAsync<MajorDurovRequest>(responseStream);
                    return await MajorDurov(responseJson ?? new());
                }
            }

            return 1;
        }

        private async Task<int> MajorDurov(MajorDurovRequest request)
        {
            var MAPI = new MajorApi(1, AccessToken, PubQuery.Index);
            var getResponse = await MAPI.MAPIGet($"https://major.bot/api/durov/");
            if (getResponse is not null)
            {
                if (getResponse.IsSuccessStatusCode)
                {
                    var getResponseStream = await getResponse.Content.ReadAsStreamAsync();
                    var getResponseJson = await JsonSerializer.DeserializeAsync<MajorCoinResponse>(getResponseStream);
                    if (getResponseJson?.Success ?? false)
                    {
                        Thread.Sleep(15000);
                        string serializedRequest = JsonSerializer.Serialize(request);
                        var serializedRequestContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
                        var httpResponse = await MAPI.MAPIPost($"https://major.bot/api/durov/", serializedRequestContent);
                        if (httpResponse != null)
                        {
                            if (httpResponse.IsSuccessStatusCode)
                            {
                                var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                                var responseJson = await JsonSerializer.DeserializeAsync<MajorDurovResponse>(responseStream);
                                return (responseJson?.Correct?.Count == 4 ? 2 : 0);
                            }
                        }

                        return 0;
                    }
                }
            }

            return 1;
        }

        public async Task<int> MajorHoldCoin()
        {
            var MAPI = new MajorApi(1, AccessToken, PubQuery.Index);
            var getResponse = await MAPI.MAPIGet($"https://major.bot/api/bonuses/coins/");
            if (getResponse is not null)
            {
                if (getResponse.IsSuccessStatusCode)
                {
                    var getResponseStream = await getResponse.Content.ReadAsStreamAsync();
                    var getResponseJson = await JsonSerializer.DeserializeAsync<MajorCoinResponse>(getResponseStream);
                    if (getResponseJson?.Success ?? false)
                    {
                        Thread.Sleep(15000);
                        var request = new MajorCoinRequest() { Coins = 915 };
                        string serializedRequest = JsonSerializer.Serialize(request);
                        var serializedRequestContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
                        var httpResponse = await MAPI.MAPIPost("https://major.bot/api/bonuses/coins/", serializedRequestContent);
                        if (httpResponse != null)
                        {
                            if (httpResponse.IsSuccessStatusCode)
                            {
                                var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                                var responseJson = await JsonSerializer.DeserializeAsync<MajorCoinResponse>(responseStream);
                                return (responseJson?.Success ?? false ? 2 : 0);
                            }
                        }

                        return 0;
                    }
                }
            }

            return 1;
        }

        public async Task<int> MajorRoulette()
        {
            var MAPI = new MajorApi(1, AccessToken, PubQuery.Index);
            var getResponse = await MAPI.MAPIGet($"https://major.bot/api/roulette/");
            if (getResponse is not null)
            {
                if (getResponse.IsSuccessStatusCode)
                {
                    var getResponseStream = await getResponse.Content.ReadAsStreamAsync();
                    var getResponseJson = await JsonSerializer.DeserializeAsync<MajorCoinResponse>(getResponseStream);
                    if (getResponseJson?.Success ?? false)
                    {
                        Thread.Sleep(15000);
                        var httpResponse = await MAPI.MAPIPost($"https://major.bot/api/roulette/", null);
                        if (httpResponse != null)
                        {
                            if (httpResponse.IsSuccessStatusCode)
                            {
                                var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                                var responseJson = await JsonSerializer.DeserializeAsync<MajorRouletteResponse>(responseStream);
                                return (responseJson?.RatingAward > 0 ? 2 : 0);
                            }
                        }

                        return 0;
                    }
                }
            }

            return 1;
        }

        public async Task<int> MajorSwipeCoin(int Coins)
        {
            var MAPI = new MajorApi(1, AccessToken, PubQuery.Index);
            var getResponse = await MAPI.MAPIGet($"https://major.bot/api/swipe_coin/");
            if (getResponse is not null)
            {
                if (getResponse.IsSuccessStatusCode)
                {
                    var getResponseStream = await getResponse.Content.ReadAsStreamAsync();
                    var getResponseJson = await JsonSerializer.DeserializeAsync<MajorCoinResponse>(getResponseStream);
                    if (getResponseJson?.Success ?? false)
                    {
                        Thread.Sleep(15000);
                        var request = new MajorCoinRequest() { Coins = Coins };
                        string serializedRequest = JsonSerializer.Serialize(request);
                        var serializedRequestContent = new StringContent(serializedRequest, Encoding.UTF8, "application/json");
                        var httpResponse = await MAPI.MAPIPost("https://major.bot/api/swipe_coin/", serializedRequestContent);
                        if (httpResponse != null)
                        {
                            if (httpResponse.IsSuccessStatusCode)
                            {
                                var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                                var responseJson = await JsonSerializer.DeserializeAsync<MajorCoinResponse>(responseStream);
                                return (responseJson?.Success ?? false ? 2 : 0);
                            }
                        }

                        return 0;
                    }
                }
            }

            return 1;
        }

    }
}