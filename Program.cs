var app = builder.Build();

// =============================================
// Migración automática de la base de datos
// Al iniciar la aplicación, se aplican todas las migraciones pendientes
// Esto asegura que las tablas y el esquema estén siempre actualizados
// =============================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // Aplica todas las migraciones pendientes automáticamente
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run(); 