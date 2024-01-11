﻿namespace eCommerce_App.Models
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        // Foreign key relationships
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }

        // Navigation properties
        public ProductType ProductType { get; set; }
        public ProductBrand ProductBrand { get; set; }

    }

}