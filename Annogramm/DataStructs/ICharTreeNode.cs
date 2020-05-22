namespace Annogramm.DataStructs
{
    interface ICharTreeNode
    {
        char Char { get; }
        bool IsEndOfWord { get; set; }

        ICharTreeNode AddAndGet(char ch);
        ICharTreeNode GetParent();
        ICharTreeNode GetRoot();
        bool TryGetChild(char ch, out ICharTreeNode charTreeNode);
    }
}