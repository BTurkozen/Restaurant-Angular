namespace Restaurant_Angular.Common.ResultConstant
{
    public interface IResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
