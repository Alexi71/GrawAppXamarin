// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace GrawApp.View
{
	[Register ("RawContentViewController")]
	partial class RawContentViewController
	{
		[Outlet]
		UIKit.UIView SubView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (SubView != null) {
				SubView.Dispose ();
				SubView = null;
			}
		}
	}
}
