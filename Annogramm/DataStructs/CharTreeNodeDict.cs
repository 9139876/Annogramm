using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annogramm.DataStructs
{
    class CharTreeNodeDict : ICharTreeNode
    {
        readonly char _char;

        public char Char => _char;

        readonly Dictionary<char, CharTreeNodeDict> childs;

        readonly ICharTreeNode _parent;

        public CharTreeNodeDict(char ch, ICharTreeNode parent)
        {
            _char = ch;
            _parent = parent;
            childs = new Dictionary<char, CharTreeNodeDict>();
        }

        public ICharTreeNode AddAndGet(char ch)
        {
            if (!childs.ContainsKey(ch))
                childs.Add(ch, new CharTreeNodeDict(ch, this));

            return childs[ch];
        }

        public bool IsEndOfWord { get; set; }

        public bool TryGetChild(char ch, out ICharTreeNode charTreeNode)
        {
            bool res = childs.ContainsKey(ch);

            charTreeNode = res ? childs[ch] : null;

            return res;
        }

        public ICharTreeNode GetParent() => _parent;

        public ICharTreeNode GetRoot() => _parent?.GetRoot() ?? this;
    }
}
