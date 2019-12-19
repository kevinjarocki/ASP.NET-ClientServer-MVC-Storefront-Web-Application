using System.Collections.Generic;
using System.Linq;
namespace CaseStudy.Models
{
    public class BrandModel
    {
        private AppDbContext _db;
        public BrandModel(AppDbContext ctx)
        {
            _db = ctx;
        }
        public List<Brand> GetAll()
        {
            return _db.Brand.ToList<Brand>();
        }
        public string GetName(int id)
        {
            Brand brand = _db.Brand.First(c => c.Id == id);
            return brand.Name;
        }

        
    }
}