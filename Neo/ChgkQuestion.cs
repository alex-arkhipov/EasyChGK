using System;
using System.Text.RegularExpressions;

namespace EasyChGK.Neo
{
    public class ChgkQuestion
    {
        String _question;
        String _answer;
        String _comment;

        bool _isImage;
        String _picture;

        public ChgkQuestion(String question, String answer, String comment = null)
        {
            _question = question;
            _answer = answer;
            _comment = comment;
            _isImage = false;
            _picture = "";
            parseImage();

        }

        private void parseImage()
        {
            if (isImage())
            {
                Console.WriteLine("Picture found:" + _question);

                string pattern = @"\(pic:\s*(\d+\.jpg)\)";
                Regex regex = new Regex(pattern);

                // Получаем совпадения в экземпляре класса Match
                Match match = regex.Match(_question);

                if (match.Success)
                {
                    // Found picture
                    _picture = match.Groups[1].Value;
                    _isImage = true;
                }
                else
                {
                    // MUST not be here
                    Console.WriteLine("!!!Cannot find picture filenname (" + _question + ")");
                }
                removeImage();
            }
        }

        public bool IsImage()
        {
            return _isImage;
        }

        private bool isImage()
        {
            return _question.StartsWith("(pic:", StringComparison.InvariantCulture) ? true : false;
        }

        private void removeImage()
        {
            string pattern = @"(\(pic:\s*(\d+)\.jpg\)\s*)";
            _question = Regex.Replace(_question, pattern, "");
        }

        public String GetQuestion()
        {
            return _question;
        }

        public String GetAnswer()
        {
            return _answer;
        }

        public String GetComment()
        {
            return _comment;
        }

    }
}
