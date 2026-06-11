// using Avalonia.Controls;

using CarnetGeneratorFACO.Services;
using Avalonia.Controls;

namespace CarnetGeneratorFACO.Views;

public partial class MainWindow : SukiUI.Controls.SukiWindow
{
    public MainWindow()
    {
        InitializeComponent();
        var _fileDialogService = new FileDialogService(this);
    }
}