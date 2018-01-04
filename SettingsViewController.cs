using Foundation;
using System;
using UIKit;

namespace EasyChGK
{
    public partial class SettingsViewController : UITableViewController
    {
        const string KEY_QUESTION_QUANTITY = "qq";
        const string KEY_SHOW_TIPS = "tips";

        protected SettingsViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            LoadPreferences();
            SaveSettings();

            var game = Neo.ChgkGame.GetGame();

            int n = game.GetNumOfQuestions();
            questionQuantityText.Text = n.ToString();

            ShowTipsSwitch.On = game.GetShowTips();

            SaveButton.TouchUpInside += (object sender, EventArgs e) => {
                Console.WriteLine("SettingsViewController: SaveButton pushed");

                SaveSettings();
                SavePreferences();
                this.NavigationController.PopViewController(true);
            };
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            SaveSettings();
            SavePreferences();
        }

        // Save preferences in application preferences
        private void SavePreferences()
        {
            using(var ns = NSUserDefaults.StandardUserDefaults)
            {
                // Question Quantity
                int n = GetQuestionQuantity();
                if (n!= 0)
                {
                    ns.SetInt(n, KEY_QUESTION_QUANTITY);
                }

                // Show tips
                ns.SetBool(ShowTipsSwitch.On, KEY_SHOW_TIPS);
            }
        }

        private void LoadPreferences()
        {
            using (var ns = NSUserDefaults.StandardUserDefaults)
            {
                // Question quantity
                int n = (int)(ns.IntForKey(KEY_QUESTION_QUANTITY));
                if (n != 0)
                {
                    questionQuantityText.Text = n.ToString();
                }

                // Show tips
                ShowTipsSwitch.On = ns.BoolForKey(KEY_SHOW_TIPS);
            }
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

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}