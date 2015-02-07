using System.Runtime.InteropServices;

namespace CuttingEdge
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Triangle
    {
        public Triangle(int index0, int index1, int index2)
        {
            Index0 = index0;
            Index1 = index1;
            Index2 = index2;
        }
        public int Index0;
        public int Index1;
        public int Index2;
    }
}
