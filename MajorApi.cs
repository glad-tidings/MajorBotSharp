﻿namespace MajorBot
{

    public class MajorApi
    {
        private readonly HttpClient client;

        public MajorApi(int Mode, string queryID, int queryIndex)
        {
            client = new HttpClient() { Timeout = new TimeSpan(0, 0, 30) };
            if (Mode == 1)
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {queryID}");
            client.DefaultRequestHeaders.Add("Accept-Language", "fa-IR,fa;q=0.9,en-US;q=0.8,en;q=0.7,en-GB;q=0.6,zh-TW;q=0.5,zh-CN;q=0.4,zh;q=0.3");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            client.DefaultRequestHeaders.Add("Origin", "https://major.bot");
            client.DefaultRequestHeaders.Add("Referer", "https://major.bot/");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
            client.DefaultRequestHeaders.Add("User-Agent", Tools.getUserAgents(queryIndex));
            client.DefaultRequestHeaders.Add("accept", "*/*");
            client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            client.DefaultRequestHeaders.Add("sec-ch-ua-platform", $"\"{Tools.getUserAgents(queryIndex, true)}\"");
        }

        public async Task<HttpResponseMessage> MAPIGet(string requestUri)
        {
            try
            {
                return await client.GetAsync(requestUri);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.ExpectationFailed, ReasonPhrase = ex.Message };
            }
        }

        public async Task<HttpResponseMessage> MAPIPost(string requestUri, HttpContent content)
        {
            try
            {
                return await client.PostAsync(requestUri, content);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.ExpectationFailed, ReasonPhrase = ex.Message };
            }
        }
    }
}