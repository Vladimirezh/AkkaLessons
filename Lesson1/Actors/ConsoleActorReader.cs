using System;
using Akka.Actor;

namespace Lesson1.Actors
{
    public class ConsoleActorReader : UntypedActor
    {
        private const string exitCommand = "end";
        private readonly IActorRef writer;

        public ConsoleActorReader( IActorRef writer )
        {
            this.writer = writer;
        }

        protected override void OnReceive( object message )
        {
            var readedLine = Console.ReadLine();
            if ( !string.IsNullOrEmpty( readedLine ) && string.Equals( readedLine, exitCommand, StringComparison.OrdinalIgnoreCase ) )
            {
                Context.System.Shutdown();
                return;
            }
            writer.Tell( new MessageClass( readedLine ) );
            Console.WriteLine( "OnReceive ended" );
            Self.Tell( "continue" );
        }
    }
}