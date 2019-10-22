using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProject
{
    public class TreeNode
    {
        public TreeNode Parent;

        public IEnumerable<IAttribute> Attrs() { return null; }

        public IEnumerable<TreeNode> Children() { return null; }

        public IKind Kind;
    }

    public interface IKind
    {

    }
}
