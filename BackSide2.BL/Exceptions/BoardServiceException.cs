﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BackSide2.BL.Exceptions
{
    [Serializable]
    class BoardServiceException : ApplicationException
    {
        public BoardServiceException()
        {
        }

        public BoardServiceException(string message) : base(message)
        {
        }

        public BoardServiceException(string message, Exception ex) : base(message)
        {
            Ex = ex;
        }

        // Конструктор для обработки сериализации типа
        protected BoardServiceException(SerializationInfo info,
            StreamingContext contex)
            : base(info, contex)
        {
        }

        public Exception Ex { get; }
    }
}