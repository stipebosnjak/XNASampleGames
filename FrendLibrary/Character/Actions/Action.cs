using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xna.Helpers;
namespace Frend.Character.Actions
{

    #region Enums

    #endregion

    class Action
    {
        #region Fields

        public  Collections.DictionaryV2<string, Action> DicActions;
        private Character.Actor _mainChar;
        #endregion

        #region Properties

        protected Actor MainChar
        {
            get { return _mainChar; }
            set { _mainChar = value; }
        }

        #endregion

        #region Constructors
       
        public Action(Actor actor)
        {
            DicActions = new Collections.DictionaryV2<string, Action>();
            _mainChar = actor;
        }

        protected Action()
        {
            
        }

        #endregion

        #region Methods
        protected virtual void Do()
        {
            
        }
        #endregion
    }
}