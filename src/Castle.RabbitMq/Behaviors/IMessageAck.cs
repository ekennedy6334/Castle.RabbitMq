﻿namespace Castle.RabbitMq
{
	using System;

	public interface IMessageAck
	{
		///	<summary>
		///	Tells the queue	we have	successfully processed the message
		///	</summary>
		void Ack();

		///	<summary>
		///	Tells the queue	we have	NOT	processed the message for some reason.
		///	Optionally instruct	the	queue to 'requeue' the same	message	for	later processing.
		///	</summary>
		void Reject(bool requeue);
	}

	public class MessageAck	: IMessageAck
	{
		private	readonly Action	_ack;
		private	readonly Action<bool> _nack;
		private	volatile bool _done;

		public MessageAck(Action ack, Action<bool> nack)
		{
			_ack = ack;
			_nack =	nack;
		}

		public void	Ack()
		{
			if (_done) return;
			_done =	true;
			_ack();
		}
		public void	Reject(bool	requeue)
		{
			if (_done) return;
			_done =	true;
			_nack(requeue);
		}
	}
}