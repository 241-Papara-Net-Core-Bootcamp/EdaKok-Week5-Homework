﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Domain.Entities
{
    [Table("FakeDataUsers")]
   public class FakeDataUser:BaseEntity
    {
        public int UserId { get; set; }
       
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
