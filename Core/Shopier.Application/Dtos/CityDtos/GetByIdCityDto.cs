using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopier.Application.Dtos.CityDtos
{
    public class GetByIdCityDto
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
