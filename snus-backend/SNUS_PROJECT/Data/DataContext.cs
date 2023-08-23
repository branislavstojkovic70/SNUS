using Azure;
using Microsoft.EntityFrameworkCore;
using SNUS_PROJECT.Models;

namespace SNUS_PROJECT.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<AlarmActivation> AlarmActivations { get; set; }
        public DbSet<AnalogInput> AnalogInputs { get; set; }
        public DbSet<AnalogOutput> AnalogOutputs { get; set; }
        public DbSet<DigitalInput> DigitalInputs { get; set; }
        public DbSet<DigitalOutput> DigitalOutputs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<AnalogInput>()
            .HasMany(e => e.Alarms)
            .WithOne(e => e.AnalogInput)
            .HasForeignKey(e => e.AnalogId)
            .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Alarm>().HasOne(ai => ai.analogInput).WithMany(a => a.Alarms);


            modelBuilder.Entity<Alarm>()
               .HasKey(a => a.Id);

            modelBuilder.Entity<Alarm>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AlarmActivation>()
               .HasKey(a => a.Id);
            modelBuilder.Entity<AlarmActivation>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Alarm>()
               .HasMany(a => a.Activations)
               .WithOne(aa => aa.Alarm)
               .HasForeignKey(aa => aa.AlarmId)
               .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}