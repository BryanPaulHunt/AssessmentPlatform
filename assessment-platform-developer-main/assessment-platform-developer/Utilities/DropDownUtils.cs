using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace assessment_platform_developer.Utilities
{
    public static class DropDownUtils
    {
        //Populates a combo with enum values (got rid of duplicate code)
        public static void FillDropDownWithEnum(DropDownList comboBox, Type E) 
        {
            comboBox.Items.Clear();
            comboBox.Items.Add(new ListItem(""));

            foreach (Enum p in Enum.GetValues(E)) {
                ListItem li = new ListItem();
                li.Text = (Attribute.GetCustomAttribute(p.GetType().GetField(p.ToString()), typeof(System.ComponentModel.DescriptionAttribute)) as System.ComponentModel.DescriptionAttribute)?.Description ?? p.ToString();
                li.Value = Convert.ToInt32(p).ToString();

                comboBox.Items.Add(li);
            }
        }

        //Populates a combo with customers
        public static void FillDropDownWithCustomers(DropDownList comboBox, List<Customer> customers, String selectableValue="0",string defaultValue="")
        {
            comboBox.Items.Clear();

            foreach (Customer c in customers)
            {
                AddOneCustomerToDropDown(comboBox, c, selectableValue);
            }

            if (defaultValue != "") {
                ListItem li = new ListItem();
                li.Text = defaultValue;
                li.Value = "0";
                if (li.Value == selectableValue)
                {
                    li.Selected = true;
                }
                comboBox.Items.Add(li);
            }
        }

        //Gets a list item for a customer to add to a combo
        public static void AddOneCustomerToDropDown(DropDownList comboBox, Customer customer, String selectableValue = "0")
        {
            ListItem li = new ListItem();
            li.Text = customer.Name;
            li.Value = customer.ID.ToString();

            if (li.Value == selectableValue)
            {
                li.Selected = true;
            }
            comboBox.Items.Add(li);
        }

    }
}