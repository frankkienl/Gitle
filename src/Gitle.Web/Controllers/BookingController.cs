﻿using Gitle.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Linq;

namespace Gitle.Web.Controllers
{
    public class BookingController : SecureController
    {
        private readonly ISession session;

        public BookingController(ISessionFactory sessionFactory)
        {
            this.session = sessionFactory.GetCurrentSession();
        }

        public void Index()
        {
            var bookings = session.Query<Booking>()
                .Where(x => x.User == CurrentUser && x.Date > DateTime.Today.AddDays(-7))
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Date.Date)
                .ToDictionary(g => new { date = g.Key, total = g.ToList().Sum(x => x.Minutes) }, g => g.ToList());
            PropertyBag.Add("bookings", bookings);
        }

        public void Save()
        {
            var booking = BindObject<Booking>("booking");
            
            booking.User = CurrentUser;
            if (booking.Issue.Id == 0)
            {
                booking.Issue = null;
            }

            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(booking);
                transaction.Commit();
            }
            RedirectToReferrer();
        }
    }
}