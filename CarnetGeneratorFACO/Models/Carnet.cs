using System;

namespace CarnetGeneratorFACO.Models;

public class Carnet
{
    
    public int? Id { get; set; }
    public string Name { get; set; }
    public int? Nh { get; set; }
    public DateTime ExpDate { get; set; }
    public string LocationName { get; set; }
    public int? LocationNumber { get; set; }
    public string Condition { get; set; } 
    public string PicPath { get; set; }
    
    public Carnet(string name, DateTime expDate, string locationName, string condition, string picPath, int? id = 0, int? nh = 0, int? locationNumber = 0)
    {
        Id = id;
        Name = name;
        Nh = nh;
        ExpDate = expDate;
        LocationName = locationName;
        LocationNumber = locationNumber;
        Condition = condition;
        PicPath = picPath;
    }
}