namespace PregledPrometa.Web
{
    using System.Data.Entity;
    using PregledPrometa.Model;
    



    public partial class PregledPrometaContext2018 : DbContext
    {
        public PregledPrometaContext2018()
            : base("name=PregledPrometaConn2018")
        {
        }

        public virtual DbSet<DNEVNIPROMETM> DNEVNIPROMETM { get; set; }     
        public virtual DbSet<DNEVNIPROMETzaWEBApp> DNEVNIPROMETzaWEBApp { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


        }
    }
}