using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Creteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; }= 
            new List<Expression<Func<T, object>>>();

        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> creteria)
        {
            Creteria = creteria;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}