using System;

namespace ManagedUI
{
    /// <summary>
    /// Args for progress events.
    /// </summary>
    public sealed class ProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Args for progress events.
        /// </summary>
        /// <param name="status">The status</param>
        /// <param name="precentage">The precentage.</param>
        public ProgressEventArgs(string status, int precentage)
        {
            Status = status;
            Precentage = precentage;
        }
        /// <summary>
        /// Get the status
        /// </summary>
        public string Status { get; private set; }
        /// <summary>
        /// Get the precentage.
        /// </summary>
        public int Precentage { get; private set; }
    }
}
