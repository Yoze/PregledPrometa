namespace PregledPrometa.Web
{
    using System.Data.Entity;
    using PregledPrometa.Model;
    



    public partial class PregledPrometaContext2017 : DbContext
    {
        public PregledPrometaContext2017()
            : base("name=PregledPrometaConn2017")
        {
        }

        public virtual DbSet<DNEVNIPROMETM> DNEVNIPROMETM { get; set; }     
        public virtual DbSet<DNEVNIPROMETzaWEBApp> DNEVNIPROMETzaWEBApp { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


        }
    }
}