using Newtonsoft.Json;

namespace CloudStorage.Applications.Extension;

public static class HttpContentExtensions
{
    public static async Task<T> JsonFormAsync<T>(this HttpContent content)
    {
        var result =await content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(result);
    }
}
