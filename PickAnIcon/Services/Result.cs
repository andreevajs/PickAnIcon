using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PickAnIcon.Services
{
    public class Result
    {
        public bool HasErrors { get; protected set; }
        public string ErrorMessage { get; protected set; }

        public static Result Success() => new Result { HasErrors = false};

        public static Result Error(string message = null) => new Result { HasErrors = true, ErrorMessage = message };
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }

        public static Result<T> Success() => new Result<T> { HasErrors = false };

        public static Result<T> Error(string message = null) => new Result<T> { HasErrors = true, ErrorMessage = message };

        public Result<T> WithValue(T value)
        {
            Value = value;
            return this;
        }
    }
}
