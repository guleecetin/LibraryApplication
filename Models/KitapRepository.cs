﻿using LibraryApplication.Utility;
using System.Linq.Expressions;
namespace LibraryApplication.Models
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {
        private UygulamaDbContext _uygulamaDbContext;
        public KitapRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Kitap kitap)
        {
            _uygulamaDbContext.Update(kitap);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
