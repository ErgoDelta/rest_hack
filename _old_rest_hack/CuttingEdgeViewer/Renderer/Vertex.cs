using OpenTK;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct Vertex
        {
            public Vertex(float x, float y, float u, float v)
            {
                Position = new Vector2(x, y);
                TexCoord = new Vector2(u, v);
            }
            public Vector2 Position;
            public Vector2 TexCoord;
        }
}
