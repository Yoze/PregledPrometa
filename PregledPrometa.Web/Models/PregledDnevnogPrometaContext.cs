namespace PregledPrometa.Web
{
    using System.Data.Entity;
    using PregledPrometa.Model;
    



    public partial class PregledDnevnogPrometaContext : DbContext
    {

        //public PregledDnevnogPrometaContext()
        //    : base("name=PregledPrometaConn2018")
        //{
        //}



        public PregledDnevnogPrometaContext(string connString) : base(connString)
        {
        }


        public virtual DbSet<DNEVNIPROMETM> DNEVNIPROMETM { get; set; }     
        public virtual DbSet<DNEVNIPROMETzaWEBApp> DNEVNIPROMETzaWEBApp { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


        }
    }
}