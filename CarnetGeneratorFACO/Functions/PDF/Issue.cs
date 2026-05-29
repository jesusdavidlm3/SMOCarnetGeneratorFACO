using System.Collections.ObjectModel;
using CarnetGeneratorFACO.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CarnetGeneratorFACO.Functions.PDF;

public class IssueCards : IDocument
{
    private ObservableCollection<Carnet> Carnets;
    
    public IssueCards(ObservableCollection<Carnet>  carnets)
    {
        Carnets = carnets;
    }
    
    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(10);
            page.Content().Element(ComposeBody);
        });
    }

    void ComposeBody(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(car =>
            {
                car.Item().Row(carRow =>
                {
                    carRow.ConstantItem(50).Image("Assets/luzlogo.png");
                    carRow.ConstantItem(50).Image("Assets/smologo.png");
                });
                car.Item().Row(carRow =>
                {
                    carRow.RelativeItem().Text(Carnets[0].Name);         
                });
            });
        });
    }
}