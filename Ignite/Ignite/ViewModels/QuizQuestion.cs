﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ignite.ViewModels
{
    public class QuizQuestion
    {
        public QuizQuestion()
        {
            this.Answers = new List<AnsweViewModel>();
        }

        public int Id { get; set; }

        public string Statement { get; set; }

        public List<AnsweViewModel> Answers { get; set; }

        [Required(ErrorMessage = "Please Choose an Answer!")]
        [Display(Name = "Answer")]
        public string ChosenAnswer { get; set; }
    }

    public class AnswerChosenAttribute : ValidationAttribute
    {
        public AnswerChosenAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

           // var question = value as QuizQuestion;

            bool isValid = value.ToString() == "NotChosen" ? false : true; // constant (will be moved to another folder)

            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("Please Select An Answer !");
        }
    }

}