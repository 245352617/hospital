using Hangfire.States;
using Hangfire.Storage;
using System;

namespace Szyjian.Ecis.Patient.BackgroundJob.Hangfire
{
    /// <summary>
    /// Hangfire 设置成功作业自动过期
    /// </summary>
    public class SucceedStateExpireHandler : IStateHandler
    {
        private readonly TimeSpan _jobExpirationTimeSpan;

        public SucceedStateExpireHandler(TimeSpan timeSpan)
        {
            _jobExpirationTimeSpan = timeSpan;
        }

        public void Apply(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            context.JobExpirationTimeout = _jobExpirationTimeSpan;
        }

        public void Unapply(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {

        }

        public string StateName => SucceededState.StateName;
    }
}