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
    public class TestWithHint
    {
        private readonly SampleContext _db;
        private readonly List<Blog> _blogList;

        public TestWithHint()
        {
            _db = new SampleContext("WithLock");
            _blogList = new List<Blog>();
            _db.Database.EnsureDeleted();
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
        [InlineData(nameof(SqlServerHints.PAGLOCK))]
        [InlineData(nameof(SqlServerHints.NOLOCK))]
        [InlineData(nameof(SqlServerHints.HOLDLOCK))]
        [InlineData(nameof(SqlServerHints.NOWAIT))]
        [InlineData(nameof(SqlServerHints.ROWLOCK))]
        [InlineData(nameof(SqlServerHints.XLOCK))]
        [InlineData(nameof(SqlServerHints.UPDLOCK))]
        [InlineData("")]
        public void Test_wih_no_lock(string hint)
        {
            var query = _db
                .Blogs
                .Include(p => p.Posts)
                .WithHint(hint)
                .Skip(5)
                .Take(10)
                .ToSql();

            if (!string.IsNullOrWhiteSpace(hint))
            {
                Assert.Contains($"WITH ({hint})", query);
            }
            else
            {
                Assert.DoesNotContain($"WITH ({hint})", query);
            }
        }
    }
}
