using Newtonsoft.Json;

namespace CloudStorage.Applications.Helpers;

public class ConfigFile
{
    private const string DefaultName= "config";
    public static async Task SaveConfigAsync(string data, string name= DefaultName)
    {
        var file = File.CreateText("./"+ name);

        await file.WriteLineAsync(data);

        file.Close();
    }

    public static async Task<string> GetConfigAsync(string name = DefaultName)
    {
        if(!File.Exists("./" + name))
        {
            return "";
        }

        var file = File.OpenText("./" + name);

        return await file.ReadLineAsync();
    }

    public static async Task<T> GetConfigAsync<T>(string name = DefaultName)
    {
        if (!File.Exists("./" + name))
        {
            return default;
        }

        var file = File.OpenText("./" + name);

         var data = await file.ReadLineAsync();

        return JsonConvert.DeserializeObject<T>(data);
    }
}
