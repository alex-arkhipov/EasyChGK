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
    [Register ("QuestionViewController")]
    partial class QuestionViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton AnswerButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PictureButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel QuestionLable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView QuestionTextView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton ResetButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel RoundLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ScoreLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AnswerButton != null) {
                AnswerButton.Dispose ();
                AnswerButton = null;
            }

            if (PictureButton != null) {
                PictureButton.Dispose ();
                PictureButton = null;
            }

            if (QuestionLable != null) {
                QuestionLable.Dispose ();
                QuestionLable = null;
            }

            if (QuestionTextView != null) {
                QuestionTextView.Dispose ();
                QuestionTextView = null;
            }

            if (ResetButton != null) {
                ResetButton.Dispose ();
                ResetButton = null;
            }

            if (RoundLabel != null) {
                RoundLabel.Dispose ();
                RoundLabel = null;
            }

            if (ScoreLabel != null) {
                ScoreLabel.Dispose ();
                ScoreLabel = null;
            }
        }
    }
}