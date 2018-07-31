// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace GrawApp.Controller.Raw
{
	[Register ("RawChartViewController")]
	partial class RawChartViewController
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
