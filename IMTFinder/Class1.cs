using System;
using System.Collections.Generic;
using System.IO;

namespace IMTFinder
{
    public static class IMT
    {

        public static Person[] FindIMT(string fileName)
        {
            Person[] data = IMT.Parse(IMT.Import(fileName));

            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = 0; j < data.Length - i - 1; j++)
                {
                    if (data[j].IMT > data[i].IMT)
                    {
                        Person temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }

            return data;
        }

        public static string[] Import(string fileName)
        {
            string[] result = new string[0];

            try
            {
                using (StreamReader reader = new StreamReader(fileName, System.Text.Encoding.UTF8))
                {
                    string text = reader.ReadToEnd().TrimEnd(';').Replace("\n", "").Replace("\r", "");
                    result = text.Split(';', (char)StringSplitOptions.RemoveEmptyEntries);
                }
            }
            catch { }
            return result;
        }

        public static Person[] Parse(string[] data)
        {
            Person[] result = new Person[data.Length];
            for (int i = 0; i < result.Length; i++) {
                if (data[i] != "" && data[i] != string.Empty) { 
                string[] temp = data[i].Split('/');
                Person pers = new Person();
                try
                {
                    pers.FullName = temp[0];
                    pers.Weight = Math.Round(double.Parse(temp[1]), 2);
                    pers.Height = Math.Round(double.Parse(temp[2]), 2);
                    pers.IMT = Math.Round(pers.Weight / Math.Pow(pers.Height, 2), 2);
                    result[i] = pers;
                }
                catch
                {

                }
            }
            }
            return result;
        }

    }


    


    public class Person
    {



        public string FullName { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double IMT { get; set; }

        public override string ToString()
        {
            return $"{this.FullName}, вес: {this.Weight}, рост: {this.Height}, ИМТ: {this.IMT}";
        }
    }
}
