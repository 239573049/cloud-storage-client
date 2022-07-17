using CloudStorage.Applications.Apis;
using CloudStorage.Domain;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace CloudStorage.Pages.Components.Home
{
    partial class FloatingFunction
    {
        private bool isShow;
        private bool ShowDirectoryPreview;
        private IReadOnlyList<IBrowserFile> _browserFiles;
        [Inject] private IJSRuntime Js { get; set; }

        [Parameter]
        public bool IsShow
        {
            get
            {
                return isShow;
            }
            set
            {
                FloatingShow?.Invoke(value);
                isShow = value;
            }
        }
        [Inject]
        public StorageApi StorageApi { get; set; }
        private void OnFileAsync()
        {
            Js.InvokeVoidAsync("onByIdClick", "fileid");
        }

        private async void SelectFolder(DirectoryPreviewInput input)
        {
            if (input?.Succeed==true)
            {
                await StorageApi.UploadFileListAsync(_browserFiles,input.StorageId);
                ShowDirectoryPreview = false;
                StateHasChanged();
            }
        }

        [Parameter]
        public Action<bool>? FloatingShow { get; set; }
        
        private void OnFileClick(IReadOnlyList<IBrowserFile> files)
        {
            IsShow = false;
            ShowDirectoryPreview =true;
            _browserFiles=files;
            StateHasChanged();
        }
    }
}
