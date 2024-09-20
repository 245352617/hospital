using System.Collections.Generic;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public class TriageDirection
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ToAreaCode { get; set; }

        public static List<TriageDirection> GetTriageDirections()
        {
            List<TriageDirection> triageDirections = new List<TriageDirection>();
            TriageDirection triageDirection = new TriageDirection()
            {
                Code = "TriageDirection_001",
                Name = "死亡",
                ToAreaCode = "RescueArea"
            };
            triageDirections.Add(triageDirection);

            TriageDirection triageDirection1 = new TriageDirection()
            {
                Code = "TriageDirection_002",
                Name = "留观区",
                ToAreaCode = "ObservationArea"
            };
            triageDirections.Add(triageDirection1);

            TriageDirection triageDirection2 = new TriageDirection()
            {
                Code = "TriageDirection_003",
                Name = "抢救区",
                ToAreaCode = "RescueArea"
            };
            triageDirections.Add(triageDirection2);

            TriageDirection triageDirection3 = new TriageDirection()
            {
                Code = "TriageDirection_004",
                Name = "专科",
                ToAreaCode = "OutpatientArea"
            };
            triageDirections.Add(triageDirection3);

            TriageDirection triageDirection4 = new TriageDirection()
            {
                Code = "TriageDirection_005",
                Name = "特护病房",
                ToAreaCode = "OutpatientArea"
            };
            triageDirections.Add(triageDirection4);

            TriageDirection triageDirection5 = new TriageDirection()
            {
                Code = "TriageDirection_006",
                Name = "急诊",
                ToAreaCode = "OutpatientArea"
            };
            triageDirections.Add(triageDirection5);

            return triageDirections;
        }
    }
}
