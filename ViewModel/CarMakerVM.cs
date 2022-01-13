using AutoChek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoChek.ViewModel
{
    public class CarMakerVM
    {
        public List<Brand> Makers { get; set; }
        public List<Model> Models { get; set; }
    }
}