using CloudStoage.Domain;
using Microsoft.AspNetCore.Components.Forms;
using Token.MAUI.Module.Dependencys;

namespace CloudStorage.Applications.Apis;

public class UserInfoApi : IScopedDependency 
{
    private readonly IHttpClientFactory httpClientFactory;

    public UserInfoApi(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public  Task UploadFileListAsync(IReadOnlyList<IBrowserFile> files)
    {
        var formData = new MultipartFormDataContent();
        foreach (var d in files)
        {
            formData.Add(new StreamContent(d.OpenReadStream(d.Size)), "files", d.Name);
        }

        var httpclient = httpClientFactory.CreateClient(string.Empty);

    }
}
