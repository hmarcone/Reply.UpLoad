using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.UpLoad.Models
{
    public class UpLoadImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public byte[] Image { get; set; }

    }
}
