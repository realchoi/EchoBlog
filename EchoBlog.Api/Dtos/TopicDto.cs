﻿using EchoBlog.Infrastructures.Core.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBlog.Api.Dtos
{
    /// <summary>
    /// 话题数据传输类
    /// </summary>
    public class TopicDto : BaseDto<long>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// 作者 Id
        /// </summary>
        public string AuthorId { get; private set; }

        /// <summary>
        /// 作者名称
        /// </summary>
        public string AuthorName { get; private set; }

        /// <summary>
        /// 节点 Id
        /// </summary>
        public string NodeId { get; private set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; private set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadTimes { get; private set; }
    }
}