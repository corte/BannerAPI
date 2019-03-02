using System;
using System.Collections.Generic;
using BannerApi.Data.DbModel;

namespace BannerApi.Logic
{
    public interface IBannerLogic
    {
        List<Banner> GetAll();
        Banner Get(int id);
        int Set(Banner banner);
        void Delete(int id);
    }
}
