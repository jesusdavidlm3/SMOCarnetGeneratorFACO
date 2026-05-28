using System;

namespace CarnetGeneratorFACO.Models;

public class Carnet
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public int Nh { get; set; }
    public DateTime ExpDate { get; set; }
    public string LocationName { get; set; }
    public int LocationNumber { get; set; }
    public string Condition { get; set; } 
    
    public Carnet(int id, string name, int nh, DateTime expDate, string locationName, int locationNumber, string condition)
    {
        Id = id;
        Name = name;
        Nh = nh;
        ExpDate = expDate;
        LocationName = locationName;
        LocationNumber = locationNumber;
        Condition = condition;
    }
}