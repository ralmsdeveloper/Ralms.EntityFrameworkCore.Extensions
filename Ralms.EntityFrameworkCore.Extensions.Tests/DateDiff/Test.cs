/*
 *          Copyright (c) 2018 Rafael Almeida (ralms@ralms.net)
 *
 *           Ralms.Microsoft.EntitityFrameworkCore.Extensions
 *
 * THIS MATERIAL IS PROVIDED AS IS, WITH ABSOLUTELY NO WARRANTY EXPRESSED
 * OR IMPLIED.  ANY USE IS AT YOUR OWN RISK.
 *
 * Permission is hereby granted to use or copy this program
 * for any purpose,  provided the above notices are retained on all copies.
 * Permission to modify the code and to distribute modified code is granted,
 * provided the above notices are retained, and a notice that the code was
 * modified is included with the above copyright notice.
 *
 */

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ralms.EntityFrameworkCore.Tests
{
    public class Test
    {
        private SampleContext db;
        private List<People> peopleList = null;

        public Test()
        {
            db = new SampleContext();
            peopleList = new List<People>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            for (int i = 0; i < 100; i++)
            {
                peopleList.Add(new People
                {
                    Name = $"Teste {i}",
                    Birthday = DateTime.Now.AddDays(i),
                    Birthday2 = DateTime.Now.AddDays(i),
                    Date = DateTimeOffset.Now.AddDays(i)
                });
            }
        }

        [Fact]
        public void ClientEval()
        {
            var list = peopleList
                .Where(p => EFCore.DateDiff(DatePart.day, DateTimeOffset.Now, p.Date) < 50)
                .ToList();

            Assert.True(list.Count == 50);
        }

        [Fact]
        public void ServerTranslate()
        {
            db.People.AddRange(peopleList);
            db.SaveChanges();

            var list = db
                .People
                .Where(p => EFCore.DateDiff(DatePart.day, DateTime.Now, p.Birthday) < 50)
                .ToList();

            Assert.True(list.Count == 50);
        }
    }
}
