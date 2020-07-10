using System;
using System.Collections.Generic;

namespace ApplicationCentreFormation.Models
{
    public partial class Niveau
    {
        public Niveau()
        {
            Formation = new HashSet<Formation>();
        }

        public Guid Id { get; set; }
        public string Nom { get; set; }

        public virtual ICollection<Formation> Formation { get; set; }
    }
}
