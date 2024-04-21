using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<DatasetFile> DatasetFiles { get; set; }

	public DbSet<PipelineType> PipelineTypes { get; set; }

	public DbSet<TrainerType> TrainerTypes { get; set; }

	public DbSet<StudyField> StudyFields { get; set; }

	public DbSet<Subject> Subjects { get; set; }

	public DbSet<StudyYear> Years { get; set; }

	public DbSet<PredictionHistory> PredictionHistories { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<StudyField>().HasKey(_ => _.Id);
		modelBuilder.Entity<StudyField>().Property(_ => _.Name).IsRequired();
		modelBuilder.Entity<StudyField>().HasData(
			new StudyField { Id = 1, Name = "Business Management" },
			new StudyField { Id = 2, Name = "Softwarový vývoj" }
		);

		modelBuilder.Entity<StudyYear>().HasKey(_ => _.Id);
		modelBuilder.Entity<StudyYear>().HasData(
			new StudyYear { Id = 1, Year = 1 },
			new StudyYear { Id = 2, Year = 2 },
			new StudyYear { Id = 3, Year = 3 }
		);

		modelBuilder.Entity<Subject>().HasKey(_ => _.Id);
		modelBuilder.Entity<Subject>().Property(_ => _.Name).IsRequired();
		modelBuilder.Entity<Subject>().HasOne(_ => _.StudyField).WithMany(_ => _.Subjects).HasForeignKey(_ => _.StudyFieldId);
		modelBuilder.Entity<Subject>().HasData(
			new Subject { Id = 1, Name = "Anglický jazyk", StudyFieldId = 1 },
			new Subject { Id = 2, Name = "Finance", StudyFieldId = 1 },
			new Subject { Id = 3, Name = "Inovace v podnikání", StudyFieldId = 1 },
			new Subject { Id = 4, Name = "Lidské zdroje", StudyFieldId = 1 },
			new Subject { Id = 5, Name = "Management", StudyFieldId = 1 },
			new Subject { Id = 6, Name = "Marketing", StudyFieldId = 1 },
			new Subject { Id = 7, Name = "Mezinárodní obchod", StudyFieldId = 1 },
			new Subject { Id = 8, Name = "Podniková etika", StudyFieldId = 1 },
			new Subject { Id = 9, Name = "Projektový management", StudyFieldId = 1 },
			new Subject { Id = 10, Name = "Strategické řízení", StudyFieldId = 1 },
			new Subject { Id = 11, Name = "Účetnictví", StudyFieldId = 1 },
			new Subject { Id = 12, Name = "Veřejné finance", StudyFieldId = 1 },
			new Subject { Id = 13, Name = "Algoritmy", StudyFieldId = 2 },
			new Subject { Id = 14, Name = "Anglický jazyk", StudyFieldId = 2 },
			new Subject { Id = 15, Name = "Bezpečnost softwaru", StudyFieldId = 2 },
			new Subject { Id = 16, Name = "Cloud Computing", StudyFieldId = 2 },
			new Subject { Id = 17, Name = "Databáze", StudyFieldId = 2 },
			new Subject { Id = 18, Name = "Data Science", StudyFieldId = 2 },
			new Subject { Id = 19, Name = "Matematika", StudyFieldId = 2 },
			new Subject { Id = 20, Name = "Obchodní právo", StudyFieldId = 2 },
			new Subject { Id = 21, Name = "Operační systémy", StudyFieldId = 2 },
			new Subject { Id = 22, Name = "Programování", StudyFieldId = 2 },
			new Subject { Id = 23, Name = "Software Engineering", StudyFieldId = 2 },
			new Subject { Id = 24, Name = "Systémová architektura", StudyFieldId = 2 },
			new Subject { Id = 25, Name = "UI/UX Design", StudyFieldId = 2 },
			new Subject { Id = 26, Name = "Umělá inteligence", StudyFieldId = 2 },
			new Subject { Id = 27, Name = "Web Development", StudyFieldId = 2 }
		);

		modelBuilder.AddEnumTable<TrainerType, TrainerTypeEnum>();
		modelBuilder.AddEnumTable<PipelineType, PipelineTypeEnum>();

		modelBuilder.Entity<DatasetFile>().HasKey(_ => _.Id);
		modelBuilder.Entity<DatasetFile>().Property(_ => _.Name).IsRequired();

		modelBuilder.Entity<PredictionHistory>().HasKey(_ => _.Id);
		modelBuilder.Entity<PredictionHistory>().HasOne(_ => _.StudyField).WithMany(_ => _.PredictionHistories).HasForeignKey(_ => _.StudyFieldId).OnDelete(DeleteBehavior.Restrict);
		modelBuilder.Entity<PredictionHistory>().HasOne(_ => _.Year).WithMany(_ => _.PredictionHistories).HasForeignKey(_ => _.YearId).OnDelete(DeleteBehavior.Restrict);
		modelBuilder.Entity<PredictionHistory>().HasOne(_ => _.Subject).WithMany(_ => _.PredictionHistories).HasForeignKey(_ => _.SubjectId).OnDelete(DeleteBehavior.Restrict);
		modelBuilder.Entity<PredictionHistory>().HasOne(_ => _.DatasetFile).WithMany(_ => _.PredictionHistories).HasForeignKey(_ => _.DatasetFileId).OnDelete(DeleteBehavior.Restrict);
		modelBuilder.Entity<PredictionHistory>().HasOne(_ => _.PipelineType).WithMany(_ => _.PredictionHistories).HasForeignKey(_ => _.PipelineTypeId).OnDelete(DeleteBehavior.Restrict);
		modelBuilder.Entity<PredictionHistory>().HasOne(_ => _.TrainerType).WithMany(_ => _.PredictionHistories).HasForeignKey(_ => _.TrainerTypeId).OnDelete(DeleteBehavior.Restrict);
	}
}
