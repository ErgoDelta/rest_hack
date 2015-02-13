using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    public class VertexBuffer<VertexT> : Disposable where VertexT : struct
    {
        public VertexBuffer(VertexT[] vertices)
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
            if (boundArrayBuffer == ID) return;
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            boundArrayBuffer = ID;
        }
        static int boundArrayBuffer = 0;

        static int vertexStride = Marshal.SizeOf(typeof(VertexT));

        public void Update(VertexT[] vertices, int count = -1)
        {
            Bind();
            if (count == -1) count = vertices.Length;
            if (count > capacity)
            {
                capacity = count;
                GL.BufferData<VertexT>(BufferTarget.ArrayBuffer, (IntPtr)(count * vertexStride), vertices, BufferUsageHint.StaticDraw);
            }
            else
            {
                GL.BufferSubData<VertexT>(BufferTarget.ArrayBuffer, IntPtr.Zero, (IntPtr)(count * vertexStride), vertices);
            }
        }
        int count;
        int capacity;

        public void DrawPoints()
        {
            GL.DrawArrays(PrimitiveType.Points, 0, count);
        }
    }
}
