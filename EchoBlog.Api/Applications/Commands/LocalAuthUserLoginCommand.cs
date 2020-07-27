﻿using EchoBlog.Api.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBlog.Api.Applications.Commands
{
    public class LocalAuthUserLoginCommand : IRequest<LocalAuthUserDto>
    {
        public LocalAuthUserLoginCommand() { }

        public LocalAuthUserLoginCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
