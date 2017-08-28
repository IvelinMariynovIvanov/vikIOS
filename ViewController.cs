using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UIKit;

namespace VikRuse
{
    public partial class ViewController : UIViewController
    {
        private static string mDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string mFilename = Path.Combine(mDocuments, "Customers.txt");

        private List<Customer> mCustomers; // = new List<Customer>(); //
        //private UINavigationBar mBar
        //{
        //    get { return NavigationItem; }
        //}

     //   public IntPtr UINavigationBar { get; private set; }

        public ViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            this.NavigationController.NavigationBar.BarTintColor = UIColor.FromRGB(0, 134, 255);
            this.NavigationController.NavigationBar.TintColor = UIColor.White;

            var listOfCustomersAsJsonString = File.ReadAllText(mFilename);

            if (listOfCustomersAsJsonString == null)
            {
                mCustomers = new List<Customer>();
            }
            else
            {
                mCustomers = JsonConvert.DeserializeObject<List<Customer>>(listOfCustomersAsJsonString);
            }

            if (mCustomers == null)
            {
                mCustomers = new List<Customer>();
            }


             EmployeesTableView.Source = new EmployeesTableViewSource(mCustomers, this.Storyboard, this);

         //   EmployeesTableView.Source = new EmployeesTableViewSource(mCustomers);

            EmployeesTableView.RowHeight = UITableView.AutomaticDimension;
            EmployeesTableView.EstimatedRowHeight = 100f;
            EmployeesTableView.ReloadData();

          
        }
      
      

        partial void UIButton2989_TouchUpInside(UIButton sender)
        {
            var detailViewController = Storyboard.InstantiateViewController("DetailView");
            detailViewController.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
            PresentViewController(detailViewController, true, null);
        }

        partial void Camera_Activated(UIBarButtonItem sender)
        {
            NavigationItem.BackBarButtonItem = new UIBarButtonItem("  Сигнал за авария", UIBarButtonItemStyle.Plain, null);    
            var signal = Storyboard.InstantiateViewController("SignalController");
            this.NavigationController.PushViewController(signal, true);

        }

        partial void AddCustomer_Activated(UIBarButtonItem sender)
        {
            var navBar =  NavigationItem.BackBarButtonItem = new UIBarButtonItem("  Добави абонат", UIBarButtonItemStyle.Plain ,null);
            var addCustomer = Storyboard.InstantiateViewController("AddCustomer");
            this.NavigationController.PushViewController(addCustomer, true);
        }
    }
}