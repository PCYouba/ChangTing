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
    /// 专辑业务逻辑
    /// </summary>
    public class AlbumServiceLogic_Admin
    {
        AlbumDataAccess dal;
        #region SelectAllAlbumWay
        /// <summary>
        /// 获取所有专辑的信息
        /// </summary>
        /// <returns></returns>
        public IList<AlbumInfo> SelectAllAlbumWay()
        {
            dal = new AlbumDataAccess();
            return dal.SelectAllAlbumWay();
        }
        #endregion


        #region SelectAlbumWay
        /// <summary>
        /// 获取id号相等单条专辑信息
        /// </summary>
        /// <param name="albumid">歌手Id</param>
        /// <returns></returns>
        public AlbumInfo SelectAlbumWay(int albumid)
        {
            dal = new AlbumDataAccess();
            return dal.SelectAlbumWay(albumid);
        }
        #endregion


        #region SelectAlbumNameWay
        /// <summary>
        /// 获取本ID号专辑名
        /// </summary>
        /// <param name="albumid">歌手Id</param>
        /// <returns></returns>
        public string SelectAlbumNameWay(int albumid)
        {
            dal = new AlbumDataAccess();
            AlbumInfo albuminfo = dal.SelectAlbumWay(albumid);
            if (albuminfo != null)
            {
                return albuminfo.Name;
            }
            return "当前专辑不存在!";
        }
        #endregion


        #region UpdateSingerWay
        /// <summary>
        /// 修改id号相等单条专辑信息
        /// </summary>
        /// <param name="albuminfo">专辑信息</param>
        /// <returns></returns>
        public string UpdateAlbumWay(AlbumInfo albuminfo)
        {
            if (albuminfo.Image == ModelInfo.imgmoren)
            {
                albuminfo.Image = null;
            }
            dal = new AlbumDataAccess();
            string fanhuizhi = null;
            bool x = dal.UpdateAlbumWay(albuminfo) > 0;
            if (x)
            {
                fanhuizhi = "已成功修改专辑信息！";
            }
            else
            {
                fanhuizhi = "修改信息失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region InsertAlbumWay
        /// <summary>
        /// 新建专辑信息
        /// </summary>
        /// <param name="singerid">歌手Id</param>
        /// <returns></returns>
        public string InsertAlbumWay(AlbumInfo albuminfo)
        {
            if (albuminfo.Image == ModelInfo.imgmoren)//当照片为默认   不存储
            {
                albuminfo.Image = null;
            }

            dal = new AlbumDataAccess();
            string fanhuizhi = null;

            albuminfo.CreateDate = DateTime.Now;

            bool x = dal.InsertAlbumWay(albuminfo) != null;
            if (x)
            {
                fanhuizhi = "专辑信息新建成功！";
            }
            else
            {
                fanhuizhi = "专辑信息新建失败！请联系管理员";
            }
            return fanhuizhi;
        }
        #endregion


        #region DeleteAlbumWay
        /// <summary>
        /// 根据专辑Id删除单条专辑数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public object[] DeleteAlbumWay(string id)
        {
            int ID = 0; int yinyue = 0;
            bool cg = int.TryParse(id, out ID);
            if (!cg)
            {
                return new object[] { cg,yinyue };
            }
            AlbumAndStorageDateAccess STOdal = new AlbumAndStorageDateAccess();
            yinyue =  STOdal.DeleteAlbumIdStorageWay(ID);//删除音乐
            
            dal = new AlbumDataAccess();
            string img = dal.SelectAlbumWay(ID).Image;
            cg = dal.DeleteAlbumWay(ID) > 0;
            if (cg)
            {
                DeleteFile.DeleteFileWay(img);
            }
            return new object[] { cg, yinyue };

        }
        #endregion


        #region DeleteAlbumMultiWay
        /// <summary>
        /// 删除多条专辑数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteAlbumMultiWay(string[] id)
        {
            AlbumAndStorageDateAccess STOdal = new AlbumAndStorageDateAccess();
            int yinyue = 0;
            dal = new AlbumDataAccess();
            int[] ID = new int[id.Length];
            int cg = 0;
            for (int i = 0; i < id.Length; i++)
            {
                yinyue += STOdal.DeleteAlbumIdStorageWay(int.Parse(id[i]));//删除音乐
                string img = dal.SelectAlbumWay(int.Parse(id[i])).Image;
                ID[i] = int.Parse(id[i]);
                if (dal.DeleteAlbumWay(ID[i]) > 0) {
                    cg++;
                    DeleteFile.DeleteFileWay(img);
                }
            }

            return cg;

        }
        #endregion



    }
}
