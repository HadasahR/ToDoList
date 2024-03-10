using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoApi;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDbContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//cors
builder.Services.AddCors(option=>option.AddPolicy("AllowAll",builder=>
{
  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();
app.UseCors("AllowAll");
//swagger/////////////
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//שליפת המשימות
 app.MapGet("/", async(ToDoDbContext db) =>
 {
    var data=await db.Items.ToListAsync();
    return Results.Ok(data);
 }
 );
 //הוספת משימה
 app.MapPost("/items/{name}", async (ToDoDbContext db, string name)=>{
  Item i=new Item(){Name=name,IsComplete=false};
  i.Name=name;
  i.IsComplete=false;
   db.Items.Add(i);
   await db.SaveChangesAsync();
   return Results.Ok(db.Items);
 });
//עדכון משימה
 app.MapPut("/Items/{id}/{isComplete}",async(int id,bool isComplete,ToDoDbContext db)=>{
   var todo=await db.Items.FindAsync(id);
   if(todo is null) return Results.NotFound();
   todo.Name=todo.Name;
   todo.IsComplete=isComplete;
   await db.SaveChangesAsync();
   return Results.NoContent();
 });
// //מחיקת משימה
 app.MapDelete("/Items/{id}", async(ToDoDbContext db,int id)=>{
  if(await db.Items.FindAsync(id) is Item todo)
  {
   db.Items.Remove(todo);
   await db.SaveChangesAsync();
   return Results.Ok(db.Items);
  }
   return Results.NotFound();
 });
app.Run();