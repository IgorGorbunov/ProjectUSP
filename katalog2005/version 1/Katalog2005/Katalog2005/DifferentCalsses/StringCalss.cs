using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace UchetUSP.DifferentCalsses
{
    class StringCalss
    {
        /// <summary>
        ///обрезает последние 3 символа (запятые, скобочки и т.д.)
        /// </summary>
        /// <param name="sentence">выражение</param>
        /// <returns>обработанная строка</returns>
        public static string CutLastLetter(string sentence)
        {
            if (sentence.Length > 2)
            {
                sentence = sentence.Remove(sentence.Length - 2);
            }

            return sentence;
        }


        /// <summary>
        ///преобразует bool checkbox.checked в 0 и 1
        /// </summary>
        /// <param name="chCox">обрабатываемый CheckBox</param>
        /// <returns>0 - false;
        /// 1- true;</returns>
        public static string ReturnCheckedStatus(CheckBox chBox)
        {
            if (chBox.Checked == true)
            {
                return "1";
            }
            else {
                return "0";
            }

        }

        /// <summary>
        ///преобразует string в bool 
        /// </summary>
        /// <param name="chCox">обрабатываемый CheckBox</param>
        /// <returns>0 - false;
        /// 1- true;</returns>
        public static bool ReturnCheckedStatus(string chBox)
        {
            if (String.Compare(chBox,"0")==0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
}
