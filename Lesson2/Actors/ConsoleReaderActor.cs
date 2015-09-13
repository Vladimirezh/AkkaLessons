using System;
using Akka.Actor;

namespace Lesson2.Actors
{
    internal sealed class ConsoleReaderActor : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "end";
        private readonly IActorRef consoleWriter;

        public ConsoleReaderActor( IActorRef writer )
        {
            consoleWriter = writer;
        }

        protected override void OnReceive( object message )
        {
            if ( message.Equals( StartCommand ) )
            {
                Console.WriteLine( "Reader started." );
                Console.WriteLine( "Write message started with # and press enter." );
            }
            else if ( message is Messages.InputError )
                consoleWriter.Tell( message );
            ReadAndValidateMessage();
        }

        private void ReadAndValidateMessage()
        {
            var readedLine = Console.ReadLine();
            if ( string.IsNullOrEmpty( readedLine ) )
                Self.Tell( new Messages.InputError( "Empty or null string" ) );
            else if ( readedLine.Equals( ExitCommand ) )
                Context.System.Shutdown();
            else if ( readedLine.StartsWith( "#", StringComparison.Ordinal ) )
            {
                consoleWriter.Tell( new Messages.InputSuccess( "Thank you! Message was valid." ) );
                Self.Tell( new Messages.ContinueProcessing() );
            }
            else
                Self.Tell( new Messages.ValidationError( "String doesn't start with #" ) );
        }
    }
}