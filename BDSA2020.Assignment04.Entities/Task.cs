using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BDSA2020.Assignment04.Entities
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int? AssignedToId { get; set; }

        public User AssignedTo { get; set; }

        public State State { get; set; }

        public string Description { get; set; }

        public virtual ICollection<TaskTag> Tags { get; set; }
    }
}