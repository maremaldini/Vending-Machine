using System;
namespace iQuest.VendingMachine.Exceptions
{
	public class TooBigMoneyException:Exception
	{
		private const string DefaultMessage = "No bigger than 1000$";

		public TooBigMoneyException() : base(DefaultMessage)
		{
		}
	}
}

