using System;

namespace iQuest.VendingMachine.Exceptions
{
	public class OutOfStockException : Exception
	{
		private const string DefaultMessage = "";

		public OutOfStockException() : base(DefaultMessage)
		{

		}
	}
}