namespace PregledPrometa.Web
{
    using System.Data.Entity;
    using PregledPrometa.Model;
    



    public partial class PregledPrometaContext2019 : DbContext
    {
        public PregledPrometaContext2019()
            : base("name=PregledPrometaConn2019")
        {
        }

        public virtual DbSet<DNEVNIPROMETM> DNEVNIPROMETM { get; set; }     
        public virtual DbSet<DNEVNIPROMETzaWEBApp> DNEVNIPROMETzaWEBApp { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


        }
    }
}