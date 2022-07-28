using CloudStoage.Domain;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Handlers;
using Token.Module.Dependencys;

namespace CloudStorage.Applications.Helpers;

public class HtttpClientHelper : IScopedDependency
{
    private const string Name = "api/storage";

    private readonly IHttpClientFactory httpClientFactory;

    public HtttpClientHelper(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task UpdateRand(IReadOnlyList<IBrowserFile> files, Guid? storageId = null, EventHandler<HttpProgressEventArgs> eventHandler = null)
    {
        var http = httpClientFactory.CreateClient(string.Empty);
        HttpClientHandler handler = new();
        ProgressMessageHandler progressMessageHandler = new(handler);
        progressMessageHandler.HttpSendProgress += eventHandler;

        using HttpClient httpClient = new(progressMessageHandler);

        httpClient.BaseAddress = new Uri(Constant.Api);
        httpClient.DefaultRequestHeaders
            .Add(Constant.Authorization, http.DefaultRequestHeaders.FirstOrDefault(x => x.Key == Constant.Authorization).Value);

        using var multipartFormData = new MultipartFormDataContent();
        foreach (var d in files)
        {
            multipartFormData.Add(new StreamContent(d.OpenReadStream(d.Size)), "files", d.Name);
        }
        var response = await httpClient.PostAsync(Name + "/upload-file-list?storageId=" + storageId, multipartFormData);

        if (response.IsSuccessStatusCode)
        {
            return;
        }


    }

}
