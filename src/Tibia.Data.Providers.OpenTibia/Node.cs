namespace Tibia.Data.Providers.OpenTibia
{
    public class Node
    {
        private Node _child;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Node" /> class.
        /// </summary>
        public Node()
        {
            Start = 0;
            PropsSize = 0;
            Type = 0;
            Next = null;
            _child = null;
        }

        /// <summary>
        ///     The child.
        /// </summary>
        public Node Child
        {
            get { return _child; }
            set { _child = value; }
        }

        /// <summary>
        ///     The next.
        /// </summary>
        public Node Next { get; set; }

        /// <summary>
        ///     The props size.
        /// </summary>
        public long PropsSize { get; set; }

        /// <summary>
        ///     The start.
        /// </summary>
        public long Start { get; set; }

        /// <summary>
        ///     The type.
        /// </summary>
        public long Type { get; set; }

        /// <summary>
        ///     Clears the next node.
        /// </summary>
        /// <param name="node">The node.</param>
        private static void ClearNext(Node node)
        {
            Node deleteNode = node;

            while (deleteNode != null)
            {
                if (deleteNode._child != null)
                    ClearChild(ref deleteNode._child);

                Node nextNode = deleteNode.Next;
                deleteNode = nextNode;
            }
        }

        /// <summary>
        ///     Clears the child nodes.
        /// </summary>
        /// <param name="node">The node.</param>
        private static void ClearChild(ref Node node)
        {
            if (node._child != null)
                ClearChild(ref node._child);

            if (node.Next != null)
                ClearNext(node.Next);

            node = null;
        }
    }
}