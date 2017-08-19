﻿using System.Collections.Generic;

namespace QvAbu.Data.Models.Questions
{
    public class AssignmentQuestion : Question
    {
        #region Properties

        public IEnumerable<AssignmentOption> Options { get; set; }
        public IEnumerable<AssignmentAnswer> Answers { get; set; }

        #endregion
    }
}
