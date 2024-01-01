using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HexImage {
    public partial class ImageInfo : ObservableObject {
        [ObservableProperty]
        private string _ImagePath;

        [ObservableProperty]
        private string _Name;


        public ImageInfo(string imagePath) {
            ImagePath = imagePath;
            Name = Path.GetFileName(ImagePath);
        }


        public override string ToString() {
            return $"{ImagePath}";
        }
    }
}