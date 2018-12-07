using System.IO;

namespace Tibia.Data.Providers.OpenTibia
{
    public abstract class FileReaderBase<TPropertyReader>
    {
        /// <summary>
        ///     Gets or sets the file stream.
        /// </summary>
        /// <value>
        ///     The file stream.
        /// </value>
        protected FileStream FileStream { get; set; }

        /// <summary>
        ///     Gets or sets the reader.
        /// </summary>
        /// <value>
        ///     The reader.
        /// </value>
        protected BinaryReader Reader { get; set; }

        /// <summary>
        ///     Gets or sets the root.
        /// </summary>
        /// <value>
        ///     The root.
        /// </value>
        protected Node Root { get; set; }

        /// <summary>
        ///     Gets the root node.
        /// </summary>
        /// <returns>The node.</returns>
        protected Node GetRootNode()
        {
            return Root;
        }

        /// <summary>
        ///     Opens the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <exception cref="FileFormatException">Could not find start node.</exception>
        protected void OpenFile(string fileName)
        {
            FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            Reader = new BinaryReader(FileStream);
        }

        /// <summary>
        ///     Reads the property.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="propertyReader">The property reader.</param>
        /// <returns>Whether the property is read successfully.</returns>
        public abstract bool ReadProperty(Node node, out TPropertyReader propertyReader);

        /// <summary>
        ///     Parses the node.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>The node.</returns>
        protected bool ParseNode(Node node)
        {
            Node currentNode = node;
            while (true)
            {
                // Node Type
                int val = FileStream.ReadByte();
                if (val != -1)
                {
                    currentNode.Type = val;
                    bool setPropSize = false;
                    while (true)
                    {
                        // search child and next node
                        val = FileStream.ReadByte();
                        if (val == -1)
                            break;

                        if (val == NodeTags.StartTag)
                        {
                            Node childNode = new Node();
                            childNode.Start = FileStream.Position;
                            currentNode.PropsSize = FileStream.Position - currentNode.Start - 2;
                            currentNode.Child = childNode;

                            setPropSize = true;

                            if (!ParseNode(childNode))
                                return false;
                        }
                        else if (val == NodeTags.EndTag)
                        {
                            if (!setPropSize)
                                currentNode.PropsSize = FileStream.Position - currentNode.Start - 2;

                            val = FileStream.ReadByte();

                            if (val != -1)
                            {
                                if (val == NodeTags.StartTag)
                                {
                                    // start next node
                                    Node nextNode = new Node();
                                    nextNode.Start = FileStream.Position;
                                    currentNode.Next = nextNode;
                                    currentNode = nextNode;
                                    break;
                                }

                                if (val == NodeTags.EndTag)
                                {
                                    // up 1 level and move 1 position back
                                    // safeTell(pos) && safeSeek(pos)
                                    FileStream.Seek(-1, SeekOrigin.Current);
                                    return true;
                                }

                                // TODO: throw new FileFormatException();
                                return false;
                            }

                            // TODO: throw new EndOfStreamException();
                            return false;
                        }
                        else if (val == NodeTags.EscapeTag)
                        {
                            FileStream.ReadByte();
                        }
                    }
                }
                else
                {
                    // TODO: throw new FileFormatException();
                    return false;
                }
            }
        }

        /// <summary>
        ///     Reads the buffer.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="size">The size.</param>
        /// <returns>The buffer.</returns>
        protected byte[] ReadBuffer(Node node, out long size)
        {
            byte[] buffer = new byte[node.PropsSize];

            FileStream.Seek(node.Start + 1, SeekOrigin.Begin);
            FileStream.Read(buffer, 0, (int) node.PropsSize);

            uint j = 0;
            bool escaped = false;
            for (uint i = 0; i < node.PropsSize; ++i, ++j)
            {
                if (buffer[i] == NodeTags.EscapeTag)
                {
                    buffer[j] = buffer[++i];
                    escaped = true;
                }
                else if (escaped)
                {
                    buffer[j] = buffer[i];
                }
            }

            size = j;
            return buffer;
        }

        protected static class NodeTags
        {
            /// <summary>
            ///     The escape tag.
            /// </summary>
            public const byte EscapeTag = 0xFD;

            /// <summary>
            ///     The start tag.
            /// </summary>
            public const byte StartTag = 0xFE;

            /// <summary>
            ///     The end tag.
            /// </summary>
            public const byte EndTag = 0xFF;
        }
    }
}