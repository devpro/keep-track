﻿namespace KeepTrack.Api.Dto.Queries
{
    /// <summary>
    /// Data query object.
    /// </summary>
    /// <remarks>
    /// See https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs
    /// </remarks>
    public class DataQuery
    {
        /// <summary>
        /// Page number to return (starts with 0).
        /// </summary>
        public int Page { get; set; } = 0;

        /// <summary>
        /// Number of elements to return per page.
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Search text.
        /// </summary>
        public string Search { get; set; } = string.Empty;
    }
}
