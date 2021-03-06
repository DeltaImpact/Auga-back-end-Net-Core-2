﻿using System;
using System.Collections.Generic;
using Auga.BL.Models.BoardDto;

namespace Auga.BL.Models.PinDto
{
    public class PinReturnDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public string Link { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Created { get; set; }
        public List<BoardReturnDto> Boards { get; set; }
        public LastPinActionDto LastAction { get; set; }
        
    }
}