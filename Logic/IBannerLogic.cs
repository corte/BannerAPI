using System;
using System.Collections.Generic;
using BannerApi.Data.DbModel;

namespace BannerApi.Logic
{
    public interface IBannerLogic
    {
        List<Banner> GetAll();
        Banner Get(int id);
        void Set(Banner banner);
        void Delete(int id);
    }
}
