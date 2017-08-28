using Foundation;
using System;
using UIKit;

namespace VikRuse
{
    public partial class SignalForAccidentController : UIViewController
    {
        private UINavigationItem mNavBar { get; set; }

        public SignalForAccidentController (IntPtr handle) : base (handle)
        {

        }

        public SignalForAccidentController(UINavigationItem mNavBar)
        {
            this.mNavBar = mNavBar;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

          //  mNavBar.BackItem.Title = "signal";

            ////NavigationItem.BackBarButtonItem = new UIBarButtonItem("  Изпрати сигнал", UIBarButtonItemStyle.Plain, null);
            //NavigationItem.BackBarButtonItem.Title = "signal";

            //this.NavigationController.NavigationBar.BarTintColor = UIColor.Blue;
            //this.NavigationItem.BackBarButtonItem = new UIBarButtonItem("  Изпрати сигнал", UIBarButtonItemStyle.Plain, null);

        }
    }
}