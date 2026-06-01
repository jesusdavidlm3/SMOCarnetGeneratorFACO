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
        container.Column(page =>
        {
            int CarRowsCount;
            if (Carnets.Count % 2 == 0)
            {
                CarRowsCount =  Carnets.Count / 2;
            }
            else
            {
                CarRowsCount = (Carnets.Count + 1) / 2;
            }

            for (int i = 0; i < CarRowsCount; i++)
            {
                page.Item().Row(carRow =>
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
                                    internalRow.RelativeItem().Text($"{Carnets[0].Name}").FontSize(10);
                                });
                                info.Item().Row(internalRow =>
                                {
                                    internalRow.RelativeItem().Text($"C.I. {Carnets[0].Id}").FontSize(10);
                                });
                                info.Item().Row(internalRow =>
                                {
                                    internalRow.RelativeItem().Text($"N.H. # {Carnets[0].Nh}   Vencimiento: {Carnets[0].ExpDate.Day}/{Carnets[0].ExpDate.Month}/{Carnets[0].ExpDate.Year}").FontSize(10);
                                });
                                info.Item().Row(internalRow =>
                                {
                                    internalRow.RelativeItem().Text($"Ubicacion {Carnets[0].LocationNumber}").FontSize(10);
                                });
                                info.Item().Row(internalRow =>
                                {
                                    internalRow.RelativeItem().Text($"{Carnets[0].LocationName}").FontSize(10);
                                });
                            });
                            carnetR.ConstantItem(0.8f, Unit.Centimetre).Column(c =>
                            {
                                c.Item().Image("Assets/cintillo.png");
                            });
                        });
                    });
                });
            }
            

        });
        // for (int i = 0; i < Carnets.Count; i++)
        // {
        // }
    }

    // private void BuildCarnet(Carnet carnet)
    // {
    //     
    // }
}