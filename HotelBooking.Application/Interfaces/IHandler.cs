namespace HotelBooking.Application.Interfaces
{
    public interface IHandler<TQuery, TResult>
    {
        Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default);
    }
}
