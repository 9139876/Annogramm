using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annogramm.DataStructs
{
    class CharTreeNodeList : ICharTreeNode
    {
        readonly char _char;

        public char Char => _char;

        readonly List<CharTreeNodeList> childs;

        readonly ICharTreeNode _parent;

        public CharTreeNodeList(char ch, ICharTreeNode parent)
        {
            _char = ch;
            _parent = parent;            
            childs = new List<CharTreeNodeList>();
        }

        public ICharTreeNode AddAndGet(char ch)
        {
            var res = childs.FirstOrDefault(n => n.Char == ch);

            if (res == null)
            {
                res = new CharTreeNodeList(ch, this);
                childs.Add(res);
            }
            
            return res;
        }

        public bool IsEndOfWord { get; set; }

        public bool TryGetChild(char ch, out ICharTreeNode charTreeNode)
        {
            charTreeNode = childs.FirstOrDefault(n => n.Char == ch);
            
            return charTreeNode != null;
        }

        public ICharTreeNode GetParent() => _parent;

        public ICharTreeNode GetRoot() => _parent?.GetRoot() ?? this;
    }
}
