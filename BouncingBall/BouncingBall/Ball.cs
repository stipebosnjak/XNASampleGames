using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BouncingBall
{
   public  class Ball
   {
       private Texture2D _texture;
       private Vector2 _velocity,_position,_acceleration,_deaccelaration;
       private float _scale, _rotation,_mass,_fDeaccelration;


       private const float GRAVITY = 9.81f;
       public Texture2D Texture
       {
           get { return _texture; }
           set { _texture = value; }
       }

       public Vector2 Velocity
       {
           get { return _velocity; }
           set { _velocity = value; }
       }

       public Vector2 Position
       {
           get { return _position; }
           set { _position = value; }
       }

       public float Scale
       {
           get { return _scale; }
           set { _scale = value; }
       }

       public float Rotation
       {
           get { return _rotation; }
           set { _rotation = value; }
       }

       public Vector2 Acceleration
       {
           get { return _acceleration; }
           set { _acceleration = value; }
       }

       public Vector2 Deaccelaration
       {
           get { return _deaccelaration; }
           set { _deaccelaration = value; }
       }

       public float Mass
       {
           get { return _mass; }
           set { _mass = value; }
       }

      

       public Vector2 Origin
       {
           get {return new Vector2(Texture.Width/2,Texture.Height/2);}
       }

       public bool GoingUp { get; set; }
       public Ball()
       {
           Scale = 1f;
           Rotation = 0f;
           Mass = 20f;
           Position=new Vector2(Game1.SCREEN_WIDTH/2,Game1.SCREEN_HEIGHT/2);
           Deaccelaration = new Vector2(0,GRAVITY)/Mass;
           _fDeaccelration = GRAVITY/Mass;
           GoingUp = false;
       }
       public void Update()
       {
           //Apply gravity  Deaccelaration = new Vector2(0,GRAVITY)/Mass;
           Velocity += Deaccelaration;

           if (Position.Y  > Game1.SCREEN_HEIGHT)
           {
               //Position ball inside bounds
               Position = new Vector2(Position.X, Game1.SCREEN_HEIGHT - Texture.Height / 2);
               //Set upward velocity
               Velocity=new Vector2(0,-20f);
           }
           Position += Velocity;
       }

       private Vector2 Bounce()
       {

           Velocity += Deaccelaration;
           Velocity.Normalize();

           



           return Vector2.Zero;
       }

       public void Draw(SpriteBatch spriteBatch)
       {
           
           spriteBatch.Draw(Texture,Position,null,Color.White,Rotation,Origin,Scale,SpriteEffects.None,0f);
       }
   }
}
