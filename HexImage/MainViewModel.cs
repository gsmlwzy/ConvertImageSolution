using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LibHexImage;

namespace HexImage;

internal partial class MainViewModel : ObservableObject {
    private IConfiguration configuration;

    public MainViewModel(IConfiguration config) {
        configuration = config;
        ColorReverse = Convert.ToBoolean(config["reverseColor"] ?? "True");
        ByteReverse = Convert.ToBoolean(config["reverseByte"] ?? "True");
    }

    // public MainViewModel() {
    // }

    [RelayCommand]
    private void GetFileList(object o) {
        string[]? list = o as string[];
        Dictionary<string, string> directory = new Dictionary<string, string>();

        foreach (var s in list) {
            // 去除文件夹
            if ((File.GetAttributes(s) & FileAttributes.Directory) == FileAttributes.Directory) {
                continue;
            }

            string fileName = Path.GetFileNameWithoutExtension(s);
            if (directory.ContainsKey(fileName)) {
                directory[fileName] = s;
            }
            else {
                directory.Add(fileName, s);
            }
        }

        ObservableCollection<ImageInfo> listCollection = new ObservableCollection<ImageInfo>();
        foreach (var val in directory.Values) {
            listCollection.Add(new ImageInfo(val));
        }

        DropFileList = listCollection;
        VisibilityStatus = Visibility.Visible;
    }

    [RelayCommand]
    private async void ConvertAll(string s) {
        await Parallel.ForEachAsync(DropFileList, async (s, e) => {
            await new HexFile(s.ImagePath, configuration)
                .GenerateAsync();
        });
    }

    [RelayCommand]
    private async void ConvertSelected() {
        if (SelectedImage is null) return;

        await new HexFile(SelectedImage.ImagePath, configuration)
            .GenerateAsync();
    }

    [RelayCommand]
    private void ClearAll() {
        DropFileList.Clear();
        VisibilityStatus = Visibility.Hidden;
    }

    [ObservableProperty]
    private bool _ColorReverse;

    [ObservableProperty]
    private bool _ByteReverse;

    [ObservableProperty]
    private ObservableCollection<ImageInfo> _DropFileList = new ObservableCollection<ImageInfo>();

    [ObservableProperty]
    private ImageInfo? _SelectedImage = null;

    [ObservableProperty]
    private string _DownloadAddress = "0x0806000";

    [ObservableProperty]
    private string _HexLineLength = "0x80";

    [ObservableProperty]
    private Visibility _VisibilityStatus = Visibility.Hidden;
}