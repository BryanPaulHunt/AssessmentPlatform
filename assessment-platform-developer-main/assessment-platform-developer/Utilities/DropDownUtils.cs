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

    }
}