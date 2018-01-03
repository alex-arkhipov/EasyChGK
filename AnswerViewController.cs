using Foundation;
using System;
using UIKit;

namespace EasyChGK
{
    public partial class AnswerViewController : UIViewController
    {
        private String _answer = "";
        private String _comment = "";
        public QuestionViewController _qvc = null;

        public AnswerViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //this.NavigationController.SetNavigationBarHidden(true, true);

            Console.WriteLine("AnswerView: ViewDidLoad called");


            AnswerTextView.Text = _answer;
            CommentTextView.Text = _comment;
            // Perform any additional setup after loading the view, typically from a nib.
            // Clear view text boxes
            GuessedButton.TouchUpInside += (object sender, EventArgs e) => {
                endAnswer(true);
            };

            NotGuessedButton.TouchUpInside += (object sender, EventArgs e) => {
                endAnswer(false);
            };
        }

        private void endAnswer(bool isCorrect)
        {
            CompleteAnswer(isCorrect);
            if (_qvc != null)
            {
                _qvc.UpdateLabels();
            }

            this.NavigationController.PopViewController(true);
        }

        public void setQVC(QuestionViewController qvc)
        {
            _qvc = qvc;
        }

        public void ShowAnswer(String answer, String comment)
        {
            Console.WriteLine("AnswerView: ShowAnswer called");

            _answer = answer;
            _comment = comment;

        }

        private void CompleteAnswer(bool isCorrect)
        {
            if (isCorrect)
            {
                Neo.ChgkGame.GetGame().AddScore();
            }

            Console.WriteLine("Question answered: " + isCorrect.ToString());
        }
    }
}