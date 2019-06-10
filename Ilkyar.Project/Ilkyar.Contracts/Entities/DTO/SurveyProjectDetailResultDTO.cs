using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class SurveyProjectDetailResultDTO
    {
        public long Id { get; set; }
        public long ProjectDetailId { get; set; }
        public string ProjectDetailName { get; set; }
        public int SurveyProjectDetailQuestionId { get; set; }
        public string SurveyProjectDetailQuestionName { get; set; }
        public int Vote { get; set; }
    }
}
