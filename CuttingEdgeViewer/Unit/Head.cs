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
            Matrix4 modelMatrix;
            GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);


            headTexture.Bind();
            foreach(Head head in heads)
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

    class Head : Segment
    {
        static Random random = new Random();
        public Head()
        {
            direction2 = new Vector3(((float)random.NextDouble()) - 0.5f, ((float)random.NextDouble()) - 0.5f, 0);
            direction2.NormalizeFast();
        }

        public void AddSegement(Segment segment)
        {
            Segment current = this;
            while (current.NextSegment != null) current = current.NextSegment;
            current.NextSegment = segment;
        }

        Vector3 direction2;

        public void Update(float elapsedTime)
        {
            if( random.Next(100) == 0)
            {
                direction2 = new Vector3(((float)random.NextDouble()) - 0.5f, ((float)random.NextDouble()) - 0.5f, 0);
                direction2.NormalizeFast();
            }

            Position += direction2 * elapsedTime * 20;
            Size = 8;

            float size = Size * 0.75f;
            Segment parent = this;
            Segment current = NextSegment;
            while (current != null)
            {
                size *= 0.95f;
                current.Size = size;
                Vector3 direction = current.Position - parent.Position;
                float distance = direction.LengthFast;
                if (distance > current.Size)
                {
                    current.Position = parent.Position + direction / distance * (current.Size);
                }

                parent = current;
                current = current.NextSegment;
            }
        }
    }

    class Segment
    {
        public Vector3 Position = new Vector3(400, 300, 0);
        public float Rotation = 0;
        public float Size = 16;
        public Segment NextSegment;
        public float Alpha = 0;

        public void GetModelMatrix(out Matrix4 modelMatrix)
        {
            Matrix4 rotateMatrix;
            Matrix4.CreateRotationZ(Rotation, out rotateMatrix);
            Matrix4 scaleMatrix;
            Matrix4.CreateScale(Size, out scaleMatrix);
            Matrix4 translateMatrix;
            Matrix4.CreateTranslation(ref Position, out translateMatrix);
            Matrix4 rotationScaleMatrix;
            Matrix4.Mult(ref rotateMatrix, ref scaleMatrix, out rotationScaleMatrix);
            Matrix4.Mult(ref rotationScaleMatrix, ref translateMatrix, out modelMatrix);
        }
    }
}
