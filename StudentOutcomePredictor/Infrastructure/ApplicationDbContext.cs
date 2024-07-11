using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Course> Courses { get; set; }

	public DbSet<Qualification> Qualifications { get; set; }

	public DbSet<Student> Students { get; set; }

	public DbSet<StudentPerformance> StudentPerformances { get; set; }

	public DbSet<EconomicIndicator> EconomicIndicators { get; set; }

	public DbSet<PredictedLabel> PredictedLabels { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
       modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId);
            entity.Property(e => e.CourseName).IsRequired();
            entity.HasData(
                new Course { CourseId = 9, CourseName = "Course 9" },
                new Course { CourseId = 10, CourseName = "Course 10" },
                new Course { CourseId = 11, CourseName = "Course 11" },
                new Course { CourseId = 12, CourseName = "Course 12" },
                new Course { CourseId = 17, CourseName = "Course 17" }
            );
        });

        modelBuilder.Entity<Qualification>(entity =>
        {
            entity.HasKey(e => e.QualificationId);
            entity.Property(e => e.QualificationName).IsRequired();
            entity.HasData(
                new Qualification { QualificationId = 1, QualificationName = "Qualification 1" },
                new Qualification { QualificationId = 10, QualificationName = "Qualification 10" },
                new Qualification { QualificationId = 13, QualificationName = "Qualification 13" },
                new Qualification { QualificationId = 14, QualificationName = "Qualification 14" },
                new Qualification { QualificationId = 22, QualificationName = "Qualification 22" },
                new Qualification { QualificationId = 27, QualificationName = "Qualification 27" }
            );
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Students)
                  .HasForeignKey(e => e.CourseId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.PreviousQualification)
                  .WithMany(q => q.StudentsWithPreviousQualification)
                  .HasForeignKey(e => e.PreviousQualificationId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.MotherQualification)
                  .WithMany(q => q.StudentsWithMotherQualification)
                  .HasForeignKey(e => e.MotherQualificationId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.FatherQualification)
                  .WithMany(q => q.StudentsWithFatherQualification)
                  .HasForeignKey(e => e.FatherQualificationId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new Student
                {
                    StudentId = 1,
                    MaritalStatus = 1,
                    ApplicationMode = 8,
                    ApplicationOrder = 1,
                    CourseId = 11,
                    DaytimeEveningAttendance = 1,
                    PreviousQualificationId = 1,
                    Nationality = 1,
                    MotherQualificationId = 1,
                    FatherQualificationId = 1,
                    MotherOccupation = 10,
                    FatherOccupation = 6
                },
                new Student
                {
                    StudentId = 2,
                    MaritalStatus = 1,
                    ApplicationMode = 13,
                    ApplicationOrder = 1,
                    CourseId = 9,
                    DaytimeEveningAttendance = 1,
                    PreviousQualificationId = 1,
                    Nationality = 1,
                    MotherQualificationId = 13,
                    FatherQualificationId = 1,
                    MotherOccupation = 5,
                    FatherOccupation = 9
                },
                new Student
                {
                    StudentId = 3,
                    MaritalStatus = 1,
                    ApplicationMode = 8,
                    ApplicationOrder = 1,
                    CourseId = 11,
                    DaytimeEveningAttendance = 1,
                    PreviousQualificationId = 1,
                    Nationality = 1,
                    MotherQualificationId = 22,
                    FatherQualificationId = 27,
                    MotherOccupation = 8,
                    FatherOccupation = 9
                },
                new Student
                {
                    StudentId = 4,
                    MaritalStatus = 2,
                    ApplicationMode = 12,
                    ApplicationOrder = 1,
                    CourseId = 17,
                    DaytimeEveningAttendance = 0,
                    PreviousQualificationId = 1,
                    Nationality = 1,
                    MotherQualificationId = 22,
                    FatherQualificationId = 27,
                    MotherOccupation = 2,
                    FatherOccupation = 3
                },
                new Student
                {
                    StudentId = 5,
                    MaritalStatus = 1,
                    ApplicationMode = 8,
                    ApplicationOrder = 1,
                    CourseId = 10,
                    DaytimeEveningAttendance = 1,
                    PreviousQualificationId = 1,
                    Nationality = 1,
                    MotherQualificationId = 22,
                    FatherQualificationId = 14,
                    MotherOccupation = 10,
                    FatherOccupation = 9
                }
            );
        });

        modelBuilder.Entity<StudentPerformance>(entity =>
        {
            entity.HasKey(e => e.PerformanceId);

            entity.HasOne(e => e.Student)
                  .WithMany(s => s.StudentPerformances)
                  .HasForeignKey(e => e.StudentId);

            entity.HasData(
                new StudentPerformance
                {
                    PerformanceId = 1,
                    StudentId = 1,
                    CurricularUnits1StSemCredited = 0,
                    CurricularUnits1StSemEnrolled = 6,
                    CurricularUnits1StSemEvaluations = 5,
                    CurricularUnits1StSemApproved = 5,
                    CurricularUnits1StSemGrade = 13.4f,
                    CurricularUnits1StSemWithoutEvaluations = 0,
                    CurricularUnits2NdSemCredited = 0,
                    CurricularUnits2NdSemEnrolled = 6,
                    CurricularUnits2NdSemEvaluations = 4,
                    CurricularUnits2NdSemApproved = 4,
                    CurricularUnits2NdSemGrade = 12.3f,
                    CurricularUnits2NdSemWithoutEvaluations = 0
                },
                new StudentPerformance
                {
                    PerformanceId = 2,
                    StudentId = 2,
                    CurricularUnits1StSemCredited = 0,
                    CurricularUnits1StSemEnrolled = 6,
                    CurricularUnits1StSemEvaluations = 6,
                    CurricularUnits1StSemApproved = 6,
                    CurricularUnits1StSemGrade = 13.4f,
                    CurricularUnits1StSemWithoutEvaluations = 0,
                    CurricularUnits2NdSemCredited = 0,
                    CurricularUnits2NdSemEnrolled = 6,
                    CurricularUnits2NdSemEvaluations = 6,
                    CurricularUnits2NdSemApproved = 6,
                    CurricularUnits2NdSemGrade = 14.2f,
                    CurricularUnits2NdSemWithoutEvaluations = 0
                }
            );
        });

        modelBuilder.Entity<EconomicIndicator>(entity =>
        {
            entity.HasKey(e => e.EconomicIndicatorId);

            entity.HasOne(e => e.Student)
                  .WithMany(s => s.EconomicIndicators)
                  .HasForeignKey(e => e.StudentId);

            entity.HasData(
                new EconomicIndicator
                {
                    EconomicIndicatorId = 1,
                    StudentId = 1,
                    UnemploymentRate = 16.2f,
                    InflationRate = 0.3f,
                    Gdp = -0.92f
                },
                new EconomicIndicator
                {
                    EconomicIndicatorId = 2,
                    StudentId = 2,
                    UnemploymentRate = 15.5f,
                    InflationRate = 2.8f,
                    Gdp = -4.06f
                }
            );
        });
	}
}
