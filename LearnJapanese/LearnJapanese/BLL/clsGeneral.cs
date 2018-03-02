using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearnJapanese
{
    public class clsGeneral
    {
        public static string Dir_Shokyuu_1 { get; private set; } = @"Data\Minna nihongo\shokyuu_1";
        public static string Dir_Shokyuu_2 { get; private set; } = @"Data\Minna nihongo\shokyuu_2";
        public static string RegexLessonID { get; private set; } = @"(lessons)(?<lesson_id>[0-9]+)";
    }
}
