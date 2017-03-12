namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Validations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {

    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "請輸入商品名稱")]
        [StringLength(5, ErrorMessage = "商品名稱過長")]
        [DisplayName("商品名稱")]
        [ProductNameIsNotAllow_Fish_String]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "請輸入價格")]
        [Range(10, 500, ErrorMessage = "價格範圍:10-500")]
        [DisplayName("價格")]
        public decimal? Price { get; set; }

        [Required]
        [DisplayName("狀態")]
        public bool? Active { get; set; }

        [Required(ErrorMessage = "尚未輸入")]
        public decimal? Stock { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
