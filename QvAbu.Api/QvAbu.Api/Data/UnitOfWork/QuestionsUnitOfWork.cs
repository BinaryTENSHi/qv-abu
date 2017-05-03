﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QvAbu.Api.Data.Repository;

namespace QvAbu.Api.Data.UnitOfWork
{
    internal interface IQuestionsUnitOfWork : IUnitOfWork
    {
        IAssignmentQuestionsRepo AssignmentQuestionsRepo { get; }
        ISimpleQuestionsRepo SimpleQuestionsRepo { get; }
        ITextQuestionsRepo TextQuestionsRepo { get; }
    }

    internal class QuestionsUnitOfWork : IQuestionsUnitOfWork
    {
        #region Members

        private readonly QuestionsContext context;

        #endregion

        #region Ctor

        public QuestionsUnitOfWork(QuestionsContext context,
            IAssignmentQuestionsRepo assignmentQuestionsRepo,
            ISimpleQuestionsRepo simpleQuestionsRepo,
            ITextQuestionsRepo textQuestionsRepo)
        {
            this.context = context;

            this.AssignmentQuestionsRepo = assignmentQuestionsRepo;
            this.SimpleQuestionsRepo = simpleQuestionsRepo;
            this.TextQuestionsRepo = textQuestionsRepo;
        }

        #endregion

        #region Props

        public IAssignmentQuestionsRepo AssignmentQuestionsRepo { get; }
        public ISimpleQuestionsRepo SimpleQuestionsRepo { get; }
        public ITextQuestionsRepo TextQuestionsRepo { get; }

        #endregion

        #region Public Methods

        public async Task<int> Complete()
        {
            return await this.context.SaveChangesAsync();
        }

        #endregion
    }
}
