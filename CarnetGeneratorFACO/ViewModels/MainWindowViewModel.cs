using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CarnetGeneratorFACO.Functions.PDF;
using CarnetGeneratorFACO.Models;
using CarnetGeneratorFACO.Services;
using QuestPDF.Fluent;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarnetGeneratorFACO.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Carnet> CarnetsReady { get; private set; } = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCarnetCommand))]
    private partial string PicPath { get; set; } = "";
    
    private string _PicStatus { get; set; } = "Seleccionar foto";
    public string PicStatus
    {
        get => _PicStatus;
        set
        {
            _PicStatus = value;
            OnPropertyChanged();
        }
    }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCarnetCommand))]
    private partial string _NameInput { get; set; }
    public string NameInput
    {
        get => _NameInput;
        set
        {
            if (_NameInput != value)
            {
                _NameInput = value;
                OnPropertyChanged();
            }
        }
    }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCarnetCommand))]
    private partial int _IdInput { get; set; }
    public int IdInput
    {
        get => _IdInput;
        set
        {
            if (_IdInput != value)
            {
                _IdInput = value;
                OnPropertyChanged();
            }
        }
    }
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddCarnetCommand))]
    private partial int _NhInput { get; set; }
    public int NhInput
    {
        get => _NhInput;
        set
        {
            if (NhInput != value)
            {
                _NhInput = value;
                OnPropertyChanged();
            }
        }
    }
    private DateTime _ExpDateInput { get; set; } = DateTime.Now;

    public DateTime ExpDateInput
    {
        get => _ExpDateInput;
        set
        {
            if (value != _ExpDateInput)
            {
                _ExpDateInput = value;
                OnPropertyChanged();
            }
        }
    }
    public int LocationNumberInput { get; set; }
    
    private string _SelectedLocationName { get; set; } = "Facultad de Odontologia";
    
    public string SelectedLocationName
    {
        get => _SelectedLocationName;
        set
        {
            if (value != _SelectedLocationName)
            {
                _SelectedLocationName = value;
                OnPropertyChanged();
            }
        }
    }
    
    public ObservableCollection<string> LocationNames { get; } = [
        "Facultad de Odontologia",
        "Facultad de Medicina",
        "Facultad de Agronomia",
        "Facultad de Ciencias Veterinarias",
        "Facultad de Humanidades y Educacion",
        "Facultad de Ciencias Juridicas y Politicas",
        "Facultad de Ingenieria",
        "Facultad de Ciencias Economicas y Sociales",
        "Facultad de Arquitectura de Diseño",
        "Facultad Experimental de Ciencias",
        "Facultad Experimental de Arte"
    ];

    private string _Condition { get; set; } = "Familiar";

    public string Condition
    {
        get => _Condition;
        set
        {
            if (value != _Condition)
            {
                _Condition = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<string> Conditions { get; } = [
        "Empleado",
        "Jubilado",
        "Familiar"
    ];

    public MainWindowViewModel()
    {
        CarnetsReady.CollectionChanged += (s, e) =>
        {
            ClearCarnetsCommand.NotifyCanExecuteChanged();
            IssueCardsCommand.NotifyCanExecuteChanged();
        };
    }
    
    [RelayCommand(CanExecute = nameof(CanAddCarnet))]
    private void AddCarnet()
    {
        var newCarnet = new Carnet(IdInput, NameInput, NhInput, ExpDateInput, SelectedLocationName, LocationNumberInput, Condition, PicPath);
        CarnetsReady.Add(newCarnet);
        IdInput = 0;
        NameInput = "";
        NhInput = 0;
        PicPath = "";
        PicStatus = "Seleccionar foto";
    }
    
    [RelayCommand(CanExecute = nameof(CanIssueCards))]
    private void ClearCarnets()
    {
        CarnetsReady.Clear();
    }
    
    [RelayCommand(CanExecute = nameof(CanIssueCards))]
    private void IssueCards()
    {
        var document = new IssueCards(CarnetsReady);
        document.GeneratePdfAndShow();
    }

    [RelayCommand]
    private async Task SelectPic()
    {
        var result = await FileDialogService.ShowSelectFileDialog();
        if (result.Count > 0)
        {
            PicStatus = "Imagen Seleccionada";
            PicPath = result[0].TryGetLocalPath();   
        }
    }

    private bool CanAddCarnet() => NameInput != "" && IdInput != 0 && NhInput != 0 && PicPath != "";

    private bool CanIssueCards() => CarnetsReady.Count > 0;
}