using System;
using Akka.Actor;

namespace Lesson1.Actors
{
    public class ConsoleWriterActor : UntypedActor
    {
        protected override void OnReceive( object messageObj )
        {
            var message = messageObj as MessageClass;
            if ( string.IsNullOrEmpty( message.Text ) )
            {
                Console.WriteLine( "Enter text" );
                return;
            }
            var even = message.Text.Length % 2 == 0;
            var color = even ? ConsoleColor.Red : ConsoleColor.Green;
            var alert = even ? "Your string had an even # of characters.\n" : "Your string had an odd # of characters.\n";
            Console.ForegroundColor = color;
            Console.WriteLine( alert );
            Console.ResetColor();
        }
    }
}