﻿using Gitle.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gitle.Model
{
    public class Invoice : ModelBase
    {
        public Invoice()
        {
            VAT = true;
            State = InvoiceState.Concept;
            Lines = new List<InvoiceLine>();
            Bookings = new List<Booking>();
            Corrections = new List<Correction>();
        }

        public Invoice(Project project, DateTime startDate, DateTime endDate)
            : this()
        {
            Project = project;
            HourPrice = project.HourPrice;
            StartDate = startDate;
            EndDate = endDate;
            Title = string.Format("{0} ({1:dd-MM-yyyy} tot {2:dd-MM-yyyy})", Project.Name, StartDate, EndDate);
        }

        public Invoice(Project project, DateTime startDate, DateTime endDate, IList<Booking> bookings)
            : this(project, startDate, endDate)
        {
            Bookings = bookings;
            foreach (var booking in bookings)
            {
                if(booking.Issue != null){
                    var invoiceLine = Lines.FirstOrDefault(il => il.Issue == booking.Issue);
                    if (invoiceLine == null)
                    {
                        invoiceLine = new InvoiceLine()
                        {
                            Description = string.Format("#{0} - {1}", booking.Issue.Number, booking.Issue.Name),
                            Issue = booking.Issue,
                            Invoice = this
                        };
                        Lines.Add(invoiceLine);
                    }
                    invoiceLine.Hours += booking.Hours;
                }
                else
                {
                    Lines.Add(new InvoiceLine() { Description = booking.Comment, Invoice = this, Hours = booking.Hours });
                }
            }
            for (var i = Corrections.Count(); i < 5; i++)
            {
                Corrections.Add(new Correction());
            }
        }

        public virtual string Number { get; set; }
        public virtual string Title { get; set; }
        public virtual int HourPrice { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual bool VAT { get; set; }
        public virtual string Remarks { get; set; }
        public virtual InvoiceState State { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual Project Project { get; set; }
        public virtual IList<InvoiceLine> Lines { get; set; }
        public virtual IList<Booking> Bookings { get; set; }
        public virtual IList<Correction> Corrections { get; set; }

        public virtual bool IsConcept { get { return State == InvoiceState.Concept; } }
        public virtual bool IsDefinitive { get { return State == InvoiceState.Definitive; } }
        public virtual bool IsArchived { get { return State == InvoiceState.Archived; } }

        public virtual double TotalExclVat { get { return Lines.Sum(x => x.Price) + Corrections.Sum(x => x.Price); } }
        public virtual double Total { get { return TotalExclVat * (VAT ? 1.21 : 1); } }

        public virtual int IssueCount { get { return Lines.Count(x => x.Issue != null); } }

        public virtual IList<InvoiceLine> ProjectLines { get { return Lines.Where(x => x.Issue == null).ToList(); } }
        public virtual IList<InvoiceLine> IssueLines { get { return Lines.Where(x => x.Issue != null).ToList(); } }

        public virtual IList<Correction> FiveCorrections
        {
            get
            {
                for (var i = Corrections.Count(); i < 5; i++)
                {
                    Corrections.Add(new Correction());
                }
                return Corrections;
            }
        }
    }
}