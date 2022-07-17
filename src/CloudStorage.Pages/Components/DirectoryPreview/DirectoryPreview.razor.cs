using CloudStorage.Applications.Apis;
using CloudStorage.Domain;

namespace CloudStorage.Pages.Components.DirectoryPreview
{
    partial class DirectoryPreview
    {
        [Parameter]
        public Action<bool>? ActionDialog { get; set; }
        private string Title { get; set; } = "根目录";
        private bool dialog;
        [Parameter]
        public bool Dialog { get
            {
                return dialog;
            } set
            {
                ActionDialog?.Invoke(value);
                dialog=value;
                if (!value)
                {
                    DirectoryPreviewInput?.Invoke(new DirectoryPreviewInput());
                }
            }
        }
        [Inject]
        public StorageApi StorageApi { get; set; }
        public GetStorageListInput Input{ get; set; } = new GetStorageListInput();

        public PagedResultDto<StorageDto> StorageList { get; set; } = new PagedResultDto<StorageDto>();

        /// <summary>
        /// 上传事件
        /// </summary>
        [Parameter]
        public Action<DirectoryPreviewInput>? DirectoryPreviewInput { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await GetStprageListAsync();
        }

        private async Task GetStprageListAsync()
        {
            StorageList = await StorageApi.GetStorageListAsync(Input);
        }

        private async Task OnStorageClick(StorageDto storage)
        {
            if (storage.Type == Domain.Shared.StorageType.Directory)
            {
                Input.StorageId = storage.Id;
                await GetStprageListAsync();
            }
        }

        /// <summary>
        /// 选择当前文件夹
        /// </summary>
        private void SelectCurrentFolder()
        {
            DirectoryPreviewInput?.Invoke(new DirectoryPreviewInput(true,Input.StorageId));
        }
    }
}
