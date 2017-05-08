﻿using System;

namespace QvAbu.Api.Models.Questions
{
    public class AssignmentAnswer : Answer
    {
        #region Properties

        public Guid CorrectOptionId { get; set; }

        public virtual AssignmentOption CorrectOption { get; set; }

        #endregion
    }
}
