// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace GrawApp
{
	[Register ("ChartTimeViewController")]
	partial class ChartTimeViewController
	{
		[Outlet]
		UIKit.UIView ChartView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ChartView != null) {
				ChartView.Dispose ();
				ChartView = null;
			}
		}
	}
}
