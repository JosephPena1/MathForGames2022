using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Sprite
    {
        private Texture2D _texture;

        public int Width
        {
            get
            {
                return _texture.width;
            }
            set
            {
                _texture.width = value;
            }
        }

        public int Height
        {
            get
            {
                return _texture.height;
            }
            set
            {
                _texture.height = value;
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public Sprite(string path)
        {
            _texture = Raylib.LoadTexture(path);
        }

        public void Draw(Matrix3 transform)
        {
            float xMagnitude = (float)Math.Round(((Vector2)(transform.m11, transform.m21)).Magnitude);
            float yMagnitude = (float)Math.Round(((Vector2)(transform.m12, transform.m22)).Magnitude);
            Width = (int)xMagnitude;
            Height = (int)yMagnitude;

            System.Numerics.Vector2 pos = new System.Numerics.Vector2(transform.m13, transform.m23);
            System.Numerics.Vector2 forward = new System.Numerics.Vector2(transform.m11, transform.m21);
            System.Numerics.Vector2 up = new System.Numerics.Vector2(transform.m12, transform.m22);
            System.Numerics.Vector2 offset = new System.Numerics.Vector2(Width/2, Height/2);
            pos -= (forward / forward.Length()) * (float)(Width / 2);
            pos -= (up/up.Length()) * (float)Height/2;
            Console.WriteLine(pos);
            float rotation = (float)Math.Atan2(transform.m21, transform.m11);
            Raylib.DrawTextureEx(_texture, pos * 32,
                (float)(rotation * 180.0f / Math.PI), 32, Color.WHITE);
        }
    }
}
