using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Flee.Classes
{
    public class Camera2D
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation
        private bool _selfUpdating;
        private KeyboardState _oldState;
        public Camera2D()
        {
            _selfUpdating = false;
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
            _oldState=new KeyboardState();
        }

        public bool SelfUpdating
        {
            get { return _selfUpdating; }
            set { _selfUpdating = value; }
        }

        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < 0.1f) _zoom = 0.1f;
            } 
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

       
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            _transform = // Thanks to o KB o for this solution
                Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0))*
                Matrix.CreateRotationZ(Rotation)*
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1))*
                Matrix.CreateTranslation(new Vector3(Game1.SCREEN_WIDTH*0.5f, Game1.SCREEN_HEIGHT*0.5f, 0));
            return _transform;
        }

        public void CheckInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            var newState = Keyboard.GetState();
            foreach (var pressedKey in keyboardState.GetPressedKeys())
            {
                if (!_selfUpdating)
                {
                    if (pressedKey == Keys.A)
                        _pos = new Vector2(_pos.X - 1, _pos.Y);

                    if (pressedKey == Keys.S)
                        _pos = new Vector2(_pos.X, _pos.Y + 1);

                    if (pressedKey == Keys.D)
                        _pos = new Vector2(_pos.X + 1, _pos.Y);

                    if (pressedKey == Keys.W)
                        _pos = new Vector2(_pos.X, _pos.Y - 1);

                    if (pressedKey == Keys.F)
                        _zoom -= 0.01f;

                    if (pressedKey == Keys.G)
                        _zoom += 0.01f;

                    if (pressedKey == Keys.R)
                        Rotation += 0.01f;

                    if (pressedKey == Keys.T)
                        Rotation -= 0.01f;

                    if (pressedKey==Keys.E)
                    {
                        _zoom = 1.0f;
                        _rotation = 0.0f;
                        _pos=new Vector2(Game1.SCREEN_WIDTH/2,Game1.SCREEN_HEIGHT);
                    }
                }
            }
            if (newState.IsKeyDown(Keys.P) && _oldState.IsKeyUp(Keys.P))
            {
                _selfUpdating = !_selfUpdating;
            }
            _oldState = newState;
        }

    }
}