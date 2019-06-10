using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class ConversationDTO
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public string UserFullName { get; set; }
        public string Message { get; set; }
        public int MessageTypeId { get; set; }
        public DateTime Date { get; set; }
    }
}
