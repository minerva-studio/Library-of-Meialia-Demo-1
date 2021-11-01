namespace Minerva.Module
{
    public interface ISimilar<T>
    {
        bool SimilarTo(T other);
    }
}
