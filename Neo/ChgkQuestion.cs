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
        String _image;

        bool _isTip;
        String _tip;

        public ChgkQuestion(String question, String answer, String comment = null)
        {
            _question = question;
            _answer = answer;
            _comment = comment;
            _isTip = false;
            _tip = "";
            _isImage = false;
            _image = "";
            parseImage();
            parseTip();
            removeLFCR();
        }

        private void parseTip()
        {
            string pattern = @"^(\[.+\])";
            Regex regex = new Regex(pattern);

            Match match = regex.Match(_question);

            if (match.Success)
            {
                _isTip = true;
                _tip = match.Groups[1].Value;
                removeTip();
            }
        }

        private void removeTip()
        {
            string pattern = @"^(\[.+\]\s*)";
            _question = Regex.Replace(_question, pattern, "");
        }

        public bool IsTip()
        {
            return _isTip;
        }

        private void parseImage()
        {
            if (isImage())
            {
                Console.WriteLine("Picture found:" + _question);

                string pattern = @"\(pic:\s*(\d+\.(jpg|gif))\)";
                Regex regex = new Regex(pattern);

                // Получаем совпадения в экземпляре класса Match
                Match match = regex.Match(_question);

                if (match.Success)
                {
                    // Found picture
                    _image = match.Groups[1].Value;
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

        // Remone Line Ending and Carriage Return
        private void removeLFCR()
        {
            // Remove all LF symbols
            _question = _question.Replace(Environment.NewLine, " ");
            // Add new line where necessary
            _question = _question.Replace("    ", Environment.NewLine + "   ");
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
            string pattern = @"(\(pic:\s*(\d+)\.(jpg|gif)\)\s*)";
            _question = Regex.Replace(_question, pattern, "");
        }

        public String GetQuestion()
        {
            return _question;
        }

        public String GetQuestionAndTip()
        {
            return _tip+_question;
        }

        public String GetAnswer()
        {
            return _answer;
        }

        public String GetComment()
        {
            return _comment;
        }

        public String GetImageURL()
        {
            return ChgkExternal.GetPictureURL(_image);
        }

    }
}
