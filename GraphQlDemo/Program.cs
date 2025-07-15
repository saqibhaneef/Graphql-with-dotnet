
using Bogus.DataSets;
using GraphQLDemo.API;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services.Course;
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
    .AddInMemorySubscriptions();

builder.Services.AddScoped<CourseRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddPooledDbContextFactory<SchoolDbContext>(x => x.UseSqlite(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseWebSockets();

app.MapGraphQL();

await app.RunAsync();


