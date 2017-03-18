using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Validations
{
    public class ProductNameIsNotAllow_Shit_StringAttribute : DataTypeAttribute
    {
        public ProductNameIsNotAllow_Shit_StringAttribute() : base(DataType.Text)
        {
            this.ErrorMessage = ("商品名稱不得有Shit字串");
        }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value).ToLower();
            if (str.Contains("shit"))
            {
                return false;
            }
            return true;
        }
    }    
}