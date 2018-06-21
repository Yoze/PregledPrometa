namespace PregledPrometa.Web
{
    using System.Data.Entity;
    using PregledPrometa.Model;
    



    public partial class PregledPrometaContext2016 : DbContext
    {

        public PregledPrometaContext2016() : base("name=PregledPrometaConn2016")
        {
        }


        public virtual DbSet<DNEVNIPROMETM> DNEVNIPROMETM { get; set; }     
        public virtual DbSet<DNEVNIPROMETzaWEBApp> DNEVNIPROMETzaWEBApp { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


        }
    }
}