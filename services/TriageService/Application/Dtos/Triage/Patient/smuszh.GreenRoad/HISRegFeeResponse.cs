namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// HIS 绿通挂账
    /// </summary>
    public class HISRegFeeResponse
    {
        public Resultinfo ResultInfo { get; set; }
        public string ResultTransID { get; set; }
        public string ResultTime { get; set; }
    }
    
    public class Resultinfo
    {
        public string ResultCode { get; set; }
        public Resultmessage ResultMessage { get; set; }
        public string Message { get; set; }
    }

    public class Resultmessage
    {
        public string CardNo { get; set; }
        public string ClinicCode { get; set; }
        public string InTimes { get; set; }
        public float TotCost { get; set; }
        public float OwnCost { get; set; }
        public float PayCost { get; set; }
        public float PubCost { get; set; }
        public object InvoiceNo { get; set; }
        public object PrintInvoiceNo { get; set; }
    }
}