using Foundation;
using System;
using UIKit;

namespace EasyChGK
{
    public partial class SettingsViewController : UITableViewController
    {
        public SettingsViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            var game = Neo.ChgkGame.GetGame();
            int n = game.GetNumOfQuestions();
            questionQuantityText.Text = n.ToString();
            ShowTipsSwitch.On = game.GetShowTips();

            SaveButton.TouchUpInside += (object sender, EventArgs e) => {
                Console.WriteLine("SettingsViewController: SaveButton pushed");
                // TODO: Implement Save functionality
                SaveSettings();
                this.NavigationController.PopViewController(true);
            };
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            SaveSettings();
        }

        private void SaveSettings()
        {
            var game = Neo.ChgkGame.GetGame();

            // Quation quantity
            String t = questionQuantityText.Text;
            int n = 0;
            try
            {
                n = Int32.Parse(t);
                game.SetNumOfQuestions(n);
            } catch (System.FormatException)
            {
                Console.WriteLine("Cannot convert to int (" + t + "). No changes.");
                return;
            }

            // Show tips
            bool s = ShowTipsSwitch.On;
            game.SetShowTips(s);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}