using System;
using Akka.Actor;

namespace Lesson3.Actors
{
    internal class ConsoleReaderActor : UntypedActor
    {
        private const string ExitCommand = "end";
        public const string StartCommand = "start";
        private readonly IActorRef validation;

        public ConsoleReaderActor( IActorRef validation )
        {
            this.validation = validation;
        }

        protected override void OnReceive( object message )
        {
            var readedLine = Console.ReadLine();
            if ( !string.IsNullOrEmpty( readedLine ) && readedLine.Equals( ExitCommand ) )
            {
                Context.System.Shutdown();
                return;
            }
            validation.Tell( readedLine );
        }
    }
}