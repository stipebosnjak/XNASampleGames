using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frend.Classes;

namespace FrendLibrary
{
    class GoalComposite :Goal
    {

        protected List<Goal> SubGoals;

        public GoalComposite(Character character, GoalType type)
            : base(character, type)
        {
            SubGoals=new List<Goal>();

        }
        public virtual void Activate()
        {
            base.Activate();
        }
        public virtual void AddSubgoal(Goal g)
        {
            SubGoals.Add(g);
           
        }
        public virtual Goal.GoalStatus Process()
        {
            return base.Process();
        }
        public virtual void Terminate()
        {
            base.Terminate();
        }
        public GoalStatus ProcessSubGoals()
        {
            //remove all completed and failed goals from the front of the subgoal list
            while (SubGoals.Count != 0 && (SubGoals[0].IsComplete || SubGoals[0].HasFailed))
            {
                SubGoals[0].Terminate();
              //  SubGoals[0] = null;
                SubGoals.RemoveAt(0);
                
            }

            //if any subgoals remain, process the one at the front of the list
            if (SubGoals.Count!=0)
            {
                //grab the status of the frontmost subgoal
               
                SubGoals[0].Process();
                GoalStatus goalStatus = SubGoals[0].GoalStatusP;
                //we have to test for the special case where the frontmost subgoal
                //reports "completed" and the subgoal list contains additional goals.
                //When this is the case, to ensure the parent keeps processing its
                //subgoal list,the "active" status is returned
                if (goalStatus ==GoalStatus.Completed && SubGoals.Count>1)
                {
                    return GoalStatus.Active;
                }

                return goalStatus;
            }

            //no more subgoals to process return "completed"
            else
            {
                return GoalStatus.Completed;
            }

        }

        public void RemoveAllSubgoals()
        {
            foreach (Goal subGoal in SubGoals)
            {
                subGoal.Terminate();
            }
            SubGoals.Clear();
        }
        
    }
}
