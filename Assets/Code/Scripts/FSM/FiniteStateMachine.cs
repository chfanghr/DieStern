using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.FSM
{

	public class FiniteStateMachine<TState> where TState : IComparable
	{
		public TState CurrentState { get; private set; }
		
		public bool IsTransitioning { get { return _currentTransition != null; } }

		public string Name { get; set; }

		private Transition<TState> _currentTransition;
		private readonly Dictionary<TState, StateController> _states;
		private readonly Dictionary<TState, Dictionary<string, Transition<TState>>> _transitions;
		private bool _isInitialisingState;
		private string _stateMachineName;

		private event Action<TState> OnStateEnter;
		private event Action<TState> OnStateExit;
		private event Action<TState, TState> OnStateChange;
		
		public static FiniteStateMachine<TState> FromEnum()
		{
			if (!typeof(Enum).IsAssignableFrom(typeof(TState)))
			{
				throw new Exception("Cannot create finite");
			}

			var states = new List<TState>();
			foreach (TState value in Enum.GetValues(typeof(TState)))
			{
				states.Add(value);
			}

			return new FiniteStateMachine<TState>(states.ToArray());
		}
		
		public FiniteStateMachine(params TState[] states)
		{
			if (states.Length < 1) { throw new ArgumentException("A FiniteStateMachine needs at least 1 state", "states"); }

			_transitions = new Dictionary<TState, Dictionary<string, Transition<TState>>>();
			_states = new Dictionary<TState, StateController>();
			foreach (var value in states)
			{
				_states.Add(value, new StateController());
				_transitions.Add(value, new Dictionary<string, Transition<TState>>());
			}
		}
		
		public FiniteStateMachine<TState> AddTransition(TState from, TState to, string command, Transition<TState> transition = null)
		{
			if (!_states.ContainsKey(from)) { throw new ArgumentException("unknown state", "from"); }
			if (!_states.ContainsKey(to)) { throw new ArgumentException("unknown state", "to"); }

			// add the transition to the db (new it if it does not exist)
			_transitions[from][command] = transition ?? new DefaultStateTransition<TState>(from, to);

			return this;
		}
		
		public FiniteStateMachine<TState> AddTransition(TState from, TState to, string command, Func<bool> condition)
		{
			if (from == null) { throw new ArgumentNullException("state"); }
			if (to == null) { throw new ArgumentNullException("to"); }
			if (!_states.ContainsKey(from)) { throw new ArgumentException("unknown state", "from"); }
			if (!_states.ContainsKey(to)) { throw new ArgumentException("unknown state", "to"); }
			if (string.IsNullOrEmpty(command)) { throw new ArgumentException("command cannot be null or empty", "command"); }

			// add a default transition to the db
			_transitions[from][command] = new DefaultStateTransition<TState>(from, to, condition);

			return this;
		}

		public FiniteStateMachine<TState> OnEnter(TState state, Action handler)
		{
			if (state == null) { throw new ArgumentNullException("state"); }
			if (handler == null) { throw new ArgumentNullException("handler"); }
			if (!_states.ContainsKey(state)) { throw new ArgumentException("unknown state", "state"); }

			OnStateEnter += enteredState =>
			{
				if (enteredState.Equals(state))
				{
					handler();
				}
			};

			return this;
		}
		
		public FiniteStateMachine<TState> OnExit(TState state, Action handler)
		{
			if (state == null) { throw new ArgumentNullException("state"); }
			if (handler == null) { throw new ArgumentNullException("handler"); }
			if (!_states.ContainsKey(state)) { throw new ArgumentException("unknown state", "state"); }

			OnStateExit += exitedState =>
			{
				if (exitedState.Equals(state))
				{
					handler();
				}
			};
			return this;
		}
		
		public FiniteStateMachine<TState> OnChange(Action<TState, TState> handler)
		{
			if (handler == null) { throw new ArgumentNullException("handler"); }

			OnStateChange += handler;

			return this;
		}
		
		public FiniteStateMachine<TState> OnChange(TState from, TState to, Action handler)
		{
			if (from == null) { throw new ArgumentNullException("from"); }
			if (to == null) { throw new ArgumentNullException("to"); }
			if (!_states.ContainsKey(from)) { throw new ArgumentException("unknown state", "from"); }
			if (!_states.ContainsKey(to)) { throw new ArgumentException("unknown state", "to"); }
			if (handler == null) { throw new ArgumentNullException("handler"); }

			OnStateChange += (fromState, toState) =>
			{
				if (fromState.Equals(from) &&
					toState.Equals(to))
				{
					handler();
				}
			};

			return this;
		}
		
		public void Begin(TState firstState)
		{
			if (firstState == null) { throw new ArgumentNullException("firstState"); }
			if (!_states.ContainsKey(firstState)) { throw new ArgumentException("unknown state", "firstState"); }

			CurrentState = firstState;
		}
		
		public void IssueCommand(string command)
		{
			if (IsTransitioning)
				return;

			var transitionsForCurrentState = _transitions[CurrentState];
			if (transitionsForCurrentState.ContainsKey(command))
			{
				if (_isInitialisingState)
				{
					Debug.LogWarning("Do not call IssueCommand from OnStateChange and OnStateEnter handlers");
					return;
				}

				var transition = transitionsForCurrentState[command];

				if (transition.TestCondition())
				{
					transition.OnComplete += HandleTransitionComplete;
					_currentTransition = transition;
					if (OnStateExit != null)
					{
						OnStateExit(CurrentState);
					}
					transition.Begin();
				}
			}
		}

		private void HandleTransitionComplete()
		{
			_currentTransition.OnComplete -= HandleTransitionComplete;

			var previousState = CurrentState;
			CurrentState = _currentTransition.ToState;

			_currentTransition = null;

			_isInitialisingState = true;

			if (OnStateChange != null)
			{
				OnStateChange(previousState, CurrentState);
			}

			_states[CurrentState].Enter();
			if (OnStateEnter != null)
			{
				OnStateEnter(CurrentState);
			}

			_isInitialisingState = false;
		}
	}
}