﻿namespace Gitle.Model
{
    public class Label : ModelBase
    {
        public virtual string Name { get; set; }
        public virtual string Color { get; set; }
        public virtual bool VisibleForCustomer { get; set; }
        public virtual Project Project { get; set; }
    }
}