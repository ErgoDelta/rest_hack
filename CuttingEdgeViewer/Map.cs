using OpenTK;

namespace CuttingEdge
{
    public class Node
    {
        public int id { get; set; }
        public string owner { get; set; }
        public int points { get; set; }
        public int[] connections { get; set; }
        public string starting_node { get; set; }
        //public Vector2 location { get; set; }
    }
    public class Map
    {
        public Node[] nodes { get; set; }
    }
}
