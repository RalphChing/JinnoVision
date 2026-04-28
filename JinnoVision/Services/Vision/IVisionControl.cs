namespace JinnoVision.Services.Vision
{
    public interface IVisionModule<TInput, TResult>
    {
        TResult Run(TInput input);
    }
}