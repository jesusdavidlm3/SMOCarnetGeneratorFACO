using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CarnetGeneratorFACO.Classes;
using CarnetGeneratorFACO.Functions.PDF;
using CarnetGeneratorFACO.Models;
using QuestPDF.Fluent;

namespace CarnetGeneratorFACO.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Carnet> CarnetsReady { get; private set; } = [];
    private string PicPath { get; set; }
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

    private string _NameInput { get; set; }

    public int IdInput
    {
        get => _NhInput;
        set
        {
            if (_NhInput != value)
            {
                _NhInput = value;
                OnPropertyChanged();
            }
        }
    }
    private  int _NhInput { get; set; }
    public int NhInput { get; set; }
    public DateTimeOffset ExpDateInput { get; set; } = DateTimeOffset.Now;
    public int LocationNumberInput { get; set; }
    
    private string _SelectedLocationName { get; set; } = "Facultad de odontologia";
    
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

    public RelayCommand Addcarnet { get; }
    public RelayCommand ClearCarnets { get; }
    public RelayCommand IssueCards { get; }
    
    public MainWindowViewModel()
    {
        Addcarnet = new RelayCommand(
            execute: _ => _AddCarnet(),
            canExecute: _ => ((CarnetsReady.Count < 6))
        );
        ClearCarnets = new RelayCommand(
            execute: _ => _ClearCarnets()
        );

        IssueCards = new RelayCommand(
            execute: _ => _IssueCards()
        );
    }
    
    private void _AddCarnet()
    {
        var newCarnet = new Carnet(IdInput, NameInput, NhInput, ExpDateInput, SelectedLocationName, LocationNumberInput, Condition);
        CarnetsReady.Add(newCarnet);
        NameInput = "";
        IdInput = 0;
        NhInput = 0;
    }
    
    private void _ClearCarnets()
    {
        CarnetsReady.Clear();
    }

    private void _IssueCards()
    {
        var document = new IssueCards(CarnetsReady);
        document.GeneratePdfAndShow();
    }
}