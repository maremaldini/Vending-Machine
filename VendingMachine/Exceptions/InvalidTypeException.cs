using System;

namespace iQuest.VendingMachine.Exceptions
{
	public class InvalidTypeException : Exception
	{
		private const string DefaultMessage = "Invalid type introduced!";

		public InvalidTypeException() : base(DefaultMessage)
		{

		}
	}
}