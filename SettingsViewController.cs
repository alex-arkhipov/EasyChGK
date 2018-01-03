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

            SaveButton.TouchUpInside += (object sender, EventArgs e) => {
                Console.WriteLine("SettingsViewController: SaveButton pushed");
                // TODO: Implement Save functionality
                SaveSettings();
                this.NavigationController.PopViewController(true);
            };
        }

        private void SaveSettings()
        {
            String t = questionQuantityText.Text;
            int n = 0;
            try
            {
                n = Int32.Parse(t);    
            } catch (System.FormatException)
            {
                Console.WriteLine("Cannot convert ot int (" + t + "). No changes.");
                return;
            }

            var game = Neo.ChgkGame.GetGame();
            game.SetNumOfQuestions(n);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }
    }
}