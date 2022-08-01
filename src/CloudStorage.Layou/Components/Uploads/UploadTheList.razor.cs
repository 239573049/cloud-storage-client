using CloudStoage.Domain;
using CloudStorage.Applications.EventHandle;
using CloudStorage.Domain.Shared;
using System.Collections.Concurrent;
using Token.EventBus;

namespace CloudStorage.Layou.Components.Uploads
{
    partial class UploadTheList
    {
        [Inject]
        public UploadingEventBus UploadingEventBus { get; set; }

        [Inject]
        public IKeyLocalEventBus<bool> UploadTheListEventBus { get; set; }

        public BlockingCollection<UploadingDto> UploadingList { get; set; } = new BlockingCollection<UploadingDto>();

        protected override async void OnInitialized()
        {
            await UploadTheListEventBus.Subscribe(KeyLoadNames.UploadingListName, a =>
            {
                UploadingList = UploadingEventBus.UploadingList;
                InvokeAsync(() => StateHasChanged());
            });

        }
    }
}
