using System;

namespace iQuest.VendingMachine.Exceptions
{
	public class InvalidIdException : Exception
	{
		private const string DefaultMessage = "Invalid ID introduced!";

		public InvalidIdException() : base(DefaultMessage)
		{

		}
	}
}