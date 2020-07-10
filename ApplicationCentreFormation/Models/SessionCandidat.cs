using System;
using System.Collections.Generic;

namespace ApplicationCentreFormation.Models
{
    public partial class SessionCandidat
    {
        public Guid SessionId { get; set; }
        public Guid CandidatId { get; set; }

        public virtual Candidat Candidat { get; set; }
        public virtual Session Session { get; set; }
    }
}
