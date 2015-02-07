using OpenTK;
using System;
using System.Collections.Generic;

namespace CuttingEdge
{
    public class Unit
    {
        static Texture texture = new Texture(@"Textures\Unit.png");

        public float Size = 24;
        public Vector4 Color = new Vector4(1, 1, 1, 1);

        public Vector3 Position
        {
            get
            {
                float x = HermiteInterpolate(Targets[0].X, Targets[1].X, Targets[2].X, Targets[3].X, mu);
                float y = HermiteInterpolate(Targets[0].Y, Targets[1].Y, Targets[2].Y, Targets[3].Y, mu);
                float z = HermiteInterpolate(Targets[0].Z, Targets[1].Z, Targets[2].Z, Targets[3].Z, mu);
                return new Vector3(x, y, z);
            }
        }

        public List<Vector3> Targets = new List<Vector3>();

        static Random random = new Random();
        public Unit()
        {
            Targets.Add(new Vector3(-40, 500, 0));
            Targets.Add(new Vector3(40, 500, 0));
            Targets.Add(new Vector3(400, 500, 0));
            Targets.Add(new Vector3(random.Next(1280), random.Next(720), 0));

            mu = 0.5f;

            Vector3 direction = Position - Targets[1];
            distance = direction.LengthFast;
            direction = Targets[2] - Position;
            distance += direction.LengthFast;

            Color.X *= 0.8f + 0.4f * ((float)random.NextDouble());
            Color.Y *= 0.8f + 0.4f * ((float)random.NextDouble());
            Color.Z *= 0.8f + 0.4f * ((float)random.NextDouble());
            //Color.W = 1;

            Size *= 0.8f + 0.4f * ((float)random.NextDouble());
        }

        float mu = 0;
        float time = 0;
        float distance = 0;
        public void Update(float elapsedTime)
        {
            time += elapsedTime * 100;
            if (time > distance)
            {
                time -= distance;
                Targets.RemoveAt(0);
                Targets.Add(new Vector3(random.Next(Viewer.Instance.Width), random.Next(Viewer.Instance.Height), 0));

                mu = 0.5f;

                Vector3 direction = Position - Targets[1];
                distance = direction.LengthFast;
                direction = Targets[2] - Position;
                distance += direction.LengthFast;
            }

            mu = time / distance;

            //if (Target.Equals(Position)) return;

            //Vector3 direction = Target - Position;

            ////direction.X += 100 * elapsedTime * ((float)random.NextDouble() - 0.5f);
            ////direction.Y += 100 * elapsedTime * ((float)random.NextDouble() - 0.5f);

            //float distance = direction.LengthFast;

            //direction.NormalizeFast();
            //direction *= (Speed * elapsedTime);
            //float moveDistance = direction.LengthFast;

            //if (distance > moveDistance)
            //{
            //    Position += direction;
            //}
            //else
            //{
            //    Position = Target;
            //}
        }

        //public bool IsAtTarget { get { return Target.Equals(Position); } }

        public void Draw()
        {
            Vector3 position = Position;
            Renderer.DrawTexture(texture, ref position, Size, ref Color);
        }



        /*
   Tension: 1 is high, 0 normal, -1 is low
   Bias: 0 is even,
         positive is towards first segment,
         negative towards the other
*/
        float HermiteInterpolate(
           float p0, float p1,
           float p2, float p3,
           float mu,
           float tension = 0.5f,
           float bias = 0)
        {
            float m0, m1, mu2, mu3;
            float a0, a1, a2, a3;

            mu2 = mu * mu;
            mu3 = mu2 * mu;
            m0 = (p1 - p0) * (1 + bias) * (1 - tension) / 2;
            m0 += (p2 - p1) * (1 - bias) * (1 - tension) / 2;
            m1 = (p2 - p1) * (1 + bias) * (1 - tension) / 2;
            m1 += (p3 - p2) * (1 - bias) * (1 - tension) / 2;
            a0 = 2 * mu3 - 3 * mu2 + 1;
            a1 = mu3 - 2 * mu2 + mu;
            a2 = mu3 - mu2;
            a3 = -2 * mu3 + 3 * mu2;

            return (a0 * p1 + a1 * m0 + a2 * m1 + a3 * p2);
        }
    }
}
