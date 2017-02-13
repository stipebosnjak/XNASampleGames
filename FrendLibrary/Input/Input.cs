using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Xna.Helpers;
namespace Frend.Input
{

    #region Enums

    #endregion

    class Input
    {
        #region Fields
        public delegate void FiredEvent(object sender);

        // Instances of delegate event.
        public event FiredEvent OnMouseOver;
        public FiredEvent OnMouseOut;
        public FiredEvent OnMouseClick; 
       
        #endregion

        #region Properties
        
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion
    }
}