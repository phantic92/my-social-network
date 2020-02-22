using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManager.Models
{
    [MetadataType(typeof(persons_Validation))]
    public partial class person
    {
        //empty class here, we just wanted to add the annotation above
    }

    [MetadataType(typeof(contacts_Validation))]
    public partial class contact
    {
        //empty class here, we just wanted to add the annotation above
    }

    [MetadataType(typeof(addresses_Validation))]
    public partial class address
    {
        //empty class here, we just wanted to add the annotation above
    }

    [MetadataType(typeof(pictures_Validation))]
    public partial class picture
    {
        //empty class here, we just wanted to add the annotation above
    }

    [MetadataType(typeof(countries_Validation))]
    public partial class country
    {
        //empty class here, we just wanted to add the annotation above
    }

    public class persons_Validation
    {
        [Display(Name = "First name")]
        [Required(ErrorMessage = "Please enter your information..")]
        [StringLength(50, MinimumLength = 2)]
        public string first_name;

        [Display(Name = "Last name")]
        public string last_name;

        [Display(Name = "Notes")]
        public string notes;

        [Display(Name = "Gender")]
        public string gender;

        [Display(Name = "Profile Picture")]
        public int profile_pic;
    }
    public class contacts_Validation
    {
        [Required(ErrorMessage = "Please enter a type")]
        [Display(Name = "Type")]
        public string type;

        [Required(ErrorMessage = "Please enter the corresponding contact information")]
        [Display(Name = "Info")]
        public string info;
    }
    public class addresses_Validation
    {
        [Display(Name = "Description")]
        public string description;

        [Display(Name = "Street")]
        public string street_address;

        [Display(Name = "City")]
        public string city;

        [Display(Name = "Province/State")]
        public string province;

        [Display(Name = "Postal Code/ZIP")]
        public string zipcode;
    }
    public class pictures_Validation
    {
        [Display(Name = "Caption")]
        public string caption;

        [Required(ErrorMessage = "Please upload a picture")]
        [Display(Name = "Image")]
        public string relative_path;

        [Display(Name = "Time")]
        public string time_info;

        [Display(Name = "Location")]
        public string location;
    }
    public class countries_Validation
    {
        [Display(Name = "Country")]
        public string country_name;
    }
}