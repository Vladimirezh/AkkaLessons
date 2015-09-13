using System;
using Akka.Actor;
using Lesson1.Actors;

namespace Lesson1
{
    internal class Program
    {
        private static void Main()
        {
            var system = ActorSystem.Create( "TestSystem" );
            var writer = system.ActorOf( Props.Create( ( () => new ConsoleWriterActor() ) ), "Writer" );
            var reader = system.ActorOf( Props.Create( typeof ( ConsoleActorReader ), writer ), "Reader" );
            reader.Tell( "start" );
            system.AwaitTermination();
            Console.ReadKey();
        }
    }
}