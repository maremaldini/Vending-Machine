using System;

namespace iQuest.VendingMachine.Exceptions
{
	public class CancelationException : Exception
	{
		private const string DefaulMessage = "Cancelation!";

		public CancelationException():base(DefaulMessage)
		{

		}
	}
}