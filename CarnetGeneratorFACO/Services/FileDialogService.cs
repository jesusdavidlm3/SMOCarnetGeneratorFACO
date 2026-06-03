using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Window = ShadUI.Window;

namespace CarnetGeneratorFACO.Services;

public class FileDialogService
{
    private static Window _window { get; set; }
    
    public FileDialogService(Window window)
    {
        _window = window;
    }

    public static async Task<IReadOnlyList<IStorageFile>> ShowSelectFileDialog()
    {
        var storage = _window.StorageProvider;
        var result = await storage.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Selecciona una foto",
            FileTypeFilter = new []{ FilePickerFileTypes.ImageAll }
        });
        return result;
    }
}