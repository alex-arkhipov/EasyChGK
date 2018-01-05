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

            AnswerButton.Enabled = true;
            PictureButton.Hidden = false;

            NextRound(true);
            UpdateUI();

            AnswerButton.TouchUpInside += (object sender, EventArgs e) => {
                GoToAnswerView();
            };

            PictureButton.TouchUpInside += (object sender, EventArgs e) => {
                GoToImageView();
            };
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
                UpdateLabels(); // Need to update scores

                return;
            }

            if (!isFirst) game.NextRound();

            UpdateUI();

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

        public void UpdateUI()
        {
            UpdateLabels();
            UpdatePicture();
        }

        private void UpdatePicture()
        {
            var game = Neo.ChgkGame.GetGame();
            var q = game.GetCurrentAll();

            PictureButton.Hidden = q.IsImage() ? false : true;
        }

        private void UpdateLabels()
        {
            var game = Neo.ChgkGame.GetGame();
            RoundLabel.Text = game.GetRound().ToString();
            ScoreLabel.Text = game.GetScore().ToString();
        }
    }
}
