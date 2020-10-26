using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
      public bool TestCalculatorBuild1(Selenium n,string url, string num1, string num2, Operator @operator, string answer, bool isIntegerOnly = false, bool expectedFailed = false)
      {
         try
         {
            n.OpenUrl(url);
            n.SelectByTextXPath("//*[@id=\"selectBuild\"]", "1");
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
            System.Threading.Thread.Sleep(2000);
            string actualResult = n.GetAttributeByXPath("//*[@id=\"numberAnswerField\"]");
            bool result = false;
            if ((!expectedFailed && actualResult.Trim().Equals(answer)) || (expectedFailed && !actualResult.Trim().Equals(answer)))
            {
               result = true;
            }
            return result;
         }catch (Exception ex)
         {
            return false;
         }
      }
      private void buttonStart_Click(object sender, EventArgs e)
      {
         Selenium n = new Selenium();
         n.startBrowser();
         bool rs = TestCalculatorBuild1(n, "https://testsheepnz.github.io/BasicCalculator.html", "1", "1", Operator.Add, "2");
         richTextBoxResult.Text += "\nTest 1: " + rs.ToString();
         rs = TestCalculatorBuild1(n, "https://testsheepnz.github.io/BasicCalculator.html", "1.1", "1.2", Operator.Add, "2", true);
         richTextBoxResult.Text += "\nTest 2: " + rs.ToString();
         rs = TestCalculatorBuild1(n, "https://testsheepnz.github.io/BasicCalculator.html", "1", "1", Operator.Concatenate, "2");
         richTextBoxResult.Text += "\nTest 3: " + rs.ToString();
         n.closeBrowser();
      }
   }
}
