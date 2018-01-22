// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace EasyChGK
{
    [Register ("MainMenuController")]
    partial class MainMenuController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SettingsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton StartNewGameButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel VersionLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (SettingsButton != null) {
                SettingsButton.Dispose ();
                SettingsButton = null;
            }

            if (StartNewGameButton != null) {
                StartNewGameButton.Dispose ();
                StartNewGameButton = null;
            }

            if (VersionLabel != null) {
                VersionLabel.Dispose ();
                VersionLabel = null;
            }
        }
    }
}