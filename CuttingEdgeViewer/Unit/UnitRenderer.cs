using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    class UnitRenderer
    {
        static Texture headTexture = new Texture(@"Textures/Unit.png");
        static Texture segementTexture = new Texture(@"Textures/Unit.png");

        public static List<Head> heads = new List<Head>();
        public static List<Segment> segments = new List<Segment>();

        public void Update(float elapsedTime)
        {
            foreach (Head head in heads)
            {
                head.Update(elapsedTime);
            }
        }

        public void Draw()
        {
            Sprite.shaderProgram.UseProgram();
            Sprite.vertexBuffer.Bind();

            // TODO REMOVE
            {
                int positionLocation = GL.GetAttribLocation(Sprite.shaderProgram.ID, "Position");
                GL.EnableVertexAttribArray(positionLocation);
                GL.VertexAttribPointer(positionLocation, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(Vertex)), 0);

                int texCoordLocation = GL.GetAttribLocation(Sprite.shaderProgram.ID, "TexCoord");
                GL.EnableVertexAttribArray(texCoordLocation);
                GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(Vertex)), 2 * 4);
            }

            Matrix4 modelMatrix;
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);

            headTexture.Bind();
            foreach (Head head in heads)
            {
                head.GetModelMatrix(out modelMatrix);
                Sprite.Draw(ref modelMatrix);
            }

            segementTexture.Bind();
            foreach (Segment segment in segments)
            {
                segment.GetModelMatrix(out modelMatrix);
                Sprite.Draw(ref modelMatrix);
            }
        }
    }
}
