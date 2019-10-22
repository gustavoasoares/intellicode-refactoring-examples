using System;
using System.Collections.Generic;
using System.Linq;

namespace ExampleProject.a
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
                var parentAttrs = parent.Attrs();
                return parentAttrs.Select(pa => new ParentAttributePred(pa));
            }
            else
            {
                return Enumerable.Empty<IPredicate>();
            }
        }

        public IEnumerable<IPredicate> SelfPredicates(TreeNode node)
        {
            var attrs = node.Attrs();
            return attrs.Select(a => new SelfAttributePred(a));
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
                    var childAttrs = child.Attrs();
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
                    var childAttrs = c.Attrs();
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
                var parentAttrs = parent.Attrs();
                var parentPreds = parentAttrs
                    .Select(pa => new AncestorAttributePred(pa));
                return parentPreds.Concat(ancestorPreds);
            }
            else
            {
                return Enumerable.Empty<IPredicate>();
            }
        }
    }
}
