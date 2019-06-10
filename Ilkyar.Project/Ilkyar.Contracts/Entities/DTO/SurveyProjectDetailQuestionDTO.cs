using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class SurveyProjectDetailQuestionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long ProjectDetailId { get; set; }
        public long UserId { get; set; }
        public bool IsAnswered { get; set; }
        public int Vote { get; set; }
    }
}
