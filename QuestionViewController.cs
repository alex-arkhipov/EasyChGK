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

            ResetGame();

            AnswerButton.TouchUpInside += (object sender, EventArgs e) => GoToAnswerView();
            PictureButton.TouchUpInside += (object sender, EventArgs e) => GoToImageView();
            ResetButton.TouchUpInside += (object sender, EventArgs e) => ResetGame();
        }

        private void GoToImageView()
        {
            Console.WriteLine("QuestionView: GoToImageView called 1");
            var imageViewContoller = this.Storyboard.InstantiateViewController("ImageViewController") as ImageViewController;
            if (imageViewContoller != null)
            {
                Console.WriteLine("QuestionView: GoToImageView called 2");
                var game = Neo.ChgkGame.GetGame();
                imageViewContoller.SetImageUrl(game.GetCurrentAll().GetImageURL()); // set callback (TODO: make it delegate later)
                this.NavigationController.PushViewController(imageViewContoller, true);
            }
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
                answerViewContoller.SetQVC(this); // set callback (TODO: make it delegate later)
                this.NavigationController.PushViewController(answerViewContoller, true);
            }
        }

        public void NextRound(bool isFirst = false)
        {
            // Game next round - check if not the end
            var game = Neo.ChgkGame.GetGame();

            if (game.IsLastRound())
            {
                // Last round
                AnswerButton.Enabled = false;
                ResetButton.Hidden = false;
            }
            else
            {
                if (!isFirst) game.NextRound();
                // Show question
                ShowQuestion(game.GetCurrentQuestion());
            }
            UpdateUI();
        }

        private void ShowQuestion(String q)
        {
            QuestionTextView.Text = q;
        }

        private void ResetGame()
        {
            Console.WriteLine("QuestionView: ResetGame called");
            QuestionTextView.Text = "";
            ResetButton.Hidden = true;
            AnswerButton.Enabled = true;
            PictureButton.Hidden = false;
            var game = Neo.ChgkGame.GetGame();
            game.ResetGame();
            game.StartNewGame();
            NextRound(true);
            UpdateUI();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void UpdateUI()
        {
            UpdateLabels();
            UpdatePicture();
        }

        private void UpdatePicture()
        {
            var game = Neo.ChgkGame.GetGame();
            var q = game.GetCurrentAll();

            PictureButton.Hidden = !q.IsImage();
        }

        private void UpdateLabels()
        {
            var game = Neo.ChgkGame.GetGame();
            RoundLabel.Text = game.GetRound().ToString();

            int g = game.GetGuessed();
            int ng = game.GetNotGuessed();
            ScoreLabel.Text = g + ":" + ng;
        }
    }
}
