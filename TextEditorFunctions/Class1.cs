using System.IO;

namespace TextEditorFunctions
{
    public static class TextEditor
    {
        
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


        public static void Save(string fileName, string text) {

            if (fileName == string.Empty)
            {
                TextEditor.SaveAs(fileName, text);
            }
            else if (fileName != string.Empty)
            {
                try
                {
                    using (StreamWriter writer = new FileInfo(fileName).CreateText()) {
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

                using (FileStream fstr = new FileStream(fileName, FileMode.Create)) { } ;
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
}
