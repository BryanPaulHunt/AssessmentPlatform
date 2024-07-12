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

namespace assessment_platform_developer
{
	public partial class Customers : Page
	{
		private static List<Customer> customers = new List<Customer>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
				var customerServiceRead = testContainer.GetInstance<ICustomerServiceRead>();

				var allCustomers = customerServiceRead.GetAllCustomers();
				ViewState["Customers"] = allCustomers;
                PopulateCustomerCountryList();
            }
            else
			{
				customers = (List<Customer>)ViewState["Customers"];
			}
            PopulateCustomerListBox();

        }

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
			CustomersDDL.Items.Clear();
			var storedCustomers = customers.Select(c => new ListItem(c.Name)).ToArray();
			if (storedCustomers.Length != 0)
			{
				CustomersDDL.Items.AddRange(storedCustomers);
				CustomersDDL.SelectedIndex = 0;
				return;
			}

			CustomersDDL.Items.Add(new ListItem("Add new customer"));
		}

		protected void AddButton_Click(object sender, EventArgs e)
		{
			var customer = new Customer
			{
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
                    Name = ContactName.Text,
                    Phone = CustomerPhone.Text,
                    Email = CustomerEmail.Text
                }
			};

			var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
			var customerServiceWrite = testContainer.GetInstance<ICustomerServiceWrite>();
            customerServiceWrite.AddCustomer(customer);
			customers.Add(customer);

			CustomersDDL.Items.Add(new ListItem(customer.Name));
			ResetForm();

        }


        //Added a reset form method to clean up AddButton_Click
        private void ResetForm()
		{
            CustomerName.Text = string.Empty;
            CustomerAddress.Text = string.Empty;
            CustomerEmail.Text = string.Empty;
            CustomerPhone.Text = string.Empty;
            CustomerCity.Text = string.Empty;
            StateDropDownList.SelectedIndex = 0;
            CustomerZip.Text = string.Empty;
            CountryDropDownList.SelectedIndex = 0;
            CustomerNotes.Text = string.Empty;
            ContactName.Text = string.Empty;
            ContactPhone.Text = string.Empty;
            ContactEmail.Text = string.Empty;
        }
	}
}