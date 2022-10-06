namespace Sat.Recruitment.Api.Results
{
    public sealed class Result
    {
        public bool IsSuccess { get; set; }

        public string[] Errors { get; set; }

        public object Data { get; set; }
    }
}
