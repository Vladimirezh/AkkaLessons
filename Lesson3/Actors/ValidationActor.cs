using System;
using Akka.Actor;

namespace Lesson3.Actors
{
    internal class ValidationActor : UntypedActor
    {
        private readonly IActorRef writer;

        public ValidationActor( IActorRef writer )
        {
            this.writer = writer;
        }

        protected override void OnReceive( object message )
        {
            var line = message as string;

            if ( string.IsNullOrEmpty( line ) )
                writer.Tell( new Messages.InputError( "Empty or null string" ) );
            else if ( line.StartsWith( "#", StringComparison.Ordinal ) )
                writer.Tell( new Messages.InputSuccess( "Thank you! Message was valid." ) );
            else
                writer.Tell( new Messages.ValidationError( "String doesn't start with #" ) );

            Sender.Tell( new Messages.ContinueProcessing() );
        }
    }
}