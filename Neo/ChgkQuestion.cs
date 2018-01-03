using System;
namespace EasyChGK.Neo
{
    public class ChgkQuestion
    {
        String _question;
        String _answer;
        String _comment;

        public ChgkQuestion(String question, String answer, String comment = null)
        {
            _question = question;
            _answer = answer;
            _comment = comment;
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
