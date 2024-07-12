using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace assessment_platform_developer.Models
{

    // Due to the Single Responsibility Principle of SOLID, I moved
    // Address and Contact into seperate objects as they represent 
    // different concepts that could potentially do different things
	[Serializable]
	public class Customer: Identity
    {
		public int ID { get; set; }
  
        public CustomerAddress CompleteAddress { get; set; }

		public CustomerContact Contact { get; set; }

        //I have left these methods here with respect to the OPEN/CLOSED principle of SOLID
        // If any other components used thsi model and the old accessors, it would not break them
        #region "deprecated methods"

        public string Address { get { return CompleteAddress.Address; } set { CompleteAddress.Address=value; } }
        public string City { get { return CompleteAddress.City; } set { CompleteAddress.City = value; } }
        public string State { get { return CompleteAddress.State; } set { CompleteAddress.State = value; } }
        public string Zip { get { return CompleteAddress.Zip; } set { CompleteAddress.Zip = value; } }
        public string Country { get { return CompleteAddress.Country; } set { CompleteAddress.Country = value; } }

        public string ContactName { get {return Contact.Name; } set { Contact.Name = value; }}
        public string ContactPhone { get { return Contact.Phone; } set { Contact.Phone = value; } }
        public string ContactEmail { get { return Contact.Email; } set { Contact.Email = value; } }
        public string ContactTitle { get { return Contact.Title; } set { Contact.Title = value; } }
        public string ContactNotes { get { return Contact.Notes; } set { Contact.Notes = value; } }

        #endregion
    }



}