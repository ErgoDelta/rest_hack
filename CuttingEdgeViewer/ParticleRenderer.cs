using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    public class ParticleRenderer
    {
        const ushort maxCount = ushort.MaxValue;

        Particle[] particles = new Particle[maxCount];
        ushort Count = 0;

        static Texture texture = new Texture(@"Textures\Particle.png");
        ShaderProgram shaderProgram;
        Random random = new Random();
        int vertexStride = Marshal.SizeOf(typeof(Particle));
        public ParticleRenderer()
        {
            for (int p = 0; p < 320; p++)
            {
                ushort parent = Count++;
                particles[parent].Position = new Vector4(0, 0, 0, random.Next(1000) / 1000.0f * 0.5f);
                particles[parent].parent = ushort.MaxValue;

                for (ushort c = 0; c < 200; c++)
                {
                    ushort c2 = (ushort)(c + Count);
                    //particles[c2].Position = new Vector4(0, 0, 0, random.Next(1000) / 1000.0f * 0.5f);
                    particles[c2].Position = new Vector4(0, 0, 0, 1);
                    particles[c2].parent = parent;
                    //particles[parent].child = c2;
                    parent = c2;
                    Count++;
                }
            }

            //for (int i = 0; i < maxCount; i++)
            //{
            //    particles[i].Position = new Vector4(random.Next(1000) / 1000.0f, random.Next(1000) / 1000.0f, 0, random.Next(1000) / 1000.0f);
            //    particles[i].Position = new Vector4(0, 0, 0, random.Next(1000) / 1000.0f * 0.5f);
            //    //particles[i].Color = new Vector4(1, 1, 1, 1);
            //    Count++;
            //}

            GL.GenBuffers(1, out vertexBufferID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferID);
            GL.BufferData<Particle>(BufferTarget.ArrayBuffer, (IntPtr)(Count * vertexStride), particles, BufferUsageHint.StaticDraw);

            shaderProgram = new ShaderProgram(@"ParticleRenderer\Particle.vert", @"ParticleRenderer\Particle.frag");
            shaderProgram.UseProgram();

            int textureSamplerLocation = GL.GetUniformLocation(shaderProgram.ID, "tex");
            GL.ProgramUniform1(shaderProgram.ID, textureSamplerLocation, 0);

            int positionLocation = GL.GetAttribLocation(shaderProgram.ID, "position");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 4, VertexAttribPointerType.Float, false, vertexStride, 0);
        }
        int vertexBufferID;

        public void Draw(float elapsedTime)
        {
            shaderProgram.UseProgram();
            texture.Bind();

            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferID);
            float size = 0;
            for (int i = 0; i < Count; i++)
            {
                if (particles[i].parent == ushort.MaxValue)
                {
                    Vector4 r = elapsedTime * new Vector4((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f, 0, 0);
                    particles[i].Position = particles[i].Position + r;
                    size = 1;
                    particles[i].Position.W = size;
                }
                else
                {
                    //Vector2 xy = particles[particles[i].parent].Position.Xy - particles[i].Position.Xy;
                    //float distance = xy.LengthFast;

                    //if (distance > 0.05f)
                    //{
                    //    particles[i].Position.Xy = particles[particles[i].parent].Position.Xy - xy * 0.05f;
                    //}
                    //else
                    {
                        float e = elapsedTime * 10f;
                        particles[i].Position = (particles[i].Position + e * particles[particles[i].parent].Position) / (1.0f + e);
                    }
                    size *= 0.99f;
                    particles[i].Position.W = size;
                }
            }
            GL.BufferSubData<Particle>(BufferTarget.ArrayBuffer, IntPtr.Zero, (IntPtr)(Count * vertexStride), particles);

            //glPointSize(whatever);              //specify size of points in pixels
            //glDrawArrays(GL_POINTS, 0, count);  //draw the points

            GL.Enable(EnableCap.PointSprite);

            GL.PointSize(16);
            GL.DrawArrays(PrimitiveType.Points, 0, Count);
        }
    }
}
