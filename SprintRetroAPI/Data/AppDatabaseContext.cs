using Microsoft.EntityFrameworkCore;
using SprintRetroAPI.Entities;

namespace SprintRetroAPI.Data;

public class AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) : DbContext(options)
{
	public DbSet<Room> Rooms { get; set; }
	public DbSet<Participant> Participants { get; set; }
	public DbSet<Comment> Comments { get; set; }
	public DbSet<Column> Columns { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Room>(room =>
		{
			room.ToTable("Rooms");
			room.Property(room => room.Id).HasColumnName("id");
			room.Property(room => room.Name).HasColumnName("name");
			room.Property(room => room.CreatedAt).HasColumnName("created_at");

			room.HasKey(room => room.Id);
			room.HasMany(room => room.Participants)
				.WithOne(participant => participant.Room)
				.HasForeignKey(participant => participant.RoomId)
				.OnDelete(DeleteBehavior.Cascade);

			room.HasMany(room => room.Columns)
				.WithOne(column => column.Room)
				.HasForeignKey(column => column.RoomId)
				.OnDelete(DeleteBehavior.Cascade);
		});

		modelBuilder.Entity<Participant>(participant =>
		{
			participant.ToTable("Participants");
			participant.Property(participant => participant.Id).HasColumnName("id");
			participant.Property(participant => participant.RoomId).HasColumnName("room_id");
			participant.Property(participant => participant.ConnectionId).HasColumnName("connection_id");
			participant.Property(participant => participant.Name).HasColumnName("name");

			participant.HasMany(participant => participant.Comments)
				.WithOne(comment => comment.Participant)
				.HasForeignKey(comment => comment.ParticipantId);
		
			participant.HasKey(participant => participant.Id);
			participant.HasIndex(participant => participant.ConnectionId).IsUnique();
			participant.Property(participant => participant.ConnectionId).HasMaxLength(128).IsRequired();
			participant.Property(participant => participant.Name).HasMaxLength(100).IsRequired();
		});

		modelBuilder.Entity<Comment>(comment =>
		{
			comment.ToTable("Comments");
			comment.Property(comment => comment.Id).HasColumnName("id");
			comment.Property(comment => comment.RoomId).HasColumnName("room_id");
			comment.Property(comment => comment.ColumnId).HasColumnName("column_id");
			comment.Property(comment => comment.ParticipantId).HasColumnName("participant_id");
			comment.Property(comment => comment.Body).HasColumnName("body");
			comment.Property(comment => comment.CreatedAt).HasColumnName("created_at");

			comment.HasKey(comment => comment.Id);
			comment.HasIndex(comment => comment.RoomId);
		});

		
		modelBuilder.Entity<Column>(column =>
		{
			column.ToTable("Comments");
			column.Property(column => column.Id).HasColumnName("id");
			column.Property(column => column.RoomId).HasColumnName("room_id");
			column.Property(column => column.Title).HasColumnName("title");
			column.Property(column => column.Position).HasColumnName("position");

			column.HasMany(column => column.Comments)
				.WithOne(comment => comment.Column)
				.HasForeignKey(comment => comment.ColumnId);

			column.HasKey(column => column.Id);
			column.HasIndex(column => column.RoomId);
		});
	}
}