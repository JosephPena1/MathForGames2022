using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{

    /// <summary>
    /// This is the base class for all objects that will 
    /// be moved or interacted with in the game
    /// </summary>
    class Actor
    {
        protected char _icon = ' ';
        protected Matrix3 _transform = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();
        protected Vector2 _velocity;
        protected ConsoleColor _color;
        protected Color _rayColor;
        protected Sprite _sprite;
        public bool Started { get; private set; }

        public Vector2 Forward
        {
            get 
            { 
                return new Vector2(_transform.m11, _transform.m21).Normalized;
            }
        }


        public Vector2 Position
        {
            get
            {
                return new Vector2(_transform.m13,_transform.m23);
            }
            set
            {
                _translation.m13 = value.X;
                _translation.m23 = value.Y;
            }
        }

        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
            }
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
        {
            _rayColor = Color.WHITE;
            _icon = icon;
            Position = new Vector2(x, y);
            _velocity = new Vector2();
            _color = color;
            _sprite = new Sprite(Raylib.LoadTexture("Images/player.png"));
        }


        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn to the console</param>
        public Actor(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : this(x,y,icon,color)
        {
            _rayColor = rayColor;
            _sprite = new Sprite(Raylib.LoadTexture("Images/player.png"));
        }

        

        public virtual void Start()
        {
            Started = true;
        }

        public void SetScale(Vector2 scale)
        {
            _scale.m11 = scale.X;
            _scale.m22 = scale.Y;
        }

        public void Scale(Vector2 scale)
        {
            if (scale.X != 0)
                _scale.m11 *= scale.X;
            if (scale.Y != 0)
                _scale.m22 *= scale.Y;
        }

        public void SetRotation(float radians)
        {
            _rotation.m11 = (float)Math.Cos(radians);
            _rotation.m12 = (float)Math.Sin(radians);
            _rotation.m21 = -(float)Math.Sin(radians);
            _rotation.m22 = (float)Math.Cos(radians);
        }

        /// <summary>
        /// Updates the actors forward vector to be
        /// the last direction it moved in
        /// </summary>
        private void UpdateFacing()
        {
            if (_velocity.Magnitude <= 0)
                return;

            //Forward = Velocity.Normalized;
        }

        public virtual void Update(float deltaTime)
        {
            _transform = Game.GetCurrentScene().World * _translation * _rotation *_scale;
            //Before the actor is moved, update the direction it's facing
            UpdateFacing();

            //Increase position by the current velocity
            Position += _velocity * deltaTime;
        }


        public virtual void Draw()
        {
            //Draws the actor and a line indicating it facing to the raylib window
            _sprite.Draw(_transform);
            Raylib.DrawLine(
                (int)(Position.X * 32),
                (int)(Position.Y * 32),
                (int)((Position.X + Forward.X) * 32),
                (int)((Position.Y + Forward.Y) * 32),
                Color.WHITE
            );

            //Changes the color of the console text to be this actors color
            Console.ForegroundColor = _color;
            
            //Reset console text color to be default color
            Console.ForegroundColor = Game.DefaultColor;
        }

        public virtual void Debug()
        {

        }

        public virtual void End()
        {
            Started = false;
        }

    }
}
