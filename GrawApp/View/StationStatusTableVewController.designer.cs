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
	[Register ("StationStatusTableVewController")]
	partial class StationStatusTableVewController
	{
		[Outlet]
		UIKit.UILabel LastDateLabel { get; set; }

		[Outlet]
		UIKit.UILabel StationNameLabel { get; set; }

		[Outlet]
		UIKit.UIImageView StatusImage { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (StationNameLabel != null) {
				StationNameLabel.Dispose ();
				StationNameLabel = null;
			}

			if (LastDateLabel != null) {
				LastDateLabel.Dispose ();
				LastDateLabel = null;
			}

			if (StatusImage != null) {
				StatusImage.Dispose ();
				StatusImage = null;
			}
		}
	}
}
