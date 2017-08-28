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

namespace VikRuse
{
    [Register ("SignalForAccidentController")]
    partial class SignalForAccidentController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Camera { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Gallery { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Sent { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView Test { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Camera != null) {
                Camera.Dispose ();
                Camera = null;
            }

            if (Gallery != null) {
                Gallery.Dispose ();
                Gallery = null;
            }

            if (Sent != null) {
                Sent.Dispose ();
                Sent = null;
            }

            if (Test != null) {
                Test.Dispose ();
                Test = null;
            }
        }
    }
}