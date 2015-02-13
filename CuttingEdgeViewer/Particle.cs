using OpenTK;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Particle
    {
        public Vector4 Position;
        public ushort parent;
        public ushort child;
        //public Vector4 Color;
    }
}
