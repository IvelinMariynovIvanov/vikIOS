using Foundation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UIKit;

namespace VikRuse
{
    public partial class ModalViewController : UIViewController
    {
        private List<Customer> mCustomers;
        private static string mDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string mFilename = Path.Combine(mDocuments, "Customers.txt");

        #region Fields

        //private bool mNewCharge;
        //private bool mLateCharge;
        //private bool mReport;

        private UIButton mEdit;
        private UIButton mCancel;

        public int mPossUp;   //imageview
        public int mPossDown;
        public int mCurrentPoss;    // textview

        private int mCurrentPosition;
        private int mCustomresCount;

        private bool mIsNewCharge;
        private bool mIsLateCharge;
        private bool mIsReport;

        public int MCurrentPosition { get => mCurrentPosition; set => mCurrentPosition = value; }
        public int MCustomresCount { get => mCustomresCount; set => mCustomresCount = value; }
        public bool MIsNewCharge { get => mIsNewCharge; set => mIsNewCharge = value; }
        public bool MIsLateCharge { get => mIsLateCharge; set => mIsLateCharge = value; }
        public bool MIsReport { get => mIsReport; set => mIsReport = value; }

        #endregion


        public event EventHandler<OnEditCustomerEventArgs> OnEditCustomerComplete;

        public ModalViewController (IntPtr handle) : base (handle)
        {
           
        }

        public ModalViewController(int mCurrentPosition, int mCustomresCount, bool isNewCharge, bool isLateCharge, bool isReport)
        {
            this.MCurrentPosition = mCurrentPosition;
            this.MCustomresCount = mCustomresCount;
            this.MIsNewCharge = isNewCharge;
            this.MIsLateCharge = isLateCharge;
            this.MIsReport = isReport;
        }

        public ModalViewController()
        {
        }

        public override void ViewDidLoad()
        {
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

            base.ViewDidLoad();
            View.BackgroundColor = UIColor.Black.ColorWithAlpha(0.5f);
            View.Opaque = false;

            #region check Ckeckbox Status
            if (MIsNewCharge == true)
            {
                mNewCharge.On = true;
            }
            else if (MIsNewCharge == false)
            {
                mNewCharge.On = false;
            }

            if (MIsLateCharge == true)
            {
                mLateCharge.On = true;
            }
            else if (MIsLateCharge == false)
            {
                mLateCharge.On = false;
            }

            if (MIsReport == true)
            {
                mReport.On = true;
            }
            else if (MIsReport == false)
            {
                mReport.On = false;
            }
            #endregion
        }

        private void TapAction(UITapGestureRecognizer sender)
        {
            DismissViewController(true, null);
        }

        partial void Cancel_TouchUpInside(UIButton sender)
        {
            DismissViewController(true, null);
        }

        partial void Save_TouchUpInside(UIButton sender)
        {
            if (OnEditCustomerComplete != null)
            {
                OnEditCustomerComplete.Invoke
               (this, new OnEditCustomerEventArgs(mIsNewCharge, mIsLateCharge, mIsReport, mCurrentPosition));
            }
            else
            {

            }

            DismissViewController(true, null);

            //var currentCustomer = mCustomers[MCurrentPosition];

            //currentCustomer.NotifyNewInvoice = this.MIsNewCharge;
            //currentCustomer.NotifyInvoiceOverdue = this.MIsLateCharge;
            //currentCustomer.NotifyReading = this.MIsReport;

            //Customer updateCustomer = mCustomers[MCurrentPosition];

            //updateCustomer.NotifyNewInvoice = this.MIsNewCharge;
            //updateCustomer.NotifyInvoiceOverdue = this.MIsLateCharge;
            //updateCustomer.NotifyReading = this.MIsReport;



            //mCustomers.RemoveAt(MCurrentPosition); //new count -1
            //mCustomers.Insert(MCurrentPosition, updateCustomer); // put in the same posstion

            //var listOfCustomersAsJson = JsonConvert.SerializeObject(this.mCustomers);
            //File.WriteAllText(mFilename, listOfCustomersAsJson);
        }
    }
}