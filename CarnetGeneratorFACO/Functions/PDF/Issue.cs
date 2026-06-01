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
        container.Row(p =>
        {
            
            p.RelativeItem().Column(s =>
            {
                s.Item().Row(row =>
                {
                    row.ConstantItem(7.7f, Unit.Centimetre).Column(car =>
                    {
                        car.Item().Row(carRow =>
                        {
                            carRow.ConstantItem(3f, Unit.Centimetre).Image("Assets/luzlogo.png");
                            carRow.ConstantItem(3f, Unit.Centimetre).Image("Assets/smologo.png");
                        });
                        car.Item().Row(carRow =>
                        {
                            carRow.RelativeItem().Text(Carnets[0].Name);         
                        });
                    });
                });
            });
            p.ConstantItem(0.8f, Unit.Centimetre).Column(c =>
            {
                c.Item().Image("Assets/cintillo.png");
            });
        });
    }
}