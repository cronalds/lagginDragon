using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lagginDragon;

namespace CrimeModule
{
    public class Models : IModule
    {
        public string Name => "CrimeModule.Models";

        // // // // // // // / // / / / //
        public void Deregister(SimpleInjector.Container container)
        {
            throw new NotImplementedException();
        }

        public void Register(SimpleInjector.Container container)
        {
            throw new NotImplementedException();
        }
        // // // // // // // // // // // // / / /  / / / /

        public class Crime
        {
            public string? ID { get; set; }
            public DateTime? crimeTookPlaceAtDateTime { get; set; }
            public List<Criminal>? criminals { get; set; } // all criminals alleged to be involved
            public List<Victim>? victims { get; set; } // all victims alleged to be involved
            public List<Event>? events { get; set; } // all events alleged to have taken place; in order of happenning
            public List<Evidence>? evidence { get; set; } // all evidences pertaining to crime
            public List<CriminalRelationship>? relationships { get; set; } // all relationships pertaining to crime
            public List<Note>? notes { get; set; }
        }

        public class CriminalRelationship
        {
            public string? ID { get; set; } // Unique ID for relationship
            public Evidence? Source { get; set; } // First evidence (e.g., Blood)
            public Evidence? Target { get; set; } // Second evidence (e.g., Weapon)
            public string? Type { get; set; } // e.g., "FoundOn", "Corroborates"
            public List<Note>? notes { get; set; }
            public Criminal? RelatedCriminal { get; set; } // Optional context
            public List<Victim>? RelatedVictim { get; set; }
            public List<Event>? RelatedEvents { get; set; }
            public List<Location>? RelatedLocations { get; set; }
        }

        public class Criminal
        {
            public string? ID { get; set; }
            public string? name { get; set; }
            public List<string>? aliases { get; set; }
            public string? DOB { get; set; }
            // i fucking hate this.
            public Types.Gender? gender { get; set; }
            public string? CRN { get; set; }
            public List<Location>? knownToFrequent { get; set; }
            public List<Crime>? suspectedOf { get; set; }
            public List<Crime>? priors { get; set; }
            public List<Note>? notes { get; set; }
        }

        public class Evidence
        {
            public string? ID { get; set; } // Unique ID for referencing
            public List<Types.StatementEvidenceType>? Statement { get; set; }
            public List<Types.PhysicalEvidenceType>? Physical { get; set; }
            public List<Types.BiologicalEvidenceType>? Biological { get; set; }
            public List<Types.DigitalEvidenceType>? Digital { get; set; }
            public List<Note>? notes { get; set; }
        }

        public class Victim
        {
            public string? ID { get; set; }
            public string? name { get; set; }
            public string? DOB { get; set; }
            public Types.Gender? gender { get; set; }
            public List<Note>? notes { get; set; }
        }

        public class Event
        {
            public string? ID { get; set; }
            public string? eventTitle { get; set; }
            public List<Action>? actions { get; set; } // actions in sequential order of happening during event
            public Location? locationEventTookPlace { get; set; }
            public DateTime? eventTookPlaceAtDateTime { get; set; }
            public List<Note>? notes { get; set; }
        }

        public class Action
        {
            public string? ID { get; set; }
            public string? actionName { get; set; }
            public List<Note>? notes { get; set; }
            public List<Criminal>? criminals { get; set; }
            public List<Victim>? victims { get; set; }
            public List<Evidence>? evidenceRelatedOrDerivedFrom { get; set; }
        }

        public class Location
        {
            public string? ID { get; set; }
            public string? name { get; set; }
            public string? address { get; set; }
            public string? googleMapsLink { get; set; }
            public List<Note>? notes { get; set; }
        }

        public class StatementEvidence
        {
            public string? ID { get; set; }
            public Types.StatementEvidenceType? Type { get; set; }
            public string? Details { get; set; }
            public List<Note>? notes { get; set; }
        }

        public class PhysicalEvidence
        {
            public string? ID { get; set; }
            public Types.PhysicalEvidenceType? Type { get; set; }
            public string? Description { get; set; }
            public List<Note>? notes { get; set; }
        }

        public class BiologicalEvidence
        {
            public string? ID { get; set; }
            public Types.BiologicalEvidenceType? Type { get; set; }
            public string? Analysis { get; set; }
            public List<Note>? notes { get; set; }
        }

        public class DigitalEvidence
        {
            public string? ID { get; set; }
            public Types.DigitalEvidenceType? Type { get; set; }
            public string? FilePath { get; set; }
            public List<Note>? notes { get; set; }
        }

        public class Note
        {
            public string? title { get; set; }
            public string? contents { get; set; }
        }
    }
}
