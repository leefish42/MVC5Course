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
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        [Required(ErrorMessage = "請輸入商品名稱")]
        [DisplayName("商品名稱")]
        [ProductNameIsNotAllow_Shit_String]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "請輸入金額")]
        [DisplayName("商品價錢")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        [Range(10, 9999, ErrorMessage = "超出金額範圍")]
        public decimal? Price { get; set; }

        [Required]
        [DisplayName("商品狀態")]
        public bool? Active { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        [DisplayName("商品股價")]
        [Required]
        public decimal? Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
