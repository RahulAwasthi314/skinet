using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string Description { get; set;} = null!;

        public decimal Price { get; set; } = 0;

        public string PictureUrl {get; set; } = null!;

        public ProductType ProductType { get; set; } = null!;

        public int ProductTypeId { get; set; }

        public ProductBrand ProductBrand { get; set; } = null!;

        public int ProductBrandId { get; set; }


    }
}