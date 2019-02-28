using Microsoft.EntityFrameworkCore;

namespace TrainTrain.Dal.Entities
{
    public class TrainTrainContext : DbContext
    {
        public TrainTrainContext(DbContextOptions options)
            : base(options)
                
        {
           // Configuration.LazyLoadingEnabled = false;
        }


        public DbSet<TrainEntity> Trains { get; set; }
        public DbSet<SeatEntity> Seats { get; set; }
    }
}