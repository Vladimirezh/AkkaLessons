using System;
using Akka.Actor;
using Lesson3.Actors;

namespace Lesson3
{
    internal class Program
    {
        private static void Main()
        {
            var system = ActorSystem.Create( "System" );

            var consoleWriterProps = Props.Create< ConsoleWriterActor >();
            var consoleWriterActor = system.ActorOf( consoleWriterProps, "Writer" );

            var validationActorProps = Props.Create< ValidationActor >( consoleWriterActor );
            var validationActor = system.ActorOf( validationActorProps, "Validation" );

            var consoleReaderProps = Props.Create< ConsoleReaderActor >( validationActor );
            var reader = system.ActorOf( consoleReaderProps, "Reader" );

            reader.Tell( ConsoleReaderActor.StartCommand );

            system.AwaitTermination();
            Console.ReadKey();
        }
    }
}