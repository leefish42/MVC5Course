using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Validations
{
    public class ProductNameIsNotAllow_Fish_StringAttribute : DataTypeAttribute
    {
        public ProductNameIsNotAllow_Fish_StringAttribute() : base(DataType.Text)
        {
            this.ErrorMessage = ("商品名稱不得有Fish字串");
        }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value);
            if (str.Contains("Fish"))
            {
                return false;
            }
            return true;
        }
    }    
}