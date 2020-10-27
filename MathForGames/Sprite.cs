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

        public Sprite(Texture2D texture, int width, int height)
        {
            _texture = texture;
            _texture.width = width;
            _texture.height = height;
        }

        public Sprite(string path, int width, int height)
        {
            _texture = Raylib.LoadTexture(path);
            _texture.width = width;
            _texture.height = height;
        }

        public void Draw(Vector2 position, float rotation)
        {
            //Raylib.DrawTextureEx()
        }
    }
}
