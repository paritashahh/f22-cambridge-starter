using Microsoft.OpenApi.Models;
using Database.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
  {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Article API", Description = "An API to track articles", Version = "v1" });
  });
  

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
  {
     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Article API V1");
  });

app.MapGet("/", () => "Hello World!");

app.MapGet("/articles/{id}", (int id) => ArticleDB.GetArticle(id));
app.MapGet("/articles", () => ArticleDB.GetArticles());
app.MapPost("/articles", (string title, string date, string author) => ArticleDB.CreateArticle(title, date, author));
app.MapDelete("/articles/{id}", (int id) => ArticleDB.RemoveArticle(id));
app.MapPut("/updatedfield", (int id, string field, string update) => ArticleDB.UpdateArticle(id, field, update));
app.MapPut("/updatedarticle", (int id, string title, string date, string author) => ArticleDB.UpdateEntireArticle(id, title, date, author));
app.MapGet("/filteredarticles", (string field, string keyword) => ArticleDB.GetFilteredArticles(field, keyword));
app.MapDelete("/filtereddelete", (string field, string keyword) => ArticleDB.RemoveArticles(field, keyword));
app.Run();
