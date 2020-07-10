using System;
using System.Collections.Generic;

namespace ApplicationCentreFormation.Models
{
    public partial class FormateurSpecialite
    {
        public Guid FormateurId { get; set; }
        public Guid SpecialiteId { get; set; }

        public virtual Formateur Formateur { get; set; }
        public virtual Specialite Specialite { get; set; }
    }
}
