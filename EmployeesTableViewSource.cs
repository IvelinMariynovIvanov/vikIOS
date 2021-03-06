﻿using System;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using System.IO;
using Newtonsoft.Json;

namespace VikRuse
{
    public class OnEditCustomerEventArgs : EventArgs
    {
        private bool isThereANewCharge;
        private bool isThereALateBill;
        private bool isThereAReport;
        private int currentPossition;

        private int possUp;
        private int possDown;

        public OnEditCustomerEventArgs()
        {

        }

        public OnEditCustomerEventArgs(bool isThereANewCharge, bool isThereALateBill, bool isThereAReport, int currentPossition)
        {
            this.IsThereANewCharge = isThereANewCharge;
            this.IsThereALateBill = isThereALateBill;
            this.IsThereAReport = isThereAReport;
            this.CurrentPossition = currentPossition;
        }

        public bool IsThereANewCharge { get => isThereANewCharge; set => isThereANewCharge = value; }
        public bool IsThereALateBill { get => isThereALateBill; set => isThereALateBill = value; }
        public bool IsThereAReport { get => isThereAReport; set => isThereAReport = value; }
        public int CurrentPossition { get => currentPossition; set => currentPossition = value; }

        public int PossUp { get => possUp; set => possUp = value; }
        public int PossDown { get => possDown; set => possDown = value; }

    }

    internal class EmployeesTableViewSource : UITableViewSource
    {
        private List<Customer> mEmployees; // = new List<Customer>(); //

        private static string mDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string mFilename = Path.Combine(mDocuments, "Customers.txt");

        private UIStoryboard mStoryBoard;
        private ViewController mViewController;
        private List<Customer> mCustomers;

        //private UIButton mDel;
        //private UIButton mEdit;



        public EmployeesTableViewSource(List<Customer> employees, UIStoryboard storyBoard, ViewController viewController) //, UIButton delete, UIButton edit)
        {
            this.mEmployees = employees;
            this.mStoryBoard = storyBoard;
            this.mViewController = viewController;
            //this.mDel = delete;
            //this.mEdit = edit;
        }

        public EmployeesTableViewSource(List<Customer> mEmployees)
        {
            this.mEmployees = mEmployees;
        }

        

        //public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        //{

        //  //  base.CommitEditingStyle(tableView, editingStyle, indexPath);


        //    switch (editingStyle)
        //    {
        //        case UITableViewCellEditingStyle.Delete:

        //            // remove the item from the underlying data source
        //            mEmployees.RemoveAt(indexPath.Row);

        //            // delete the row from the table
        //            tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);

        //            var listOfCustomersAsJson = JsonConvert.SerializeObject(this.mEmployees);

        //            File.WriteAllText(mFilename, listOfCustomersAsJson);

        //            break;   
        //    }
        //}



        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            EmployeeCell cell = (EmployeeCell)tableView.DequeueReusableCell("Custom_Cell", indexPath);

            Customer currentEmployee = mEmployees[indexPath.Row];

            cell.UpdateCell(currentEmployee);

            

            // setTag to button to identify in which row button is pressed 
            cell.DeleteBtn.Tag = indexPath.Row;
            cell.EditBtn.Tag = indexPath.Row;

            //assign action
            cell.DeleteBtn.TouchUpInside += (sender, e) =>
            {
                var row = ((UIButton)sender).Tag;
                var currentCustomer = mEmployees[indexPath.Row];

                mEmployees.RemoveAt((int)row);

                var listOfCustomersAsJson = JsonConvert.SerializeObject(this.mEmployees);
                File.WriteAllText(mFilename, listOfCustomersAsJson);

                tableView.ReloadData();

            };
            cell.EditBtn.TouchUpInside += (sender, e) =>
            {
                var row = ((UIButton)sender).Tag;
                var currentCustomer = mEmployees[indexPath.Row];

                //var detailViewController = new ModalViewController(indexPath.Row, mEmployees.Count, currentCustomer.NotifyNewInvoice,
                //                                                currentCustomer.NotifyInvoiceOverdue, currentCustomer.NotifyReading);

              //  var detailViewController = mStoryBoard.InstantiateViewController("DetailView");
                var detailViewController = new ModalViewController();

                detailViewController.MCurrentPosition = indexPath.Row;
                detailViewController.MCustomresCount = mEmployees.Count;
                detailViewController.MIsNewCharge = currentCustomer.NotifyNewInvoice;
                detailViewController.MIsLateCharge = currentCustomer.NotifyInvoiceOverdue;
                detailViewController.MIsReport = currentCustomer.NotifyReading;

                detailViewController.ModalPresentationStyle = UIModalPresentationStyle.OverCurrentContext;
                mViewController.PresentViewController(detailViewController, true, null); //mViewController

                detailViewController.OnEditCustomerComplete += (object sender1, OnEditCustomerEventArgs e1) =>
                {
                    currentCustomer.NotifyNewInvoice = e1.IsThereANewCharge;
                    currentCustomer.NotifyInvoiceOverdue = e1.IsThereALateBill;
                    currentCustomer.NotifyReading = e1.IsThereAReport;

                    Customer updateCustomer = mCustomers[indexPath.Row];

                    updateCustomer.NotifyNewInvoice = e1.IsThereANewCharge;
                    updateCustomer.NotifyInvoiceOverdue = e1.IsThereALateBill;
                    updateCustomer.NotifyReading = e1.IsThereAReport;



                    mCustomers.RemoveAt(indexPath.Row); //new count -1
                    mCustomers.Insert(e1.CurrentPossition, updateCustomer); // put in the same posstion

                    var listOfCustomersAsJson = JsonConvert.SerializeObject(this.mEmployees);
                    File.WriteAllText(mFilename, listOfCustomersAsJson);

                    tableView.ReloadData();
                };

            };

         //   cell.UpdateCell(currentEmployee);

            return cell;



            //var width = tableView.ParentView.Width;
            //var height = tableView.RenderHeight;

            //var rect = new CGRect(cell.Bounds.X, cell.Bounds.Y, (nfloat)width, (nfloat)height);

            //var gradient = new CAGradientLayer
            //{
            //    Colors = new[] { UIColor.White.CGColor, UIColor.FromWhiteAlpha((nfloat)0.9, (nfloat)1.0).CGColor },
            //    Frame = rect
            //};
            //cell.Layer.InsertSublayer(gradient, 0);


            //cell.SeparatorInset = UIEdgeInsets.Zero;
            //tableView.SeparatorStyle = UITableViewCellStyle.Default;


            //cell.Layer.BorderWidth = 2.0f;
            //cell.Layer.BorderColor = UIColor.Gray.CGColor;

            //  return cell;

        }

        //private void DetailViewController_OnEditCustomerComplete(object sender, OnEditCustomerEventArgs e)
        //{
            
        //}

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return mEmployees.Count;
        }
    }
}