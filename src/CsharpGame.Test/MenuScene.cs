using CsharpGame.Engine.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpGame.Test
{
    public class MenuScene : Scene
    {
        public MenuScene(Engine.Base.Engine engine) : base("Menu", engine)
        {
        }

        private Sprite texture { get; set; }
        private Sprite texture1 { get; set; }
        private GameObject GObject { get; set; }
        private GameObject GObject1 { get; set; }
        private Layer Middle { get; set; }
        private Layer Top { get; set; }
        public bool  Done { get; set; }

        public override bool OnCreate()
        {
            Middle = new Layer("Middle");
            Top = new Layer("Top");
            texture = new Sprite(30, 100);
            texture.LoadFromFile(CsharpGame.Test.Properties.Resources.men_1);
            texture1 = new Sprite(191, 63);
            texture1.LoadFromFile(CsharpGame.Test.Properties.Resources.charachter);
            GObject = new GameObject(new PointF(20, 15), texture);
            GObject1 = new GameObject(new PointF(10, 10), texture1);
            Middle.RegisterGameObject(GObject);
            Top.RegisterGameObject(GObject1);
            RegisterLayer(Top);
            RegisterLayer(Middle);
            return base.OnCreate();
        }

        public override bool OnUpdate(double ElapsedTime)
        {
            Engine.Drawer.String("This is a menu", "Arial", 48, Color.Red, new PointF(100, 100));
            Engine.Drawer.String("Clique Space to go out of menu", "Arial", 18, Color.Red, new PointF(100, 160));

            if (Engine.KeyClicked(System.Windows.Forms.Keys.Space))
            {
                this.Engine.GoToScene(this.Engine.Scenes[0]);
            }
            return Done;
        }
    }
}
