﻿using System.Collections.Generic;

namespace QvAbu.Api.Models.Questions
{
    public class AssignmentQuestion : Question
    {
        #region Properties

        public ICollection<AssignmentOption> Options { get; set; }
        public ICollection<AssignmentAnswer> Answers { get; set; }

        #endregion
    }
}
