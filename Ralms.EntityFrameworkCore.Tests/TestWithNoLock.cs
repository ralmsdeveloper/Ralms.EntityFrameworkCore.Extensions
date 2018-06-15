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
    public class TestWithNoLock
    {
        private SampleContext _db;
        private List<Blog> _blogList;

        public TestWithNoLock()
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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_wih_no_lock(bool isNolock)
        {
            var query = _db
                .Blogs 
                .Include(p => p.Posts)
                .Take(10)
                .WithNoLock(isNolock)
                .ToSql();

            if (isNolock)
            {
                Assert.Contains("WITH (NOLOCK)", query);
            }
            else
            {
                Assert.DoesNotContain("WITH (NOLOCK)", query);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_hints(bool repeatForInclude)
        {
            var hints = _db
                .Blogs
                .Include(p => p.Posts)
                .Hint("WITH (NOLOCK)", repeatForInclude)
                .ToSql(); 

             Assert.Contains("WITH (NOLOCK)", hints);
        }
    }
}
