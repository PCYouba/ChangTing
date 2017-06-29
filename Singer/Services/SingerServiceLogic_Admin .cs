using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Singer.Models;
using ChangTing.Singer.Repositories;
using ChangTing.Core.Models;

namespace ChangTing.Singer.Services
{ 
    /// <summary>
    /// 歌手表操作的业务逻辑
    /// </summary>
     public class SingerServiceLogic
    {
        SingerDataAccess dal;

        #region SelectAllSingerWay
        /// <summary>
        /// 获取所有歌手的信息
        /// </summary>
        /// <returns></returns>
        public IList<SingerInfo> SelectAllSingerWay()
        {
            dal = new SingerDataAccess();
            return dal.SelectAllSingerWay();
        }
        #endregion
        #region
        /// <summary>
        /// 取前几个歌手
        /// </summary>
        /// <returns></returns>
        public IList<SingerInfo> SelectTopSinger(int top)
        {
            dal = new SingerDataAccess();
            return dal.SelectTopSinger(top);
        }
        #endregion
        #region
        
        #endregion

        #region SelectSingerWay
        /// <summary>
        /// 获取id号相等单条歌手信息
        /// </summary>
        /// <param name="singerid">歌手Id</param>
        /// <returns></returns>
        public SingerInfo SelectSingerWay(int singerid)
        {
            dal = new SingerDataAccess();
            return dal.SelectSingerWay(singerid);
        }
        #endregion


        #region UpdateSingerWay
        /// <summary>
        /// 修改id号相等单条歌手信息
        /// </summary>
        /// <param name="singerid">歌手Id</param>
        /// <returns></returns>
        public string UpdateSingerWay(SingerInfo singerinfo)
        {
            dal = new SingerDataAccess();
            string fanhuizhi = null;
            bool x = dal.UpdateSingerWay(singerinfo) > 0;
            if (x)
            {
                fanhuizhi = "已成功修改歌手信息！";
            }
            else
            {
                fanhuizhi = "修改信息失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region InsertSingerWay
        /// <summary>
        /// 新建歌手信息
        /// </summary>
        /// <param name="singerid">歌手Id</param>
        /// <returns></returns>
        public string InsertSingerWay(SingerInfo singerinfo)
        {
            if (singerinfo.HeadPortrait == ModelInfo.imgmoren)//当照片为默认   不存储
            {
                singerinfo.HeadPortrait = null;
            }

            dal = new SingerDataAccess();
            string fanhuizhi = null;
            
            singerinfo.CreateDate = DateTime.Now;

            bool x = dal.InsertSingerWay(singerinfo) !=null;
            if (x)
            {
                fanhuizhi = "歌手信息新建成功！";
            }
            else
            {
                fanhuizhi = "歌手信息新建失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region DeleteSingerWay
        /// <summary>
        /// 删除单条歌手数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteSingerWay(string id)
        {
            int ID = 0;
            bool cg = int.TryParse(id, out ID);
            if (!cg)
            {
                return cg;
            }

            dal = new SingerDataAccess();
            string img = dal.SelectSingerWay(ID).HeadPortrait;
            cg = dal.DeleteSingerWay(ID) > 0;
            if (cg)
            {
                DeleteFile.DeleteFileWay(img);
            }
            return cg;

        }
        #endregion


        #region DeleteSingerMultiWay
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteSingerMultiWay(string[] id)
        {
            dal = new SingerDataAccess();
            int[] ID = new int[id.Length];
            int cg = 0; int fhz = 0;//条数
            for (int i = 0; i < id.Length; i++)
            {
                SingerServiceLogic bll = new SingerServiceLogic();
                fhz += bll.DeleteSingerIdAlbumWay(int.Parse(id[i]));//删除专辑并返回删除条数
                string img = dal.SelectSingerWay(int.Parse(id[i])).HeadPortrait;
                ID[i] = int.Parse(id[i]);
                if (dal.DeleteSingerWay(ID[i]) > 0)
                {
                    cg++;
                    DeleteFile.DeleteFileWay(img);
                }
            }

            return cg;

        }
        #endregion


        #region DeleteSingerIdAlbumWay
        /// <summary>
        /// 通过歌手ID删除所有相关专辑
        /// </summary>
        /// <param name="id">歌手ID</param>
        /// <returns></returns>
        public int DeleteSingerIdAlbumWay(int id)
        {
            IList<AlbumInfo> AlbumInfoList = new List<AlbumInfo>();

            AlbumDataAccess Albumdal = new AlbumDataAccess();
            AlbumInfoList = Albumdal.SelectSingerIdAlbumWay(id);//提取当前歌手全部的专辑信息
            string[] idlist = new string[AlbumInfoList.Count];//存储专辑id
            for (int i = 0; i < AlbumInfoList.Count; i++)//遍历专辑信息提取id
            {
                idlist[i] = AlbumInfoList[i].AlbumId.ToString();//提取专辑id
            }
            AlbumServiceLogic_Admin albumbll = new AlbumServiceLogic_Admin();
            int fhz = albumbll.DeleteAlbumMultiWay(idlist);//删除该歌手全部专辑
            return fhz;
        }
        #endregion


        #region SingerIdAndName
        /// <summary>
        /// 通过歌手Id获取歌手姓名
        /// </summary>
        /// <param name="albuminid">歌手ID</param>
        /// <returns></returns>
        public string SingerIdAndName(int albuminid)
        {
            dal = new SingerDataAccess();
            return dal.SelectSingerWay(albuminid).Name;
        }
        #endregion
    }
}
