using System;
using System.Collections.Generic;

namespace ApplicationCentreFormation.Models
{
    public partial class Session
    {
        public Session()
        {
            SessionCandidat = new HashSet<SessionCandidat>();
            SessionFormateur = new HashSet<SessionFormateur>();
        }

        public Guid Id { get; set; }
        public DateTime DateDeb { get; set; }
        public DateTime DateFin { get; set; }
        public string Planning { get; set; }
        public Guid FormationId { get; set; }

        public virtual Formation Formation { get; set; }
        public virtual ICollection<SessionCandidat> SessionCandidat { get; set; }
        public virtual ICollection<SessionFormateur> SessionFormateur { get; set; }
    }
}
