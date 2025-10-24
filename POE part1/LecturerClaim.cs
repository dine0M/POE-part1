using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_part1
{
    public class LecturerClaim
    {
        public int ClaimID { get; set; }
        public string? LecturerID { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public double TotalAmount { get; set; }
        public string? DateSubmitted { get; set; }
        public string? Status { get; set; }
        public int StatusProgress { get; set; }
        public string? Notes { get; set; }
        public string? AttachedFile { get; set; }
    }
}
