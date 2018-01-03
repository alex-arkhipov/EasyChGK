using System;
using System.Collections.Generic;
using System.Xml;

namespace EasyChGK.Neo
{
    public class ChgkGame
    {
        const int DEFAULT_NUM_OF_QUESTIONS = 6;
        const int MAX_NUM_OF_QUESTIONS = 100;

        private static ChgkGame _chgkGame;
        private int _round;
        private int _score;
        private int _num_of_questions;

        private LinkedList<ChgkQuestion> questions;
        private LinkedListNode<ChgkQuestion> currentQuestionNode;

        private ChgkGame()
        {
            _num_of_questions = DEFAULT_NUM_OF_QUESTIONS;
            ResetGame();
        }

        public int GetRound()
        {
            return _round;
        }

        public int GetNumOfQuestions()
        {
            return _num_of_questions;
        }

        public void SetNumOfQuestions(int n)
        {
            if (n < 1)
            {
                Console.WriteLine("Cannot set number of questions less than 1 (" + n + ")");
            } else if (n > MAX_NUM_OF_QUESTIONS)
            {
                Console.WriteLine("Cannot set number of questions to very big value (" + n + ")");
                Console.WriteLine("Setting to maximum value (" + MAX_NUM_OF_QUESTIONS + ")");
                _num_of_questions = MAX_NUM_OF_QUESTIONS;
            } else
            {
                Console.WriteLine("Setting number of questions to '" + n + "'");
                _num_of_questions = n;
            }
        }

        public int GetScore()
        {
            return _score;
        }

        public bool IsLastRound()
        {
            return _round == _num_of_questions;
        }

        public void ResetGame()
        {
            _round = 1;
            _score = 0;
            if (questions != null)
            {
                questions.Clear();
            }
            questions = new LinkedList<ChgkQuestion>();
            currentQuestionNode = null;
        }

        public void StartNewGame()
        {
            // Download questions
            var text = ChgkExternal.GetQuestionsXML(_num_of_questions);

            // Parse xml
            ParseQuestionsXML(text);
            var qnum = questions.Count;
            if (qnum > 0)
            {
                currentQuestionNode = questions.First;
            }
            else 
            {
                Console.WriteLine("There is no questions in XML: " + text);   
            }
            if (questions.Count < _num_of_questions)
            {
                Console.WriteLine("Warning: num_of_q != real num (num_of_q = " + _num_of_questions + " | real = " + qnum);
            }
        }

        // Get current (by round) question, answer, comment
        public ChgkQuestion GetCurrentAll()
        {
            return currentQuestionNode.Value ?? null;
        }

        public String GetCurrentQuestion()
        {
            return currentQuestionNode.Value.GetQuestion() ?? "";
        }

        public String GetCurrentAnswer()
        {
            return currentQuestionNode.Value.GetAnswer() ?? "";
        }

        public String GetCurrentComment()
        {
            return currentQuestionNode.Value.GetComment()?? "";
        }

        public bool NextRound()
        {
            if (IsLastRound()) return false;
            _round++;
            if (currentQuestionNode == null)
            {
                Console.WriteLine("Something wrong happen - null pointer 'currentQuestionNode'");
                return false;
            }
            currentQuestionNode = currentQuestionNode.Next;
            return true;
        }

        public void AddScore()
        {
            _score++;
        }

        public static ChgkGame GetGame()
        {
            if (_chgkGame == null)
            {
                _chgkGame = new ChgkGame();
            }
            return _chgkGame;
        }

        public void ParseQuestionsXML(String xml)
        {
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            var nodelist = xmldoc.SelectNodes("/search/question");
            if (nodelist.Count == 0)
            {
                Console.WriteLine("No questions in xml. Exiting...");
                return;
            }
            var i = 1;
            foreach (XmlNode node in nodelist)
            {
                String q = node["Question"].InnerText;
                String a = node["Answer"].InnerText;
                String c = node["Comments"].InnerText;

                if (q.StartsWith("(pic:", StringComparison.InvariantCulture))
                {
                    // TODO: Extract picture from question
                    Console.WriteLine("Picture found:" + q);
                }

                var question = new ChgkQuestion(q, a, c);
                questions.AddLast(question);
                i++;
            }
        }
    }
}
