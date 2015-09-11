using Akka.Actor;
using System;

namespace Lesson1.Actors
{
    public class ConsoleActorReader : UntypedActor
    {
        private readonly string ExitCommand = "end";
        private IActorRef writer;

        public ConsoleActorReader(IActorRef writer)
        {
            this.writer = writer;
        }

        protected override void OnReceive(object message)
        {
            var readedLine = Console.ReadLine();
            if (!string.IsNullOrEmpty(readedLine) && String.Equals(readedLine, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {

                Context.System.Shutdown();
                return;
            }
            writer.Tell(new MessageClass(readedLine));
            Console.WriteLine("OnReceive ended");
            Self.Tell("continue");
        }
    }
}
