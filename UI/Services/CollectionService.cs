using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChangTing.Users.Models;
using ChangTing.Users.Services;
using ChangTing.Users.ViewModels;
using ChangTing.Music.Services;
using ChangTing.Singer.Services;

namespace ChangTing.UI.Services
{
    /// <summary>
    /// 收藏页 
    /// </summary>
     public class CollectionService
    {
        /// <summary>
        /// 通过个人用户获取当前收藏
        /// </summary>
        /// <returns></returns>
        public IList<CollectionViewInfo> SelectUserIdAndCollectionWay(int id)
        {
            CollectionServiceLogic_Admin clobll = new CollectionServiceLogic_Admin();
            IList<CollectionViewInfo> viewlist = viewcolllist(clobll.SelectUserIdCollectionWay(id));

            return viewlist;
        }

        public IList<CollectionViewInfo> viewcolllist(IList<CollectionInfo> listcollectioninfo)
        {
            UsersServiceLogic_Admin userbll = new UsersServiceLogic_Admin();//用户操作
            StorageServiceLogic_Admin stobll = new StorageServiceLogic_Admin();//歌曲操作
            AlbumServiceLogic_Admin albbll = new AlbumServiceLogic_Admin();//专辑操作
            
            IList<CollectionViewInfo> viewlist = new List<CollectionViewInfo>();//存储view用的所有收藏数据
            CollectionViewInfo viewinfo;//单条view专辑数据
            foreach (CollectionInfo item in listcollectioninfo)
            {
                viewinfo = new CollectionViewInfo();
                
                bool huoqv = true;//音乐名或专辑获取成功？
                if (item.AlbumId > 0)//为专辑时
                {
                    viewinfo.AlbumId = item.AlbumId;//专辑ID
                    viewinfo.AlbumName = albbll.SelectAlbumNameWay(item.AlbumId);//专辑名称
                    huoqv = viewinfo.AlbumName != "当前专辑不存在!";
                }
                else if (item.StorageId>0)//为音乐时
                {
                    viewinfo.CollectionId = item.CollectionId;//专辑ID
                    Music.Models.StorageInfo stoinfo = stobll.SelectStorageWay(item.CollectionId);
                    if (stoinfo != null && stoinfo != new Music.Models.StorageInfo())
                    {
                        viewinfo.StorageName = stoinfo.RealName;//音乐名
                        viewinfo.Frequency = stoinfo.Frequency;//播放次数
                        viewinfo.Path = stoinfo.Path;//地址
                    }
                    else
                    {
                        huoqv = false;
                    }
                }
                else
                {
                    huoqv = false;
                }
                if(huoqv) //获取到音乐和专辑
                {
                    viewinfo.CollectionId = item.CollectionId;//收藏ID
                    viewinfo.CreateDate = item.CreateDate;//创建时间
                    viewinfo.UserId = item.UserId;//用户ID
                    viewinfo.NickName = userbll.SelectUserNickNameWay(item.UserId);//收藏人的昵称
                }
                viewlist.Add(viewinfo);
            }


            return viewlist;
        } 

    }
}
