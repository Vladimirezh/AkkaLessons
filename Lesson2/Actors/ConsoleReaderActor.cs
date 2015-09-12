using Akka.Actor;
using System;

namespace Lesson2.Actors
{
    public class ConsoleReaderActor : UntypedActor
    {
        private IActorRef consoleWriter;
        public static string StartCommand = "start";
        public static string ExitCommand = "end";

        public ConsoleReaderActor(IActorRef writer)
        {
            consoleWriter = writer;
        }

        protected override void OnReceive(object message)
        {
            if (message.Equals(StartCommand))
            {
                Console.WriteLine("Reader started.");
                Console.WriteLine("Write message started with # and press enter.");
            }
            else if (message is Messages.InputError)
            {
                consoleWriter.Tell(message);
            }
            ReadAndValidateMessage();
        }

        private void ReadAndValidateMessage()
        {
            var readedLine = Console.ReadLine();
            if (string.IsNullOrEmpty(readedLine))
            {
                Self.Tell(new Messages.InputError("Empty or null string"));
            }
            else if (readedLine.Equals(ExitCommand))
            {
                Context.System.Shutdown();

            }
            else if (readedLine.StartsWith("#"))
            {
                consoleWriter.Tell(new Messages.InputSuccess("Thank you! Message was valid."));
                Self.Tell(new Messages.ContinueProcessing());
            }
            else
            {
                Self.Tell(new Messages.ValidationError("String doesnt start with #"));
            }

        }
    }
}
