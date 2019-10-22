using FluentValidation;
using System;
using System.Linq.Expressions;

namespace Conduit.Infrastructure
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        protected void NotNullOrEmpty<U>(Expression<Func<T, U>> expression)
        {
            RuleFor(expression).NotNull().NotEmpty();
        }
    }
}