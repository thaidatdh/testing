using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Selenium
{
   public enum Operator
   {
      Add,
      Subtract,
      Multiply,
      Divide,
      Concatenate,
   }
   public partial class SeleniumForm : Form
   {
      public SeleniumForm()
      {
         InitializeComponent();
      }
      public bool TestCalculatorBuild1(Selenium n,out string actualResult, string url, string build, string num1, string num2, Operator @operator, string answer, bool isIntegerOnly = false, bool expectedError = false)
      {
         try
         {
            n.OpenUrl(url);
            n.SelectByTextXPath("//*[@id=\"selectBuild\"]", build);
            n.ClearByXPath("//*[@id=\"number1Field\"]");
            n.SendKeysByXPath("//*[@id=\"number1Field\"]", num1);
            n.ClearByXPath("//*[@id=\"number2Field\"]");
            n.SendKeysByXPath("//*[@id=\"number2Field\"]", num2);
            n.SelectByTextXPath("//*[@id=\"selectOperationDropdown\"]", @operator.ToString());
            

            if (@operator != Operator.Concatenate)
            {
               bool isEnabled = n.isEnableByXPath("//*[@id=\"integerSelect\"]");
               if ((isEnabled && !isIntegerOnly) || (!isEnabled && isIntegerOnly))
               {
                  n.ClickByXPath("//*[@id=\"integerSelect\"]");
               }
            }
            n.ClickByXPath("//*[@id=\"calculateButton\"]");
            System.Threading.Thread.Sleep(5000);
            if (expectedError)
            {
               actualResult = n.GetTextByXPath("//*[@id=\"errorMsgField\"]").ToString();
               if (actualResult.Trim().ToUpper().Equals(answer.ToUpper()))
               {
                  return true;
               }
               if (!String.IsNullOrEmpty(actualResult))
               {
                  return false;
               }
            }
            actualResult = n.GetAttributeByXPath("//*[@id=\"numberAnswerField\"]");
            bool result = false;
            if (actualResult.Trim().Equals(answer))
            {
               result = true;
            }
            return result;
         }catch (Exception ex)
         {
            actualResult = "ERROR";
            return false;
         }
      }
      public static DataTable ReadExcel(string filePath)
      {
         // Open the Excel file using ClosedXML.
         // Keep in mind the Excel file cannot be open when trying to read it
         using (XLWorkbook workBook = new XLWorkbook(filePath))
         {
            //Read the first Sheet from Excel file.
            IXLWorksheet workSheet = workBook.Worksheet(1);

            //Create a new DataTable.
            DataTable dt = new DataTable();

            //Loop through the Worksheet rows.
            bool firstRow = true;
            foreach (IXLRow row in workSheet.Rows())
            {
               //Use the first row to add columns to DataTable.
               if (firstRow)
               {
                  foreach (IXLCell cell in row.Cells())
                  {
                     dt.Columns.Add(cell.Value.ToString());
                  }
                  firstRow = false;
               }
               else
               {
                  //Add rows to DataTable.
                  dt.Rows.Add();
                  int i = 0;

                  foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                  {
                     dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                     i++;
                  }
               }
            }

            return dt;
         }
      }
      Operator GetOperator(string opt)
      {
         switch (opt.ToString().ToUpper().Trim())
         {
            case "ADD":
               return Operator.Add;
            case "SUBTRACT":
               return Operator.Subtract;
            case "DIVIDE":
               return Operator.Divide;
            case "MULTIPLY":
               return Operator.Multiply;
            default:
               return Operator.Concatenate;
         }
      }
      private void buttonStart_Click(object sender, EventArgs e)
      {
         Selenium n = new Selenium();
         n.startBrowser();
         //Đọc file input TestCase từ TestCase.xlsx ở desktop
         DataTable tb = ReadExcel(@"C:\Users\vnzhs\Desktop\TestCase.xlsx");
         //Thực hiện chạy test
         foreach (DataRow row in tb.Rows)
         {
            string testNum = row["TC"].ToString();
            string build = row["Build"].ToString();
            string num1 = row["Num1"].ToString();
            string num2 = row["Num2"].ToString();
            string operatorStr = row["Operator"].ToString();
            string expected = row["ExpectedResult"].ToString();
            bool isInt = row["Integer"].ToString().ToUpper().Equals("TRUE") ? true : false;
            bool isError = row["ExpectedError"].ToString().ToUpper().Equals("TRUE") ? true : false;
            Operator opt = GetOperator(operatorStr);
            bool rs = TestCalculatorBuild1(n, out string actualResult, "https://testsheepnz.github.io/BasicCalculator.html", build, num1, num2, opt, expected, isInt, isError);
            richTextBoxResult.Text += "\nTest " + testNum + ": " + rs.ToString();
            row["Result"] = rs ? "Passed" : "Failed";
            row["ActualResult"] = actualResult;
         }
         n.closeBrowser();
         using (var workbook = new XLWorkbook())
         {
            tb.TableName = "TestCases";
            workbook.Worksheets.Add(tb);
            //Lưu vào RS.xlsx ở Desktop
            workbook.SaveAs(@"C:\Users\vnzhs\Desktop\RS.xlsx");
         }
      }
   }
}
