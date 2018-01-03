using System;

using UIKit;

namespace EasyChGK
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            // Clear view text boxes
            ResetGame();
            AnswerButton.Enabled = false;

            StartNewGameButton.TouchUpInside += (object sender, EventArgs e) => {
                // Use URL handler with tel: prefix to invoke Apple's Phone app...
                var game = Neo.ChgkGame.GetGame();
                game.ResetGame();
                game.StartNewGame();
                NextRound(true);
                AnswerButton.Enabled = true;
            };

            AnswerButton.TouchUpInside += (object sender, EventArgs e) => {
                Answer();
            };
        }

        private void Answer()
        {
            var game = Neo.ChgkGame.GetGame();

            // Show comments text view if available
            ShowComment(game.GetCurrentComment());

            // Ask for guess/not guess
            ShowAndCheckAnswer();

            // NextRound() will be called from inside ShowAndCheckAnswer()
            // NextRound();
        }

        private void ShowAndCheckAnswer()
        {
            // TODO: Show message box and ask if guss/not guess

            var game = Neo.ChgkGame.GetGame();

            //Create Alert
            var AlertController = UIAlertController.Create("Correct answer", game.GetCurrentAnswer(), UIAlertControllerStyle.Alert);

            //Add Actions
            AlertController.AddAction(UIAlertAction.Create("Guessed",     UIAlertActionStyle.Default, alert => CompleteAnswer(true)));
            AlertController.AddAction(UIAlertAction.Create("Not guessed", UIAlertActionStyle.Default, alert => CompleteAnswer(false)));

            //Present Alert
            PresentViewController(AlertController, true, null);
        }

        private void CompleteAnswer(bool isCorrect)
        {
            if (isCorrect)
            {
                Neo.ChgkGame.GetGame().AddScore();
            }
            NextRound();
            Console.WriteLine("Question answered: " + isCorrect.ToString());
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

            // CLear comment
            ClearComment();
        }

        private void ClearComment()
        {
            CommentTextView.Text = "";
        }

        private void ShowComment(String c)
        {
            CommentTextView.Text = c;
        }

        private void ShowQuestion(String q)
        {
            QuestionTextView.Text = q;
        }

        private void ResetGame()
        {
            QuestionTextView.Text = "";
            CommentTextView.Text = "";
            var game = Neo.ChgkGame.GetGame();
            game.ResetGame();
            UpdateLabels();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void UpdateLabels()
        {
            var game = Neo.ChgkGame.GetGame();
            RoundLabel.Text = game.GetRound().ToString();
            ScoreLabel.Text = game.GetScore().ToString();
        }
    }
}
