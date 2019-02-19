﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadSheetWorking.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace SpreadSheetWorking
{
    public class SpreadsheetHelper
    {
       public static void CalculateSumOfCellRange(Stream stream, string worksheetName, string firstCellName, string lastCellName, string resultCell)
        {           
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(stream, true))
            {
                IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == worksheetName);
                if (sheets.Count() == 0)
                {
                    // The specified worksheet does not exist.
                    return;
                }
                WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);
                Worksheet worksheet = worksheetPart.Worksheet;
                
                // Get the row number and column name for the first and last cells in the range.
                uint firstRowNum = GetRowIndex(firstCellName);
                uint lastRowNum = GetRowIndex(lastCellName);
                string firstColumn = GetColumnName(firstCellName);
                string lastColumn = GetColumnName(lastCellName);

                double sum = 0;

                // Iterate through the cells within the range and add their values to the sum.
                foreach (Row row in worksheet.Descendants<Row>().Where(r => r.RowIndex.Value >= firstRowNum && r.RowIndex.Value <= lastRowNum))
                {
                    foreach (Cell cell in row)
                    {
                        string columnName = GetColumnName(cell.CellReference.Value);
                        if (CompareColumn(columnName, firstColumn) >= 0 && CompareColumn(columnName, lastColumn) <= 0)
                        {
                            sum += double.Parse(cell.CellValue.Text);
                        }
                    }
                }

                // Get the SharedStringTablePart and add the result to it.
                // If the SharedStringPart does not exist, create a new one.
                SharedStringTablePart shareStringPart;
                if (document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
                {
                    shareStringPart = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
                }
                else
                {
                    shareStringPart = document.WorkbookPart.AddNewPart<SharedStringTablePart>();
                }

                // Insert the result into the SharedStringTablePart.
                int index = InsertSharedStringItem("Result:" + sum, shareStringPart);

                Cell result = InsertCellInWorksheet(GetColumnName(resultCell), GetRowIndex(resultCell), worksheetPart);

                // Set the value of the cell.
                result.CellValue = new CellValue(index.ToString());
                result.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                worksheetPart.Worksheet.Save();
            }

        }

        // Given a cell name, parses the specified cell to get the row index.
        private static uint GetRowIndex(string cellName)
        {
            // Create a regular expression to match the row index portion the cell name.
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(cellName);

            return uint.Parse(match.Value);
        }

        // Given a cell name, parses the specified cell to get the column name.
        private static string GetColumnName(string cellName)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellName);

            return match.Value;
        }

        // Given two columns, compares the columns.
        private static int CompareColumn(string column1, string column2)
        {
            if (column1.Length > column2.Length)
            {
                return 1;
            }
            else if (column1.Length < column2.Length)
            {
                return -1;
            }
            else
            {
                return string.Compare(column1, column2, true);
            }
        }

        // Given text and a SharedStringTablePart, creates a SharedStringItem with the specified text 
        // and inserts it into the SharedStringTablePart. If the item already exists, returns its index.
        private static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {
            // If the part does not contain a SharedStringTable, create it.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    // The text already exists in the part. Return its index.
                    return i;
                }

                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        // Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet. 
        // If the cell already exists, returns it. 
        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }
        }

        public static async Task<Stream> filepathhelper()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".xlsx");
            StorageFile file = await openPicker.PickSingleFileAsync();
            IRandomAccessStream streams=await file.OpenAsync(FileAccessMode.ReadWrite);
            //StorageLibrary doclibrary = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
            //string filepath = doclibrary.SaveFolder.Path;
            return streams.AsStream();
        }

        private static List<string> GetColumnNames(Stream stream, string worksheetName)
        {   List<string> columnnames = new List<string>();
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(stream, true))
            {
                IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == worksheetName);
                if (sheets.Count() == 0)
                {
                    // The specified worksheet does not exist.
                    
                }
                WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);
                Worksheet worksheet = worksheetPart.Worksheet;

                //
                foreach (Row row in worksheet.Descendants<Row>().Where(r => r.RowIndex.Value == 1))
                {
                    foreach (Cell cell in row)
                    {
                        columnnames.Add(cell.InnerText);
                    }
                }
            }
            return columnnames;
        }

        /// <summary>
        /// hmmm actually maybe I should't write this code. It looks like this may not work or this is not a good practice when using reflection,
        /// Anyway, let me have a try
        /// OK I want to forget about it...
        /// </summary>
        private static void generateclass()
        {

        }


        //All strings in an Excel worksheet are stored in a array like structure called the SharedStringTable.
        //The goal of this table is to centralize all strings in an index based array and then if that string 
        //is used multiple times in the document to just reference the index in this array.That being said, the 
        //0 you received when you got the text value of the A1 cell is the index into the SharedStringTable.
        //To get the real value you can use this helper function:

        public static SharedStringItem GetSharedStringItemById(WorkbookPart workbookPart, int id)
        {
            return workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
        }

        public static string GetTextfromCell(Cell cell, SpreadsheetDocument document)
        {
            string cellValue = string.Empty;
            int id = -1;
            if (Int32.TryParse(cell.InnerText, out id))
            {
                SharedStringItem item = GetSharedStringItemById(document.WorkbookPart, id);
                if (item.Text != null)
                {
                    cellValue = item.Text.Text;
                }
                else if (item.InnerText != null)
                {
                    cellValue = item.InnerText;
                }
                else if (item.InnerXml != null)
                {
                    cellValue = item.InnerXml;
                }
            }
            return cellValue;
        }

        //Responseale for loading data from a excel. Row 1 should be the Column name and the data should go from Row 2 
        public static void ReadDataFromExcel(Stream stream, string worksheetName, string firstCellName, string lastCellName,ObservableCollection<MemberInfo> membercollection)
        {
            MemberInfo member = new MemberInfo();
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(stream, false))
            {
                IEnumerable<Sheet> sheets = document.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == worksheetName);
                if (sheets.Count() == 0)
                {
                    // The specified worksheet does not exist.
                    // return;
                }
                WorksheetPart worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheets.First().Id);
                Worksheet worksheet = worksheetPart.Worksheet;
                
                // Get the row number and column name for the first and last cells in the range.
                uint firstRowNum = GetRowIndex(firstCellName);
                uint lastRowNum = GetRowIndex(lastCellName);
                string firstColumn = GetColumnName(firstCellName);
                string lastColumn = GetColumnName(lastCellName);


                foreach (Row row in worksheet.Descendants<Row>().Where(r => r.RowIndex.Value >= firstRowNum && r.RowIndex.Value <= lastRowNum))
                {
                   
                    var x=row.Descendants<Cell>().Count();
                    int i = 1;
                    foreach (Cell cell in row.Descendants<Cell>())
                    {
                        if (cell.DataType != null)
                        {
                            if (cell.DataType == CellValues.SharedString)
                            {
                               
                                if(i==1)
                                {
                                    member.UserName = GetTextfromCell(cell,document);
                                } 
                                else if(i==2)
                                {
                                    member.Alias = GetTextfromCell(cell, document);
                                }
                                else if (i == 3)
                                {
                                    member.WsAlias = GetTextfromCell(cell, document);
                                }
                                else if (i == 4)
                                {
                                    member.Technology = GetTextfromCell(cell, document);
                                }
                                else if (i == 5)
                                {
                                    member.Group = GetTextfromCell(cell, document);
                                }
                                else if (i == 6)
                                {
                                    member.VacationHour = Int32.Parse(GetTextfromCell(cell, document));
                                }                             
                            }
                                
                        }                           
                        i++;                      
                    }
                    membercollection.Add(
                        new MemberInfo() { UserName = member.UserName, Alias = member.Alias, WsAlias = member.WsAlias, Technology = member.Technology, Group = member.Group, VacationHour = member.VacationHour });
                }
             
            }
        }
    }

}