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
            _texture.width = (int)transform.m11;
            _texture.height = (int)transform.m22;
            float rotation = (float)Math.Atan2(transform.m21, transform.m11);
            Raylib.DrawTextureEx(_texture, new System.Numerics.Vector2(transform.m13 * 32, transform.m23 * 32),
                (float)(rotation * 180.0f / Math.PI), 32, Color.WHITE);
        }
    }
}
