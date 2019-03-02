//using System.Collections.Generic;
//using BackSide2.BL.Models.BoardDto;
//using BackSide2.BL.Models.PinDto;
//using BackSide2.DAO.Entities;

//namespace BackSide2.BL.Extensions
//{
//    public static class BoardExtensions
//    {
//        public static BoardReturnDto ToBoardReturnDto(this Item item)
//        {
//            return new BoardReturnDto
//            {
//                Id = item.Id,
//                Name = item.Name,
//                Description = item.Description,
//                Img = item.Img,
//                IsPrivate = item.IsPrivate,
//                Modified = item.Modified,
//                Created = item.Created,
//            };
//        }

//        public static BoardReturnDto ToBoardReturnDto(this Item item, int? pinsCount, bool? isOwner)
//        {
//            return new BoardReturnDto
//            {
//                Id = item.Id,
//                Name = item.Name,
//                Description = item.Description,
//                Img = item.Img,
//                IsPrivate = item.IsPrivate,
//                Modified = item.Modified,
//                Created = item.Created,
//                PinsCount = pinsCount ?? 0,
//                IsOwner = isOwner
//            };
//        }

//        public static BoardReturnDto ToBoardReturnDto(this Item item, bool? isOwner)
//        {
//            return new BoardReturnDto
//            {
//                Id = item.Id,
//                Name = item.Name,
//                Description = item.Description,
//                Img = item.Img,
//                IsPrivate = item.IsPrivate,
//                Modified = item.Modified,
//                Created = item.Created,
//                IsOwner = isOwner
//            };
//        }

//        public static BoardReturnDto ToBoardReturnDto(this Item item, bool? isOwner, bool isLast)
//        {
//            return new BoardReturnDto
//            {
//                Id = item.Id,
//                Name = item.Name,
//                Description = item.Description,
//                Img = item.Img,
//                IsPrivate = item.IsPrivate,
//                Modified = item.Modified,
//                Created = item.Created,
//                IsOwner = isOwner,
//                IsLast = isLast
//            };
//        }
//        public static BoardReturnDto ToBoardReturnDto(this Item item, List<PinReturnDto> pins, bool? isOwner)
//        {
//            return new BoardReturnDto
//            {
//                Id = item.Id,
//                Name = item.Name,
//                Description = item.Description,
//                Img = item.Img,
//                IsPrivate = item.IsPrivate,
//                Modified = item.Modified,
//                Created = item.Created,
//                Pins = pins,
//                IsOwner = isOwner
//            };
//        }
//    }
//}