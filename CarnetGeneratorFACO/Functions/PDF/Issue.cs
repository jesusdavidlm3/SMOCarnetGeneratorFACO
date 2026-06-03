using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
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
                        internalRow.Spacing(1.5f, Unit.Centimetre);
                        internalRow.ConstantItem(3f, Unit.Centimetre).PaddingLeft(0.1f, Unit.Centimetre).PaddingTop(0.1f, Unit.Centimetre).Image("Assets/luzlogo.png");
                        internalRow.ConstantItem(3f, Unit.Centimetre).PaddingLeft(1f, Unit.Centimetre).PaddingTop(0.5f, Unit.Centimetre).Image("Assets/smologo.png");
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem().PaddingLeft(0.2f, Unit.Centimetre).Text($"{currenCarnet.Name}").FontSize(9);
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem().PaddingLeft(0.2f, Unit.Centimetre).Text($"C.I. {currenCarnet.Id}").FontSize(9);
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem().PaddingLeft(0.2f, Unit.Centimetre)
                            .Text(
                                $"N.H. #{currenCarnet.Nh}      Vencimiento: {currenCarnet.ExpDate.Day}/{currenCarnet.ExpDate.Month}/{currenCarnet.ExpDate.Year}")
                            .FontSize(9);
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem().PaddingLeft(0.2f, Unit.Centimetre).Text($"Ubicacion {currenCarnet.LocationNumber}")
                            .FontSize(9);
                    });
                    info.Item().Row(internalRow =>
                    {
                        internalRow.RelativeItem().Height(1.8f, Unit.Centimetre).PaddingLeft(0.2f, Unit.Centimetre).PaddingTop(0.1f, Unit.Centimetre).Image(currenCarnet.PicPath).FitUnproportionally();
                        internalRow.ConstantItem(6f, Unit.Centimetre).Column(infoColumn =>
                        {
                            infoColumn.Item().PaddingLeft(0.2f, Unit.Centimetre).Text($"{currenCarnet.LocationName}").FontSize(9);
                            infoColumn.Item().PaddingLeft(0.2f, Unit.Centimetre).Text($"{currenCarnet.Condition}").FontSize(9);
                        });
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