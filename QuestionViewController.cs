using System;
using Foundation;

using UIKit;

namespace EasyChGK
{
    public partial class QuestionViewController : UIViewController
    {
        protected QuestionViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Console.WriteLine("View Did Load of QuestionViewController called");

            NextRound(true);
            UpdateLabels();
            AnswerButton.Enabled = true;

            AnswerButton.TouchUpInside += (object sender, EventArgs e) => {
                GoToAnswerView();
                Answer(); // Next Round
            };
        }

        private void GoToAnswerView()
        {
            Console.WriteLine("QuestionView: GoToAnswerView called 1");
            var answerViewContoller = this.Storyboard.InstantiateViewController("AnswerViewController") as AnswerViewController;
            if (answerViewContoller != null)
            {
                Console.WriteLine("QuestionView: GoToAnswerView called 2");
                var game = Neo.ChgkGame.GetGame();
                answerViewContoller.ShowAnswer(game.GetCurrentAnswer(), game.GetCurrentComment());
                answerViewContoller.setQVC(this); // set callback (TODO: make it delegate later)
                this.NavigationController.PushViewController(answerViewContoller, true);
            }
        }

        private void Answer()
        {
            Console.WriteLine("Answer of QuestionViewController called");

            //var game = Neo.ChgkGame.GetGame();

            // Show comments text view if available
            //ShowComment(game.GetCurrentComment());

            // Ask for guess/not guess
            ShowAndCheckAnswer();

            // NextRound() will be called from inside ShowAndCheckAnswer()
            // NextRound();
        }

        private void ShowAndCheckAnswer()
        {
            var game = Neo.ChgkGame.GetGame();

            /*
            //Create Alert
            var AlertController = UIAlertController.Create("Correct answer", game.GetCurrentAnswer(), UIAlertControllerStyle.Alert);

            //Add Actions
            AlertController.AddAction(UIAlertAction.Create("Guessed", UIAlertActionStyle.Default, alert => CompleteAnswer()));
            AlertController.AddAction(UIAlertAction.Create("Not guessed", UIAlertActionStyle.Default, alert => CompleteAnswer()));

            //Present Alert
            PresentViewController(AlertController, true, null);
            */
            CompleteAnswer();
        }

        private void CompleteAnswer()
        {
            NextRound();
        }

        private void NextRound(bool isFirst = false)
        {
            // Game next round - check if not the end
            var game = Neo.ChgkGame.GetGame();

            if (game.IsLastRound())
            {
                // Last round
                AnswerButton.Enabled = false;
                UpdateLabels(); // Need to update scores

                return;
            }

            if (!isFirst) game.NextRound();

            UpdateLabels();

            // Show question
            ShowQuestion(game.GetCurrentQuestion());

        }

        private void ShowQuestion(String q)
        {
            QuestionTextView.Text = q;
        }

        private void ResetGame()
        {
            QuestionTextView.Text = "";
            var game = Neo.ChgkGame.GetGame();
            game.ResetGame();
            UpdateLabels();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void UpdateLabels()
        {
            var game = Neo.ChgkGame.GetGame();
            RoundLabel.Text = game.GetRound().ToString();
            ScoreLabel.Text = game.GetScore().ToString();
        }
    }
}
