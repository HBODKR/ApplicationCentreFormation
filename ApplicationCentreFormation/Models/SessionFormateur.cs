using System;
using System.Collections.Generic;

namespace ApplicationCentreFormation.Models
{
    public partial class SessionFormateur
    {
        public Guid SessionId { get; set; }
        public Guid FormateurId { get; set; }

        public virtual Formateur Formateur { get; set; }
        public virtual Session Session { get; set; }
    }
}
