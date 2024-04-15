using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoundAndEat.Dtos.Follow
{
    public class FollowDto
    {
        public long Id { get; set; }
        public long ReceptId { get; set; }
        public long UserId { get; set; }
        public string ReceptName { get; set; }
        public string ReceptImage { get; set; }
    }
}
