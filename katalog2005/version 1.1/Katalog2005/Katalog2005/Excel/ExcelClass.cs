using System;
using System.Drawing;
using System.IO;
using System.Reflection;

using Excel =  Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

    public class ExcelClass
    {   
        
        
        Excel.ApplicationClass xlApp;
        Excel.Workbook xlWorkBook;      
        Excel.Worksheet xlWorkSheet;     
        Excel.Range range;
        Excel.Pictures p;
        Excel.Picture pic = null;
        private object misValue = Type.Missing;


        //private string templatePath = @"D:\USP\UchetUSP\Templates\";
        private string templatePath = System.IO.Path.GetTempPath() + "\\";

        public ExcelClass()
        {
            xlApp = new Excel.ApplicationClass();
        }


        //ВИДИМОСТЬ EXCEL
        public bool Visible
        {
            set
            {
                if (false == value)
                    xlApp.Visible = false;

                else
                    xlApp.Visible = true;
            }
        }



        //СОЗДАТЬ НОВЫЙ ДОКУМЕНТ
        public void NewDocument()
        {
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        }

        //СОЗДАТЬ НОВЫЙ ДОКУМЕНТ C ШАБЛОНОМ
        public void NewDocument(string templateName)
        {
            xlWorkBook = xlApp.Workbooks.Add(templatePath + templateName);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        }

        //ОТКРЫТЬ ДОКУМЕНТ
        public void OpenDocument(string name)
        {
            xlWorkBook = xlApp.Workbooks.Open(name, misValue, misValue, misValue, misValue,
                misValue, misValue, misValue, misValue, misValue, misValue,
                misValue, misValue, misValue, misValue);
            xlApp.Visible = true;
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        }


        //СОХРАНИТЬ ДОКУМЕНТ
        public void SaveDocument(string name)
        {
            xlApp.DisplayAlerts = true;
            if (File.Exists(name))
            {
                DialogResult result = MessageBox.Show("Такой файл уже существует! Заменить его?", "Error",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);

                 if (result == DialogResult.Yes)
                {
                    File.Delete(name);
                    xlWorkBook.SaveAs(name, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                }
            }else
            {
                xlWorkBook.SaveAs(name, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            }      
            
        }

        //Выделить ячейки
        public void SelectCells(Object start, Object end)
        {
            if (start == null)
            {
                start = misValue;
            }

            if (end == null)
            {
                start = misValue;
            }            
            range = xlWorkSheet.get_Range(start, end);
        }

        //Скопировать выделенные ячейки
        public void CopyTo(Object cell)
        {
            Excel.Range rangeDest = xlWorkSheet.get_Range(cell, misValue);
            range.Copy(rangeDest);
        }

        //Выделить лист
        public void SelectWorksheet(int count)
        {
            if (count <= xlWorkBook.Worksheets.Count)
            {
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(count);
            }
            else {

                MessageBox.Show("Произошла ошибка в коде! Выделен несуществующий лист!");

            }
            
        }



        //УСТАНОВКА ЦВЕТА ФОНА ЯЧЕЙКИ

        public void SetColor(int color)
        {            
            range.Interior.Color = color;
            range.Interior.PatternColorIndex = Excel.Constants.xlAutomatic;
        }

        //ОРИЕНТАЦИИ СТРАНИЦЫ
       
        public void SetOrientation(Excel.XlPageOrientation Orientation)
        {
            xlWorkSheet.PageSetup.Orientation = Orientation;
        }

        //УСТАНОВКА РАЗМЕРОВ ПОЛЕЙ ЛИСТА
        public void SetMargin(double Left, double Right, double Top, double Bottom)
        {
            //Range.PageSetup.LeftMargin - ЛЕВОЕ
            //Range.PageSetup.RightMargin - ПРАВОЕ 
            //Range.PageSetup.TopMargin - ВЕРХНЕЕ
            //Range.PageSetup.BottomMargin - НИЖНЕЕ

            xlWorkSheet.PageSetup.RightMargin = Right;
            xlWorkSheet.PageSetup.LeftMargin = Left;
            xlWorkSheet.PageSetup.TopMargin = Top;
            xlWorkSheet.PageSetup.BottomMargin = Bottom;

        }


        //УСТАНОВКА РАЗМЕРА ЛИСТА
        public void SetPaperSize(Excel.XlPaperSize Size)
        {
            xlWorkSheet.PageSetup.PaperSize = Size;
        }

        //УСТАНОВКА МАСШТАБА ПЕЧАТИ
        public void SetZoom(int percent)
        {
            xlWorkSheet.PageSetup.Zoom = percent;
                    
        }

        
        //ПЕРЕИМЕНОВАТЬ ЛИСТ
        public void ReNamePage(int n, string Name)
        {

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(n);

            xlWorkSheet.Name = Name;
        }

        //ДОБАВЛЕНИЕ ЛИСТА В НАЧАЛО СПИСКА
        public void AddNewPageAtTheStart(string Name)
        {          
           xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Add(misValue, misValue, misValue, misValue);

           this.ReNamePage(xlWorkSheet.Index, Name); 
           
        }

        //ДОБАВЛЕНИЕ ЛИСТА В Конец Списка
        public void AddNewPageAtTheEnd(string Name)
        {
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.Add(misValue, xlWorkBook.Worksheets.get_Item(xlWorkBook.Worksheets.Count), 1, misValue);

            this.ReNamePage(xlWorkSheet.Index, Name);

        }

        //ПРИМЕНЕНИЕ ШРИФТА К ЯЧЕЙКЕ
        public void SetFont(Font font,int colorIndex)
        {            
            range.Font.Size = font.Size;
            range.Font.Bold = font.Bold;
            range.Font.Italic = font.Italic;
            range.Font.Name = font.Name;
            range.Font.ColorIndex = colorIndex;
        }


        //ЗАПИСЬ ЗНАЧЕНИЯ В ЯЧЕЙКУ
        public void WriteDataToCell(string value)
        {
            range.Value2 = value;
        }

        

        public void WriteDataToCell(string cell, string value)
        {
            range = xlWorkSheet.get_Range(cell, misValue);
            range.Value2 = value;
        }

        /// <summary>
        /// Добавляет к имеющимся данным в ячейке новые
        /// </summary>
        /// <param name="cell">Номер ячейки в формате "A1"</param>
        /// <param name="addValue">Данные</param>
        public void addValueToCell(string cell, string addValue)
        {
            range = xlWorkSheet.get_Range(cell, Type.Missing);
            range.Value2 += addValue;
        }

        //ОБЪЕДИНЕНИЕ ЯЧЕЕК
        public void SetMerge()
        {
            range.Merge(misValue);
        }

        //УСТАНОВКА ШИРИНЫ СТОЛБЦОВ
        public void SetColumnWidth(double Width)
        {
            range.ColumnWidth = Width;           
        }

        //УСТАНОВКА НАПРАВЛЕНИЯ ТЕКСТА
        public void SetTextOrientation(int Orientation)
        {
            range.Orientation = Orientation;           
        }

        //ВЫРАВНИВАНИЕ ТЕКСТА В ЯЧЕЙКЕ ПО ВЕРТИКАЛИ
        public void SetVerticalAlignment(int Alignment)
        {
            range.VerticalAlignment = Alignment;
        }

        //ВЫРАВНИВАНИЕ ТЕКСТА В ЯЧЕЙКЕ ПО ГОРИЗОНТАЛИ
        public void SetHorisontalAlignment(int Alignment)
        {
            range.HorizontalAlignment = Alignment;   
        }



        //ПЕРЕНОС СЛОВ В ЯЧЕЙКЕ
        public void SetWrapText(bool Value)
        {
            range.WrapText = Value;            
        }

        //УСТАНОВКА ВЫСОТЫ СТРОКИ
        public void SetRowHeight(double Height)
        {
            range.RowHeight = Height;         
        }

        //УСТАНОВКА ВИДА ГРАНИЦ
        public void SetBorderStyle(int color, Excel.XlLineStyle LineStyle, Excel.XlBorderWeight Weight)
        {
            range.Borders.ColorIndex = color;
            range.Borders.LineStyle = LineStyle;
            range.Borders.Weight = Weight;
        }

        //ЧТЕНИЕ ЗНАЧЕНИЯ ИЗ ЯЧЕЙКИ
        public string GetValue()
        {
            return range.Value2.ToString();
        }



        //Закрыть документ
        public void closeDocument()
        {            
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();            
        }

        //Завершить процесс
        public void exit()
        {
            xlApp.Quit();
        }

        //Уничтожение объекта
        public void Dispose()
        {
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);         
            GC.GetTotalMemory(true);
        }
 
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        //ЗАПИСЬ КАРТИНКИ В ЯЧЕЙКУ
        public void WritePictureToCell(string path)
        {            
            p = xlWorkSheet.Pictures(misValue) as Excel.Pictures;           
            pic = p.Insert(path, misValue);           ;
            pic.Left = Convert.ToDouble(range.Left);
            pic.Top = Convert.ToDouble(range.Top);            

        }

	/// <summary>
        /// Метод копирует выбранный диапазон ячеек на новое место
        /// </summary>
        /// <param name="start">Левая верхняя ячейка диапазона</param>
        /// <param name="end">Правая нижняя ячейка диапазона</param>
        /// <param name="destination">Ячейка нового местоположения</param>
        public void copyCells(object start, object end, object destination)
        {
            Excel.Range rangeDest = xlWorkSheet.get_Range(destination, misValue);
            range = xlWorkSheet.get_Range(start, end);
            range.Copy(rangeDest);
        }

        /// <summary>
        /// Метод делает жирным выделенные ячейки
        /// </summary>
        /// <param name="start">Начало диапазона</param>
        /// <param name="end">Конец диапазона</param>
        public void setBold(object start, object end)
        {
            SelectCells(start, end);
            range.Font.Bold = true;
        }

        /// <summary>
        /// Метод выравнивает ширину столбцов по max ячейке
        /// </summary>
        /// <param name="columnName">Наименование столбца/столбцов в формате "А:А"</param>
        public void setAutoFit(string columnName)
        {
            range = xlWorkSheet.get_Range(columnName, misValue);
            range.Columns.AutoFit();
        }
  	
    }
