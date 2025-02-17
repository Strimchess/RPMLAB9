using System;
using IMTFinder;
using TextEditorFunctions;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Printing;
using System.Drawing;

namespace TextEditor
{
    public partial class Form1 : Form
    {

        public string fileName = string.Empty;
        public bool isSaved;
        Form2 form2 = new Form2();


        public Form1()
        {
            InitializeComponent();
            панельИнструментовToolStripMenuItem.Checked = true;
            toolStripStatusLabel3.Text = "Состояние: создание файла";
        }




        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }



        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isSaved = true;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text|*.txt|All|*.*";
            fileDialog.ShowDialog();
            this.fileName = fileDialog.FileName;
            if (fileDialog.FileName != string.Empty)
            {
                string text = TextEditorFunctions.TextEditor.Open(fileDialog.FileName);
                richTextBox1.Text = text;
                this.Text = fileDialog.FileName;
                toolStripStatusLabel3.Text = "Состояние: открытие файла"; 
            }
        }




        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName == string.Empty)
            {
                сохранитьКакToolStripMenuItem_Click(sender, e);
            }
            else
            {
                TextEditorFunctions.TextEditor.Save(this.fileName, richTextBox1.Text);
                isSaved = true;
                MessageBox.Show("Файл успешно сохранен!", "", MessageBoxButtons.OK);
            }
        }



        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isSaved = true;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text|*.txt|All|*.*";
            saveFileDialog.ShowDialog();
            TextEditorFunctions.TextEditor.SaveAs(saveFileDialog.FileName, richTextBox1.Text);
            MessageBox.Show("Файл успешно сохранен!", "", MessageBoxButtons.OK);
        }



        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton5_Click(sender, e);
        }



        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintPageHandler);

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                printDocument.Print();
            }

        }

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, new PointF(50, 50));
        }



        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            открытьToolStripMenuItem_Click(sender, e);
        }



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (fileName != string.Empty && isSaved)
            {
                richTextBox1.Text = string.Empty;
                fileName = string.Empty;
                isSaved = false;
                this.Text = "Новый файл";
                toolStripStatusLabel3.Text = "Состояние: создание файла";
                return;
            }
            if ((fileName != string.Empty && !isSaved) || (fileName == string.Empty && richTextBox1.Text != string.Empty))
            {
                DialogResult result = MessageBox.Show(
             "У вас есть несохраненные изменения. Хотите сохранить изменения перед выходом?",
             "Подтвердите сохранение",
             MessageBoxButtons.YesNoCancel,
             MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    TextEditorFunctions.TextEditor.Save(fileName, richTextBox1.Text);
                    richTextBox1.Text = string.Empty;
                    fileName = string.Empty;
                    isSaved = false;
                    this.Text = "Новый файл";
                    toolStripStatusLabel3.Text = "Состояние: создание файла";
                }
                else if (result == DialogResult.No)
                {
                    richTextBox1.Text = string.Empty;
                    fileName = string.Empty;
                    isSaved = false;
                    this.Text = "Новый файл";
                    toolStripStatusLabel3.Text = "Состояние: создание файла";
                }
                return;
            }
        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.Text != string.Empty)
            {
                toolStripStatusLabel1.Text = $"Количество символов: {richTextBox1.Text.Length}";
                toolStripStatusLabel2.Text = $"Строк: {richTextBox1.Text.Count(nchar => nchar == '\n') + 1}";
            }
            else
            {
                toolStripStatusLabel1.Text = "Количество символов: 0";
                toolStripStatusLabel2.Text = "Строк: 0";
            }
            isSaved = false;
        }



        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            сохранитьToolStripMenuItem_Click(sender, e);
        }



        private void панельИнструментовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = !панельИнструментовToolStripMenuItem.Checked;
            панельИнструментовToolStripMenuItem.Checked = !панельИнструментовToolStripMenuItem.Checked;
            if (панельИнструментовToolStripMenuItem.Checked)
            {
                richTextBox1.Height -= 60;
                richTextBox1.Location = new System.Drawing.Point(6, 87);
            }
            else 
            {
                richTextBox1.Height += 60;
                richTextBox1.Location = new System.Drawing.Point(6, 27);
            }
        }



        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontDialog ftDialog = new FontDialog();

            ftDialog.ShowDialog();

            richTextBox1.Font = ftDialog.Font;
        }

        


        private void Form1_Load(object sender, EventArgs e)
        {
            fileName = string.Empty;
            isSaved = false;
            this.Text = "Новый файл";
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
             "У вас есть несохраненные изменения. Хотите сохранить изменения перед выходом?",
             "Подтвердите сохранение",
             MessageBoxButtons.YesNoCancel,
             MessageBoxIcon.Warning);


            if (result == DialogResult.Yes)
            {
                if (fileName == string.Empty)
                {
                    isSaved = false;
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text|*.txt|All|*.*";
                    DialogResult res = saveFileDialog.ShowDialog();
                    if (res == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Сохранение файла было отменено.");
                    }
                    fileName = saveFileDialog.FileName;
                }
                TextEditorFunctions.TextEditor.Save(fileName, richTextBox1.Text);
                richTextBox1.Text = string.Empty;
                fileName = string.Empty;
                isSaved = false;
            }
            else if (result == DialogResult.No)
            {
                richTextBox1.Text = string.Empty;
                fileName = string.Empty;
                isSaved = false;
            }
            else if(result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void иМТToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);

            DialogResult result = MessageBox.Show(
             "Функция ИМТ использует только текстовые файлы с определенным форматированием\n" +
             "Пример:\n" +
             "Иванов Сергей/40/1,99;\nПетров Петр/65/1,81;\nИванов Иван/5/1,7",
             "Пожалуйста, используйте правильный формат!",
             MessageBoxButtons.OKCancel,
             MessageBoxIcon.Information);

            if (result == DialogResult.OK)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "Text|*.txt|All|*.*";
                fileDialog.ShowDialog();
                if (fileDialog.FileName != string.Empty)
                {
                    Person[] data = IMT.FindIMT(fileDialog.FileName);
                    List<Person> smallIMT = new List<Person>();
                    List<Person> GoodIMT = new List<Person>();
                    List<Person> BigIMT = new List<Person>();

                    foreach (Person person in data)
                    {
                        if (person.IMT < 18)
                        {
                            smallIMT.Add(person);
                        }
                        else if (person.IMT > 25)
                        {
                            BigIMT.Add(person);
                        }
                        else
                        {
                            GoodIMT.Add(person);
                        }
                    }

                    richTextBox1.AppendText("Недостаточный ИМТ (< 18):\n");
                    foreach (Person person in smallIMT)
                    {
                        richTextBox1.AppendText(person.ToString() + "\n");
                    }

                    richTextBox1.AppendText("\nИМТ в норме (18 - 25):\n");
                    foreach (Person person in GoodIMT)
                    {
                        richTextBox1.AppendText(person.ToString() + "\n");
                    }

                    richTextBox1.AppendText("\nИзбыточный ИМТ (> 25):\n");
                    foreach (Person person in BigIMT)
                    {
                        richTextBox1.AppendText(person.ToString() + "\n");
                    }
                }
            }
            else
            {
                MessageBox.Show("Отмена");
            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!form2.Created) {
                form2 = new Form2();
                form2.Show();
            }
            else
            {
                form2.Activate();
            }
        }
    }
}
