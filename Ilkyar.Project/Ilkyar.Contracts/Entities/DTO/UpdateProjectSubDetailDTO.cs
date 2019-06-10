using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilkyar.Contracts.Entities.DTO
{
    public class UpdateProjectSubDetailDTO
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public int CityId { get; set; }
        public int TownId { get; set; }
        public int SchoolTypeId { get; set; }
        public string School { get; set; }
        public int TransportationTypeId { get; set; }
        public int ArrivalTransportationTypeId { get; set; }
        public DateTime DetailStartDate { get; set; }
        public DateTime TrnsStartDate { get; set; }
        public DateTime DetailEndDate { get; set; }
        public DateTime TrnsEndDate { get; set; }
        public string ProjectInfo { get; set; }
        public int AccNumOfPeople { get; set; }
        public int TrnsNumOfPeople { get; set; }
        public int TrnsArrNumOfPeople { get; set; }
        public string Inn { get; set; }
        public string Comeback { get; set; }
        public string Departure { get; set; }
        public string DepartureFirm { get; set; }
        public string ComebackFirm { get; set; }
        public string ReqText { get; set; }
        public int StatusId { get; set; }
    }
}
