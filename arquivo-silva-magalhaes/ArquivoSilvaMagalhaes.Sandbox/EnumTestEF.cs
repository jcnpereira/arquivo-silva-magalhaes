using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArquivoSilvaMagalhaes.Sandbox
{
    enum EventType : byte
    {
        RegularEvent,
        School
    }

    class Event
    {
        public string Name { get; set; }
        public EventType Type { get; set; }
    }

    class EventDb : DbContext
    {
        public DbSet<Event> Events { get; set; }
    }


    class EnumTestEF
    {
        static void Main(string[] args)
        {
            using (var db = new EventDb())
            {
                db.Events.Add(
                    new Event
                    {
                        Name = "Event 1",
                        Type = EventType.RegularEvent
                    }
                );

                var evt = db.Events.First(e => true);

                Console.Write(String.Format("Name: {0}, Type: {1}", evt.Name, evt.Type));
            }
        }
    }
}

