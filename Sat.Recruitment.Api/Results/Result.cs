namespace Sat.Recruitment.Api.Results
{
    sealed class Result
    {
        public bool IsSuccess { get; set; }

        public string[] Errors { get; set; }

        public object Data { get; set; }
    }
}
