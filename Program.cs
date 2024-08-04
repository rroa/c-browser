using System;
using System.Collections.Generic;

namespace BrowserRenderingSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            string html = "<html><body><h1>Hello, World!</h1><p>This is a simple browser rendering simulation.</p></body></html>";

            // Step 1: Parse HTML
            Node root = ParseHTML(html);

            // Step 2: Build Render Tree (Simplified: Same as DOM in this example)
            RenderNode renderRoot = BuildRenderTree(root);

            // Step 3: Layout Calculation
            CalculateLayout(renderRoot, 0, 0);

            // Step 4: Rendering
            Render(renderRoot);

            Console.ReadLine();
        }

        static Node ParseHTML(string html)
        {
            // Simplified HTML parser
            Node root = new ElementNode("html");
            Node body = new ElementNode("body");
            root.Children.Add(body);

            body.Children.Add(new ElementNode("h1", "Hello, World!"));
            body.Children.Add(new ElementNode("p", "This is a simple browser rendering simulation."));

            return root;
        }

        static RenderNode BuildRenderTree(Node node)
        {
            RenderNode renderNode = new RenderNode(node.Tag, node.Text);
            foreach (var child in node.Children)
            {
                renderNode.Children.Add(BuildRenderTree(child));
            }
            return renderNode;
        }

        static void CalculateLayout(RenderNode node, int x, int y)
        {
            // Simplified layout calculation
            node.X = x;
            node.Y = y;
            int currentY = y;
            foreach (var child in node.Children)
            {
                CalculateLayout(child, x + 20, currentY + 20);
                currentY += 40;
            }
        }

        static void Render(RenderNode node)
        {
            // Simplified rendering to console
            Console.SetCursorPosition(node.X, node.Y);
            Console.WriteLine(node.Tag + ": " + node.Text);
            foreach (var child in node.Children)
            {
                Render(child);
            }
        }
    }

    class Node
    {
        public string Tag { get; set; }
        public string Text { get; set; }
        public List<Node> Children { get; set; }

        public Node(string tag, string text = "")
        {
            Tag = tag;
            Text = text;
            Children = new List<Node>();
        }
    }

    class ElementNode : Node
    {
        public ElementNode(string tag, string text = "") : base(tag, text) { }
    }

    class RenderNode
    {
        public string Tag { get; set; }
        public string Text { get; set; }
        public List<RenderNode> Children { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public RenderNode(string tag, string text = "")
        {
            Tag = tag;
            Text = text;
            Children = new List<RenderNode>();
        }
    }
}
