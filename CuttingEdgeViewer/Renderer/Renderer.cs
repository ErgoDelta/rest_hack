using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace CuttingEdge
{
    public static class Renderer
    {
        static Renderer()
        {
            GL.Disable(EnableCap.CullFace);
            // Load our OpenGL GLSL Shader Program for drawing sprites
            shaderProgram = new ShaderProgram(@"Renderer\Texture.vert", @"Renderer\Texture.frag");
            shaderProgram.UseProgram();

            // Get our texture sampler location in the shader program and set it to location 0
            // Note: This is really not needed in out case as we are using only one texture
            int textureSamplerLocation = GL.GetUniformLocation(shaderProgram.ID, "TextureSampler");
            GL.ProgramUniform1(shaderProgram.ID, textureSamplerLocation, 0);

            // Get the shader program locations for our view matrix and model matrix
            viewMatrixLocation = GL.GetUniformLocation(shaderProgram.ID, "ViewMatrix");
            modelMatrixLocation = GL.GetUniformLocation(shaderProgram.ID, "ModelMatrix");

            colorLocation = GL.GetUniformLocation(shaderProgram.ID, "Color");

            // Create our geometry mesh
            mesh = new Mesh<Vertex, Triangle>(vertices, triangleBuffer);

            // Bind our mesh vertex attributes to the correct locations in the shader
            int positionLocation = GL.GetAttribLocation(shaderProgram.ID, "Position");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(Vertex)), 0);

            int texCoordLocation = GL.GetAttribLocation(shaderProgram.ID, "TexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, Marshal.SizeOf(typeof(Vertex)), 2 * 4);
        }
        static ShaderProgram shaderProgram;
        static int viewMatrixLocation;
        static int modelMatrixLocation;
        static int colorLocation;
        static Mesh<Vertex, Triangle> mesh;

        static readonly Triangle[] triangleBuffer = new Triangle[]
        {
            new Triangle(0,1,2),
            new Triangle(0,2,3)
        };

        static readonly Vertex[] vertices = new Vertex[]
        {
            new Vertex( -0.5f, -0.5f, 0.0f, 1.0f ),
            new Vertex( -0.5f, +0.5f, 0.0f, 0.0f ),
            new Vertex( +0.5f, +0.5f, 1.0f, 0.0f ),
            new Vertex( +0.5f, -0.5f, 1.0f, 1.0f),
        };

        public static void Resize(int width, int height)
        {
            GL.Viewport(0, 0, width, height);

            Matrix4 viewMatrix = Matrix4.CreateOrthographicOffCenter(0, width, 0, height, -1, 1);
            shaderProgram.SetMatrix(viewMatrixLocation, viewMatrix);
        }

        public static double Time;

        public static void DrawTexture(Texture texture, ref Vector3 position, float size, ref Vector4 color)
        {
            texture.Bind();
            shaderProgram.SetVector(colorLocation, ref color);
            shaderProgram.SetMatrix(modelMatrixLocation, ref position, size);
            mesh.Draw();
        }
    }
}
