using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace CaseStudy.Models
{
    public class BrandViewModel
    {
        private List<Brand> _brands;
        [Required]
        public int Qty { get; set; }
        public BrandViewModel() { }
        public string BrandName { get; set; }
        public string Id { get; set; }
        public int BrandId { get; set; }
        public List<Brand> Brand { get; set; }

        public string Description { get; set; }

        public IEnumerable<Product> Products { get; set; } 
        public IEnumerable<SelectListItem> GetBrands()
        {
            return _brands.Select(brand => new SelectListItem
            {
                Text = brand.Name,
                Value = Convert.ToString(brand.Id)
            });
        }
        public void SetBrands(List<Brand> brands)
        {
            _brands = brands;
        }
    }
    }
