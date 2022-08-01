using CloudStoage.Domain;
using CloudStoage.Domain.Etos;
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

    public async Task UpdateRand(UploadingEto files, EventHandler<HttpProgressEventArgs> eventHandler = null)
    {
        var http = httpClientFactory.CreateClient(string.Empty);
        HttpClientHandler handler = new();
        ProgressMessageHandler progressMessageHandler = new(handler);
        progressMessageHandler.HttpSendProgress += eventHandler;

        using HttpClient httpClient = new(progressMessageHandler);

        httpClient.BaseAddress = new Uri(Constant.Api);
        httpClient.DefaultRequestHeaders
            .Add(Constant.Authorization, http.DefaultRequestHeaders.FirstOrDefault(x => x.Key == Constant.Authorization).Value);
        httpClient.DefaultRequestHeaders.Add("id", files.Id.ToString());
        
        using var multipartFormData = new MultipartFormDataContent
        {
            { new StreamContent(files.Stream), "file", files.FileName }
        };

        var response = await httpClient.PostAsync(Name + "/upload-file?storageId=" + files.StorageId, multipartFormData);

    }

}
