using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
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

        Vector3 targetNode = new Vector3(600, 300, 0);
        Vector3 target = new Vector3(600, 300, 0);

        float TargetRotation;
        Vector3 direction2;

        float DirectionToAngle(Vector3 direction)
        {
            direction.NormalizeFast();
            float angle = (float)Math.Atan2(direction.Y, direction.X);
            return angle;
        }
        Vector3 AngleToDirection(float angle)
        {
            Vector3 direction = new Vector3(
            (float)Math.Cos(angle), (float)Math.Sin(angle), 0);
            return direction;
        }

        Vector3[] locations = new Vector3[4];

        float t = (float)random.NextDouble() * 8;
        public void Update(float elapsedTime)
        {
            t+= elapsedTime;
            if (random.Next(100) == 0)
            {
                Vector3 randomDirection = new Vector3(((float)random.NextDouble()) - 0.5f, ((float)random.NextDouble()) - 0.5f, 0);
                randomDirection.NormalizeFast();
                target = targetNode + randomDirection * 400;
            }
            direction2 = target - Position;
            direction2.NormalizeFast();

            TargetRotation = DirectionToAngle(direction2);
            TargetRotation += (float)Math.Sin(t*4) * 0.5f;
            //direction2 = AngleToDirection(Rotation);
            //if (TargetRotation - Rotation > MathHelper.PiOver2) TargetRotation -= MathHelper.Pi;
            //if (Rotation - TargetRotation > MathHelper.PiOver2) TargetRotation += MathHelper.Pi;

            Rotation = Rotation * (1 - elapsedTime*2) + TargetRotation * elapsedTime*2;

            Position += direction2 * elapsedTime * 20;
            Size = 32;


            float size = Size * 0.75f;
            //float breath = (float)Math.Sin(t*20);
            //if (breath > 0) size *= (1.0f + breath);


            Segment parent = this;
            Segment current = NextSegment;
            while (current != null)
            {
                size *= 0.95f;


                current.targetSize = size;
                current.Size = current.Size * (1.0f - elapsedTime) + current.targetSize * elapsedTime;


                Vector3 directionToParent = parent.Position - current.Position;
                float distanceToParent = directionToParent.LengthFast;
                directionToParent.NormalizeFast();
                current.Rotation = DirectionToAngle(directionToParent);

                float maxDistance = (parent.Size + current.Size) / 4;
                //float maxDistance = (current.Size + current.Size) / 2;
                if (distanceToParent > maxDistance)
                {
                    current.Position = parent.Position - directionToParent * maxDistance;
                }
                else
                {
                    current.Position = current.Position * (1 - elapsedTime*2) + parent.Position * elapsedTime*2;
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
        public float Size = 0;
        public float targetSize = 16;
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
