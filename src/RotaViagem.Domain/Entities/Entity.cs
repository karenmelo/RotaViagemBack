using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Domain.Entities
{
    public class Entity
    {
        public int Id { get; protected set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }
    }
}
