using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LearnJapanese
{
    public class clsData
    {
        public static List<WORD> GetData()
        {
            List<string> Paths = new List<string>();
            Paths.AddRange(clsIO.LoadFilesFromDirectory(clsGeneral.Dir_Shokyuu_1));
            Paths.AddRange(clsIO.LoadFilesFromDirectory(clsGeneral.Dir_Shokyuu_2));

            List<WORD> Words = new List<WORD>();
            foreach (string path in Paths)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                if (Regex.IsMatch(fileNameWithoutExtension, clsGeneral.RegexLessonID))
                {
                    Match match = Regex.Match(fileNameWithoutExtension, clsGeneral.RegexLessonID);
                    int lesson_id = int.Parse(match.Groups["lesson_id"].Value);

                    List<string> Texts = clsIO.LoadTexts(path);
                    foreach (string text in Texts)
                    {
                        try
                        {
                            string[] texts = text.Split(';');

                            WORD word = new WORD();
                            word.LESSON_ID = lesson_id;
                            word.KANJI = texts[0];
                            word.KANA = texts[1];
                            word.ROMANJI = texts[2];
                            word.MEAN = texts[3];
                            Words.Add(word);
                        }
                        catch { }
                    }
                }
            }

            return Words;
        }
    }
}
