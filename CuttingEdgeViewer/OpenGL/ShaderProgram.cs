using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using System.IO;

namespace CuttingEdge
{
    public class ShaderProgram
    {
        public ShaderProgram(string vertexShaderName, string fragmentShaderName)
        {
            string vertexShaderText = File.ReadAllText(vertexShaderName);
            int vertexShaderID = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderID, vertexShaderText);
            GL.CompileShader(vertexShaderID);
            int status;
            GL.GetShader(vertexShaderID, ShaderParameter.CompileStatus, out status);
            Debug.Assert(status == 1, "Shader Error: " + vertexShaderName, GL.GetShaderInfoLog(vertexShaderID));

            string fragmentShaderText = File.ReadAllText(fragmentShaderName);
            int fragmentShaderID = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderID, fragmentShaderText);
            GL.CompileShader(fragmentShaderID);
            GL.GetShader(fragmentShaderID, ShaderParameter.CompileStatus, out status);
            Debug.Assert(status == 1, "Shader Error: " + fragmentShaderName, GL.GetShaderInfoLog(vertexShaderID));

            ID = GL.CreateProgram();
            GL.AttachShader(ID, vertexShaderID);
            GL.AttachShader(ID, fragmentShaderID);
            GL.LinkProgram(ID);
            GL.GetProgram(ID, GetProgramParameterName.LinkStatus, out status);
            Debug.Assert(status == 1, "Shader Program Error: " + vertexShaderName + " " + fragmentShaderName, GL.GetProgramInfoLog(ID));

            UseProgram();
        }
        public readonly int ID;

        public void UseProgram()
        {
            GL.UseProgram(ID);
        }

        public void SetVector(int matrixLocation, ref Vector4 vector)
        {
            GL.ProgramUniform4(ID, matrixLocation, vector.X, vector.Y, vector.Z, vector.W);
        }

        public void SetMatrix(int matrixLocation, ref Vector3 position, float scale)
        {
            Matrix4 scaleMatrix;
            Matrix4.CreateScale(scale, out scaleMatrix);
            Matrix4 translateMatrix;
            Matrix4.CreateTranslation(ref position, out translateMatrix);
            Matrix4 matrix;
            Matrix4.Mult(ref scaleMatrix, ref translateMatrix, out matrix);
            unsafe
            {
                GL.ProgramUniformMatrix4(ID, matrixLocation, 1, false, &matrix.Row0.X);
            }
        }

        public void SetMatrix(int matrixLocation, Matrix4 matrix)
        {
            unsafe
            {
                GL.ProgramUniformMatrix4(ID, matrixLocation, 1, false, &matrix.Row0.X);
            }
        }
    }
}
