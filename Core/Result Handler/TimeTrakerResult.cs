using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Result_Handler
{
    public class TimeTrakerResult : Exception
    {
        readonly List<Exception> _exceptions;

        public TimeTrakerResult(List<Exception> exceptions)
        {
            _exceptions = exceptions;
        }

        public IEnumerable<Exception> Exceptions => _exceptions;
        public IEnumerable<string> ExeptionMessages => _exceptions.Select(ex => ex.Message);
    }
}
