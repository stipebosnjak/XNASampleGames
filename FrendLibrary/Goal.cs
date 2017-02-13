using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FrendLibrary
{
    public class Goal
    {
        public enum GoalStatus
        {
            Active,
            Inactive,
            Completed,
            Failed,
            Stuck,
            None
        }

        public enum GoalType
        {
            Atomic,
            Composite
        }

        protected GoalStatus Status;
        protected GoalType Type;
        protected Frend.Classes.Character CharOwner;
       

        public Goal(Frend.Classes.Character character, GoalType type)
        {
            Type = type;
            CharOwner = pE;
            Status = GoalStatus.Inactive;
            StartTime = TimeSpan.Zero;
        }

        public virtual void Activate() { }
        public virtual GoalStatus Process() { return GoalStatus.None; }
        public virtual void Terminate() { }        
 
        #region Methods
 
      
 
        public void ActivateIfInactive()
        {
            if (IsInactive)
            {
                Activate();
            }
        }
 
        public void  ReactivateIfFailed()
        {
          if (HasFailed)
          {
             Status = GoalStatus.Inactive;
          }
        }
 
        public bool IsComplete
        {
            get { return Status == GoalStatus.Completed; }
        }
        public bool IsActive
        {
            get { return Status == GoalStatus.Active; }
        }

        public bool IsInactive
        {
            get { return Status == GoalStatus.Inactive; }
        }

        public bool HasFailed
        {
            get { return Status == GoalStatus.Failed; }
        }

        public bool HasStuck
        {
            get { return Status == GoalStatus.Stuck; }
        }

        public GoalType GoalTypeP
        {
            get { return Type; }
        }
        public string GoalTypeString
        {
            get { return Type.ToString(); }
        }

        public GoalStatus GoalStatusP
        {
            get { return Status; }
            set { Status = value; }
        }

        public string GetGoalTypeString()
        {
            return Type.ToString();
        }
 
    
        #endregion
    }


    
}
