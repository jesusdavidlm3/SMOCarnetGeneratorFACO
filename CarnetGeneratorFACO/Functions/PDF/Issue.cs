using System.Collections.ObjectModel;
using CarnetGeneratorFACO.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CarnetGeneratorFACO.Functions.PDF;

public class IssueCards : IDocument
{
    private ObservableCollection<Carnet> _carnets;
    
    public IssueCards(ObservableCollection<Carnet>  carnets)
    {
        _carnets = carnets;
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
        container.Column(page =>
        {
            page.Spacing(0.5f, Unit.Centimetre);
            for (int i = 0; i < _carnets.Count; i++)
            {
                page.Item().Row(carRow =>
                {
                    carRow.Spacing(0.5f, Unit.Centimetre);
                    BuildCarnet(_carnets[i], carRow);
                    if (_carnets.Count >= i+2)
                    {
                        BuildCarnet(_carnets[i+1], carRow);
                        i++;
                    }
                });
            }
        });
    }

    private void BuildCarnet(Carnet currenCarnet, RowDescriptor carRow)
    {
        carRow.ConstantItem(8.5f, Unit.Centimetre).Border(1, Colors.Black).Column(carnet =>
        {
            carnet.Item().Row(carnetR =>
            {
                carnetR.ConstantItem(7.7f, Unit.Centimetre).Column(info =>
                {
                    info.Item().Row(internalRow =>
                    {
                        internalRow.ConstantItem(3f, Unit.Centimetre).Image("Assets/luzlogo.png");
                        internalRow.ConstantItem(3f, Unit.Centimetre).Image("Assets/smologo.png");
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem().Text($"{currenCarnet.Name}").FontSize(10);
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem().Text($"C.I. {currenCarnet.Id}").FontSize(10);
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem()
                            .Text(
                                $"N.H. # {currenCarnet.Nh}   Vencimiento: {currenCarnet.ExpDate.Day}/{currenCarnet.ExpDate.Month}/{currenCarnet.ExpDate.Year}")
                            .FontSize(10);
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem().Text($"Ubicacion {currenCarnet.LocationNumber}")
                            .FontSize(10);
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem().Text($"{currenCarnet.LocationName}").FontSize(10);
                    });
                });
                carnetR.ConstantItem(0.8f, Unit.Centimetre).Column(c =>
                {
                    c.Item().Image("Assets/cintillo.png");
                });
            });
        });
    }
}