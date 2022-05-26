﻿using Hangfire.SqlServer;
using Hangfire.Tags.Dashboard;

namespace Hangfire.Tags.SqlServer
{
    /// <summary>
    /// Provides extension methods to setup Hangfire.Tags
    /// </summary>
    public static class GlobalConfigurationExtensions
    {
        /// <summary>
        /// Configures Hangfire to use Tags.
        /// </summary>
        /// <param name="configuration">Global configuration</param>
        /// <param name="options">Options for tags</param>
        /// <param name="sqlOptions">Options for sql storage</param>
        /// <param name="jobStorage">The jobStorage for which this configuration is used.</param>
        /// <returns></returns>
        public static IGlobalConfiguration UseTagsWithSql(this IGlobalConfiguration configuration,
            TagsOptions options = null, SqlServerStorageOptions sqlOptions = null, JobStorage jobStorage = null)
        {
            options = options ?? new TagsOptions();
            sqlOptions = sqlOptions ?? new SqlServerStorageOptions();

            var storage = new SqlTagsServiceStorage(sqlOptions);
            (jobStorage ?? JobStorage.Current).Register(options, storage);

            var config = configuration.UseTags(options);
            return config;
        }
    }
}
