﻿using CloudStoage.Domain.HttpModule;
using CloudStoage.Domain.HttpModule.Input;
using CloudStoage.Domain.HttpModule.Result;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Token.Module.Dependencys;

namespace CloudStorage.Applications.Apis;

public class StorageApi : IScopedDependency
{
    private readonly IHttpClientFactory httpClientFactory;

    private const string Name = "api/storage";

    public StorageApi(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task UploadFilesAsync(IBrowserFile file, Guid? storageId = null)
    {
        var formData = new MultipartFormDataContent
        {
            { new StreamContent(file.OpenReadStream(file.Size)), "file", file.Name }
        };

        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var message = await httpclient.PostAsync(Name + "/upload-file?storageId=" + storageId, formData);

        if(message.IsSuccessStatusCode)
        {
            return;
        }

    }

    /// <summary>
    /// 批量上传文件
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public async Task UploadFileListAsync(IReadOnlyList<IBrowserFile> files, Guid? storageId = null)
    {
        var formData = new MultipartFormDataContent();
        foreach(var d in files)
        {
            formData.Add(new StreamContent(d.OpenReadStream(d.Size)), "files", d.Name);
        }

        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var message = await httpclient.PostAsync(Name + "/upload-file-list?storageId=" + storageId, formData);

        if(message.IsSuccessStatusCode)
        {
            return;
        }

    }

    /// <summary>
    /// 获取云盘列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedResultDto<StorageDto>> GetStorageListAsync(GetStorageListInput input)
    {
        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var message = await httpclient.GetAsync(Name + $"/storage-list?Page={input.Page}&PageSize={input.PageSize}&StorageId={input.StorageId}&Keywords={input.Keywords}");

        var data = JsonConvert.DeserializeObject<ModelStateResult<PagedResultDto<StorageDto>>>(await message.Content.ReadAsStringAsync());

        return data.Data;
    }

    /// <summary>
    /// 获取最新操作文件
    /// </summary>
    /// <returns></returns>
    public async Task<ModelStateResult<GetNewestStorageDto>> GetNewestFile()
    {
        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var message = await httpclient.GetAsync(Name + "/newest-file");

        return JsonConvert.DeserializeObject<ModelStateResult<GetNewestStorageDto>>(await message.Content.ReadAsStringAsync());
    }

    /// <summary>
    /// 创建文件夹
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task CreateDirectoryAsync(CreateDirectoryInput input)
    {
        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var message = await httpclient.PostAsJsonAsync(Name + "/directory", input);

        if(message.IsSuccessStatusCode)
        {
            return;
        }
    }

    /// <summary>
    /// 获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<StorageDto> GetStorageAsync(Guid? id)
    {
        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var data = JsonConvert.DeserializeObject<ModelStateResult<StorageDto>>(await httpclient.GetStringAsync(Name + "/storage/" + id));

        return data.Data;
    }

    /// <summary>
    /// 获取上一级
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<Guid?> GoBackAsync(Guid? Id)
    {
        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var data = JsonConvert.DeserializeObject<ModelStateResult<Guid?>>(await httpclient.GetStringAsync(Name + "/go-back?id=" + Id));

        return data.Data;
    }

    /// <summary>
    /// 删除指定文件或文件夹
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteStorageAsync(Guid id)
    {
        var httpclient = httpClientFactory.CreateClient(string.Empty);

        var message = await httpclient.DeleteAsync(Name + "/storage/" + id);

        if(message.IsSuccessStatusCode)
        {
            return;
        }

    }
}
