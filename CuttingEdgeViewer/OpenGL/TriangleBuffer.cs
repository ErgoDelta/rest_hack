using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    public class TriangleBuffer : Disposable
    {
        public TriangleBuffer(Triangle[] triangles)
        {
            ID = GL.GenBuffer();
        }
        public readonly int ID;

        protected override void Dispose(bool disposing)
        {
            GL.DeleteBuffer(ID);
            base.Dispose(disposing);
        }

        public void Bind()
        {
            if (boundElementArrayBuffer == ID) return;
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            boundElementArrayBuffer = ID;
        }
        static int boundElementArrayBuffer = 0;

        static int triangleStride = Marshal.SizeOf(typeof(Triangle));

        public void Update(Triangle[] triangles, int count = -1)
        {
            Bind();
            if (count == -1) count = triangles.Length;
            if (count > capacity)
            {
                capacity = count;
                GL.BufferData<Triangle>(BufferTarget.ArrayBuffer, (IntPtr)(count * triangleStride), triangles, BufferUsageHint.StaticDraw);
            }
            else
            {
                GL.BufferSubData<Triangle>(BufferTarget.ArrayBuffer, IntPtr.Zero, (IntPtr)(count * triangleStride), triangles);
            }
        }
        int count;
        int capacity;

        public void DrawTriangles()
        {
            GL.DrawElements(BeginMode.Triangles, count, DrawElementsType.UnsignedInt, 0);
        }
    }
}
