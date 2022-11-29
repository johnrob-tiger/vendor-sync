﻿using System.Linq.Expressions;

namespace Tsi.Vendors.DataSync.Domain.Shared.Specification
{
    public class And<T> : BaseSpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;


        public And(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> SpecExpression
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");

                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(
                        Expression.Invoke(_left.SpecExpression, objParam),
                        Expression.Invoke(_right.SpecExpression, objParam)),
                    objParam);

                return newExpr;
            }
        }
    }
}
