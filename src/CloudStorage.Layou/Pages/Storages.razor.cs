using CloudStoage.Domain.Etos;
using CloudStoage.Domain.HttpModule.Input;
using CloudStoage.Domain.HttpModule.Result;
using CloudStorage.Applications.Helpers;
using CloudStorage.Layou.Helper;
using Masa.Blazor;
using Microsoft.AspNetCore.Components.Forms;
using System.Diagnostics;
using System.Net.Http.Handlers;
using Token.EventBus;
using Token.EventBus.EventBus;

namespace CloudStorage.Layou.Pages;


partial class Storages
{
    private bool CreateFolder = false;
    private bool HasFybctuib;
    private bool Load = false;

    /// <summary>
    /// 当前点击的文件id
    /// </summary>
    public Guid? ClickStorageId { get; set; }
    public GetStorageListInput GetStorageListInput { get; set; } = new GetStorageListInput();

    /// <summary>
    /// 列表
    /// </summary>
    public PagedResultDto<StorageDto> StorageList { get; set; } = new PagedResultDto<StorageDto>();

    [Inject]
    public IKeyLocalEventBus<string> DistributedEventBus { get; set; }

    [Inject]
    public ILocalEventBus LocalEventBus { get; set; }

    [Inject]
    public IPopupService PopupService { get; set; }

    /// <summary>
    /// js工具
    /// </summary>
    [Inject]
    public JsHelper jsHelper { get; set; }

    [Inject]
    public StorageApi StorageApi { get; set; }

    [Inject]
    public HtttpClientHelper HtttpClientHelper { get; set; }

    public const string inputFileId = "inputfile";

    private async Task ClickInputFileAsync()
    {
        await jsHelper.ClickInputFileAsync(inputFileId);
    }

    private async Task OnFunctionClickAsync(StorageDto dto)
    {
        ClickStorageId = dto.Id;
        if (dto.Type == Domain.Shared.StorageType.File)
        {
            HasFybctuib = true;
        }
        else
        {
            GetStorageListInput.StorageId = dto.Id;
            await GetStorageListAsync();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        await GetStorageListAsync();
        StorageListBus();
    }

    private void StorageListBus()
    {
        DistributedEventBus.Subscribe(nameof(Storages), async x =>
        {
            await GetStorageListAsync();
            StateHasChanged();
        });
    }

    /// <summary>
    /// 获取云盘列表
    /// </summary>
    /// <returns></returns>
    private async Task GetStorageListAsync()
    {
        Load = true;
        StorageList = await StorageApi.GetStorageListAsync(GetStorageListInput);
        Load = false;
    }

    /// <summary>
    /// 刷新
    /// </summary>
    /// <returns></returns>
    private async Task RefreshAsync()
    {
        await GetStorageListAsync();
    }

    /// <summary>
    /// 返回上一级
    /// </summary>
    /// <returns></returns>
    private async Task GOBackAsync()
    {
        var id = await StorageApi.GoBackAsync(GetStorageListInput.StorageId);
        GetStorageListInput.StorageId = id;
        await GetStorageListAsync();
    }

    private async Task UploadFilesAsync(InputFileChangeEventArgs eventArgs)
    {
        var files = eventArgs.GetMultipleFiles(10);
        if (files.Count > 0)
        {
            await PopupService.ToastAsync("上传文件", BlazorComponent.AlertTypes.Info);
            var uploadings = files.Select(x => new UploadingEto
            {
                Id = Guid.NewGuid(),
                FileName = x.Name,
                Length = x.Size,
                Stream = x.OpenReadStream(x.Size)
            }).ToList();

            await LocalEventBus.PublishAsync(uploadings,false);
            StateHasChanged();
        }
    }

}