﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoundAndEat.Dtos.Recept.Image
{
    public class ImageDto
    {
        public long Id { get; set; }
        public string UrlAdress { get; set; }
        public long ReceptId { get; set; }
    }
}