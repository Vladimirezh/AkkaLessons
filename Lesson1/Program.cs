using Akka.Actor;
using System;

namespace Lesson1
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("TestSystem");
            var writer = system.ActorOf(Props.Create((() => new Actors.ConsoleWriterActor())), "Writer");
            var reader = system.ActorOf(Props.Create(typeof(Actors.ConsoleActorReader), writer), "Reader");
            reader.Tell("start");
            system.AwaitTermination();
            Console.ReadKey();
        }
    }
}
