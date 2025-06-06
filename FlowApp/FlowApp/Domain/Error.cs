namespace FlowApp.Domain
{
    public class Error
    {
        public int Status { get; set; }
        public int Code { get; set; }
        public string Property { get; set; }
        public string Message { get; set; }
        public string DeveloperMessage { get; set; }
    }
}
