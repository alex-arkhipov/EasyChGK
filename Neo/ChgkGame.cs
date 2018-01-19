using System;
using System.Collections.Generic;
using System.Xml;
using Foundation;

namespace EasyChGK.Neo
{
    public class ChgkGame
    {
        const bool IS_DEBUG = true;

        const int DEFAULT_NUM_OF_QUESTIONS = 6;
        const int MAX_NUM_OF_QUESTIONS = 100;

        // Keys to save preferences
        const string KEY_QUESTION_QUANTITY = "qq";
        const string KEY_SHOW_TIPS = "tips";

        private static ChgkGame _chgkGame;
        private int _round;
        private int _guessed;
        private int _notguessed;
        private int _num_of_questions;
        private bool _show_tips;

        private LinkedList<ChgkQuestion> questions;
        private LinkedListNode<ChgkQuestion> currentQuestionNode;

        private ChgkGame()
        {
            _num_of_questions = DEFAULT_NUM_OF_QUESTIONS;
            _show_tips = true;
            LoadPreferences();
            ResetGame();
        }

        public bool IsDebug()
        {
            return IS_DEBUG;
        }

        public int GetGuessed()
        {
            return _guessed;
        }

        public void AddGuessed()
        {
            _guessed++;
        }

        public int GetNotGuessed()
        {
            return _notguessed;
        }

        public void AddNotGuessed()
        {
            _notguessed++;
        }

        public int GetRound()
        {
            return _round;
        }

        public bool GetShowTips()
        {
            return _show_tips;
        }

        public void SetShowTips(bool s)
        {
            _show_tips = s;
        }

        public int GetNumOfQuestions()
        {
            return _num_of_questions;
        }

        public void SetNumOfQuestions(int n)
        {
            if (n < 1)
            {
                Console.WriteLine("ERROE: ChgkGame: Cannot set number of questions less than 1 (" + n + ")");
            } else if (n > MAX_NUM_OF_QUESTIONS)
            {
                Console.WriteLine("ERROR: ChgkGame: Cannot set number of questions to very big value (" + n + ")");
                Console.WriteLine("ChgkGame: Setting to maximum value (" + MAX_NUM_OF_QUESTIONS + ")");
                _num_of_questions = MAX_NUM_OF_QUESTIONS;
            } else
            {
                Console.WriteLine("ChgkGame: Setting number of questions to '" + n + "'");
                _num_of_questions = n;
            }
        }

        public bool IsLastRound()
        {
            return _round == _num_of_questions;
        }

        public void ResetGame()
        {
            _round = 1;
            _guessed = _notguessed = 0;
            if (questions != null)
            {
                questions.Clear();
            }
            questions = new LinkedList<ChgkQuestion>();
            currentQuestionNode = null;
            Console.WriteLine("ChgkGame: Game is reset");
        }

        public void StartNewGame()
        {
            // Download questions
            String text;

            if (!IsDebug())
            {
                text = ChgkExternal.GetQuestionsXML(_num_of_questions);
            }
            else
            {
                text = ChgkExternal.GetQuestionsXMLTest();   
            }

            // Parse xml
            ParseQuestionsXML(text);
            var qnum = questions.Count;
            if (qnum > 0)
            {
                currentQuestionNode = questions.First;
            }
            else 
            {
                Console.WriteLine("ChgkGame: here is no questions in XML: " + text);   
            }
            if (qnum < _num_of_questions)
            {
                Console.WriteLine("WARNING: ChgkGame: num_of_q != real num (num_of_q = " + _num_of_questions + " | real = " + qnum);
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
                Console.WriteLine("ChgkGame: Something wrong happen - null pointer 'currentQuestionNode'");
                return false;
            }
            currentQuestionNode = currentQuestionNode.Next;
            return true;
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
                Console.WriteLine("WARNING: ChgkGame: No questions in xml. Exiting...");
                return;
            }
            var i = 1;
            foreach (XmlNode node in nodelist)
            {
                String q = node["Question"].InnerText;
                String a = node["Answer"].InnerText;
                String c = node["Comments"].InnerText;

                var question = new ChgkQuestion(q, a, c);
                questions.AddLast(question);

                if (IsDebug())
                {
                    Console.WriteLine("ChgkGame:" + i.ToString() + ") Question: " + q);    
                }
                i++;
            }
        }

        // Save preferences in application preferences
        public void SavePreferences()
        {
            using (var ns = NSUserDefaults.StandardUserDefaults)
            {
                Console.WriteLine("ChgkGame: Saving preferences");
                // Question Quantity
                int n = _num_of_questions;
                ns.SetInt(n, KEY_QUESTION_QUANTITY);
                Console.WriteLine("ChgkGame: Question quantity to save = " + n.ToString());

                // Show tips
                ns.SetBool(_show_tips, KEY_SHOW_TIPS);
                Console.WriteLine("ChgkGame: Show tips to save = " + _show_tips.ToString());
            }
        }

        private void LoadPreferences()
        {
            Console.WriteLine("ChgkGame: Loading preferences");
            using (var ns = NSUserDefaults.StandardUserDefaults)
            {
                // Question quantity
                int n = (int)(ns.IntForKey(KEY_QUESTION_QUANTITY));
                if (n != 0)
                {
                    _num_of_questions = n;
                    Console.WriteLine("ChgkGame: Question quantity = " + n.ToString());
                }

                // Show tips
                var on = ns.BoolForKey(KEY_SHOW_TIPS);
                _show_tips = on;
                Console.WriteLine("ChgkGame: Show tips = " + on.ToString());

            }
        }
    }
}
