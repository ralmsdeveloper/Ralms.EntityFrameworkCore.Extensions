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
    public class TestDataDiff
    {
        private SampleContext _db;
        private List<Blog> _blogList;

        public TestDataDiff()
        {
            _db = new SampleContext();
            _blogList = new List<Blog>();
            _db.Database.EnsureCreated();

            for (int i = 0; i < 100; i++)
            {
                _blogList.Add(new Blog
                {
                    Name = $"Teste {i}",
                    Date = DateTime.Now,
                    Posts = new[]
                    {
                        new Post
                        {
                            Content = $"Test Content {i}",
                            Title = $"Title Post {i}"
                        }
                    }
                });
            }

            _db.Blogs.AddRange(_blogList);
            _db.SaveChanges();
        }

        [Fact]
        public void ClientEval()
        {
        var list = _blogList
            .Where(p => EFCore.DateDiff(DatePart.day, DateTimeOffset.Now, p.Date) < 50)
            .Take(50)
            .ToList();

            Assert.True(list.Count == 50);
        }

        [Fact]
        public void ServerTranslate()
        {
            var sql = _db
                .Blogs
                .Where(p => EFCore.DateDiff(DatePart.day, DateTime.Now, p.Date) < 50)
                .ToSql();

            Assert.Contains("DATEDIFF",sql);
        }
    }
}
