namespace Domain.Core
{
    public enum CommitOption
    {
        /// <summary>
        /// Optimistic concurrency is not managed
        /// </summary>
        None = 0,
        /// <summary>
        /// In case of optimistic concurrency client data wins
        ///  "You modify my record while I wash eating a sandwich :-)"
        /// </summary>
        ClientWins = 1,
        /// <summary>
        /// In case of optimistic concurrency problem IUnityOfWork throw a exception
        /// </summary>
        ThrowError = 2
    }
}