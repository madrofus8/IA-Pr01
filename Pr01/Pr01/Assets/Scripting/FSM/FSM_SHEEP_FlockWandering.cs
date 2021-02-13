using UnityEngine;
using Steerings;

namespace FSM
{
    [RequireComponent(typeof(SHEEP_Blackboard))]
    [RequireComponent(typeof(FlockingAround))]

    public class FSM_SHEEP_FlockWandering : FiniteStateMachine
    {
        public enum State {INITIAL, GOING_TO_A, GOING_TO_B, GOING_TO_C};

        public State currentState = State.INITIAL;
        private State lastState = State.INITIAL; 

        private FlockingAround flockWander;
		private SHEEP_Blackboard blackboard;
		public float elapsedTime = 0f;

       void Awake()
       {
           currentState = State.INITIAL;
            flockWander = GetComponent<FlockingAround>();
            blackboard = GetComponent<SHEEP_Blackboard>();
            flockWander.enabled = false;
       }
        void Start ()
		{
            flockWander = GetComponent<FlockingAround>();
            blackboard = GetComponent<SHEEP_Blackboard>();
            flockWander.enabled = false;
		}

		public override void Exit () {
			flockWander.enabled = false;
			base.Exit();
		}

		public override void ReEnter() {
            currentState = State.INITIAL;
            ChangeState(lastState);
            base.ReEnter();
		}

		void Update ()
		{
			switch (currentState) {
				case State.INITIAL:
					ChangeState (State.GOING_TO_A);
				    break;
				case State.GOING_TO_A:
					elapsedTime+=Time.deltaTime;
					if(elapsedTime >= blackboard.intervalBetweenOuts){
						elapsedTime = 0;
						if(flockWander.seekWeight < 1)
							flockWander.SetSeekWeight(flockWander.seekWeight + blackboard.seekIncrement);
					}
					if((transform.position - blackboard.locationA.transform.position).magnitude < blackboard.locationReachedRadius){
						ChangeState(State.GOING_TO_B);
                	    break;
					}
                	break;
            	case State.GOING_TO_B:
					elapsedTime+=Time.deltaTime;
					if(elapsedTime >= blackboard.intervalBetweenOuts){
						if(flockWander.seekWeight < 1)
							flockWander.SetSeekWeight(flockWander.seekWeight + blackboard.seekIncrement);
					}
					if((transform.position - blackboard.locationB.transform.position).magnitude < blackboard.locationReachedRadius){
						ChangeState(State.GOING_TO_C);
                	    break;
					}
                	break;
                case State.GOING_TO_C:
					elapsedTime+=Time.deltaTime;
					if(elapsedTime >= blackboard.intervalBetweenOuts){
						if(flockWander.seekWeight < 1)
							flockWander.SetSeekWeight(flockWander.seekWeight + blackboard.seekIncrement);
					}
					if((transform.position - blackboard.locationC.transform.position).magnitude < blackboard.locationReachedRadius){
						ChangeState(State.GOING_TO_A);
                	    break;
					}
                	break;
			}   
	    }

		void ChangeState (State newState) {
			// exit logic
			switch (currentState) {
				case State.INITIAL:
					flockWander.enabled = false;
                	break;
				case State.GOING_TO_A:
					flockWander.enabled = false;
                	break;
            	case State.GOING_TO_B:
					flockWander.enabled = false;
                	break;
                case State.GOING_TO_C:
					flockWander.enabled = false;
                	break;
			}

			// enter logic
			switch (newState) {
				case State.GOING_TO_A:
                    lastState = newState;
					elapsedTime = 0;
					flockWander.enabled = true;
					flockWander.attractor = blackboard.locationA;
					flockWander.SetSeekWeight(blackboard.initialSeekWeight);
                	break;
            	case State.GOING_TO_B:
                    lastState = newState;
					elapsedTime = 0;
					flockWander.enabled = true;
					flockWander.attractor = blackboard.locationB;
					flockWander.SetSeekWeight(blackboard.initialSeekWeight); 
                	break;
                case State.GOING_TO_C:
                    lastState = newState;
					elapsedTime = 0;
					flockWander.enabled = true;
					flockWander.attractor = blackboard.locationC;
					flockWander.SetSeekWeight(blackboard.initialSeekWeight); 
                	break;
			}

			currentState = newState;
		}
    }
}
