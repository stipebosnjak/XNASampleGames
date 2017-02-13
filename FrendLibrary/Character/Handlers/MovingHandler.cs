namespace Frend.Character.Handlers
{

   

    class MovingHandler
    { 
        #region Enums
        public enum State
        {
            Dance,
            Wander,
            MovingFromWall,
            Stoped
        }
        #endregion

        #region Constants   
        private const int MAX_SPEED_STARTING = 5;
        private const int SPEED_MULTIPLIER = 3;
        
        #endregion

        #region Fields

        private float _maxSpeed,_speedMultiplier;
        
        #endregion

        #region Properties

        #endregion

        #region Constructors
        public MovingHandler()
        {
              _maxSpeed = MAX_SPEED_STARTING;
             _speedMultiplier = SPEED_MULTIPLIER;
        }
        #endregion

        #region Methods

        #endregion
    }
}