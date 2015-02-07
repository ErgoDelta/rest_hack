using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace CuttingEdge
{
    public class Viewer : GameWindow
    {
        static void Main()
        {
            using (Viewer viewer = new Viewer())
            {
                Instance = viewer;
                viewer.Run();
            }
        }
        public static Viewer Instance;

        public Viewer()
            : base(1280, 720, new GraphicsMode(new ColorFormat(32), 0, 0, 4), "Title", GameWindowFlags.Default, DisplayDevice.Default, 3, 0, GraphicsContextFlags.Default)
        {
            string jsonString = File.ReadAllText("map_one.json");
            Map map = SimpleJson.DeserializeObject<Map>(jsonString);
            map = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.4f, 0.8f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            SwapBuffers();

            for (int i = 0; i < 10000; i++)
            {
                units.Add(new Unit());
            }

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }
        List<Unit> units = new List<Unit>();

        protected override void OnUnload(EventArgs e)
        {
        }

        protected override void OnResize(EventArgs e)
        {
            Renderer.Resize(Width, Height);
            base.OnResize(e);
        }

        double time = 0;
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            time += e.Time;

            Renderer.Time = time;
            GL.Clear(ClearBufferMask.ColorBufferBit);
            foreach (Unit unit in units)
            {
                unit.Draw();
            }
            SwapBuffers();
        }

        Random random = new Random();
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            foreach (Unit unit in units)
            {
                //if (unit.IsAtTarget)
                //{
                //    unit.Target = new Vector3(random.Next(Width), random.Next(Height), 0);
                //}
                unit.Update((float)e.Time);
            }
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F:
                    break;

                case Key.C:
                    //var client = new RestClient("http://example.com");
                    //// client.Authenticator = new HttpBasicAuthenticator(username, password);

                    //var request = new RestRequest("resource/{id}", Method.POST);
                    //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
                    //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

                    //// easily add HTTP Headers
                    //request.AddHeader("header", "value");

                    //// add files to upload (works with compatible verbs)
                    //request.AddFile(path);

                    //// execute the request
                    //RestResponse response = client.Execute(request);
                    //var content = response.Content; // raw content as string

                    //// or automatically deserialize result
                    //// return content type is sniffed but can be explicitly set via RestClient.AddHandler();
                    //RestResponse<Person> response2 = client.Execute<Person>(request);
                    //var name = response2.Data.Name;

                    //// easy async support
                    //client.ExecuteAsync(request, response => {
                    //    Console.WriteLine(response.Content);
                    //});

                    //// async with deserialization
                    //var asyncHandle = client.ExecuteAsync<Person>(request, response => {
                    //    Console.WriteLine(response.Data.Name);
                    //});

                    //// abort the request on demand
                    //asyncHandle.Abort();
                    break;
            }
        }
    }
}
