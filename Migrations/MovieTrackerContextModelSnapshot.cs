// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieTracker;

#nullable disable

namespace MovieTracker.Migrations
{
    [DbContext(typeof(MovieTrackerContext))]
    partial class MovieTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MovieTracker.Models.Entities.Actor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Score")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Episode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Score")
                        .HasColumnType("tinyint");

                    b.Property<Guid>("SeriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Watched")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Score")
                        .HasColumnType("tinyint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Watched")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ActorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EpisodeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Score")
                        .HasColumnType("tinyint");

                    b.Property<Guid?>("SeriesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("EpisodeId");

                    b.HasIndex("MovieId");

                    b.HasIndex("SeriesId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Series", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Score")
                        .HasColumnType("tinyint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Watched")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Episode", b =>
                {
                    b.HasOne("MovieTracker.Models.Entities.Series", "Series")
                        .WithMany("Episodes")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Series");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Role", b =>
                {
                    b.HasOne("MovieTracker.Models.Entities.Actor", "Actor")
                        .WithMany("Roles")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MovieTracker.Models.Entities.Episode", "Episode")
                        .WithMany("Roles")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MovieTracker.Models.Entities.Movie", "Movie")
                        .WithMany("Roles")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("MovieTracker.Models.Entities.Series", "Series")
                        .WithMany("Roles")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Actor");

                    b.Navigation("Episode");

                    b.Navigation("Movie");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Actor", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Episode", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Movie", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("MovieTracker.Models.Entities.Series", b =>
                {
                    b.Navigation("Episodes");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
