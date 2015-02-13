using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using System.Drawing;

namespace CuttingEdge
{
    public class Texture
    {
        public Texture(string fileName)
        {
            textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureParameterName.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapR, (int)TextureParameterName.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            using (Bitmap bitmap = Bitmap.FromFile(fileName) as Bitmap)
            {
                FixBitmap(bitmap);

                switch (bitmap.PixelFormat)
                {
                    case System.Drawing.Imaging.PixelFormat.Alpha:
                    case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                    case System.Drawing.Imaging.PixelFormat.PAlpha:
                        {
                            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Alpha);
                            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Alpha, bitmap.Width, bitmap.Height, 0, PixelFormat.Alpha, PixelType.UnsignedByte, bitmapData.Scan0);
                            bitmap.UnlockBits(bitmapData);
                        }
                        break;

                    case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
                    case System.Drawing.Imaging.PixelFormat.Format16bppRgb555:
                    case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                    case System.Drawing.Imaging.PixelFormat.Format4bppIndexed:
                    case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    case System.Drawing.Imaging.PixelFormat.Format48bppRgb:
                        {
                            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, bitmap.Width, bitmap.Height, 0, PixelFormat.Bgr, PixelType.UnsignedByte, bitmapData.Scan0);
                            bitmap.UnlockBits(bitmapData);
                        }
                        break;

                    case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                    case System.Drawing.Imaging.PixelFormat.Format64bppPArgb:
                    case System.Drawing.Imaging.PixelFormat.Indexed:
                    case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
                    case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                        {
                            System.Drawing.Imaging.BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
                            bitmap.UnlockBits(bitmapData);
                        }
                        break;

                    default:
                        Debug.Assert(false);
                        break;
                }
            }

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }
        int textureID;

        public void Bind()
        {
            if (boundTexture != this)
            {
                boundTexture = this;
                GL.ActiveTexture(TextureUnit.Texture0 + 0);
                GL.BindTexture(TextureTarget.Texture2D, textureID);
            }
        }
        static Texture boundTexture;

        public void FixBitmap(Bitmap bitmap)
        {
            Vector3 averageColor = Vector3.Zero;
            int count = 0;

            for( int x = 0 ; x < bitmap.Width ; x++ )
            {
                bool lastWasEmpty = true;
                for( int y = 0 ; y < bitmap.Height ; y++ )
                {
                    Color color = bitmap.GetPixel(x, y);

                    if (color.A == 0)
                    {
                        lastWasEmpty = true;
                    }
                    else
                    {
                        if (lastWasEmpty)
                        {
                            averageColor.X += color.R;
                            averageColor.Y += color.G;
                            averageColor.Z += color.B;
                            count++;
                        }
                        lastWasEmpty = false;
                    }
                }
            }

            Color avg = Color.FromArgb(0, (byte)(averageColor.X / count), (byte)(averageColor.Y / count), (byte)(averageColor.Z / count));

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    if (color.A == 0)
                    {
                        bitmap.SetPixel(x,y,avg);
                    }
                }
            }
        }
    }
}
