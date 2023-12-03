using life_quality_back.Data;
using life_quality_back.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options => options.AddPolicy("corspolicy", policy =>
{
    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<DoctorRepository>();
builder.Services.AddTransient<DiseaseRepository>();
builder.Services.AddTransient<TreatmentStrategyRepository>();
builder.Services.AddTransient<PatientRepository>();
builder.Services.AddTransient<AnswerRepository>();
builder.Services.AddTransient<QuestionRepository>();
builder.Services.AddTransient<QuestionnaireRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<ResultsRepository>();

builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corspolicy");

app.UseAuthorization();

app.MapControllers();

AppDbInitializer.Seed(app);
app.Run();
