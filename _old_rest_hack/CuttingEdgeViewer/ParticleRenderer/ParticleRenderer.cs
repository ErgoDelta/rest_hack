using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    public class ParticleRenderer
    {
        VertexBuffer<Particle> vertexBuffer;
        const ushort maxCount = ushort.MaxValue;

        Particle[] particles = new Particle[maxCount];
        ushort Count = 0;

        static Texture texture = new Texture(@"Textures\Particle.png");
        ShaderProgram shaderProgram;
        Random random = new Random();
        int vertexStride = Marshal.SizeOf(typeof(Particle));
        public ParticleRenderer()
        {
            for (int p = 0; p < 100; p++)
            {
                ushort parent = Count++;
                //particles[parent].Position = new Vector4(0, 0, 0, random.Next(1000) / 1000.0f * 0.5f);
                particles[parent].Position = new Vector4(random.Next(1000) / 1000.0f - 0.5f, random.Next(1000) / 1000.0f - 0.5f, 0, 1);
                particles[parent].Position = new Vector4(random.Next(1000) / 1000.0f - 0.5f, random.Next(1000) / 1000.0f - 0.5f, 0, 1);

                particles[parent].Position.Xyz *= 2;
                //particles[parent].parent = ushort.MaxValue;

                for (ushort c = 0; c < 100; c++)
                {
                    ushort c2 = (ushort)(c + Count);
                    particles[c2].Position = new Vector4(random.Next(1000) / 1000.0f - 0.5f, random.Next(1000) / 1000.0f - 0.5f, 0, random.Next(1000) / 1000.0f * 0.5f);
                    particles[c2].Position.Xyz *= 2;
                    //particles[c2].Position = new Vector4(random.Next(1000) / 1000.0f - 0.5f, random.Next(1000) / 1000.0f - 0.5f,0, 1);
                    //particles[c2].parent = parent;
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

            vertexBuffer = new VertexBuffer<Particle>(particles);
            vertexBuffer.Update(particles, Count);
            vertexBuffer.Bind();

            shaderProgram = new ShaderProgram(@"ParticleRenderer\Particle.vert", @"ParticleRenderer\Particle.frag");
            shaderProgram.UseProgram();

            int textureSamplerLocation = GL.GetUniformLocation(shaderProgram.ID, "tex");
            GL.ProgramUniform1(shaderProgram.ID, textureSamplerLocation, 0);

            int positionLocation = GL.GetAttribLocation(shaderProgram.ID, "position");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 4, VertexAttribPointerType.Float, false, vertexStride, 0);
        }

        public void Draw(float elapsedTime)
        {
            float size = 0;
            for (int i = 0; i < Count; i++)
            {
                //if (particles[i].parent == ushort.MaxValue)
                {
                    Vector4 r = elapsedTime / 16 * new Vector4((float)random.NextDouble() - 0.5f, (float)random.NextDouble() - 0.5f, 0, 0);
                    particles[i].Position.Xyz = particles[i].Position.Xyz + r.Xyz;
                    //size = 0.2f;
                    //particles[i].Position.W = size;
                }
                //else
                //{
                //    //Vector2 xy = particles[particles[i].parent].Position.Xy - particles[i].Position.Xy;
                //    //float distance = xy.LengthFast;

                //    //if (distance > 0.05f)
                //    //{
                //    //    particles[i].Position.Xy = particles[particles[i].parent].Position.Xy - xy * 0.05f;
                //    //}
                //    //else
                //    {
                //        float e = elapsedTime * 10f;
                //        particles[i].Position = (particles[i].Position + e * particles[particles[i].parent].Position) / (1.0f + e);
                //    }
                //    size *= 0.95f;
                //    particles[i].Position.W = size;
                //}
            }
            shaderProgram.UseProgram();
            texture.Bind();
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
            vertexBuffer.Update(particles, Count);
            int positionLocation = GL.GetAttribLocation(shaderProgram.ID, "position");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 4, VertexAttribPointerType.Float, false, vertexStride, 0);


            GL.Enable(EnableCap.PointSprite);
            GL.PointSize(3);
            //GL.DisableVertexAttribArray();
            vertexBuffer.DrawPoints();
            GL.Disable(EnableCap.PointSprite);
        }
    }
}
