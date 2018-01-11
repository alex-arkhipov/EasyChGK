using System;
using Foundation;

using UIKit;

namespace EasyChGK
{
    public partial class MainMenuController : UIViewController
    {
        protected MainMenuController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            StartNewGameButton.TouchUpInside += (object sender, EventArgs e) => {
                Console.WriteLine("Start New Game pushed");
                GoToQuestionView();
            };

            SettingsButton.TouchUpInside += (object sender, EventArgs e) => {
                Console.WriteLine("Settings button pushed");
                GoToSettingsView();
            };
        }

        private void GoToQuestionView()
        {
            Console.WriteLine("MainMenuView: GoToQuestionView called 1");
            var questionView = this.Storyboard.InstantiateViewController("QuestionViewController") as QuestionViewController;
            if (questionView != null)
            {
                Console.WriteLine("MainMenuView: GoToQuestionView called 2");
                this.NavigationController.PushViewController(questionView, true);
            }
        }

        private void GoToSettingsView()
        {
            Console.WriteLine("MainMenuView: GoToSettingsView called 1");
            var settingsView = this.Storyboard.InstantiateViewController("SettingsViewController") as SettingsViewController;
            if (settingsView != null)
            {
                Console.WriteLine("MainMenuView: GoToSettingsView called 2");
                this.NavigationController.PushViewController(settingsView, true);
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

    }
}
