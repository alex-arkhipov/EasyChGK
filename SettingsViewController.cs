using Foundation;
using System;
using UIKit;

namespace EasyChGK
{
    public partial class SettingsViewController : UITableViewController
    {

        protected SettingsViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var title = NSBundle.MainBundle.LocalizedString("Settings view title", "");
            Title = title;

            var game = Neo.ChgkGame.GetGame();

            int n = game.GetNumOfQuestions();
            questionQuantityText.Text = n.ToString();

            ShowTipsSwitch.On = game.GetShowTips();

            SaveButton.TouchUpInside += (object sender, EventArgs e) => {
                Console.WriteLine("SettingsViewController: SaveButton pushed");

                this.NavigationController.PopViewController(true);
            };
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            SaveSettings();
        }

        private int GetQuestionQuantity()
        {
            String t = questionQuantityText.Text;
            int n = 0;
            try
            {
                n = Int32.Parse(t);
                if (n < 1) { n = 0; }
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Cannot convert to int (" + t + "). No changes.");
            }
            return n;
        }

        // Save settings in Game object
        private void SaveSettings()
        {
            Console.WriteLine("SettingsViewController: Saving settings in Game object");
            var game = Neo.ChgkGame.GetGame();

            // Quation quantity
            int n = GetQuestionQuantity();
            if (n != 0)
            {
                game.SetNumOfQuestions(n);
            }

            // Show tips
            bool s = ShowTipsSwitch.On;
            game.SetShowTips(s);

            game.SavePreferences();

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}