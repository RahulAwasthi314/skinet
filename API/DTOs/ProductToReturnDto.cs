using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set;} = null!;

        public decimal Price { get; set; } = 0;

        public string PictureUrl {get; set; } = null!;

        public string ProductType { get; set; } = null!;

        public string ProductBrand { get; set; } = null!;
    }
}