#region

using System;
using System.Collections.Generic;
using Humanity.Actors;
using Humanity.Goap.GoalEvaluators;
using Xna.Helpers;
using Xna.Helpers.Goap;

#endregion

namespace Humanity.Goap.Goals
{
    public class GoalThink : GoalComposite<Human>
    {
        private readonly List<GoalEvaluator<Human>> _goalEvaluators;

        public GoalThink(Human character, GoalType type)
            : base(character, type)
        {
            //these biases could be loaded in from a script on a per bot basis
            //but for now we'll just give them some random values
            const float lowRangeOfBias = 0.5f;
            const float highRangeOfBias = 1.5f;
            
            float wanderBias = Generators.RandomNumber(lowRangeOfBias, highRangeOfBias);
            float hungerBias = Generators.RandomNumber(lowRangeOfBias, highRangeOfBias);

            _goalEvaluators = new List<GoalEvaluator<Human>>();
            

            //create the evaluator objects
            _goalEvaluators.Add(new GoalEvaluatorWander(wanderBias));
            _goalEvaluators.Add(new GoalEvaluatorHunger(hungerBias));

        }

        public void Dispose()
        {
            _goalEvaluators.Clear();
        }

        public override void Activate()
        {
            //if (!CharOwner.po isPossessed())  todo:for human
            //{
                Arbitrate();
            //}

            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            ActivateIfInactive();

            var subgoalStatus = ProcessSubGoals();

            if (subgoalStatus ==GoalStatus.Completed  || subgoalStatus ==GoalStatus.Failed)
            {
                Status = GoalStatus.Inactive;
            }

            return Status;
        }

        public void Arbitrate()
        {
            float best = 0;
            GoalEvaluator<Human> mostDesirable = null;
             
            //iterate through all the evaluators to see which produces the highest score

            foreach (GoalEvaluator<Human> goalEvaluator in _goalEvaluators)
            {
                float dessirability = goalEvaluator.CalculateDesirability(CharOwner);

                if (dessirability>=best)
                {
                    best = dessirability;
                    mostDesirable = goalEvaluator;
                }
            }
             //Debug.Assert(MostDesirable != null && "<Goal_Think::Arbitrate>: no evaluator selected");

            mostDesirable.SetGoal(CharOwner);
        }


        public bool NotPresent(Type goalType)
        {
            if (SubGoals.Count!=0)
            {
                System.Type goalType1 = SubGoals[0].GetType();
                var result= goalType1 ==  goalType;
                return !result;
            }

            return true;
        }
        public void AddGoalWander()
        {
            if (NotPresent(typeof(GoalWander)))
            {
                  SubGoals.Clear();
                  SubGoals.Add(new GoalWander(CharOwner,GoalType.Atomic));   
            }
        
        }
        public void AddGoalFeed()
        {
            if (NotPresent(typeof(GoalFeed)))
            {
                SubGoals.Clear();
                SubGoals.Add(new GoalFeed(CharOwner,GoalType.Atomic));
            }
            
        }
        //public void AddGoal_Explore()
        //{
        //    if (notPresent(goal_explore))
        //    {
        //        RemoveAllSubgoals();
        //        AddSubgoal(new Goal_Explore(m_pOwner));
        //    }
        //}
        //public void AddGoal_GetItem(uint ItemType)
        //{
        //    if (notPresent(ItemTypeToGoalType(ItemType)))
        //    {
        //        RemoveAllSubgoals();
        //        AddSubgoal(new Goal_GetItem(m_pOwner, ItemType));
        //    }
        //}
        //public void AddGoal_AttackTarget()
        //{
        //    if (notPresent(goal_attack_target))
        //    {
        //        RemoveAllSubgoals();
        //        AddSubgoal(new Goal_AttackTarget(m_pOwner));
        //    }
        //}
        //public void QueueGoal_MoveToPosition(Vector2D pos)
        //{
        //    m_SubGoals.push_back(new Goal_MoveToPosition(m_pOwner, pos));
        //}
    }
}