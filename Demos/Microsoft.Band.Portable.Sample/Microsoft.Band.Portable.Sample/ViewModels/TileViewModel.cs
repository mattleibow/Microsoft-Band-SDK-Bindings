using System.Windows.Input;

using Xamarin.Forms;

using Microsoft.Band.Portable.Tiles;

namespace Microsoft.Band.Portable.Sample.ViewModels
{
    public class TileViewModel : BaseViewModel
    {
        private BandTileManager tileManager;

        public TileViewModel(BandTile tile, BandTileManager tiles)
        {
            tileManager = tiles;

            Tile = tile;
        }
        
        public BandTile Tile { get; private set; }
    }
}
