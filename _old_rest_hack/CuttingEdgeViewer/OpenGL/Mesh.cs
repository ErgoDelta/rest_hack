using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    public class Mesh<VertexT, TriangleT>
        where VertexT : struct
        where TriangleT : struct
    {
        static int vertexStride = Marshal.SizeOf(typeof(VertexT));
        static int triangleStride = Marshal.SizeOf(typeof(TriangleT));

        public Mesh(VertexT[] vertices, TriangleT[] triangles)
        {
            GL.GenBuffers(1, out vertexBufferID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferID);
            GL.BufferData<VertexT>(BufferTarget.ArrayBuffer, (IntPtr)(vertices.Length * vertexStride), vertices, BufferUsageHint.StaticDraw);

            GL.GenBuffers(1, out triangleBufferID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, triangleBufferID);
            GL.BufferData<TriangleT>(BufferTarget.ElementArrayBuffer, (IntPtr)(triangles.Length * triangleStride), triangles, BufferUsageHint.StaticDraw);

            triangleCount = triangles.Length;
        }
        int vertexBufferID;
        int triangleBufferID;

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, triangleBufferID);
        }
        int triangleCount;

        public void Draw()
        {
            GL.DrawElements(BeginMode.Triangles, triangleCount * 3, DrawElementsType.UnsignedInt, 0);
        }
    }
}
