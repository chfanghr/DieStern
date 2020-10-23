using System;

namespace Code.Scripts.FSM
{
	public abstract class Transition<TState> where TState : IComparable
	{
		public TState FromState { get; private set; }
		public TState ToState { get; private set; }

		private readonly Func<bool> _testConditionFunc;

		public event Action OnComplete;

		protected Transition(TState from, TState to, Func<bool> testConditionFunction = null)
		{
			FromState = from;
			ToState = to;
			_testConditionFunc = testConditionFunction;
		}

		protected void Complete()
		{
			OnComplete?.Invoke();
		}

		public abstract void Begin();

		public bool TestCondition()
		{
			return _testConditionFunc == null || _testConditionFunc();
		}
	}

	public class DefaultStateTransition<TState> : Transition<TState> where TState : IComparable
	{
		public DefaultStateTransition(TState from, TState to, Func<bool> testConditionFunction = null) 
			: base(from, to, testConditionFunction)
		{
		}

		public override void Begin()
		{
			Complete();
		}
	}
}