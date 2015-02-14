using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SpriteVertex
    {
        public SpriteVertex(float x, float y, float u, float v)
        {
            Position = new Vector2(x, y);
            TexCoord = new Vector2(u, v);
        }
        public Vector2 Position;
        public Vector2 TexCoord;
    }

    class Sprite
    {
        public static ShaderProgram shaderProgram = new ShaderProgram(@"Sprite\Sprite.vert", @"Sprite\Sprite.frag");
        public static VertexBuffer<SpriteVertex> vertexBuffer = new VertexBuffer<SpriteVertex>();
        static Sprite()
        {
            SpriteVertex[] vertices = new SpriteVertex[]
            {
                new SpriteVertex(-0.5f, -0.5f, 0.0f, 0.0f),
                new SpriteVertex(+0.5f, -0.5f, 1.0f, 0.0f),
                new SpriteVertex(+0.5f, +0.5f, 1.0f, 1.0f),
                new SpriteVertex(-0.5f, -0.5f, 0.0f, 0.0f),
                new SpriteVertex(+0.5f, +0.5f, 1.0f, 1.0f),
                new SpriteVertex(-0.5f, +0.5f, 0.0f, 1.0f)
            };
            vertexBuffer.Update(vertices);

            int positionLocation = GL.GetAttribLocation(shaderProgram.ID, "Position");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(Vertex)), 0);

            int texCoordLocation = GL.GetAttribLocation(shaderProgram.ID, "TexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(Vertex)), 2 * 4);

            viewMatrixLocation = GL.GetUniformLocation(shaderProgram.ID, "ViewMatrix");
            float screenHeight = 768;
            float screenWidth = screenHeight / Viewer.Instance.Height * Viewer.Instance.Width;
            Matrix4 viewMatrix = Matrix4.CreateOrthographicOffCenter(0, screenWidth, 0, screenHeight, -1, 1);
            shaderProgram.SetMatrix(viewMatrixLocation, viewMatrix);

            modelMatrixLocation = GL.GetUniformLocation(shaderProgram.ID, "ModelMatrix");

            Viewer.Instance.Resize += Instance_Resize;
        }
        static void Instance_Resize(object sender, System.EventArgs e)
        {
            float screenHeight = 768;
            float screenWidth = screenHeight / Viewer.Instance.Height * Viewer.Instance.Width;

            Matrix4 viewMatrix = Matrix4.CreateOrthographicOffCenter(0, screenWidth, 0, screenHeight, -1, 1);
            shaderProgram.SetMatrix(viewMatrixLocation, viewMatrix);
        }
        static int viewMatrixLocation;
        static int modelMatrixLocation;

        public static void Draw(ref Matrix4 modelMatrix)
        {
            shaderProgram.SetMatrix(modelMatrixLocation, modelMatrix);
            vertexBuffer.DrawTriangles();
        }
    }
}
