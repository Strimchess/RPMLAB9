using System.IO;
using System.Drawing;
using System.Drawing.Printing;


namespace TextEditorFunctions
{
    public static class TextEditor
    {
        public static string Text;
        public static string Open(string fileName)
        {
            string text = "";
            try
            {
                using (StreamReader reader = new StreamReader(fileName, System.Text.Encoding.GetEncoding("UTF-8")))
                {
                    text = reader.ReadToEnd();
                }
            }
            catch
            {

            }
            return text;
        }


        public static void Save(string fileName, string text)
        {

            if (fileName == string.Empty)
            {
                TextEditor.SaveAs(fileName, text);
            }
            else if (fileName != string.Empty)
            {
                try
                {
                    using (StreamWriter writer = new FileInfo(fileName).CreateText())
                    {
                        writer.Write(text);
                    }
                }
                catch
                {

                }
            }
        }


        public static void SaveAs(string fileName, string text)
        {
            try
            {

                using (FileStream fstr = new FileStream(fileName, FileMode.Create)) { };
                FileInfo fileInfo = new FileInfo(fileName);
                using (StreamWriter writer = new FileInfo(fileName).CreateText())
                {
                    writer.Write(text);
                }
            }
            catch
            {

            }
        }
    }

    public static class Printer
    {
        public static string textForPrint;
        public static Font fontForPrint;
        public static int currentCharIndex;

        public static PrintDocument GetPrintDocument(string text, Font font)
        {
            textForPrint = text;
            fontForPrint = font;
            currentCharIndex = 0;

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintPageHandler;
            return printDocument;
        }

        private static void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            e.Graphics.MeasureString(textForPrint.Substring(currentCharIndex),
                                     fontForPrint,
                                     e.MarginBounds.Size,
                                     StringFormat.GenericTypographic,
                                     out charactersOnPage,
                                     out linesPerPage);

            e.Graphics.DrawString(textForPrint.Substring(currentCharIndex, charactersOnPage),
                                  fontForPrint,
                                  Brushes.Black,
                                  e.MarginBounds);

            currentCharIndex += charactersOnPage;

            e.HasMorePages = currentCharIndex < textForPrint.Length;

            if (!e.HasMorePages)
            {
                currentCharIndex = 0;
            }
        }
    }
}