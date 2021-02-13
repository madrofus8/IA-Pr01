using UnityEngine;
using Steerings;

namespace FSM
{
    public class FSM_SHEEP_EatOrWander : FiniteStateMachine
    {
        public enum State {INITIAL, FLOCKING, GOING_TO_EAT, EATING};

        public State currentState = State.INITIAL;

        private FSM_SHEEP_FlockWandering flockWander;
        private Arrive arrive;
		private SHEEP_Blackboard blackboard;
		public float elapsedTime = 0f;

       void Awake()
       {
           currentState = State.INITIAL;
       }
        void Start ()
		{
            flockWander = GetComponent<FSM_SHEEP_FlockWandering>();
            arrive = GetComponent<Arrive>();
            blackboard = GetComponent<SHEEP_Blackboard>();
            arrive.enabled = false;
            flockWander.enabled = false;
		}

		public override void Exit () {
			arrive.enabled = false;
            flockWander.enabled = false;
			base.Exit();
		}

		public override void ReEnter() {
            currentState = State.INITIAL;
            base.ReEnter();
		}

		void Update ()
		{
			switch (currentState) {
				case State.INITIAL:
					ChangeState (State.FLOCKING);
				    break;
				case State.FLOCKING:
					elapsedTime+=Time.deltaTime;
					if(elapsedTime >= blackboard.timeToBeingHungry){
						elapsedTime = 0;
						if(Random.Range(0,2) == 1){
							ChangeState(State.GOING_TO_EAT);
                    	    break;
                        }					
                    }
                	break;
            	case State.GOING_TO_EAT:
					if((transform.position - blackboard.Eater.transform.position).magnitude <= blackboard.eaterReachedRadius){
						ChangeState(State.EATING);
                	    break;
					}
                	break;
                case State.EATING:
					elapsedTime+=Time.deltaTime;
					if(elapsedTime >= blackboard.timeToEat){
						ChangeState(State.FLOCKING);
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
				case State.FLOCKING:
					flockWander.Exit();
                	break;
            	case State.GOING_TO_EAT:
					arrive.enabled = false;
                	break;
                case State.EATING:
					flockWander.Exit();
					arrive.enabled = false;
                	break;
			}

			// enter logic
			switch (newState) {
				case State.FLOCKING:
					elapsedTime = 0;
					flockWander.ReEnter();
                	break;
            	case State.GOING_TO_EAT:
					elapsedTime = 0;
					arrive.enabled = true;
                    arrive.target = blackboard.Eater;
                	break;
                case State.EATING:
					elapsedTime = 0;
                	break;
			}

			currentState = newState;
		}
    }
}
