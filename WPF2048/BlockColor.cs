using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
namespace WPF2048
{
    class BlockColor
    {
        private static readonly SolidColorBrush Tile2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EEE4DA"));
        private static readonly SolidColorBrush Tile4 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDE0C8"));
        private static readonly SolidColorBrush Tile8 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2B179"));
        private static readonly SolidColorBrush Tile16 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F59563"));
        private static readonly SolidColorBrush Tile32 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F67C5F"));
        private static readonly SolidColorBrush Tile64 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F65E3B"));
        private static readonly SolidColorBrush Tile128 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDCF72"));
        private static readonly SolidColorBrush Tile256 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDCC61"));
        private static readonly SolidColorBrush Tile512 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDC850"));
        private static readonly SolidColorBrush Tile1024 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDC53F"));
        private static readonly SolidColorBrush Tile2048 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EDC22E"));
        private static readonly SolidColorBrush TileSuper = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C3A32"));
        public static SolidColorBrush getBlockColorBrush(Block b)
        {
            SolidColorBrush color = null;
            switch (b.num)
	        {
                case 2:
                    color = Tile2;
                    break;
                case 4:
                    color = Tile4;
                    break;
                case 8:
                    color = Tile8;
                    break;
                case 16:
                    color = Tile16;
                    break;
                case 32:
                    color = Tile32;
                    break;
                case 64:
                    color = Tile64;
                    break;
                case 128:
                    color = Tile128;
                    break;
                case 256:
                    color = Tile256;
                    break;
                case 512:
                    color = Tile512;
                    break;
                case 1024:
                    color = Tile1024;
                    break;
                case 2048:
                    color = Tile2048;
                    break;
		        default:
                    color = TileSuper;
                    break;
    
	        }
            return color;
        }
    }
}
