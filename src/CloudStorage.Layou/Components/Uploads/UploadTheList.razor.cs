using CloudStoage.Domain;
using CloudStorage.Applications.EventHandle;
using CloudStorage.Domain.Shared;
using System.Collections.Concurrent;
using System.Drawing;
using Token.EventBus;

namespace CloudStorage.Layou.Components.Uploads
{
    partial class UploadTheList
    {
        [Inject]
        public UploadingEventBus UploadingEventBus { get; set; }

        [Inject]
        public IKeyLocalEventBus<UploadingDto> UploadTheListEventBus { get; set; }

        public static BlockingCollection<UploadingDto> UploadingList { get; set; }

        protected override async void OnInitialized()
        {

            UploadingList = UploadingEventBus.UploadingList;

            await UploadTheListEventBus.Subscribe(KeyLoadNames.UploadingListName, a =>
            {
                foreach (var d in UploadingList)
                {
                    if (d.Id == a.Id)
                    {
                        d.Stats = a.Stats;
                        d.UploadingSize = a.UploadingSize;
                        StateHasChanged();
                        return;
                    }
                }

            });

        }
    }
}
