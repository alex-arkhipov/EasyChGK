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
    [Register ("SettingsViewController")]
    partial class SettingsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel questionQuantityLable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField questionQuantityText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SaveButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ShowTipsLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch ShowTipsSwitch { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (questionQuantityLable != null) {
                questionQuantityLable.Dispose ();
                questionQuantityLable = null;
            }

            if (questionQuantityText != null) {
                questionQuantityText.Dispose ();
                questionQuantityText = null;
            }

            if (SaveButton != null) {
                SaveButton.Dispose ();
                SaveButton = null;
            }

            if (ShowTipsLabel != null) {
                ShowTipsLabel.Dispose ();
                ShowTipsLabel = null;
            }

            if (ShowTipsSwitch != null) {
                ShowTipsSwitch.Dispose ();
                ShowTipsSwitch = null;
            }
        }
    }
}