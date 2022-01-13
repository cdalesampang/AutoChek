using AutoChek.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoChek.Models
{
    public class Shop : BaseEntity
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int BrgyId { get; set; }
        public string Street { get; set; }
        public string UnitNo { get; set; }
        public string ZipCode { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
        [ForeignKey("CityId")]
        public virtual Municipality City { get; set; }
        [ForeignKey("BrgyId")]
        public virtual Barangay Barangay { get; set; }
    }
}