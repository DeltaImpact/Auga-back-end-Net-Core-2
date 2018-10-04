﻿using System.Threading.Tasks;
using BackSide2.BL.Models.ParseDto;

namespace BackSide2.BL.ParsePageService
{
    public interface IParsePageService
    {
        Task<object> ParsePageAsync(ParsePageDto entity);
    }
}