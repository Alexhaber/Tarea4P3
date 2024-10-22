using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using App.Service;

namespace Database.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Manejando los nombres de las tablas

            modelBuilder.Entity<User>().ToTable("Usuarios");
            modelBuilder.Entity<Post>().ToTable("Publicaciones");

            #endregion

            #region Definiendo las llaves primarias y campos unicos

            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<Post>().HasKey(p => p.Id);

            #endregion

            #region Estableciendo las relaciones

            // User y post, de una a muchos
            //modelBuilder.Entity<User>()
            //    .HasMany<Post>(user => user.Posts)
            //    .WithOne(post => post.Users)
            //    .HasForeignKey(post => post.Id)
            //    .OnDelete(DeleteBehavior.Cascade);

            // Post y coment
            //modelBuilder.Entity<Genre>()
            //    .HasMany<Serie>(g => g.Series)
            //    .WithOne(s => s.Genres_1)
            //    .HasForeignKey(s => s.Genre1_Id)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Genre>()
            //    .HasMany<Serie>(g => g.Series2)
            //    .WithOne(s => s.Genres_2)
            //    .HasForeignKey(s => s.Genre2_Id)
            //    .OnDelete(DeleteBehavior.Restrict);

            //fin

            #endregion

            #region Caracteristicas de las tablas

            #region Usuarios

            modelBuilder.Entity<User>().Property(s => s.Username).HasColumnName("Nombre_usuaio");
            modelBuilder.Entity<User>().Property(s => s.Name).HasColumnName("Nombre").HasColumnType("varchar").HasMaxLength(50);
            modelBuilder.Entity<User>().Property(s => s.Pfp).HasColumnName("Imagen");
            modelBuilder.Entity<User>().Property(s => s.Lastname).HasColumnName("Apellido");
            modelBuilder.Entity<User>().Property(s => s.Email).HasColumnName("Correo");
            modelBuilder.Entity<User>().Property(s => s.CellPhone).HasColumnName("Telefono");
            modelBuilder.Entity<User>().Property(s => s.Password).HasColumnName("Contraseña");



            #endregion

            #region Publicaciones

            modelBuilder.Entity<Post>().Property(p => p.CreatedAt).HasColumnName("Hora de creación").HasDefaultValue(DateTime.Now);
            modelBuilder.Entity<Post>().Property(p => p.Description).HasColumnName("Descripción").HasMaxLength(100);

            #endregion

            #endregion
        }
    }
}
