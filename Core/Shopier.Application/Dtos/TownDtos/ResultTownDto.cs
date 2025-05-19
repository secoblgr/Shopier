using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.Dtos.TownDtos
{
   public  class ResultTownDto
    {
        public int Id { get; set; }
        public int TownId { get; set; }
        public int CityId { get; set; }
        public string TownName { get; set; }
    }
}
