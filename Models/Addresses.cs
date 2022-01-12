using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutoChek.Models
{
    public class RegionVM
    {
        public int Id { get; set; }
        public string psgcCode { get; set; }
        public string regDesc { get; set; }
        public string regCode { get; set; }
    }
    public class Region
    {
        [Key]
        public int Id { get; set; }
        public string Psgc { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }

    public class ProvinceVM
    {
        public int Id { get; set; }
        public string psgcCode { get; set; }
        public string provDesc { get; set; }
        public string regCode { get; set; }
        public string provCode { get; set; }
    }
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string Psgc { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int RegionId { get; set; }
        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }
    }

    public class MunicipalityVM
    {
        public int Id { get; set; }
        public string psgcCode { get; set; }
        public string citymunDesc { get; set; }
        public string regCode { get; set; }
        public string provCode { get; set; }
        public string citymunCode { get; set; }
    }
    public class Municipality
    {
        [Key]
        public int Id { get; set; }
        public string Psgc { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
    }



    public class BarangayVM
    {
        public int Id { get; set; }
        public string brgyDesc { get; set; }
        public string regCode { get; set; }
        public string brgyCode { get; set; }
        public string provCode { get; set; }
        public string citymunCode { get; set; }
    }
    public class Barangay
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int MunicipalityId { get; set; }
        [ForeignKey("MunicipalityId")]
        public virtual Municipality Municipality { get; set; }
    }
}