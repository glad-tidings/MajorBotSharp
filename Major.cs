using System.Text.Json.Serialization;

namespace MajorBot
{

    public class MajorQuery
    {
        public int Index { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Auth { get; set; } = string.Empty;
    }

    public class MajorAccessTokenRequest
    {
        [JsonPropertyName("init_data")]
        public string InitData { get; set; } = string.Empty;
    }

    public class MajorAccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = string.Empty;
        [JsonPropertyName("user")]
        public MajorAccessTokenUser? User { get; set; }
    }

    public class MajorAccessTokenUser
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("is_premium")]
        public bool is_premium { get; set; }
        [JsonPropertyName("rating")]
        public int rating { get; set; }
        [JsonPropertyName("squad_id")]
        public long squad_id { get; set; }
    }

    public class MajorUserDetailResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;
        [JsonPropertyName("rating")]
        public long Rating { get; set; }
    }

    public class MajorGetTaskResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("award")]
        public int Award { get; set; }
        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }
    }

    public class MajorDoneTaskRequest
    {
        [JsonPropertyName("task_id")]
        public int TaskId { get; set; }
    }

    public class MajorDoneTaskResponse
    {
        [JsonPropertyName("task_id")]
        public int TaskId { get; set; }

        [JsonPropertyName("is_completed")]
        public bool IsCompleted { get; set; }
    }

    public class MajorDurovRequest
    {
        [JsonPropertyName("choice_1")]
        public int Choice1 { get; set; }

        [JsonPropertyName("choice_2")]
        public int Choice2 { get; set; }
        [JsonPropertyName("choice_3")]
        public int Choice3 { get; set; }
        [JsonPropertyName("choice_4")]
        public int Choice4 { get; set; }
    }

    public class MajorDurovResponse
    {
        [JsonPropertyName("correct")]
        public List<int>? Correct { get; set; }
    }

    public class MajorRouletteResponse
    {
        [JsonPropertyName("rating_award")]
        public int RatingAward { get; set; }
    }

    public class MajorCoinRequest
    {
        [JsonPropertyName("coins")]
        public int Coins { get; set; }
    }

    public class MajorCoinResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; } = false;
    }
}