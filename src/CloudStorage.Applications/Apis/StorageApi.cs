using CloudStorage.Applications.Extension;
using CloudStorage.Applications.Services;
using CloudStorage.Domain;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System;
using System.Text;

namespace CloudStorage.Applications.Apis;

public class StorageApi : IScopedDependency
{
    private readonly IHttpClientFactory _httpClientFactor;
    private readonly LoginService loginService;

    private const string Name = "api/storage";
    public StorageApi(IHttpClientFactory httpClientFactor, LoginService loginService)
    {
        _httpClientFactor = httpClientFactor;
        this.loginService = loginService;
    }

    /// <summary>
    /// 获取最近文件操作
    /// </summary>
    /// <returns></returns>
    public async Task<GetNewestStorageDto> GetNewestFileAsync()
    {
        try
        {

            var httpClient = _httpClientFactor.CreateClient();
            var message = await httpClient.GetAsync(Name + "/newest-file");
            var result = await message.Content.JsonFormAsync<ResultDto<GetNewestStorageDto>>();

            if (result.Code == "200")
            {
                return result.Data;
            }

            throw new Exception(result.Message);
        }
        catch (Exception ex)
        {
            if (ex.Message == "账号未授权")
            {
                await loginService.RemoveTokenAsync();
            }
            return null;
        }
    }

    /// <summary>
    /// 获取云盘列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<PagedResultDto<StorageDto>> GetStorageListAsync(GetStorageListInput input)
    {
        var httpClient = _httpClientFactor.CreateClient();

        var message = await httpClient.GetAsync(Name + $"/storage-list?Keywords={input.Keywords}&StorageId={input.StorageId}&Page={input.Page}&PageSize={input.PageSize}");
        var result = await message.Content.JsonFormAsync<ResultDto<PagedResultDto<StorageDto>>>();

        if (result.Code == "200")
        {
            return result.Data;
        }
        throw new Exception(result.Message);
    }

    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task CreateDirectoryAsync(CreateDirectoryInput input)
    {
        var httpClient = _httpClientFactor.CreateClient();

        var reposrt = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
        var message = await httpClient.PostAsync(Name + $"/directory", reposrt);
        var result = await message.Content.JsonFormAsync<ResultDto<PagedResultDto<StorageDto>>>();

        if (result.Code != "200")
        {
            throw new Exception(result.Message);
        }

    }

    /// <summary>
    /// 批量上传文件
    /// </summary>
    /// <param name="files"></param>
    /// <param name="storageId"></param>
    /// <returns></returns>
    public async Task UploadFileListAsync(IReadOnlyList<IBrowserFile> files, Guid? storageId)
    {
        var formData = new MultipartFormDataContent();
        foreach (var d in files)
        {
            formData.Add(new StreamContent(d.OpenReadStream(d.Size)), "files",d.Name);
        }

        var request = new HttpRequestMessage(HttpMethod.Post, Name + $"/upload-file-list?storageId={storageId}")
        {
            Content = formData
        };
        var httpClient = _httpClientFactor.CreateClient();
        _ = await httpClient.SendAsync(request);

    }
}
