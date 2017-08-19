﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QvAbu.CLI.Wrappers;
using QvAbu.Data.Data.UnitOfWork;
using QvAbu.Data.Models.Questions;

namespace QvAbu.CLI
{
    public interface IImportExportService
    {
        Task<(int importedQuestions, List<string> erroredFiles)> Import(string name, string[] filesToImport);
        Task Export();
    }

    public class ImportExportService : IImportExportService
    {
        #region Members

        private readonly IQuestionnairesUnitOfWork questionnairesUow;
        private readonly IQuestionsUnitOfWork questionsUow;
        private readonly IFile file;

        #endregion

        #region Ctors

        public ImportExportService(IQuestionnairesUnitOfWork questionnairesUow, 
            IQuestionsUnitOfWork questionsUow,
            IFile file)
        {
            this.questionnairesUow = questionnairesUow;
            this.questionsUow = questionsUow;
            this.file = file;
        }

        #endregion

        #region Props

        #endregion

        #region Methods

        public async Task<(int importedQuestions, List<string> erroredFiles)> Import(string name, string[] filesToImport)
        {
            var questionnaire = new Questionnaire
            {
                ID = Guid.NewGuid(),
                Revision = 1,
                Name = name
            };
            this.questionnairesUow.QuestionnairesRepo.Add(questionnaire);

            var erroredFiles = new List<string>();
            var questionsCount = 0;

            foreach (var file in filesToImport)
            {
                List<List<string>> csv;
                QuestionType type;

                var text = await this.file.ReadAllText(file);
                csv = text.Split('\n').Select(_ => _.Split(';').ToList()).ToList();
                type = (QuestionType) Convert.ToInt32(csv[0][0]);

                foreach (var line in csv.Skip(2))
                {
                    if (line.Count < 4)
                    {
                        continue;
                    }

                    var question = new SimpleQuestion
                    {
                        ID = Guid.NewGuid(),
                        Revision = 1,
                        Text = line[0],
                        SimpleQuestionType = (SimpleQuestionType) Convert.ToInt32(line[1]),
                        Answers = new List<SimpleAnswer>()
                    };

                    for (var i = 2; i < line.Count; i += 2)
                    {
                        question.Answers.Add(new SimpleAnswer
                        {
                            ID = Guid.NewGuid(),
                            Text = line[i],
                            IsCorrect = Convert.ToBoolean(line[i + 1])
                        });
                    }

                    this.questionsUow.SimpleQuestionsRepo.Add(question);
                    await this.questionnairesUow.QuestionnairesRepo.AddQuestion(questionnaire.ID, question);
                    await this.questionsUow.Complete();

                    questionsCount++;
                }
            }

            return (questionsCount, null);
        }

        public Task Export()
        {
            //var currentDirectory = Directory.GetCurrentDirectory();
            //var targetDirectory = currentDirectory;
            //do
            //{
            //    Console.WriteLine("Please enter the path where the files should be exported.");
            //    Console.WriteLine($"Leave empty for current folder ({currentDirectory}).");
            //} while (!new DirectoryInfo(targetDirectory).Exists);

            //Console.WriteLine($"\n\nStarting export to \"{currentDirectory}\"...");

            // TODO: Export
            return Task.CompletedTask;
        }

        #endregion
    }
}
