using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using VacationScaffold.RepositoryImplementation;
using WebApplication1.Repository;
using Quartz.Simpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProjectContext>();

builder.Services.AddScoped<IRamEntityRepository, RamEntityRepository>();

builder.Services.AddQuartz(options =>
{
    options.UseMicrosoftDependencyInjectionJobFactory();
    options.AddJob<RamInsertJob>(JobKey.Create(nameof(RamInsertJob)))
    .AddTrigger(trigger =>
    {
        trigger.ForJob(nameof(RamInsertJob))
         //.StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(1)
                .RepeatForever());

    });
});

builder.Services.AddQuartzHostedService();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();