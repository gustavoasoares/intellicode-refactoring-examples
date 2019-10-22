using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleProject
{
    public class AttributePredicateGenerator
    {
        private PredicateContext _context;

        public AttributePredicateGenerator(PredicateContext context)
        {
            this._context = context;
        }

        public IEnumerable<IPredicate> AllPredicates(TreeNode node)
        {
            return ParentPredicates(node)
                .Concat(SelfPredicates(node))
                .Concat(RelativeChildPredicates(node))
                .Concat(FixedChildPredicates(node))
                .Concat(AncestorPredicates(node));
        }

        public IEnumerable<IPredicate> ParentPredicates(TreeNode node)
        {
            var parent = node.Parent;
            if (parent != null)
            {
                var parentAttrs = parent.Attrs().Where(a => IsValid(a, parent.Kind));
                return parentAttrs.Select(pa => new ParentAttributePred(pa));
            }
            else
            {
                return Enumerable.Empty<IPredicate>();
            }
        }

        public IEnumerable<IPredicate> SelfPredicates(TreeNode node)
        {
            var attrs = node.Attrs().Where(a => IsValid(a, node.Kind));
            return attrs.Select(a => new SelfAttributePred(a));
        }

        private bool IsValid(IAttribute a, IKind kind)
        {
            return this._context.IsKindAttrValid(a, kind);
        }

        public IEnumerable<IPredicate> RelativeChildPredicates(TreeNode node)
        {
            var children = node.Children();
            var childrenWithSelectors =
                children.Select(c => Tuple.Create(c, new RelativeSelectors(c, children)));
            return childrenWithSelectors
                .SelectMany(cs =>
                {
                    var child = cs.Item1;
                    var relativeSelector = cs.Item2;
                    var childAttrs = child.Attrs().Where(a => IsValid(a, child.Kind));
                    return childAttrs
                        .Select(ca =>
                                new RelativeChildAttrPred(relativeSelector, ca));
                });
        }

        public IEnumerable<IPredicate> FixedChildPredicates(TreeNode node)
        {
            var children = node.Children();
            return children
                .SelectMany((c, i) =>
                {
                    var childAttrs = c.Attrs().Where(a => IsValid(a, c.Kind));
                    return childAttrs
                        .Select(ca => new FixedChildAttrPred(i, ca));
                });
        }

        public IEnumerable<IPredicate> AncestorPredicates(TreeNode node)
        {
            var parent = node.Parent;
            if (parent != null)
            {
                var ancestorPreds = AncestorPredicates(parent);
                var parentAttrs = parent.Attrs().Where(a => IsValid(a, parent.Kind));
                var parentPreds = parentAttrs
                    .Select(pa => new AncestorAttributePred(pa));
                return parentPreds.Concat(ancestorPreds);
            }
            else
            {
                return Enumerable.Empty<IPredicate>();
            }
        }

        //public bool IsValid(IAttribute attribute, IKind kind)
        //{
        //    return this._context.IsKindAttrValid(attribute, kind);
        //}
    }
}
