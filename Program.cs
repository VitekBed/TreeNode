using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            TreeNode root = new TreeNode("RootFolder");
            root.AddChild(new TreeNode("folder1"));

            root.GetChildernByName("folder1").AddChild(new TreeNode("folder2"));
            TreeNode node = root.GetChildernByName("folder1").GetChildernByName("folder2");

            Console.WriteLine(string.Join("\\", node.GetFullPath()));
        }
    }

    public class TreeNode
    {
        public string Name { get; }
        public NodeType Type { get; }
        public Dictionary<TreeNode, NodeType> Childern { get; }
        public TreeNode GetChildernByName(string name) => Childern.First(x => x.Key.Name == name).Key;
        public TreeNode Parent { get; private set; }
        public string FullName => IsRoot ? string.Empty : this.Parent.FullName + "/" + Name;
        public TreeNode(string name, NodeType type, TreeNode parent)
        {
            this.Name = name;
            this.Type = type;
            this.Parent = parent;
            this.Childern = new Dictionary<TreeNode, NodeType>();
        }
        public TreeNode(string name) : this(name, NodeType.Folder, null) { }
        public TreeNode(string name, NodeType type) : this(name, type, null) { }
        public bool IsRoot => Parent == null;
        public void AddChild(params TreeNode[] nodes) => AddChild(true, nodes);
        public void AddChild(bool setParent, params TreeNode[] nodes)
        {
            foreach (TreeNode node in nodes)
            {
                Childern.Add(node, node.Type);
                if (setParent) node.Parent = this;
            }
        }
        public IEnumerable<TreeNode> GetFullPath()
        {
            List<TreeNode> parents = new List<TreeNode>();
            if (this.Parent != null)
            {
                parents.AddRange(Parent.GetFullPath());
            }
            parents.Add(this);
            return parents;
        }
        public override string ToString() => Name;
    }
    public enum NodeType
    {
        Folder,
        File
    }
}
