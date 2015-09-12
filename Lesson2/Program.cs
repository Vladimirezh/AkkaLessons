using Akka.Actor;
using Lesson2.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("TestSystem");
            var writer = system.ActorOf(Props.Create((() => new Actors.ConsoleWriterActor())), "Writer");
            var reader = system.ActorOf(Props.Create(typeof(Actors.ConsoleReaderActor), writer), "Reader");
            reader.Tell(ConsoleReaderActor.StartCommand);
            system.AwaitTermination();
            Console.ReadKey();
        }
    }
}
