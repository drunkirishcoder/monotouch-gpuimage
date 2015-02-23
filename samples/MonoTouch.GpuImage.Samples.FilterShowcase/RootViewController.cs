using System;
using CoreGraphics;
using System.Collections.Generic;
using UIKit;
using Foundation;

namespace MonoTouch.GpuImage.Samples.FilterShowcase
{
	public partial class RootViewController : UITableViewController
	{
		public RootViewController() : base ("RootViewController", null)
		{
			Title = NSBundle.MainBundle.LocalizedString("Filters", "Filters");

			// Custom initialization
		}

		public DetailViewController DetailViewController
		{
			get;
			set;
		}

		public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			TableView.Source = new DataSource(this);
		}

		class DataSource : UITableViewSource
		{
			private static readonly NSString CellIdentifier = new NSString("DataSourceCell");
			private FilterType[] filters;
			private RootViewController controller;

			public DataSource(RootViewController controller)
			{
				this.controller = controller;

				filters = (FilterType[])Enum.GetValues(typeof(FilterType));
				Array.Sort(filters, (x, y) => x.ToString().CompareTo(y.ToString()));
			}

			public override nint NumberOfSections(UITableView tableView)
			{
				return 1;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return filters.Length;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(CellIdentifier);
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}

				cell.TextLabel.Text = filters[indexPath.Row].ToString();

				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				if (controller.DetailViewController == null)
					controller.DetailViewController = new DetailViewController();

				controller.DetailViewController.SetDetailItem(filters[indexPath.Row]);

				// Pass the selected object to the new view controller.
				controller.NavigationController.PushViewController(controller.DetailViewController, true);
			}
		}
	}
}
