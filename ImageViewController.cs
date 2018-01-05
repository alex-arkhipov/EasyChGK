using Foundation;
using System;
using UIKit;

namespace EasyChGK
{
    public partial class ImageViewController : UIViewController
    {
        private String _url = null;
        public ImageViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Console.WriteLine("View Did Load of ImageViewController called");

            if (_url != null)
            {
                WebView.LoadRequest(new NSUrlRequest(new NSUrl(_url)));    
            }
        }

        public void SetImageUrl(String url)
        {
            Console.WriteLine("ImageViewController: Setting URL: " + url);
            _url = url;
        }
    }
}