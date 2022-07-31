using CloudStorage.Applications.EventHandle;
using CloudStorage.Domain.Shared;
using Token.EventBus;

namespace CloudStorage.Layou.Components.Uploads
{
    partial class UploadTheList
    {
        [Inject]
        public UploadingEventBus UploadingEventBus { get; set; }

        [Inject]
        public IKeyLocalEventBus<bool> UploadTheListEventBus { get; set; }

        protected override async void OnInitialized()
        {
            await UploadTheListEventBus.Subscribe(KeyLoadNames.UploadingListName, a =>
            {
                InvokeAsync(()=> StateHasChanged());
            });

        }
    }
}
