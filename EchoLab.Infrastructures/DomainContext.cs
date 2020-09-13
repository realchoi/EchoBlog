﻿using EchoLab.Domains.ArticleAggregate;
using EchoLab.Domains.CategoryAggregate;
using EchoLab.Domains.TopicAggregate;
using EchoLab.Domains.UserAggregate;
using EchoLab.Infrastructures.Core;
using EchoLab.Infrastructures.Core.Snowflake;
using EchoLab.Infrastructures.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EchoLab.Infrastructures
{
    public class DomainContext : EFContext
    {
        private readonly SnowflakeId _snowflakeId;

        public DomainContext(DbContextOptions options, SnowflakeId snowflakeId) : base(options)
        {
            this._snowflakeId = snowflakeId;
        }

        /// <summary>
        /// 用户口令认证
        /// </summary>
        public DbSet<LocalAuthUser> LocalAuthUsers { get; set; }

        /// <summary>
        /// 用户资料信息
        /// </summary>
        public DbSet<UserProfile> UserProfiles { get; set; }

        /// <summary>
        /// 话题
        /// </summary>
        public DbSet<Topic> Topics { get; set; }

        /// <summary>
        /// 话题分类
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        public DbSet<Article> Articles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleEntityTypeConfiguration(this._snowflakeId));
            modelBuilder.ApplyConfiguration(new TopicEntityTypeConfiguration(this._snowflakeId));
            modelBuilder.ApplyConfiguration(new UserProfileEntityTypeConfiguration(this._snowflakeId));
            modelBuilder.ApplyConfiguration(new LocalAuthUserEntityTypeConfiguration(this._snowflakeId));
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration(this._snowflakeId));

            base.OnModelCreating(modelBuilder);
        }
    }
}