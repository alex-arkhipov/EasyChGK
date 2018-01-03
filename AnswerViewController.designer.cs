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
    [Register ("AnswerViewController")]
    partial class AnswerViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView AnswerTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CommentLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView CommentTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton GuessedButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton NotGuessedButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AnswerTextView != null) {
                AnswerTextView.Dispose ();
                AnswerTextView = null;
            }

            if (CommentLabel != null) {
                CommentLabel.Dispose ();
                CommentLabel = null;
            }

            if (CommentTextView != null) {
                CommentTextView.Dispose ();
                CommentTextView = null;
            }

            if (GuessedButton != null) {
                GuessedButton.Dispose ();
                GuessedButton = null;
            }

            if (NotGuessedButton != null) {
                NotGuessedButton.Dispose ();
                NotGuessedButton = null;
            }
        }
    }
}