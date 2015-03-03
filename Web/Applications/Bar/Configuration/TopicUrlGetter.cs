//------------------------------------------------------------------------------
// <copyright company="Tunynet">
//     Copyright (c) Tunynet Inc.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using Tunynet;
using System.Linq;
using System.Collections.Generic;
using System;
using Tunynet.Common;
using Spacebuilder.Common;
using SpecialTopic.Topic;

namespace Spacebuilder.Bar
{
    public class TopicUrlGetter : IBarUrlGetter
    {
        TopicService topicService = new TopicService();

        /// <summary>
        /// 租户类型id
        /// </summary>
        public string TenantTypeId
        {
            get { return TenantTypeIds.Instance().Topic(); }
        }

        /// <summary>
        /// 动态拥有者类型
        /// </summary>
        public int ActivityOwnerType
        {
            get { return ActivityOwnerTypes.Instance().Topic(); }
        }

        /// <summary>
        /// 是否为私有状态
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public bool IsPrivate(long sectionId)
        {
            TopicEntity group = topicService.Get(sectionId);
            if (group == null)
                return false;
            return !group.IsPublic;
        }

        public string ThreadDetail(long threadId, bool onlyLandlord = false, SortBy_BarPost sortBy = SortBy_BarPost.DateCreated, int pageIndex = 1, long? anchorPostId = null, bool isAnchorPostList = false, long? childPostIndex = null)
        {
            BarThread thread = new BarThreadService().Get(threadId);
            if (thread == null)
                return string.Empty;
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(thread.SectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicThreadDetail(spaceKey, threadId, onlyLandlord, sortBy, pageIndex, anchorPostId, isAnchorPostList, childPostIndex);
        }

        /// <summary>
        /// 贴吧详细显示页面
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        public string SectionDetail(long sectionId, SortBy_BarThread? sortBy = null, bool? isEssential = null, long? categoryId = null)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicSectionDetail(spaceKey, categoryId, isEssential, sortBy);
        }

        /// <summary>
        /// 编辑页面
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        public string Edit(long sectionId, long? threadId = null)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicThreadEdit(spaceKey, threadId);
        }

        /// <summary>
        /// 编辑回帖页面
        /// </summary>
        /// <param name="threadId">帖子id</param>
        /// <param name="postId">回帖id</param>
        /// <returns>编辑回帖页面</returns>
        public string EditPost(long threadId, long? postId = null)
        {
            BarThread thread = new BarThreadService().Get(threadId);
            if (thread == null)
                return string.Empty;
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(thread.SectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicEditPost(spaceKey, threadId, postId);
        }

        /// <summary>
        /// 用户帖子页面
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="sectionId">贴吧id</param>
        /// <returns>用户帖子页面</returns>
        public string UserThreads(long userId, long? sectionId = null)
        {
            if (sectionId == null)
                return string.Empty;
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId.Value);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicUserThreads(spaceKey);
        }

        /// <summary>
        /// 用户回帖页面
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="sectionId">贴吧id</param>
        /// <returns>用户回帖页面</returns>
        public string UserPosts(long userId, long? sectionId = null)
        {
            if (sectionId == null)
                return string.Empty;
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId.Value);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicUserThreads(spaceKey, true);
        }

        /// <summary>
        /// 前台管理帖子页面
        /// </summary>
        /// <param name="sectionId">帖子id</param>
        /// <returns>前台管理帖子页面</returns>
        public string ManageThreads(long sectionId)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicManageThreads(spaceKey);
        }

        /// <summary>
        /// 管理回帖
        /// </summary>
        /// <param name="sectionId">帖子id</param>
        /// <returns>管理回帖</returns>
        public string ManagePosts(long sectionId)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicManagePosts(spaceKey);
        }

        /// <summary>
        /// 管理分类
        /// </summary>
        /// <param name="sectionId">贴吧id</param>
        /// <returns>管理分类</returns>
        public string ManageCategories(long sectionId)
        {
            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;
            return SiteUrls.Instance().TopicManageThreadCategories(spaceKey);
        }

        /// <summary>
        /// 标签下的帖子
        /// </summary>
        /// <param name="tagName">标签名</param>
        /// <param name="sectionId">帖子id</param>
        /// <returns>标签下的帖子</returns>
        public string ListByTag(string tagName, long? sectionId = null, SortBy_BarThread? sortBy = null, bool? isEssential = null)
        {
            if (sectionId == null)
                return string.Empty;

            string spaceKey = TopicIdToTopicKeyDictionary.GetTopicKey(sectionId.Value);
            if (string.IsNullOrEmpty(spaceKey))
                return string.Empty;

            return SiteUrls.Instance().TopicThreadListByTag(spaceKey, tagName, sortBy, isEssential);
        }

        /// <summary>
        /// 后台管理首页
        /// </summary>
        /// <returns></returns>
        public string BackstageHome()
        {
            return SiteUrls.Instance().ManageTopics();
        }


        /// <summary>
        /// 后台导航
        /// </summary>
        /// <returns></returns>
        public string _ManageSubMenu()
        {
            return "~/Applications/Topic/Views/ControlPanelTopic/_ManageTopicSideMenuShortcut.cshtml";
        }

        /// <summary>
        /// 用户首页
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="sectionId">贴吧id</param>
        /// <returns></returns>
        public string UserSpaceHome(long userId, long? sectionId = null)
        {
            return SiteUrls.Instance().SpaceHome(userId);
        }
    }
}