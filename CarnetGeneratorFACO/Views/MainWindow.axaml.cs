// using Avalonia.Controls;

using CarnetGeneratorFACO.Services;
using ShadUI;

namespace CarnetGeneratorFACO.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var _fileDialogService = new FileDialogService(this);
    }
}