using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CarnetGeneratorFACO.Classes;
using CarnetGeneratorFACO.Models;

namespace CarnetGeneratorFACO.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Carnet> CarnetsReady { get; private set; } =
        [new(1, "Jesus David Lozano Marin", 2, new DateTime(2002, 02, 24), "Facultad de odontologia", 5, "Empleado")];
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
    public string NameInput { get; set; }
    public int IdInput { get; set; }
    public int NhInput { get; set; }
    public DateTime ExpDateInput { get; set; }
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
        "Facultad de odontologia",
        "Facultad de medicina",
        "Facultad de agronomia",
        "Facultad de veterinaria",
        "Facultad de humanidades y educacion"
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
    
    public MainWindowViewModel()
    {
        Addcarnet = new RelayCommand(
            execute: _ => _AddCarnet()
        );
    }
    
    private void _AddCarnet()
    {
        var newCarnet = new Carnet(IdInput, NameInput, NhInput, ExpDateInput, SelectedLocationName, LocationNumberInput, Condition);
        CarnetsReady.Add(newCarnet);
    }
}