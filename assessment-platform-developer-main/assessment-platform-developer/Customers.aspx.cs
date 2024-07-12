using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using assessment_platform_developer.Services;
using Container = SimpleInjector.Container;
using Newtonsoft.Json.Linq;
using System.IO;
using assessment_platform_developer.Services.Interfaces;
using System.Security.Policy;
using System.Web.Security;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using assessment_platform_developer.Validators;

namespace assessment_platform_developer
{
	public partial class Customers : Page
	{
		private static List<Customer> customers = new List<Customer>();
        private static string customerListValue = "0";
        private static Customer currentCustomer = null;

        protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
				var customerServiceRead = testContainer.GetInstance<ICustomerServiceRead>();

				var allCustomers = customerServiceRead.GetAllCustomers();
				ViewState["Customers"] = allCustomers;
                ViewState["CustListValue"] = "0";
                ViewState["CurrCust"] = null;
                PopulateCustomerCountryList();
                PopulateCustomerListBox();
                AddButton.Enabled = true;
                EditButton.Enabled = false;
                DeleteButton.Enabled = false;
            }
            else
			{
				customers = (List<Customer>)ViewState["Customers"];
                customerListValue = (string)ViewState["CustListValue"];
                currentCustomer = (Customer)ViewState["CurrCust"];
            }

     
        }

        #region listboxes
        private void PopulateCustomerCountryList()
		{
			//I refactered the combo population code as it was duplicate code really
            Utilities.DropDownUtils.FillDropDownWithEnum(CountryDropDownList, typeof(Countries));

            CountryDropDownList.SelectedValue = ((int)Countries.Canada).ToString();

			//After initial selection, update the provinces
			PopulateCustomerProvinceStateDropDownList();
        }


		//When the country changes, update the states or provinces
        protected void CountryDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
			PopulateCustomerProvinceStateDropDownList();
        }

        //When the country changes, update the states or provinces
        protected void CustomerDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerListValue= CustomersDDL.SelectedValue;
            ViewState["CustListValue"] = CustomersDDL.SelectedValue;

            int custID = 0;

            Int32.TryParse(customerListValue, out custID);

            if (custID == 0)
            {
                ResetForm();
            }
            else
            {
                ShowCustomer(custID);
            }
        }

        //I Added this method to populate the states or provinces dependant on country
        private void PopulateCustomerProvinceStateDropDownList()
		{
			StateDropDownList.Items.Clear();

            if (CountryDropDownList.SelectedValue == ((int)Countries.Canada).ToString())
			{
                Utilities.DropDownUtils.FillDropDownWithEnum(StateDropDownList, typeof(CanadianProvinces));
            }
            else if (CountryDropDownList.SelectedValue == ((int)Countries.UnitedStates).ToString())
            {
				Utilities.DropDownUtils.FillDropDownWithEnum(StateDropDownList, typeof(USStates));
            }
        }

		protected void PopulateCustomerListBox()
		{
            Utilities.DropDownUtils.FillDropDownWithCustomers(CustomersDDL, customers, customerListValue, "Add new customer");

            return;
        }
        #endregion

        #region buttons
        protected void AddButton_Click(object sender, EventArgs e)
		{
            AddCustomer();
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            UpdateCustomer();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            DeleteCustomer();
        }
        #endregion

        #region 'methods actually doing some work'
        //Show a customer for edit
        private void ShowCustomer(int id)
        {
            var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
            var customerServiceRead = testContainer.GetInstance<ICustomerServiceRead>();

            Customer customer = customerServiceRead.GetCustomer(id);

            if (customer != null)
            {
                currentCustomer = customer;
                ViewState["CurrCust"] = customer;

                FormTitle.Text = "Edit Contact";
                CustomerName.Text= customer.Name;
                CustomerEmail.Text=customer.Email;
                CustomerPhone.Text = customer.Phone;
                CustomerNotes.Text = customer.Notes;
                CustomerAddress.Text = customer.CompleteAddress.Address;
                CustomerCity.Text = customer.CompleteAddress.City;
                CountryDropDownList.SelectedValue = customer.CompleteAddress.Country;
                StateDropDownList.SelectedValue = customer.CompleteAddress.State;
                CustomerZip.Text = customer.CompleteAddress.Zip;
                ContactTitle.Text = customer.Contact.Title;
                ContactName.Text = customer.Contact.Name;
                ContactPhone.Text = customer.Contact.Phone;
                ContactEmail.Text = customer.Contact.Email;
                ContactNotes.Text = customer.Contact.Notes;
                AddButton.Enabled = false;
                EditButton.Enabled = true;
                DeleteButton.Enabled = true;
            }
        }


        //I put this in it's own function so it was seperate from GUI stuff
        private void AddCustomer()
        {
            var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
            var customerServiceRead = testContainer.GetInstance<ICustomerServiceRead>();
            var customerServiceWrite = testContainer.GetInstance<ICustomerServiceWrite>();

            if (Page.IsValid)
            {
                var customer = new Customer
                {
                    ID = customerServiceRead.GetNextID(),
                    Name = CustomerName.Text,
                    Email = CustomerEmail.Text,
                    Phone = CustomerPhone.Text,
                    Notes = CustomerNotes.Text,
                    CompleteAddress = new CustomerAddress //Modified to use the sub objects
                    {
                        Address = CustomerAddress.Text,
                        City = CustomerCity.Text,
                        State = StateDropDownList.SelectedValue,
                        Zip = CustomerZip.Text,
                        Country = CountryDropDownList.SelectedValue,
                    },
                    Contact = new CustomerContact //Modified to use the sub objects
                    {
                        Title = ContactTitle.Text, //I added the title field
                        Name = ContactName.Text,
                        Phone = ContactPhone.Text, //These two were in error, I pointed them to the right fields
                        Email = ContactEmail.Text, //These two were in error, I pointed them to the right fields
                        Notes = ContactNotes.Text, //I added the notes field
                    }
                };              

                customerServiceWrite.AddCustomer(customer);
                customers.Add(customer);

                Utilities.DropDownUtils.AddOneCustomerToDropDown(CustomersDDL, customer);
                ResetForm();
            }

        }

        //Commit changes to the customer (update)
        private void UpdateCustomer()
        {
            var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
            var customerServiceWrite = testContainer.GetInstance<ICustomerServiceWrite>();

            if (Page.IsValid)
            {
                if (currentCustomer != null)
                {
                    currentCustomer.Name = CustomerName.Text;
                    currentCustomer.Email = CustomerEmail.Text;
                    currentCustomer.Phone = CustomerPhone.Text;
                    currentCustomer.Notes = CustomerNotes.Text;
                    currentCustomer.CompleteAddress.Address = CustomerAddress.Text;
                    currentCustomer.CompleteAddress.City = CustomerCity.Text;
                    currentCustomer.CompleteAddress.State = StateDropDownList.SelectedValue;
                    currentCustomer.CompleteAddress.Zip = CustomerZip.Text;
                    currentCustomer.CompleteAddress.Country = CountryDropDownList.SelectedValue;
                    currentCustomer.Contact.Title = ContactTitle.Text;
                    currentCustomer.Contact.Name = ContactName.Text;
                    currentCustomer.Contact.Phone = ContactPhone.Text;
                    currentCustomer.Contact.Email = ContactEmail.Text;
                    currentCustomer.Contact.Notes = ContactNotes.Text;

                    customerServiceWrite.UpdateCustomer(currentCustomer);
                    for (int i = 0; i < customers.Count; i++)
                    {
                        if (customers[i].ID == currentCustomer.ID)
                        {
                            customers[i] = currentCustomer;
                        }
                    }

                    CustomersDDL.Items[CustomersDDL.SelectedIndex].Text = CustomerName.Text;
                }
            }
        }

        //Delete a customer
        private void DeleteCustomer()
        {
            var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
            var customerServiceWrite = testContainer.GetInstance<ICustomerServiceWrite>();

            if (currentCustomer != null)
            {
                customerServiceWrite.DeleteCustomer(currentCustomer.ID);
                customers.Remove(currentCustomer);

                CustomersDDL.Items.RemoveAt(CustomersDDL.SelectedIndex);
                ResetForm();
            }
        }

        //Added a reset form method to clean up code
        private void ResetForm()
		{
            FormTitle.Text = "Add Contact";
            CustomerName.Text = string.Empty;
            CustomerAddress.Text = string.Empty;
            CustomerEmail.Text = string.Empty;
            CustomerPhone.Text = string.Empty;
            CustomerCity.Text = string.Empty;
            StateDropDownList.SelectedIndex = 0;
            CustomerZip.Text = string.Empty;
            CountryDropDownList.SelectedIndex = 0;
            CustomerNotes.Text = string.Empty;
            ContactTitle.Text = string.Empty;
            ContactName.Text = string.Empty;
            ContactPhone.Text = string.Empty;
            ContactEmail.Text = string.Empty;
            ContactNotes.Text = string.Empty;
            AddButton.Enabled = true;
            EditButton.Enabled = false;
            DeleteButton.Enabled = false;
        }
        #endregion

        #region validations
        protected void CustomerZipValidation_Validate(object source, ServerValidateEventArgs args)
        {
            try
            {
                ZipPostalValidator zipPostalValidator = new ZipPostalValidator();

                args.IsValid = zipPostalValidator.IsCorrectPostalForCountry(args.Value, CountryDropDownList.SelectedItem.Text);
            }

            catch (Exception ex)
            {
                args.IsValid = false;
            }
        }
        #endregion
    }
}