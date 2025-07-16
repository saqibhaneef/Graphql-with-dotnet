
using Bogus.DataSets;
using GraphQLDemo.API;
using GraphQLDemo.API.Models.DataLoader;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services.Course;
using GraphQLDemo.API.Services.Instructor;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.API.Schema.Mutations;
using PizzaOrder.API.Schema.Queries;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddDataLoader<InstructorDataLoader>()
    .AddInMemorySubscriptions()
    .AddProjections();

builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<InstructorRepository>();
////builder.Services.AddScoped<InstructorDataLoader>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddPooledDbContextFactory<SchoolDbContext>(x => x.UseSqlite(connectionString).LogTo(Console.WriteLine));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseWebSockets();

app.MapGraphQL();

await app.RunAsync();


