using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.ComponentModel;
using System.Xml.Linq;

namespace PortalGalaxy.Services.Implementaciones;

public class PdfService : IPdfService
{
    private readonly ITallerService _tallerService;
    private readonly IInscripcionService _inscripcionService;
    private readonly ILogger<PdfService> _logger;

    public PdfService(ITallerService tallerService, IInscripcionService inscripcionService, ILogger<PdfService> logger)
    {
        _tallerService = tallerService;
        _inscripcionService = inscripcionService;
        _logger = logger;
    }
    public async Task<BaseResponseGeneric<Document>> Generar(BusquedaTallerRequest request)
    {
        var response = new BaseResponseGeneric<Document>();

        try
        {
            request.Filas = 1000;

            var data = await _tallerService.ListAsync(request);
            if (data is { Success: true, Data: not null })
            {
                QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

                var doc = Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Margin(30);
                        page.Header().ShowOnce().Row(row =>
                        {
                            row.RelativeItem(1).Text("LISTADO DE TALLERES").AlignCenter();
                            //using var stream = new FileStream("C:\\Users\\espin\\Desktop\\APLICACIONES EMPRESARIALES\\CURS-000187\\Portal Galaxy\\PortalGalaxy\\PortalGalaxy.Client\\wwwroot\\assets\\images\\galaxy-training-logo.png", FileMode.Open);
                            //using var stream = new FileStream("http://localhost:7000/assets/images/galaxy-training-logo.png", FileMode.Open);
                            //row.ConstantItem(40).AlignRight().Image(stream);


                        });

                        page.Content().PaddingVertical(10).Column(col =>
                        {
                            col.Item().LineHorizontal(0.5f);


                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background("#446e9b").Padding(2).Text("ID").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Taller").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Categoria").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Instructor").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Fecha").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Situacion").FontColor("#fff");
                                });
                                foreach (var taller in data.Data)
                                {

                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Id.ToString()).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Nombre).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Categoria).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Instructor).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Fecha).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Situacion).FontSize(12);
                                }
                                col.Spacing(10);
                            });
                        });

                        page.Footer().AlignRight().Text(txt =>
                        {
                            txt.Span("Pagina ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });
                });

                response.Data = doc;
                response.Success = true;
            }

        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al generar el PDF";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
    public async Task<BaseResponseGeneric<Document>> Generar(BusquedaInscripcionRequest request)
    {
        var response = new BaseResponseGeneric<Document>();

        try
        {
            request.Filas = 1000;

            var data = await _inscripcionService.ListAsync(request);
            if (data is { Success: true, Data: not null })
            {
                QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

                var doc = Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Margin(30);
                        page.Header().ShowOnce().Row(row =>
                        {
                            row.RelativeItem(1).Text("LISTADO DE INSCRIPCIONES").AlignCenter();
                        });

                        page.Content().PaddingVertical(10).Column(col =>
                        {
                            col.Item().LineHorizontal(0.5f);

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();

                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background("#446e9b").Padding(2).Text("ID").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Nombre").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Taller").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Inscripción").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Situacion").FontColor("#fff");
                                });

                                foreach (var inscripcionPorTaller in data.Data)
                                {

                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(inscripcionPorTaller.Id.ToString()).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(inscripcionPorTaller.Nombre).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(inscripcionPorTaller.Taller).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(inscripcionPorTaller.Fecha).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(inscripcionPorTaller.Situacion).FontSize(12);
                                }
                                col.Spacing(10);
                            });
                        });

                        page.Footer().AlignRight().Text(txt =>
                        {
                            txt.Span("Pagina ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });
                });

                response.Data = doc;
                response.Success = true;
            }

        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al generar el PDF";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<Document>> Generar(BusquedaInscritosPorTallerRequest request)
    {
        var response = new BaseResponseGeneric<Document>();

        try
        {
            request.Filas = 1000;

            var data = await _tallerService.ListAsync(request);
            if (data is { Success: true, Data: not null })
            {
                QuestPDF.Settings.CheckIfAllTextGlyphsAreAvailable = false;

                var doc = Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Margin(30);
                        page.Header().ShowOnce().Row(row =>
                        {
                            row.RelativeItem(1).Text("LISTADO DE INSCRITOS POR TALLER").AlignCenter();
                            //using var stream = new FileStream("C:\\Users\\espin\\Desktop\\APLICACIONES EMPRESARIALES\\CURS-000187\\Portal Galaxy\\PortalGalaxy\\PortalGalaxy.Client\\wwwroot\\assets\\images\\galaxy-training-logo.png", FileMode.Open);
                            //using var stream = new FileStream("http://localhost:7000/assets/images/galaxy-training-logo.png", FileMode.Open);
                            //row.ConstantItem(40).AlignRight().Image(stream);
                        });
                        page.Content().PaddingVertical(10).Column(col =>
                        {
                            col.Item().LineHorizontal(0.5f);

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();

                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background("#446e9b").Padding(2).Text("ID").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Taller").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Categoria").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Instructor").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Fecha").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Situacion").FontColor("#fff");
                                    header.Cell().Background("#446e9b").Padding(2).Text("Cantidad").FontColor("#fff");
                                });
                                foreach (var taller in data.Data)
                                {
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Id.ToString()).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Taller).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Categoria).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Instructor).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Fecha).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Situacion).FontSize(12);
                                    table.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(taller.Cantidad.ToString()).FontSize(12);
                                }
                                col.Spacing(10);
                            });
                        });

                        page.Footer().AlignRight().Text(txt =>
                        {
                            txt.Span("Pagina ").FontSize(10);
                            txt.CurrentPageNumber().FontSize(10);
                            txt.Span(" de ").FontSize(10);
                            txt.TotalPages().FontSize(10);
                        });
                    });
                });

                response.Data = doc;
                response.Success = true;
            }

        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al generar el PDF";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }


}