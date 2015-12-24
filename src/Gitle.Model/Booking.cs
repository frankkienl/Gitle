﻿using Gitle.Model.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gitle.Model
{
    public class Booking : ModelBase
    {
        public Booking()
        {
            CreatedAt = DateTime.Now;
        }

        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
        public virtual Issue Issue { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double Minutes { get; set; }
        public virtual string Comment { get; set; }

        public virtual IList<Invoice> Invoices { get; set; }

        public virtual double Hours { get { return Minutes / 60.0; } }
        public virtual string Time
        {
            get
            {
                return string.Format("{0}:{1:00}", Math.Floor(Hours), Minutes - (Math.Floor(Hours) * 60));
            }
        }
    }
}