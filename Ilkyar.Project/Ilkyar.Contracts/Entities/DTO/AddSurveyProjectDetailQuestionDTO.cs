using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class AddSurveyProjectDetailQuestionDTO
    {
        public long ProjectDetailId { get; set; }
        public long UserId { get; set; }
        public int SurveyProjectDetailQuestionId { get; set; }
        public short Vote { get; set; }
    }
}
