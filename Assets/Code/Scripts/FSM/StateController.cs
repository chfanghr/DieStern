using System;

 namespace Code.Scripts.FSM
{
	public class StateController
	{
		public event Action OnEntered;
		public event Action OnExited;
		
		public void Enter()
		{
			OnEntered?.Invoke();
		}

		public void Exit()
		{
			OnExited?.Invoke();
		}
	}
}