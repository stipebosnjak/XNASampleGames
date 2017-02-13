using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Frend.Classes
{

    #region Enums

    #endregion

    public class PointOfInterest
    {
        #region Fields

        private string _name;
        private Vector2 _position;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        #endregion

        #region Constructors

        public PointOfInterest(string name, Vector2 position)
        {
            _name = name;
            _position = position;
        }

        #endregion

        #region Methods

        #endregion
    }
}