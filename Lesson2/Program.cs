using System;
using Akka.Actor;
using Lesson2.Actors;

namespace Lesson2
{
    internal class Program
    {
        private static void Main()
        {
            var system = ActorSystem.Create( "TestSystem" );
            var writer = system.ActorOf( Props.Create( ( () => new ConsoleWriterActor() ) ), "Writer" );
            var reader = system.ActorOf( Props.Create( typeof ( ConsoleReaderActor ), writer ), "Reader" );
            reader.Tell( ConsoleReaderActor.StartCommand );
            system.AwaitTermination();
            Console.ReadKey();
        }
    }
}