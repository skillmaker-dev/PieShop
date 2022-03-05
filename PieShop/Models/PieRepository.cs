using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDbContext appDbContext;

        public PieRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public IEnumerable<Pie> AllPies
        {
            get
            {
                return appDbContext.Pies.Include(c => c.Category);
            }
        }
        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return appDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return appDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
