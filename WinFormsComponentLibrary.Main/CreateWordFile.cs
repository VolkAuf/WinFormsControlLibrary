using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Reflection;
using WinFormsComponentLibrary.Main.HelperModels;
using WinFormsComponentLibrary.Main.HelperModels.Word;

namespace WinFormsControlLibrary.Main
{
    static class CreateWordFile
    {
        public static void CreateWordContextTables(ComponentWordContextTablesConfig config)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument
            .Create(config.WordInfo.Path, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());

                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordParagraphProperties)> { (config.WordInfo.Title,
                        new WordParagraphProperties {Bold = true, Size = "24", } ) },
                    ParagraphProperties = new WordParagraphProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));

                foreach (string[,] tables in config.Tables)
                {
                    Table table = new Table();
                    TableProperties tblProp = new TableProperties(
                        new TableBorders(
                            new TopBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 14
                            },
                            new BottomBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 14
                            },
                            new LeftBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 14
                            },
                            new RightBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 14
                            },
                            new InsideHorizontalBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 10
                            },
                            new InsideVerticalBorder
                            {
                                Val = new EnumValue<BorderValues>(BorderValues.Single),
                                Size = 12
                            }
                        )
                    );
                    table.AppendChild(tblProp);

                    for (int i = 0; i < tables.GetLength(0); i++)
                    {
                        TableRow tableRow = new TableRow();
                        tableRow.Append(new TableRowProperties(
                    new TableRowHeight() { Val = Convert.ToUInt32(2400) })
                    );
                        for (int j = 0; j < tables.GetLength(1); j++)
                        {
                            TableCell rowCell = new TableCell();
                            rowCell.Append(new TableCellProperties(
                            new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            rowCell.Append(new Paragraph(new Run(new Text(tables[i, j]))));

                            tableRow.Append(rowCell);
                        }
                        table.Append(tableRow);
                    }

                    docBody.AppendChild(table);
                }
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        public static void CreateWordTable<T>(ComponentWordTable<T> config)
        {

            using (WordprocessingDocument wordDocument = WordprocessingDocument
             .Create(config.WordInfo.Path, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());

                // Добавляем тайтл

                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordParagraphProperties)> { (config.WordInfo.Title, new
                        WordParagraphProperties {Bold = true, Size = "24", } ) },
                    ParagraphProperties = new WordParagraphProperties
                    {
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                 
                // Создаём таблицу

                Table table = new Table();

                // Добавляем бордеры

                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 14
                        },
                        new BottomBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 14
                        },
                        new LeftBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 14
                        },
                        new RightBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 14
                        },
                        new InsideHorizontalBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 10
                        },
                        new InsideVerticalBorder
                        {
                            Val = new EnumValue<BorderValues>(BorderValues.Single),
                            Size = 12
                        }
                    )
                );

                table.AppendChild(tblProp);

                // Создаём строку-шапку

                TableRow tableRowHeader = new TableRow();
                tableRowHeader.Append(new TableRowProperties(
                    new TableRowHeight() { Val = Convert.ToUInt32(config.RowHeight[0])})
                    );

                foreach (var item in config.ColumnWidthAndHeader)
                {
                    TableCell cellHeader = new TableCell();
                    cellHeader.Append(new TableCellProperties(
                        new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = item.Key.ToString()},
                        new Bold())
                        );
                    cellHeader.Append(new Paragraph(new Run(new Text(item.Value))));
                    tableRowHeader.Append(cellHeader);
                }

                table.Append(tableRowHeader);

                // Заносим в таблицу остальные данные

                for (int i = 0; i < config.ListData.Count; i++)
                {
                    // Создаём строку 

                    TableRow tableRow = new TableRow();
                    tableRow.Append(new TableRowProperties(
                    new TableRowHeight() { Val = Convert.ToUInt32(config.RowHeight[i]) })
                    );

                    // Получаем поля

                    T tempObject = config.ListData[i];
                    Type t = config.ListData[i].GetType();
                    PropertyInfo[] props = t.GetProperties();

                    bool isFirst = false;
                    int j = 0;

                    //Бегаем по полям)
                    
                    foreach (var prop in props) {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            if (!isFirst)
                            {
                                // Добавляем ячейку-шапку в строку
                                TableCell firstCellHeader = new TableCell();
                                firstCellHeader.Append(new TableCellProperties(
                                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = config.ColumnWidthAndHeader[j]},
                                    new Bold()));
                                firstCellHeader.Append(new Paragraph(new Run(new Text(prop.GetValue(config.ListData[i]).ToString()))));
                                tableRow.Append(firstCellHeader);

                                isFirst = true;
                            }
                            else
                            {
                                // Добавляем ячейку в строку
                                TableCell tableCell = new TableCell();
                                tableCell.Append(new TableCellProperties(
                                    new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = config.ColumnWidthAndHeader[j] }));
                                tableCell.Append(new Paragraph(new Run(new Text(prop.GetValue(config.ListData[i]).ToString()))));
                                tableRow.Append(tableCell);
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }

                        // Добавляем строку с таблицу
                        table.Append(tableRow);
                        j++;
                    }
                }

                // Добавляем таблицу в документ
                docBody.AppendChild(table);
                wordDocument.MainDocumentPart.Document.Save();
            }
        }

        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();
                docParagraph.AppendChild(CreateParagraphProperties(paragraph.ParagraphProperties));
                foreach ((string, WordParagraphProperties) run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize { Val = run.Item2.Size });
                    if (run.Item2.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text { Text = run.Item1, Space = SpaceProcessingModeValues.Preserve });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }

        private static ParagraphProperties CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize { Val = paragraphProperties.Size });
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}