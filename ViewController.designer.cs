// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace EasyChGK
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AnswerButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView CommentTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView QuestionTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RoundLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ScoreLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton StartNewGameButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AnswerButton != null) {
                AnswerButton.Dispose ();
                AnswerButton = null;
            }

            if (CommentTextView != null) {
                CommentTextView.Dispose ();
                CommentTextView = null;
            }

            if (QuestionTextView != null) {
                QuestionTextView.Dispose ();
                QuestionTextView = null;
            }

            if (RoundLabel != null) {
                RoundLabel.Dispose ();
                RoundLabel = null;
            }

            if (ScoreLabel != null) {
                ScoreLabel.Dispose ();
                ScoreLabel = null;
            }

            if (StartNewGameButton != null) {
                StartNewGameButton.Dispose ();
                StartNewGameButton = null;
            }
        }
    }
}