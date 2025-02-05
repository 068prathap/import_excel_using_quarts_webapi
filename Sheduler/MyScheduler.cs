﻿using ImportExcelUsingQuartz.Models;
using Quartz.Spi;
using Quartz;

namespace ImportExcelUsingQuartz.Sheduler
{
    public class MyScheduler : IHostedService
    {
        public IScheduler Scheduler { get; set; }
        private readonly IJobFactory jobFactory;
        private readonly JobMetadata jobMetadata;
        private readonly ISchedulerFactory schedulerFactory;

        public MyScheduler(ISchedulerFactory schedulerFactory, JobMetadata jobMetadata, IJobFactory jobFactory)
        {
            this.jobFactory = jobFactory;
            this.schedulerFactory = schedulerFactory;
            this.jobMetadata = jobMetadata;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //Creating Schdeular
            Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;
            //Create Job
            IJobDetail jobDetail = CreateJob(jobMetadata);
            //Create trigger
            ITrigger trigger = CreateTrigger(jobMetadata);
            //Schedule Job
            await Scheduler.ScheduleJob(jobDetail, trigger, cancellationToken);
            //Start The Schedular
            await Scheduler.Start(cancellationToken);
        }

        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create().WithIdentity(jobMetadata.JobId.ToString()).WithCronSchedule(jobMetadata.CronExpression).WithDescription(jobMetadata.JobName).Build();
        }

        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder.Create(jobMetadata.JobType).WithIdentity(jobMetadata.JobId.ToString()).WithDescription(jobMetadata.JobName).Build();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler.Shutdown();
        }
    }
}