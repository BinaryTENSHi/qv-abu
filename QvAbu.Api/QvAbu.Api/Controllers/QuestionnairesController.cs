﻿using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using QvAbu.Api.Services.Questions;
using QvAbu.Data.Models.Questions.ReadModel;
using QvAbu.Data.Models.Questions;

namespace QvAbu.Api.Controllers
{
    [Route("api/[controller]")]
    public class QuestionnairesController : Controller
    {
        #region Members

        private readonly IQuestionsService service;

        #endregion

        #region Ctor

        public QuestionnairesController(IQuestionsService service)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        [HttpGet("previews")]
        public async Task<IEnumerable<QuestionnairePreview>> GetPreviews()
        {
            return await this.service.GetQuestionnairePreviewsAsync();
        }

        [HttpGet("{id}/{revision}/questions")]
        public async Task<IEnumerable<Question>> GetQuestions(Guid id, int revision)
        {
            return await this.service.GetQuestionsForQuestionnaireAsync(id, revision);
        }

        #endregion
    }
}
