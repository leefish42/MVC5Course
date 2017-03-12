using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public override IQueryable<Product> All()
        {
            return base.All().Where(p => false == p.IsDeleted && p.Stock < 500);
        }

        public IQueryable<Product> All(bool ShowAll)
        {
            if(ShowAll)
            {
                return base.All();
            }
            else
            {
                return this.All();
            }
        }
        /// <summary>
        /// �b�e���W�R��,�����R����Ʈw���e
        /// </summary>
        /// <param name="entity"></param>
        public override void Delete(Product entity)
        {
            entity.IsDeleted = true;
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}