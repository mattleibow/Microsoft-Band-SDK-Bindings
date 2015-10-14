using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.Band.Portable.Tiles;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class TilesViewModel : BaseClientViewModel
    {
        private BandTileManager tileManager;

        public TilesViewModel(BandDeviceInfo info, BandClient bandClient)
            : base(info, bandClient)
        {
            tileManager = bandClient.TileManager;
            Tiles = new ObservableCollection<TileViewModel>();
        }

        public override async Task Prepare()
        {
            await base.Prepare();

            IsLoading = true;

            RemainingCapacity = await tileManager.GetRemainingTileCapacityAsync();
            OnPropertyChanged("RemainingCapacity");

            var newTiles = (await tileManager.GetTilesAsync()).ToArray();
            var toAdd = newTiles.Where(n => Tiles.All(o => o.Tile.Id != n.Id)).ToArray();
            var toRemove = Tiles.Where(o => newTiles.All(n => n.Id != o.Tile.Id)).ToArray();
            foreach (var tile in toAdd)
            {
                Tiles.Add(new TileViewModel(tile, tileManager));
            }
            foreach (var tile in toRemove)
            {
                Tiles.Remove(tile);
            }

            IsLoading = false;
        }

        public int RemainingCapacity { get; private set; }

        public ObservableCollection<TileViewModel> Tiles { get; private set; }
    }
}
