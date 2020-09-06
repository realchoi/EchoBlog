﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EchoBlog.Api.Applications.Queries.CategoryQueries;
using EchoBlog.Api.Dtos;
using EchoBlog.Core;
using EchoBlog.Infrastructures.Core.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EchoBlog.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        /// <summary>
        /// 获取话题分类列表
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpPost("getList")]
        public async Task<Result<IEnumerable<CategoryDto>>> GetList()
        {
            var result = new Result<IEnumerable<CategoryDto>>();
            try
            {
                var categoryDto = await _mediator.Send(new CategoryQuery());
                result.Data = categoryDto;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = $"程序发生异常：{ex.Message}";
            }
            return result;
        }
    }
}
