using OpenTK;
using System.Drawing;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Particle
    {
        public Vector4 Position;
        //public byte Red;
        //public byte Green;
        //public byte Blue;
        //public byte Alpha;
    }
}
